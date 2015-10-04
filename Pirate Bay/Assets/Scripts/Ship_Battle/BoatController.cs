using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatController : MonoBehaviour {

    public Text countText;
    public Text fireText;
    public Text diedText;
    public Canvas canvas;

    public float speed;
    public Rigidbody2D boatBody;
    public Transform boat;
    public Transform dotPrefab;
    public Transform cannonballPrefab;
    public Queue dots = new Queue();
    public int health;

    private Transform currentDot;
    private Touch lastTouch;
    private Vector2 rotation;
    private int dotCount;
    private int fireCount;
    private float endCount;
    // Use this for initialization
    void Start()
    { 
        dotCount = 0;
        fireText.text = 0.ToString();
        diedText.text = "";
        endCount = 0;
    }

    void Awake()
    { 
        boatBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health <=0)
        {
            endCount += Time.deltaTime;
            diedText.text = "You Died";
            if (endCount>5)
            {
                Application.LoadLevel("Main");
            }
            //new ScreenFader(canvas);
        }
        countText.text = dotCount.ToString();
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.tapCount == 1)
                {
                    Fire(true);
                    Fire(false);
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 lastTouchPos = Camera.main.ScreenToWorldPoint(lastTouch.position);
                Vector2 newTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (Vector2.Distance(newTouchPos, lastTouchPos) > 1 && dotCount < 10)
                {
                    dots.Enqueue(MakeADot(Camera.main.ScreenToWorldPoint(touch.position)));
                    lastTouch = touch;
                }
            }
        }
    }
    
    void Fire(bool left) {
        int mod;
        if (left)
        {
            mod = -1;
        }
        else
        {
            mod = 1;
        }
        Vector2 ballForce = mod*boat.right;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(boatBody.position.x,boatBody.position.y)+ballForce), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(200 * ballForce.normalized);
        ball.GetComponent <BallController>().fireText = fireText;
    }
    void FixedUpdate()
    {
        if (dotCount != 0)
        {
            if (currentDot == null)
            {
                currentDot = (Transform)dots.Dequeue();
            }
            Vector2 aimDotPos = currentDot.position;
            Vector2 dirToDot = aimDotPos - boatBody.position;
            rotateTowards(dirToDot);
            Vector2 shipForce = dirToDot.normalized * speed;
            if (boatBody.velocity.magnitude < speed)
            {
                boatBody.AddForce(shipForce);
            }
            Debug.DrawLine(boatBody.position, boatBody.position + shipForce);
        }
    }
    void rotateTowards(Vector2 directionOfTravel)
    {
        float angle = -(90 - (Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg));
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
    Transform MakeADot(Vector2 position)
    {
        Transform dot = (Transform)Instantiate(dotPrefab, position, Quaternion.identity);
        dotCount++;
        return dot;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dot"))
        {
            Destroy(other.gameObject);
            dotCount--;
            currentDot = null;
        } else if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            health--;
        }
    }
}
