using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExitPanelController : MonoBehaviour {

	public void exitPanel(){


		GameObject panel  = GameObject.FindGameObjectWithTag ("Panel");

		Image[] images = panel.GetComponentsInChildren<Image> ();
		foreach (Image r in images) {
			r.enabled = false;
		}

		Renderer[] renderers = panel.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in renderers) {
			r.enabled = false;
		}

		// set text to false
		Text[] texts = panel.GetComponentsInChildren<Text> ();
		foreach (Text r in texts) {
			r.enabled = false;
		}

		panel.GetComponent<Image> ().enabled = false;



	}
}
