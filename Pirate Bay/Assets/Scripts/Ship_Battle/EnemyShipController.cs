﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
* This class controls the enemy's ship.
* It uses the methods from the base class Ship.
* Authors: Benjamin Frew, Nick Molloy
*/
public class EnemyShipController : Ship
{
    private float rivalTimer = 0;
    public Sprite rivalSprite;
    public Sprite blueSprite;
    private bool started = false;
    // Use this for initialization
    void Start()
    {
        //Calls the initialisation method from the base class
        base.OnCreate();
        //This generates a random number to decide if you are facing your rival 1/7 chance
        int isRival = Random.Range(1,8);
        panel.SetActive(true);
        if (isRival == 1)
        {
            //Initialise rival ship here
            maxHealth = manager.hullHealth[4];
            speed = manager.sailsSpeed[4];
            cannonLevel = 5;
            //Change the sprite to a rival sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = rivalSprite;
            //Display the message to the player about the rival battle
            GameObject.Find("WinText").GetComponent<Text>().text = "Rival Battle!";
        }
        else
        {
            //Random ship sprite
            if (isRival<=4)
                gameObject.GetComponent<SpriteRenderer>().sprite = blueSprite;
            //Match players ship
            maxHealth = manager.hullHealth[manager.hullLevel-1];
            speed = manager.sailsSpeed[manager.sailsLevel-1];
            cannonLevel = manager.cannonLevel;
            GameObject.Find("WinText").GetComponent<Text>().text = "Ship Battle!";
        }
        health = maxHealth;
        cannonDamage = manager.cannonDamage[cannonLevel - 1];
    }
    
    // Update is called once per frame
    void Update()
    {
        if (rivalTimer < 3)
        {
            rivalTimer += Time.deltaTime;
            GameObject.Find("TapText").GetComponent<Text>().text = "Starting in " + (3-(int)rivalTimer).ToString();
        }
        else if (started != true)
        {
            panel.SetActive(false);
            started = true;
        }
        timeSinceFire += Time.deltaTime;
        if (IsDead())
        {
            //Displays win mesage and changes scene
            endCount += Time.deltaTime;
            if (endCount > 5)
            {
                Application.LoadLevel("ExtendableMap");
            }
        }
    }

    void FixedUpdate()
    {
        if (!panel.activeSelf)
        {
            //The vector between the two boats
            Vector2 directionOfTravel = theirBody.position - myBody.position;
            //AI logic that changes when the enemy ship is "in range".
            if (directionOfTravel.magnitude < 6)
            {
                directionOfTravel = Aim(directionOfTravel);
                //If in range, attempt to fire
                TryCooldown(cannonLevel, cannonDamage);
            }
            
            //Normalises the direction of travel
            directionOfTravel = directionOfTravel.normalized * speed;
            Vector2 shipForce = myBody.GetComponent<Transform>().up.normalized * speed;
            Vector2 worldBounds = new Vector2(Screen.width, Screen.height);
            worldBounds = Camera.main.ScreenToWorldPoint(worldBounds);
            //Stops the ship from exiting the screen
            if ((myBody.position.y + directionOfTravel.y > worldBounds.y) ||
              (myBody.position.y + directionOfTravel.y < -worldBounds.y))
            {
                directionOfTravel.y = -directionOfTravel.y;
            }
            if ((myBody.position.x + directionOfTravel.x > worldBounds.x) ||
                (myBody.position.x + directionOfTravel.x < -worldBounds.x))
            {
                directionOfTravel.x = -directionOfTravel.x;
            }
            Debug.DrawLine(myBody.position, (myBody.position + directionOfTravel));
            rotateTowards(directionOfTravel);

            Debug.DrawLine(myBody.position, (myBody.position + shipForce));
            //Restricts the speed of the ship
            if (myBody.velocity.magnitude < speed)
            {
                myBody.AddForce(shipForce);
            }
        }
    }

    //Handles collision with cannonballs
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            CreateExplosion(other.transform.position);
            int damage = other.gameObject.GetComponent<BallController>().getDamage();
            health -= damage;
            Destroy(other.gameObject);
            if (health <= 0)
                if (!(panel.activeSelf))
                {
                    StartEnd(true,
                        maxHealth,
                        manager.hullHealth[manager.hullLevel - 1] -health);
                }
        }
    }
    
    //Makes the ship try to aim towards the player's ship
    Vector2 Aim(Vector2 directionOfTravel)
    {
        //Switches the direction to 90 degrees away.
        directionOfTravel = new Vector2(directionOfTravel.y, -directionOfTravel.x);
        return directionOfTravel;
    }
}
