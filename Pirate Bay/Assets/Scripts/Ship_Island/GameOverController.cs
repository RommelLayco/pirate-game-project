using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (GameManager.getInstance().crewMembers.Count == 0) {
            //Crew must be empty, so they all dies on an exploration therefore GameOver
            Application.LoadLevel("GameOver");
        }
	}
}
