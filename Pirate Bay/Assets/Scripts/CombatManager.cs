using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatManager : MonoBehaviour {
    public Text combatText;
    private enum State {CombatStart}
    private State state;
	// Use this for initialization
	void Start () {
        state = State.CombatStart;
	}
	
	// Update is called once per frame
	void Update () {
        combatText.text = StateToString(state);
	}

    string StateToString(State s){
        switch(s)
        {
            case State.CombatStart: return "Combat Start";
            default: return "Unknown State";
        }
    }
}
