using UnityEngine;
using System.Collections;

/*
* This class represents the green part of the health bar used for the ship.
* It has been adapted from the HealthBarFront script written by Luke Wolyncewicz
* Edited by: Benjamin Frew
*/
public class HealthBack : MonoBehaviour {

    public Ship owner;

    // Used for initialization
    void Start()
    {
        if (owner != null)
        {
            //Sets the position to be that of the ships
            transform.position = owner.transform.position + new Vector3(-0.8f, 1.5f, 0.0f);
            transform.SetParent(owner.gameObject.transform);
            transform.localScale = new Vector3(owner.health/100.0f, 1.0f, 1.0f);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null && owner.IsDead())
        {
            gameObject.SetActive(false);
        }
        //Sets the bar to be horizontal
        transform.rotation = Quaternion.identity;
    }
}
