using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColourGuideController : MonoBehaviour {

	void Start () {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        foreach (Button b in buttons) {
            b.interactable = false;
        }
	}
}
