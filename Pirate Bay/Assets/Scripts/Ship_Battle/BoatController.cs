using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatController : Ship {

    public Transform boat;
    public Transform dotPrefab;
    public Sprite xSprite;
    public Queue dots = new Queue();

    private Transform currentDot;
    private Vector2 lastTouchPos;
    private Vector2 rotation;
    private int dotCount;
    private bool deleteDots;

    // Use this for initialization
    void Start()
    { 
        dotCount = 0;
        lastTouchPos = myBody.position;
        base.OnCreate();
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
        }
        countText.text = dotCount.ToString();
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                deleteDots = true;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (deleteDots)
                    ClearDots();
                Vector2 newTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (Vector2.Distance(newTouchPos, lastTouchPos) > 1 && dotCount < 10)
                {
                    diedText.text = "Drawing Dots";
                    MakeADot(newTouchPos);
                    lastTouchPos = newTouchPos;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                deleteDots = false;
                if (touch.tapCount==1)
                {
                    TryCooldown();
                }
                Transform[] dotArray = (Transform[])dots.ToArray();
                Transform lastDot = dotArray[dotArray.Length - 1];
                lastDot.GetComponent<SpriteRenderer>().sprite = xSprite;
            }
        }
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
            Vector2 dirToDot = aimDotPos - myBody.position;
            rotateTowards(dirToDot);
            Vector2 shipForce = dirToDot.normalized * speed;
            if (myBody.velocity.magnitude < speed)
            {
                myBody.AddForce(shipForce);
            }
        }
    }
    void ClearDots()
    {
        diedText.text = "Destroying Dots";
        foreach (Transform d in dots)
        {
            Destroy(d.gameObject);
        }
        dotCount = 0;
        if (currentDot != null)
            Destroy(currentDot.gameObject);
        currentDot = null;
        dots.Clear();
        diedText.text = "Dots Destroyed";
        deleteDots = false;
    }
    Transform MakeADot(Vector2 position)
    {
        Transform dot = (Transform)Instantiate(dotPrefab, position, Quaternion.identity);
        dotCount++;
        dots.Enqueue(dot);
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
            CreateExplosion(other.transform.position);
            FireCountUpdate(false);
            Destroy(other.gameObject);
            health-=20;
        }
    }
}
