﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    public Text combatInfo;
    private enum State {CombatStart, CrewMemberTurn, ChooseEnemy, EnemyTurn, CleanupActions, Resolve, EndTurn, CombatFinish}
    private State state;
    private int currentIndex;
    private bool choseAttack = false;
    private bool choseAbility = false;
    private List<Combatant> combatants = new List<Combatant>();
    private List<Combatant> enemies = new List<Combatant>();
    private List<Combatant> crewMembers = new List<Combatant>();
    private Combatant target = null;
    private ActionList actions;
    private bool skip = false;

    private List<Vector3> enemyPositions;
    private List<Vector3> crewPositions;

    // Use this for initialization
    void Start()
    {
        state = State.CombatStart;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Crew"))
        {
            combatants.Add(g.GetComponent<CrewMember>());
            crewMembers.Add(g.GetComponent<CrewMember>());
        }
        currentIndex = 0;
        actions = new ActionList();
        combatInfo.text = "";

        // Arbitrary 5 fixed positions for enemy placement
        enemyPositions = new List<Vector3>();
        enemyPositions.Add(new Vector3(2.97f, -1.16f));
        enemyPositions.Add(new Vector3(7.26f, 0.26f));
        enemyPositions.Add(new Vector3(6.48f, 3.1f));
        enemyPositions.Add(new Vector3(3.69f, 1.56f));
        enemyPositions.Add(new Vector3(5.96f, -2.51f));

        List<GameObject> enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().GenerateEnemyList();
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
        combatText.text = StateToString(state);
        switch (state)
        {
            case State.CombatStart: CombatStart(); break;
            case State.CrewMemberTurn: CrewMemberTurn(); break;
            case State.ChooseEnemy: ChooseEnemy(); break;
            case State.CleanupActions: CleanupActions(); break;
            case State.EnemyTurn: EnemyTurn(); break;
            case State.Resolve: Resolve(); break;
            case State.EndTurn: EndTurn(); break;
            case State.CombatFinish: CombatFinish(); break;
        }
    }

    string StateToString(State s)
    {
        switch(s)
        {
            case State.CombatStart: return "Combat Start";
            case State.CrewMemberTurn: return "Crew Member Turn";
            case State.ChooseEnemy: return "Choosing Enemy";
            case State.EnemyTurn: return "Enemy Turn";
            case State.CleanupActions: return "Cleanup Actions";
            case State.Resolve: return "Resolve";
            case State.EndTurn: return "End Turn";
            case State.CombatFinish: return "Combat Finish";
            default: return "Unknown State";
        }
    }

    void CombatStart()
    {
        //Sort combatants by speed to determine order
        combatants.Sort();
        currentIndex = 0;
        if(combatants[currentIndex] as CrewMember != null)
        {
            state = State.CrewMemberTurn;
        }
        if (combatants[currentIndex] as Enemy != null)
        {
            state = State.EnemyTurn;
        }
        combatants[currentIndex].SetSelectionRing();
    }

    void CrewMemberTurn()
    {
        choseAttack = false;
        choseAbility = false;
        ShowAttackButton(true);
    }

    void ChooseEnemy()
    {
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
        if (skip)
        {
            state = State.Resolve;
            Combatant target = GetEnemyTarget();

            GameObject enemyObj = combatants[currentIndex].gameObject;
            Combatant enemy = combatants[currentIndex];

            Vector3 original = enemy.transform.position;
            Vector3 targetVec = target.transform.position + new Vector3(1.0f, 0.0f, 0.0f);

            Action action = new ActionWaitForInput();
            actions.Add(action);
            action = new ActionMove(enemyObj, targetVec);
            actions.Add(action);
            action = new ActionAttack(enemy, target);
            actions.Add(action);
            action = new ActionMove(enemyObj, original);
            actions.Add(action);
        }
    }

    void CleanupActions()
    {
        if (choseAttack)
        {
            state = State.Resolve;
            GameObject crew = combatants[currentIndex].gameObject;
            Combatant crewMember = combatants[currentIndex];
            Enemy enemy = target.GetComponent<Enemy>();
            Vector3 original = crew.transform.position;
            Vector3 targetPos = target.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
            Action action = new ActionMove(crew, targetPos);
            actions.Add(action);
            action = new ActionAttack(crewMember, enemy);
            actions.Add(action);
            action = new ActionMove(crew, original);
            actions.Add(action);
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
        checkWinLoss();
        if (skip)
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
        }
    }

    void CombatFinish()
    {
        //do win/loss stuff here
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
            combatInfo.text = "You Win!";
            state = State.CombatFinish;
        }
        else if (lose)
        {
            combatInfo.text = "You Lose...";
            state = State.CombatFinish;
        }
            
    }

    public void ChoseAttack()
    {
        ShowAttackButton(false);
        choseAttack = true;
        state = State.ChooseEnemy;
        foreach (Enemy e in enemies)
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
            foreach (Enemy e in enemies)
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
            GameObject.Find("ButtonAttack").transform.position += new Vector3(1.0f, 2.0f);
            bool hasAbility = (ability != null && ability.GetCD() <= 0);
            GameObject.Find("ButtonAbility").GetComponent<Button>().interactable = hasAbility;
            GameObject.Find("ButtonAbility").transform.position = combatants[currentIndex].transform.position;
            GameObject.Find("ButtonAbility").transform.position += new Vector3(-1.0f, 2.0f);

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

    private Combatant GetEnemyTarget()
    {
        List<CrewMember> taunts = new List<CrewMember>();
        foreach( CrewMember c in crewMembers)
        {
            if(c.buffs.HasBuff("Taunt"))
            {
                taunts.Add(c);
            }
        }
        int index;
        if (taunts.Count > 0)
        {
            do
            {
                index = UnityEngine.Random.Range(0, taunts.Count);
            } while (taunts[index].IsDead());
            return taunts[index];
        }
        else
        {
            do
            {
                index = UnityEngine.Random.Range(0, crewMembers.Count);
            } while (crewMembers[index].IsDead());
            return crewMembers[index];
        }
        
    }
}
