using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatController : MonoBehaviour {

    public Text countText;
    public Text fireText;
    public float speed;
    public Rigidbody2D boatBody;
    public Transform boat;
    public Transform dotPrefab;
    public Transform cannonballPrefab;
    public Transform[] dots = new Transform[10];

    private Touch lastTouch;
    private Vector2 velocity;
    private Vector2 rotation;
    private int dotCount;
    private int fireCount;


    // Use this for initialization
    void Start()
    { 
        dotCount =0;
    }

    void Awake()
    { 
        boatBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        countText.text = dotCount.ToString();
        fireText.text = fireCount.ToString();

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.tapCount == 1)
                {
                    Fire();
                    fireCount++;
                } else if ( touch.phase == TouchPhase.Moved) { 
                    foreach (Transform dot in dots)
                    {
                        Destroy(dot.gameObject);
                    }
                    dotCount = 0;
                    dots.Initialize();
                }
            } else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 lastTouchPos = Camera.main.ScreenToWorldPoint(lastTouch.position);
                Vector2 newTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (Vector2.Distance(newTouchPos,lastTouchPos) > 1 && dotCount < 10)
                {
                    dots[dotCount] = MakeADot(Camera.main.ScreenToWorldPoint(touch.position));
                    lastTouch = touch;
                }
            }
        }
    }
    
    void Fire() {
        Vector2 ballVelocity = -1*boat.right;
        Transform ball = (Transform)Instantiate(cannonballPrefab, (
            new Vector2(boatBody.position.x,boatBody.position.y)+ballVelocity), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = 2*ballVelocity.normalized;
    }

    void FixedUpdate()
    {
        if (dotCount != 0)
        {
            Vector2 aimDotPos = dots[0].position;
            Vector2 dirToDot = aimDotPos - boatBody.position;
            rotateTowards(dirToDot);
            boatBody.velocity = (dirToDot.normalized * speed);
        }
        else
        {
            boatBody.velocity =(Vector2.zero);
        }
    }
    void rotateTowards(Vector2 directionOfTravel)
    {
        float angle = Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg;
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
            other.gameObject.SetActive(false);
            dotCount--;
            shuffle();
        }
    }
    void shuffle()
    {
        for (int i=1;i<dots.Length;i++)
        {
            dots[i - 1] = dots[i];
        }
    }
}
