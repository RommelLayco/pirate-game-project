using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectWeaponController : MonoBehaviour {

    // Use this for initialization
    private GameObject panel = null;
    private Image image;


    // Use this for initialization
    void Start() {
        panel = GameObject.FindGameObjectWithTag("Panel");

        image = panel.GetComponent<Image>();

        // this makes the panel not visible
        Image[] images = panel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = false;
        }

        // make all the sprites inivisible
        Renderer[] renderers = panel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = false;
        }

        // set any text to invisible when panel is shown
        Text[] texts = panel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = false;
            Destroy(r);
        }


        image.enabled = false;
    }

    // Update is called once per frame
    void Update() {

        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if (contained)
                    clicked();
            }
        }
    }

    void OnMouseDown() {
        clicked();
    }

    // when the weapon image is clicked...make everything visible
    void clicked() {
        GameObject.Find("SelectPanel").GetComponent<DisplayController>().weaponClicked();

        image.enabled = true;

        Image[] images = panel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = true;
        }

        Renderer[] renderers = panel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = true;
        }

        // set text to true
        Text[] texts = panel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = true;
        }


    }
}
