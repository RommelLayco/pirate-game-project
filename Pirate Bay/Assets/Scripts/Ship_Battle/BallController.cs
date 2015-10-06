using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    public float limit;

    private float currentTime;
    private Transform ball;
    private int fireCount;

	// Use this for initialization
	void Start () { 
        currentTime = 0;
	}
	void Awake()
    {
        ball = GetComponent<Transform>();
    }
	// Update is called once per frame
	void Update () {
	    if (currentTime<limit)
        {
            currentTime = currentTime+Time.deltaTime;
        } else
        {
            Destroy(ball.gameObject);
            this.enabled = false;
        }
	}
}
