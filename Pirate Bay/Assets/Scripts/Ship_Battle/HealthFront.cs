using UnityEngine;
using System.Collections;

/*
* This class represents the green part of the health bar used for the ship.
* It has been adapted from the HealthBarFront script written by Luke Wolyncewicz
* Edited by: Benjamin Frew
*/
public class HealthFront : MonoBehaviour {

    public Ship owner;

    // Used for initialization
    void Start()
    {
        if (owner != null)
        {
            //Puts the bar at the ship
            transform.position = (new Vector2(owner.transform.position.x,owner.transform.position.y)
                + new Vector2(-0.8f, 1.5f));
            transform.SetParent(owner.gameObject.transform);
            GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
        {
            //Scales the bar to reflect the ships health
            transform.localScale = new Vector3(owner.health / 100.0f, 1.0f, 1.0f);

            if (owner.IsDead())
            {
                gameObject.SetActive(false);
            }
        }
        //Keep the bar horizontal
        transform.rotation = Quaternion.identity;
    }
}
