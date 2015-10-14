using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
* This class controls the enemy's ship.
* It uses the methods from the base class Ship.
* Authors: Benjamin Frew, Nick Molloy
*/
public class EnemyShipController : Ship
{
    public Sprite blueSprite;
    public Sprite redSprite;
    public Sprite whiteSprite;

    private bool started = false;
    private float rivalTimer = 0;
    private int bluechance;
    private int redchance;
    private int whitechance;
    private int isRival;

    // Use this for initialization
    void Start()
    {
        //Calls the initialisation method from the base class
        base.OnCreate();

        bluechance = manager.blueRivalry;
        redchance = manager.blueRivalry + manager.redRivalry;
        whitechance = manager.blueRivalry + manager.redRivalry + manager.whiteRivalry;
        //This generates a random number to decide if you are facing your rival 1/7 chance
        isRival = Random.Range(1,11);
        panel.SetActive(true);
        if (isRival <=bluechance)
        {
            BlueBattle();
        }
        else if (isRival <=(redchance))
        {
            RedBattle();
        }
        else if (isRival <=(whitechance))
        {
            WhiteBattle();
        }
        else
        {
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
                    ResetRival();
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
    void BlueBattle()
    {
        //Display the message to the player about the rival battle
        GameObject.Find("WinText").GetComponent<Text>().text = "Rival Battle!";
        GameObject.Find("GoldText").GetComponent<Text>().text = "Facing BlueBeard";
        //Initialise rival ship here
        maxHealth = manager.hullHealth[4];
        speed = manager.sailsSpeed[0];
        cannonLevel = 1;
        //Change the sprite to bluebeard's sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = blueSprite;
    }
    void RedBattle()
    {
        //Display the message to the player about the rival battle
        GameObject.Find("WinText").GetComponent<Text>().text = "Rival Battle!";
        GameObject.Find("GoldText").GetComponent<Text>().text = "Facing RedBeard";
        //Initialise rival ship here
        maxHealth = manager.hullHealth[1];
        speed = manager.sailsSpeed[1];
        cannonLevel = 5;
        //Change the sprite to redbeard's sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = redSprite;
    }
    void WhiteBattle()
    {
        //Display the message to the player about the rival battle
        GameObject.Find("WinText").GetComponent<Text>().text = "Rival Battle!";
        GameObject.Find("GoldText").GetComponent<Text>().text = "Facing Whitebead";
        //Initialise rival ship here
        maxHealth = manager.hullHealth[0];
        speed = manager.sailsSpeed[4];
        cannonLevel = 1;
        //Change the sprite to whitebeard's sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
    }
    void ResetRival()
    {
        //Resets the rivalry of the battled rival to 1.
        if (isRival <= bluechance)
        {
            manager.blueRivalry = 1;
        }
        else if (isRival <= (redchance))
        {
            manager.redRivalry = 1;
        }
        else if (isRival <= (whitechance))
        {
            manager.whiteRivalry = 1;
        }
    }
}
