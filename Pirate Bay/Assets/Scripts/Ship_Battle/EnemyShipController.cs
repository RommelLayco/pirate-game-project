using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyShipController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D boatBody;
    public Transform boat;
    public Transform cannonballPrefab;
    public Rigidbody2D enemyBody;
    public float coolDown;
    public Text fireText;
    public Text diedText;
    public int health;

    private float timeSinceFire;
    private Vector2 rotation;

    // Use this for initialization
    void Start()
    {
        timeSinceFire = 0;
    }

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health<=0)
        {
            diedText.text = "YOU WIN";
        }
        timeSinceFire = timeSinceFire + Time.deltaTime;
        if (timeSinceFire>coolDown)
        {
            timeSinceFire = 0;
            Fire(true);
            Fire(false);
        }
    }

    void Fire(bool left)
    {
        int mod;
        if (left)
        {
            mod = -1;
        }
        else
        {
            mod = 1;
        }
        Vector2 ballForce = mod * boat.right;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(enemyBody.position.x, enemyBody.position.y) + ballForce), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(200 * ballForce.normalized);
        ball.GetComponent<BallController>().fireText = fireText;
    }
    void FixedUpdate()
    {
        Vector2 destination = boatBody.position;
        //Make the enemy aim towards somewhere 90 degrees away from the boat
        Vector2 directionOfTravel = boatBody.position - enemyBody.position;
        directionOfTravel = new Vector2(directionOfTravel.y, -directionOfTravel.x);
        rotateTowards(directionOfTravel);
        Vector2 shipForce = directionOfTravel.normalized * speed;
        if(enemyBody.velocity.magnitude<speed)
        {
            enemyBody.AddForce(shipForce);
        }
        Debug.DrawLine(enemyBody.position, enemyBody.position + shipForce);
    }
    void rotateTowards(Vector2 directionOfTravel)
    {
        float angle = -(90-(Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg));
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            health--;
        }

    }
}