using UnityEngine;
using System.Collections;

public class ViewTransition : MonoBehaviour {

    //Load Maze scene
    public void Maze()
    {
        Application.LoadLevel("Maze");
    }
    public void Map()
    {
        Application.LoadLevel("ExtendableMap");
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
        Application.LoadLevel("ship_battle");
    }

    public void Main()
    {
        Application.LoadLevel("Main");
    }
}
