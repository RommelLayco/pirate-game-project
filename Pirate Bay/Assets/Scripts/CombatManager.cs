using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    private enum State {CombatStart, CrewMemberTurn, EnemyTurn, Resolve, EndTurn}
    private State state;
    private CrewMember cm;
    private Enemy e;
    private int currentIndex;
    private List<Combatant> combatants;
    private ActionList actions;

	// Use this for initialization
	void Start ()
    {
        state = State.CombatStart;
        cm = new CrewMember();
        e = new Enemy();
        combatants = new List<Combatant>();
        combatants.Add(cm);
        combatants.Add(e);
        currentIndex = 0;
        actions = new ActionList();
    }
	
	// Update is called once per frame
	void Update ()
    {
        combatText.text = StateToString(state);
        switch (state)
        {
            case State.CombatStart: CombatStart(); break;
            case State.CrewMemberTurn: CrewMemberTurn(); break;
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
            case State.Resolve: return "Resolve";
            case State.EnemyTurn: return "Enemy Turn";
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
            state = State.Resolve;
            Action action = new WaitFor();
            actions.Add(action);
        }
    }

    void EnemyTurn()
    {

    }
    
    void Resolve()
    {
        if (actions.IsDone())
        {
            state = State.EndTurn;
            return;
        }
        actions.Work();
    }

    void EndTurn()
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

    private class WaitFor : Action
    {
        public override void Work()
        {
            if (Input.GetButtonDown("Submit"))
            {
                done = true;
            }
        }
    }
}
