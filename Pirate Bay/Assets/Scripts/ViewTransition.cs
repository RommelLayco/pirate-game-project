﻿using UnityEngine;
using System.Collections;

public class ViewTransition : MonoBehaviour {

    //Load Maze scene
    public void Maze()
    {
        Application.LoadLevel("Maze");
    }

    public void Ship()
    {
        Application.LoadLevel("Ship");
    }

    public void Turn()
    {
        Application.LoadLevel("combat");
    }

    public void sBattle()
    {
        
    }
}
