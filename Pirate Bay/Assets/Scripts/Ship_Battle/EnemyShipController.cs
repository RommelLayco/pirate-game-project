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
   
    // Use this for initialization
    void Start()
    {
        endCount = 0;
        timeSinceFire = 0;
        //Calls the initialisation method from the base class
        base.OnCreate();
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceFire += Time.deltaTime;
        if (IsDead())
        {
            //Displays win mesage and changes scene
            endCount += Time.deltaTime;
            diedText.text = "YOU WIN";
            if (endCount > 5)
            {
                Application.LoadLevel("ExtendableMap");
            }
        }
    }

    void FixedUpdate()
    {
        //The vector between the two boats
        Vector2 directionOfTravel = theirBody.position - myBody.position;
        //AI logic that changes when the enemy ship is "in range".
        if (directionOfTravel.magnitude < 6)
        {
            directionOfTravel = Aim(directionOfTravel);
            //If in range, attempt to fire
            TryCooldown(false);
        }

        //Stops the ship from exiting the screen

        //Normalises the direction of travel
        directionOfTravel = directionOfTravel.normalized * speed;
        Vector2 shipForce = myBody.GetComponent<Transform>().up.normalized * speed;
        Vector2 worldBounds = new Vector2(Screen.width, Screen.height);
        worldBounds = Camera.main.ScreenToWorldPoint(worldBounds);
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
              
        Debug.DrawLine(myBody.position,(myBody.position + shipForce));
        //Restricts the speed of the ship
        if (myBody.velocity.magnitude<speed)
        {
            myBody.AddForce(shipForce);
        }
    }

    //Handles collision with cannonballs
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            CreateExplosion(other.transform.position);
            Destroy(other.gameObject);
            health-=20;
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