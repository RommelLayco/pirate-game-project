using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
    }
}
