using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    public float limit;

    private float currentTime;
    private Transform ball;

	// Use this for initialization
	void Start () { 
        currentTime = 0;
        ball = GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update () {
        //Destroys the ball after a certain amount of time
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
