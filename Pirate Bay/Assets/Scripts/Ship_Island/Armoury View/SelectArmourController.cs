using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectArmourController : MonoBehaviour {
    private GameManager manager;

    private GameObject panel = null;
    private Image image;


    void Awake() {
        manager = GameManager.getInstance();
    }
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
        }
        image.enabled = false;
        
    }

    // when the armour image is clicked...make everything visible
    public void clicked() {
        //Need to get the panel then call get component on that
        gameObject.GetComponent<Button>().interactable = false;
        GameObject.Find("SelectPanel").GetComponent<DisplayController>().armourClicked();

        image.enabled = true;

        Image[] images = panel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = true;
        }

        Renderer[] renderers = panel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = true;
        }

        Text[] texts = panel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = true;
        }


    }
}
