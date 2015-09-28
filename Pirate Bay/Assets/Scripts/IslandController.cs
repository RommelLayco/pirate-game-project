using UnityEngine;
using System.Collections;

public class IslandController : MonoBehaviour {
    void Update() {
        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if (contained) {
                    Debug.Log("LEEDLE LEEDLE LEEDLE LEE");
                }
            }
        }
    }
    void OnMouseDown() {
        Debug.Log("On MouseDown");
    }

}
