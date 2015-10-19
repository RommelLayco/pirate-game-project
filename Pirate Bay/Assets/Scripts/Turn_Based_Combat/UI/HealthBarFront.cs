using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Controls the foreground (green) part of the health bars
public class HealthBarFront : MonoBehaviour {

    private Combatant owner;

	void Start () {
        if (owner != null)
        {
            transform.position = owner.transform.position + new Vector3(-0.8f, 1.5f, 0.0f);
            transform.SetParent(owner.gameObject.transform);
            GetComponent<SpriteRenderer>().enabled = true;
        }

	}
	
	void Update () {
        if (owner != null)
        {
            // Shrinks/grows according to owner's health
            transform.localScale = new Vector3(owner.health / 100.0f, 1.0f, 1.0f);

            // disappears if owner is dead
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
