using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitPanelController : MonoBehaviour {

	public void exitPanel(){

		GameObject panel  = GameObject.FindGameObjectWithTag ("Panel");

		Image[] images = panel.GetComponentsInChildren<Image> ();
		foreach (Image r in images) {
			r.enabled = false;
		}

		panel.GetComponent<Image> ().enabled = false;



	}
}
