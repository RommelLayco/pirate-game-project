﻿using UnityEngine;
using System.Collections;

public class Hire_FireRoomController : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {

                //bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                //if (contained) {
                  //  clicked();
                //}
            }
        }
    }

    void OnMouseDown() {
        clicked();
    }

    void clicked() {
        Application.LoadLevel("HireFire");
    }
}