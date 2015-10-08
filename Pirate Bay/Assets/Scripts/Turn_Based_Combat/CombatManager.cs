using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    public Text combatInfo;
    private enum State {CombatStart, CrewMemberTurn, ChooseEnemy, EnemyTurn, Resolve, EndTurn, CombatFinish}
    private State state;
    private GameObject targetObj;
    private int currentIndex;
    private List<Combatant> combatants;
    private ActionList actions;
    private bool selecting = false;
    private bool skip = false;

	// Use this for initialization
	void Start ()
    {
        state = State.CombatStart;
        combatants = new List<Combatant>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            combatants.Add(g.GetComponent<Combatant>());
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Crew"))
        {
            combatants.Add(g.GetComponent<Combatant>());
        }
        currentIndex = 0;
        actions = new ActionList();
        selecting = false;
        combatInfo.text = "";
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
        SetSelectionRing();
    }

    void CrewMemberTurn()
    {
        ShowAttackButton(true);
    }

    void ChooseEnemy()
    {
        ShowAttackButton(false);
        if (!selecting)
        {
            state = State.Resolve;
            GameObject crew = combatants[currentIndex].gameObject;
            Combatant crewMember = combatants[currentIndex];
            Combatant enemy = targetObj.GetComponent<Combatant>();
            Vector3 original = crew.transform.position;
            Vector3 target = targetObj.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
            Action action = new ActionMove(crew, target);
            actions.Add(action);
            action = new ActionAttack(crewMember, enemy);
            actions.Add(action);
            action = new ActionMove(crew, original);
            actions.Add(action);
        }
    }

    void EnemyTurn()
    {
        if (skip)
        {
            state = State.Resolve;
            GameObject[] crewMembers = GameObject.FindGameObjectsWithTag("Crew");
            int index;
            do
            {
                index = UnityEngine.Random.Range(0, crewMembers.Length);
            } while (crewMembers[index].GetComponent<CrewMember>().IsDead());
            Combatant target = crewMembers[index].GetComponent<Combatant>();

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
            SetSelectionRing();
        }
    }

    void CombatFinish()
    {
        //do win/loss stuff here
    }

    void SetSelectionRing()
    {
        GameObject ring = GameObject.Find("SelectionRing");
        if (ring == null)
            throw new Exception("Ring does not exist");
        Combatant c = combatants[currentIndex];
        float height = (c.GetComponent<BoxCollider>().size.y)*c.transform.localScale.y / 2.0f;
        ring.transform.position = combatants[currentIndex].transform.position + new Vector3(0.0f,-height,0.0f);
        ring.transform.parent = combatants[currentIndex].gameObject.transform;
    }

    public void SelectTarget(GameObject obj)
    {
        if(selecting && obj != null)
        {
            selecting = false;
            targetObj = obj;
        }
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

    public void chooseEnemy()
    {
        state = State.ChooseEnemy;
        selecting = true;
    }

    public void ShowAttackButton(bool show)
    {
        GameObject.Find("ButtonAttack").GetComponent<Button>().interactable = show;
        GameObject.Find("ButtonAbility").GetComponent<Button>().interactable = show;
        if (show)
        {
            GameObject.Find("ButtonAttack").GetComponentInChildren<Text>().text = "Attack";
            GameObject.Find("ButtonAttack").transform.position = combatants[currentIndex].transform.position;
            GameObject.Find("ButtonAttack").transform.position += new Vector3(1.0f, 2.0f);
            GameObject.Find("ButtonAbility").GetComponentInChildren<Text>().text = "Ability";
            GameObject.Find("ButtonAbility").transform.position = combatants[currentIndex].transform.position;
            GameObject.Find("ButtonAbility").transform.position += new Vector3(-1.0f, 2.0f);
        }
        else
        {
            GameObject.Find("ButtonAttack").GetComponentInChildren<Text>().text = "";
            GameObject.Find("ButtonAbility").GetComponentInChildren<Text>().text = "";
        }
    }
}
