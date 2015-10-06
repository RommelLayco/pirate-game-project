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
        base.OnCreate();
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceFire = timeSinceFire + Time.deltaTime;
        if (health<=0)
        {
            endCount += Time.deltaTime;
            diedText.text = "YOU WIN";
            if (endCount > 5)
            {
                Application.LoadLevel("Main");
            }
        }
        TryCooldown();
    }

    void FixedUpdate()
    {
        //Make the enemy aim towards somewhere 90 degrees away from the boat
        Vector2 directionOfTravel = theirBody.position - myBody.position;
        /*if (directionOfTravel.distance > something do 1
        else do 2*/
        directionOfTravel = new Vector2(directionOfTravel.y, -directionOfTravel.x);
        directionOfTravel = directionOfTravel.normalized * speed;
        Vector2 worldBounds = new Vector2(Screen.width, Screen.height);
        worldBounds = Camera.main.ScreenToWorldPoint(worldBounds);

        //else the player must be within range
        if ((myBody.position.y+directionOfTravel.y>worldBounds.y)||
            (myBody.position.y+directionOfTravel.y<-worldBounds.y))
        {
            directionOfTravel.y = -directionOfTravel.y;
        }
        if ((myBody.position.x + directionOfTravel.x > worldBounds.x) ||
            (myBody.position.x + directionOfTravel.x < -worldBounds.x))
        {
            directionOfTravel.x = -directionOfTravel.x;
        }
        rotateTowards(directionOfTravel);
        Vector2 shipForce = directionOfTravel;
        if(myBody.velocity.magnitude<speed)
        {
            myBody.AddForce(shipForce);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            CreateExplosion(other.transform.position);
            Destroy(other.gameObject);
            health-=20;
        }
    }
}