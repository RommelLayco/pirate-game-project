using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    public float limit;
    public Text fireText;

    private float currentTime;
    private Transform ball;
    private int fireCount;

	// Use this for initialization
	void Start () { 
        currentTime = 0;
        fireCount = int.Parse(fireText.text);
        fireCount++;
	}
	void Awake()
    {
        ball = GetComponent<Transform>();
    }
	// Update is called once per frame
	void Update () {
        fireCount = int.Parse(fireText.text);
	    if (currentTime<limit)
        {
            currentTime = currentTime+Time.deltaTime;
        } else
        {
            Destroy(ball.gameObject);
            fireCount--;
            fireText.text = fireCount.ToString();
            this.enabled = false;
        }
	}
}
