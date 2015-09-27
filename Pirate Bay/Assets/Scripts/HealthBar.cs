using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Combatant owner;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (owner.IsDead())
        {
            GetComponent<Text>().text = "Dead";
        }
        else
        {
            GetComponent<Text>().text = "Health: " + owner.health.ToString();
        }
	}
}
