using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OutlineController : MonoBehaviour {

    public Sprite red;
    public Sprite yellow;
    public Sprite green;
    public enum colours {RED, YELLOW, GREEN, NONE };

    public void setSprite(colours c) {
        switch (c) {
            case colours.RED:
                GetComponent<Image>().sprite = red;
                break;
            case colours.GREEN:
                GetComponent<Image>().sprite = green;
                break;

            case colours.YELLOW:
                GetComponent<Image>().sprite = yellow;
                break;
            default:
                GetComponent<Image>().sprite = null;
                break;
        }
    }

}
