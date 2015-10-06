using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
                Application.LoadLevel("Main");
            }
        }
    }

    void FixedUpdate()
    {
        //The vector between the two boats
        Vector2 directionOfTravel = theirBody.position - myBody.position;
        //AI logic that changes when the enemy ship is "in range".
        if (directionOfTravel.magnitude < 7)
        {
            directionOfTravel = Aim(directionOfTravel);
            //If in range, attempt to fire
            TryCooldown();
        }
        //Normalises the direction of travel
        directionOfTravel = directionOfTravel.normalized * speed;
              
        rotateTowards(directionOfTravel);
        Vector2 shipForce = directionOfTravel;
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

        //Switches the direction to 90 degrees away.
        directionOfTravel = new Vector2(directionOfTravel.y, -directionOfTravel.x);
        return directionOfTravel;
    }
}