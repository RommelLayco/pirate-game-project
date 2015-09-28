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
    private Vector2 rotation;

    // Use this for initialization
    void Start()
    {
    }

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    void Fire()
    {
        Vector2 ballForce = boat.up;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(enemyBody.position.x, enemyBody.position.y) + ballForce), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(10 * ballForce.normalized);
    }
    void FixedUpdate()
    {
        Vector2 destination = boatBody.position;
        Vector2 directionOfTravel = boatBody.position - enemyBody.position;
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
}
