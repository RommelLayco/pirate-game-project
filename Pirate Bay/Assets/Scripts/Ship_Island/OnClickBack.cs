using UnityEngine;
using System.Collections;

public class OnClickBack : MonoBehaviour {
    public void backClicked() {
		// load the scene previous
        Application.LoadLevel("Ship");

		// also save the crew member weapon and armour

    }

    public void loadScene(string sceneName) {
        Application.LoadLevel(sceneName);
    }
}