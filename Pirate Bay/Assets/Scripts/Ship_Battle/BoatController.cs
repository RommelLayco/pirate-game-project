using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
* This class controls the player's ship.
* It uses methods from the base class Ship.
* Authors: Benjamin Frew, Nick Molloy.
*/
public class BoatController : Ship {

    //The prefab for the destination dots
    public Transform dotPrefab;

    //The destination points as a queue.
    public Queue dots = new Queue();
    
    //The current destination of the ship (first dot in the queue)
    private Transform currentDot;

    //The location of the last dot
    private Vector2 lastTouchPos;
    
    //The number of dots
    private int dotCount;

    //Whether or not a new line is being drawn
    private bool deleteDots;

    // Used for initialization
    void Start()
    { 
        base.OnCreate();
        dotCount = 0;
        lastTouchPos = myBody.position;
        maxHealth = manager.hullHealth[manager.hullLevel - 1];
        health = maxHealth;
        speed = manager.sailsSpeed[manager.sailsLevel - 1];
        cannonLevel = manager.cannonLevel;
        cannonDamage = manager.cannonDamage[cannonLevel - 1];

        //Calls base class initialisation
    }
    
    // Update is called once per frame
    void Update()
    {
        timeSinceFire += Time.deltaTime;

        //Displays failure message and ends scene if dead.
        if (IsDead())
        {
            endCount += Time.deltaTime;
            if (endCount > 5)
            {
                Application.LoadLevel("ExtendableMap");
            }
        }

        //Loops through the touches in the last frame.
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                //Assume a line is being drawn upon starting touch
                deleteDots = true;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //Realise that a line is in fact being drawn, delete previous line if start of a new one.
                if (deleteDots)
                    ClearDots();
                Vector2 newTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

                //Create a new destination point if it is far enough away from the last one and not too many dots.
                if (Vector2.Distance(newTouchPos, lastTouchPos) > 1 && dotCount < 10)
                {
                    MakeADot(newTouchPos);
                    lastTouchPos = newTouchPos;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //End of a line or tap, attempt to fire.
                deleteDots = false;
                TryCooldown(cannonLevel,cannonDamage);
            }
        }
    }

    void FixedUpdate()
    {
        //Go to a dot if there is one
        if ((dotCount != 0)&&(!panel.activeSelf))
        {
            if (currentDot == null)
            {
                currentDot = (Transform)dots.Dequeue();
            }
            Vector2 aimDotPos = currentDot.position;
            Vector2 dirToDot = aimDotPos - myBody.position;
            rotateTowards(dirToDot);
            Vector2 shipForce = dirToDot.normalized * speed;
            if (myBody.velocity.magnitude < speed)
            {
                myBody.AddForce(shipForce);
            }
        }
    }

    //Clear the list of dots and destroy them
    void ClearDots()
    {
        foreach (Transform d in dots)
        {
            Destroy(d.gameObject);
        }
        dotCount = 0;
        if (currentDot != null)
            Destroy(currentDot.gameObject);
        currentDot = null;
        dots.Clear();
        deleteDots = false;
    }

    //Create a dot and add it to the queue
    Transform MakeADot(Vector2 position)
    {
        Transform dot = (Transform)Instantiate(dotPrefab, position, Quaternion.identity);
        dotCount++;
        dots.Enqueue(dot);
        return dot;
    }

    //Remove dots from queue, destroy balls
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dot"))
        {
            //Checks if it is the current desination
            if (other.gameObject.Equals(currentDot.gameObject))
            {
                Destroy(other.gameObject);
                dotCount--;
                currentDot = null;
            }
        } else if (other.gameObject.CompareTag("Ball"))
        {
            CreateExplosion(other.transform.position);
            int damage = other.gameObject.GetComponent<BallController>().getDamage();
            health -= damage;
            Destroy(other.gameObject);
            if (health <= 0) { 
                if (!(panel.activeSelf)) {
                    StartEnd(false,
                        theirBody.GetComponent<EnemyShipController>().maxHealth -
                        theirBody.GetComponent<EnemyShipController>().health,
                        manager.hullHealth[manager.hullLevel - 1]);
                }
            }
        }
    }
}
