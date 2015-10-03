using UnityEngine;
using System.Collections;

public class OnClickBack : MonoBehaviour {
    public void backClicked() {
        Debug.Log("Inside Back Clicked");
        Application.LoadLevel("Ship");
    }

    public void loadScene(string sceneName) {
        Application.LoadLevel(sceneName);
    }
}