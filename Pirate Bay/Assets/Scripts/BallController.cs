using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
    private float lifetime;
    public float limit;
    public Transform ball;
    public Text fireText;
    private int fireCount;

	// Use this for initialization
	void Start () {
        lifetime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        fireCount = int.Parse(fireText.text);
	    if (lifetime<limit)
        {
            lifetime = lifetime + 1 * Time.deltaTime;
        } else
        {
            Destroy(ball);
            fireCount--;
            fireText.text = fireCount.ToString();
        }
	}
}
