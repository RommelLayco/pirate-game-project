using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarFront : MonoBehaviour {

    public Combatant owner;

	// Use this for initialization
	void Start () {
        transform.position = owner.transform.position + new Vector3(-0.8f, 1.5f, 0.0f);
        transform.SetParent(owner.gameObject.transform);

	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(owner.health / 100.0f, 1.0f, 1.0f);

        if (owner.IsDead())
        {
            gameObject.SetActive(false);
        }
	}
}
