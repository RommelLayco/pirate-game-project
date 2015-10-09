using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
* This class is a base class for the PlayerShip and EnemyShip classes
* It mainly contains protected variables and functions.
* Authors: Benjamin Frew, Nicholas Molloy
*/
public class Ship : MonoBehaviour {
    //The health of the ship
    public int health;

    //The text element used to display to the screen.
    public Text diedText;

    //The speed of the ship, also controls speed of rotation.
    public float speed;

    //The rigidbodies of both ships
    public Rigidbody2D myBody;
    public Rigidbody2D theirBody;

    //The prefabs used for creating explosions and firing cannonballs
    public Transform explosionPrefab;
    public Transform cannonballPrefab;

    //The time between firing of cannonballs
    public float coolDown;

    //Keeps count of if the cannons have cooled down
    protected float timeSinceFire;

    //Keeps count of the end screen
    protected float endCount;

    //The game manager object
    protected GameManager manager;

    //Method called by the inheriting class
    protected void OnCreate() {
        //Initialisation of variables
        manager = GameManager.getInstance();
        endCount = 0;
        timeSinceFire = 0;
        diedText.text = "";
        myBody = GetComponent<Rigidbody2D>();
    }

    //Checks if the boat has been destroyed
    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    //Checks if the cannons have cooled down enough to fire
    protected void TryCooldown(int level, int damage)
    {
        if (timeSinceFire > coolDown)
        {
            if (level > 4)
            {
                Fire(true, -1, damage);
                Fire(false, -1, damage);
            }
            if (level > 2)
            {
                Fire(true, 1, damage);
                Fire(false, 1, damage);
            } 
            Fire(true, 0, damage);
            Fire(false, 0, damage);
            timeSinceFire = 0;
        } 
    }

    //Fires left if the bool is true, right if false
    protected void Fire(bool left, int offset, int damage)
    {
        int mod;
        if (left)
            mod = -1;
        else
            mod = 1;

        //Calculates the force to be added to the ball, fires the ball.
        Vector2 ballForce = mod * myBody.GetComponent<Transform>().right;
        Vector2 upDirection = myBody.GetComponent<Transform>().up * offset/3;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(myBody.position.x, myBody.position.y) + ballForce + upDirection), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(200 * ballForce.normalized);
        ball.GetComponent<BallController>().setDamage(damage);
    }

    //Rotates the ship towards the direction in which it is travelling
    protected void rotateTowards(Vector2 directionOfTravel)
    {
        float angle = -(90 - (Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg));
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    //Creates explosion at a position
    protected void CreateExplosion(Vector2 position)
    {
        Transform explosion = (Transform)Instantiate(explosionPrefab, position, Quaternion.identity);
    }
}
