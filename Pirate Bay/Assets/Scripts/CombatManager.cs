using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    private enum State {CombatStart, CrewMemberTurn, ChooseEnemy, EnemyTurn, Resolve, EndTurn}
    private State state;
    private GameObject targetObj;
    private int currentIndex;
    private List<Combatant> combatants;
    private ActionList actions;
    private bool selecting = false;

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
    }

    void CrewMemberTurn()
    {
        if(Input.GetButtonDown("Submit"))
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
            Vector3 original = crew.transform.position;
            Vector3 target = targetObj.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
            Action action = new Move(crew, target);
            actions.Add(action);
            action = new Move(crew, original);
            actions.Add(action);
        }
    }

    void EnemyTurn()
    {
        if (Input.GetButtonDown("Submit"))
        {
            state = State.Resolve;
            Action action = new WaitFor();
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
        if (Input.GetButtonDown("Submit"))
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
        }
    }

    private class WaitFor : Action
    {
        public override void Work(float deltaTime)
        {
            if (Input.GetButtonDown("Submit"))
            {
                done = true;
            }
        }
    }
    private class Move : Action
    {
        private GameObject obj = null;
        private Vector3 target;

        //target is in worldspace
        public Move(GameObject obj, Vector3 target)
        {
            this.obj = obj;
            this.target = target;
            if (obj == null)
                done = true;
        }
        public override void Work(float deltaTime)
        {
            Vector3 diff = target - obj.transform.position;
            if (diff.magnitude < 1.0f)
            {
                obj.transform.Translate(diff);
                done = true;
                return;
            }
            diff.Normalize();
            obj.transform.Translate(diff);
            
        }
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
