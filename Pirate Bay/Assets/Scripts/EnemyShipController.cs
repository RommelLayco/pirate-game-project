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

    private Vector2 velocity;
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
        Vector2 ballVelocity = boat.up;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(enemyBody.position.x, enemyBody.position.y) + ballVelocity), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = 10 * ballVelocity.normalized;
    }
    void FixedUpdate()
    {
        Vector2 destination = boatBody.position;
        Vector2 directionOfTravel = boatBody.position - enemyBody.position;
        rotateTowards(directionOfTravel);
        enemyBody.velocity = (directionOfTravel.normalized * speed);
    }
    void rotateTowards(Vector2 directionOfTravel)
    {
        float angle = -(90-(Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg));
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}
