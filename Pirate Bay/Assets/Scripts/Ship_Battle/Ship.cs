using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

    public int health;
    public Canvas canvas;
    public Text fireText;
    public Text diedText;
    public Text countText;

    public float speed;
    public Rigidbody2D myBody;
    public Rigidbody2D theirBody;
    public Transform cannonballPrefab;
    public float coolDown;

    protected float timeSinceFire;
    protected float endCount;
    protected int fireCount;
    // Use this for initialization
    protected void OnCreate() {
        endCount = 0;
        timeSinceFire = 0;
        fireText.text = 0.ToString();
        diedText.text = "";
        myBody = GetComponent<Rigidbody2D>();
    }
    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }
    protected void TryCooldown()
    {
        if (timeSinceFire > coolDown)
        {
            Fire(true);
            Fire(false);
            timeSinceFire = 0;

        } 
    }
    protected void Fire(bool left)
    {
        FireCountUpdate(true);
        int mod;
        if (left)
        {
            mod = -1;
        }
        else
        {
            mod = 1;
        }
        Vector2 ballForce = mod * myBody.GetComponent<Transform>().right;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(myBody.position.x, myBody.position.y) + ballForce), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(200 * ballForce.normalized);
        ball.GetComponent<BallController>().fireText = fireText;
    }
    protected void rotateTowards(Vector2 directionOfTravel)
    {
        float angle = -(90 - (Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg));
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
    protected void FireCountUpdate(bool plus)
    {
        fireCount = int.Parse(fireText.text);
        if (plus)
            fireCount++;
        else
            fireCount--;
        fireText.text = fireCount.ToString();
    }
    protected void CreateExplosion(Vector2 position)
    {
        //Make boom
    }
}
