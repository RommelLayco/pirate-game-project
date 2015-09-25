using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    private enum State {CombatStart, CrewMemberTurn, EnemyTurn, Resolve, EndTurn}
    private State state;
    private CrewMember cm;
    private Enemy e;
    private Combatant current;
    private List<Combatant> combatants;
	// Use this for initialization
	void Start ()
    {
        state = State.CombatStart;
        cm = new CrewMember();
        e = new Enemy();
        combatants = new List<Combatant>();
        combatants.Add(cm);
        combatants.Add(e);
        current = null;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        combatText.text = StateToString(state);
        switch (state)
        {
            case State.CombatStart: CombatStart(); break;
            case State.CrewMemberTurn: CrewMemberTurn(); break;
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
        current = combatants[0];
        if(current as CrewMember != null)
        {
            state = State.CrewMemberTurn;
        }
        if (current as Enemy != null)
        {
            state = State.EnemyTurn;
        }
    }

    void CrewMemberTurn()
    {
        if(Input.GetButtonDown("Submit"))
        {
            state = State.Resolve;
        }
    }

    void EnemyTurn()
    {

    }
    
    void Resolve()
    {

    }

    void EndTurn()
    {

    }
}
