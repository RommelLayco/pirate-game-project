using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    public Text combatInfo;
    private enum State {CombatStart, CrewMemberTurn, ChooseEnemy, EnemyTurn, Resolve, EndTurn}
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
        CrewMember cm = GameObject.Find("CrewMember").GetComponent<CrewMember>();
        Enemy e = GameObject.Find("Enemy").GetComponent<Enemy>();
        combatants = new List<Combatant>();
        combatants.Add(cm);
        combatants.Add(e);
        currentIndex = 0;
        actions = new ActionList();
        selecting = false;
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
        if(skip)
        {
            state = State.ChooseEnemy;
            selecting = true;
        }
    }

    void ChooseEnemy()
    {
        if (!selecting)
        {
            state = State.Resolve;
            GameObject crew = GameObject.Find("CrewMember");
            Combatant crewMember = crew.GetComponent<Combatant>();
            Combatant enemy = GameObject.Find("Enemy").GetComponent<Combatant>();
            Vector3 original = crew.transform.position;
            Vector3 target = targetObj.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
            Action action = new ActionMove(crew, target);
            actions.Add(action);
            action = new AttackAction(crewMember, enemy);
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
            Combatant crewMember = GameObject.Find("CrewMember").GetComponent<Combatant>();
            GameObject enemyObj = GameObject.Find("Enemy");
            Combatant enemy = enemyObj.GetComponent<Combatant>();

            Vector3 original = enemy.transform.position;
            Vector3 target = crewMember.transform.position + new Vector3(1.0f, 0.0f, 0.0f);

            Action action = new ActionWaitForInput();
            actions.Add(action);
            action = new ActionMove(enemyObj, target);
            actions.Add(action);
            action = new AttackAction(enemy, crewMember);
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
        if (skip)
        {
            List<Combatant> deadCombatants = new List<Combatant>();
            foreach (Combatant combatant in combatants)
            {
                if (combatant.IsDead())
                {
                    deadCombatants.Add(combatant);
                }
            }
            foreach (Combatant combatant in deadCombatants)
            {
                combatants.Remove(combatant);
            }


            if (combatants.Count == 1)
            {
                if (combatants[currentIndex] as CrewMember != null)
                {
                    combatInfo.text = "You Win!";
                }
                if (combatants[currentIndex] as Enemy != null)
                {
                    combatInfo.text = "You Lose!";
                }
            }
            else
            {
                currentIndex += 1;
                if (currentIndex >= combatants.Count)
                    currentIndex = 0;
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

}
