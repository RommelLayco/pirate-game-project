﻿using UnityEngine;
using System.Collections;

public class Hire_FireRoomController : MonoBehaviour {
    //Opens the HireFire scene that manages the crew size
    void OnMouseDown() {
        clicked();
    }

    void clicked() {
        Application.LoadLevel("HireFire");
    }
}