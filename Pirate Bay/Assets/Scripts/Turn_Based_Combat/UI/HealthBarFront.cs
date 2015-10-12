﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarFront : MonoBehaviour {

    private Combatant owner;

	// Use this for initialization
	void Start () {
        if (owner != null)
        {
            transform.position = owner.transform.position + new Vector3(-0.8f, 1.5f, 0.0f);
            transform.SetParent(owner.gameObject.transform);
            GetComponent<SpriteRenderer>().enabled = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (owner != null)
        {
            transform.localScale = new Vector3(owner.health / 100.0f, 1.0f, 1.0f);

            if (owner.IsDead())
            {
                gameObject.SetActive(false);
            }
        }
	}

   public void SetOwner (Combatant owner)
    {
        this.owner = owner;
    }
}