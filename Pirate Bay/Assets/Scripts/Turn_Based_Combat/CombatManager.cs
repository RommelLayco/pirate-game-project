using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class CombatManager : MonoBehaviour {

    private enum State {CombatStart, CrewMemberTurn, ChooseEnemy, EnemyTurn, CleanupActions,
        Resolve, EndTurn, CombatWon, CombatLost, CombatFinish}
    private State state;
    private int currentIndex;
    private bool choseAttack = false;
    private bool choseAbility = false;
    private List<Combatant> combatants = new List<Combatant>();
    private List<Combatant> enemies = new List<Combatant>();
    private List<Combatant> crewMembers = new List<Combatant>();
    private Combatant target = null;
    private ActionList actions = new ActionList();
    private bool skip = false;
    private bool won = false;

    private List<Vector3> enemyPositions;
    private List<Vector3> crewPositions;

    // Use this for initialization
    void Start()
    {
        state = State.CombatStart;

        // Arbitrary 5 fixed positions for enemy placement
        enemyPositions = new List<Vector3>();
        enemyPositions.Add(new Vector3(2.97f, -1.16f));
        enemyPositions.Add(new Vector3(7.26f, 0.26f));
        enemyPositions.Add(new Vector3(6.48f, 3.1f));
        enemyPositions.Add(new Vector3(3.69f, 1.56f));
        enemyPositions.Add(new Vector3(5.96f, -2.51f));

        HashSet<EnemyGenerator.EnemyType> enemytypes = new HashSet<EnemyGenerator.EnemyType>();
        enemytypes.Add(EnemyGenerator.EnemyType.Maneater);
        enemytypes.Add(EnemyGenerator.EnemyType.EnemyPirate);
        List<GameObject> enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
            GenerateEnemyList(enemytypes);
        for (int i = 0; i < enemyList.Count; i++)
        {
            GameObject g = Instantiate(enemyList[i]);
            g.transform.position = enemyPositions[i];
            combatants.Add(g.GetComponent<Enemy>());
            enemies.Add(g.GetComponent<Enemy>());
        }

        crewPositions = new List<Vector3>();
        crewPositions.Add(new Vector3(-3.5f, 0f));
        crewPositions.Add(new Vector3(-6f, -2f));
        crewPositions.Add(new Vector3(-6.5f, 2.3f));
        List<CrewMember> crewList = GameObject.Find("CrewGenerator").GetComponent<CrewGenerator>().GenerateCrewList();
        for (int i = 0; i < crewList.Count; i++)
        {
            crewList[i].transform.position = crewPositions[i];
            combatants.Add(crewList[i]);
            crewMembers.Add(crewList[i]);
        }

        GameObject.Find("Exp Info").GetComponent<Text>().enabled = false;

    }

    //Check for touch input and set skip to true if necessary
    void Update()
    {
        skip = false;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Ended)
                skip = true;
        }
        if (Input.GetButtonUp("Submit"))
        {
            skip = true;
        }

    }

    
	//Use Time.deltaTime to get time since last frame 
	void FixedUpdate ()
    {
        switch (state)
        {
            case State.CombatStart: CombatStart(); break;
            case State.CrewMemberTurn: CrewMemberTurn(); break;
            case State.ChooseEnemy: ChooseEnemy(); break;
            case State.CleanupActions: CleanupActions(); break;
            case State.EnemyTurn: EnemyTurn(); break;
            case State.Resolve: Resolve(); break;
            case State.EndTurn: EndTurn(); break;
            case State.CombatWon: CombatWon(); break;
            case State.CombatLost: CombatLost(); break;
            case State.CombatFinish: CombatFinish(); break;
        }
    }

    void CombatStart()
    {
        //Sort combatants by speed to determine order
        combatants.Sort();
        currentIndex = combatants.Count - 1;
        /*currentIndex = 0;
        if(combatants[currentIndex] as CrewMember != null)
        {
            state = State.CrewMemberTurn;
        }
        if (combatants[currentIndex] as Enemy != null)
        {
            state = State.EnemyTurn;
        }
        combatants[currentIndex].SetSelectionRing();*/
        actions.Add(new ActionInfo("Enemy encountered!"));
        actions.Add(new ActionPauseForFrames(90));
        actions.Add(new ActionHideInfo());
        state = State.Resolve;
    }

    void CrewMemberTurn()
    {
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText(combatants[currentIndex].combatantName + " 's turn!");
        choseAttack = false;
        choseAbility = false;
        ShowAttackButton(true);
    }

    void ChooseEnemy()
    {
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText("Choose enemy");
        foreach (Enemy e in enemies)
        {
            if (e.IsTargeted())
            {
                target = e;
                foreach (Enemy e1 in enemies)
                {
                    e1.Untarget();
                }
                state = State.CleanupActions;
                break;
            }
        }
    }

    void EnemyTurn()
    {
        state = State.Resolve;

        Enemy currentEnemy = combatants[currentIndex] as Enemy;
        Queue<Action> abilityActions = currentEnemy.ActionAI(crewMembers, enemies);
        while (abilityActions.Count > 0)
        {
            actions.Add(abilityActions.Dequeue());
        }
    }

    void CleanupActions()
    {
        if (choseAttack)
        {
            state = State.Resolve;
            AbilityBasicAttack basicAttack = new AbilityBasicAttack();
            basicAttack.SetTarget(target);
            Queue<Action> abilityActions = basicAttack.GetActions(combatants[currentIndex], crewMembers, enemies);
            while (abilityActions.Count > 0)
            {
                actions.Add(abilityActions.Dequeue());
            }
        }
        if (choseAbility)
        {
            AbilityTargeted ability = combatants[currentIndex].ability as AbilityTargeted;
            ability.SetTarget(target);
            if (ability != null)
            {
                Combatant me = combatants[currentIndex];
                Queue<Action> abilityActions = ability.GetActions(me, crewMembers, enemies);
                while (abilityActions.Count > 0)
                {
                    actions.Add(abilityActions.Dequeue());
                }
                combatants[currentIndex].ability.PutOnCD();
            }
        }
        state = State.Resolve;
    }


    void Resolve()
    {
        if (actions.IsDone())
        {
            state = State.EndTurn;
            return;
        }
        actions.Work(Time.deltaTime);
    }

    void EndTurn()
    {
        combatants[currentIndex].ability.ReduceCD();
        combatants[currentIndex].buffs.ReduceDuration();
        combatants[currentIndex].UnsetSelectionRing();
        do
        {
            currentIndex += 1;
            if (currentIndex >= combatants.Count)
                currentIndex = 0;
        } while (combatants[currentIndex].IsDead());
        if (combatants[currentIndex] as CrewMember != null)
        {
            state = State.CrewMemberTurn;
        }
        if (combatants[currentIndex] as Enemy != null)
        {
            state = State.EnemyTurn;
        }
        combatants[currentIndex].SetSelectionRing();

        checkWinLoss();
    }

    void CombatWon()
    {       
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText("You Win!");
        float expGained = 0;
        foreach (Enemy e in enemies)
        {
            expGained += e.getExp();
        }
        StringBuilder expDisplay = new StringBuilder();
        foreach (CrewMember m in crewMembers)
        {
            int levelUp = m.persistExp(expGained);
            if (levelUp > 0)
            {
                expDisplay.Append(m.combatantName + " gained " + expGained + " experience!\n");
                expDisplay.Append(m.combatantName + " levelled up by " + levelUp + "!\n");
            }
            else if (levelUp == 0)
            {
                expDisplay.Append(m.combatantName + " gained " + expGained + " experience!\n");
            }
            else
            {
                expDisplay.Append(m.combatantName + " is already at maximum level!\n");
            }
            m.persistHealth();
        }
        GameObject.Find("Exp Info").GetComponent<Text>().enabled = true;
        GameObject.Find("Exp Info").GetComponent<Text>().text = expDisplay.ToString();

        won = true;
        state = State.CombatFinish;
    }

    void CombatLost()
    {
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText("You Lose...");
        won = false;
        state = State.CombatFinish;
    }

    public void checkWinLoss()
    {
        bool win = true;
        bool lose = true;
        foreach (Combatant combatant in combatants)
        {
            if (!combatant.IsDead())
            {
                if (combatant as CrewMember != null)
                {
                    lose = false;
                }
                if (combatant as Enemy != null)
                {
                    win = false;
                }
            }
        }
        if (win)
        {
            state = State.CombatWon;
        }
        else if (lose)
        {
            state = State.CombatLost;
        }
            
    }

    public void ChoseAttack()
    {
        ShowAttackButton(false);
        choseAttack = true;
        state = State.ChooseEnemy;
        List<Combatant> targetables = combatants[currentIndex].GetTargetable(enemies);
        foreach (Combatant e in targetables)
        {
            if (!e.IsDead())
                e.SetTargetable();
        }
        state = State.ChooseEnemy;
    }

    public void ChoseAbility()
    {
        choseAbility = true;
        ShowAttackButton(false);
        if(combatants[currentIndex].ability.needsTarget)
        {
            List<Combatant> targetables = combatants[currentIndex].GetTargetable(enemies);
            foreach (Combatant e in targetables)
            {
                if (!e.IsDead())
                    e.SetTargetable();
            }
            state = State.ChooseEnemy;
        }
        else
        {
            Ability ability = combatants[currentIndex].ability;
            if (ability != null)
            {
                Combatant me = combatants[currentIndex];
                Queue<Action> abilityActions = ability.GetActions(me, crewMembers, enemies);
                while (abilityActions.Count > 0)
                {
                    actions.Add(abilityActions.Dequeue());
                }
                combatants[currentIndex].ability.PutOnCD();
            }
            state = State.Resolve;
        }
    }

    public void ShowAttackButton(bool show)
    {
        GameObject.Find("ButtonAttack").GetComponent<Button>().interactable = show;
        GameObject.Find("ButtonAbility").GetComponent<Button>().interactable = show;
        if (show)
        {
            Ability ability = combatants[currentIndex].ability;
            GameObject.Find("ButtonAttack").GetComponentInChildren<Text>().text = "Attack";
            GameObject.Find("ButtonAttack").transform.position = combatants[currentIndex].transform.position;
            GameObject.Find("ButtonAttack").transform.position += new Vector3(1.0f, 2.2f);
            bool hasAbility = (ability != null && ability.GetCD() <= 0);
            GameObject.Find("ButtonAbility").GetComponent<Button>().interactable = hasAbility;
            GameObject.Find("ButtonAbility").transform.position = combatants[currentIndex].transform.position;
            GameObject.Find("ButtonAbility").transform.position += new Vector3(-1.0f, 2.2f);

            if (ability == null)
                GameObject.Find("ButtonAbility").GetComponentInChildren<Text>().text = "No Ability";
            else if (ability.GetCD() == 0)
                GameObject.Find("ButtonAbility").GetComponentInChildren<Text>().text = ability.name;
            else
                GameObject.Find("ButtonAbility").GetComponentInChildren<Text>().text = ability.name+" ("+ability.GetCD()+")";


        }
        else
        {
            GameObject.Find("ButtonAttack").GetComponentInChildren<Text>().text = "";
            GameObject.Find("ButtonAbility").GetComponentInChildren<Text>().text = "";
        }
    }

    public void Continue()
    {
        if (won)
        {
            Debug.Log("Maze");
            Application.LoadLevel("Maze");
        }
        else
        {
            Debug.Log("Ship");
            Application.LoadLevel("Ship");
        }

    }

    public void CombatFinish()
    {
        GameObject.Find("ContinueButton").GetComponent<Button>().interactable = true;
        GameObject.Find("ContinueButton").GetComponentInChildren<Text>().enabled = true;
    }

}
