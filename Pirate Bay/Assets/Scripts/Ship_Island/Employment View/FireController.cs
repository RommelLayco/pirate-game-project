using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireController : MonoBehaviour {
    private Text capacityInfo;
    private int crewSize;

    void Awake() {
        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        crewSize = GameObject.Find("GameManager").GetComponent<GameManager>().crewSize;
        if (crewSize == 1) {
            gameObject.GetComponent<Button>().interactable = false;
        } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
        setInfoText();
    }

    public void onClickFire() {
        GameObject.Find("GameManager").GetComponent<GameManager>().crewSize = crewSize - 1;
    }

    private void setInfoText() {
        capacityInfo.text = "Capacity: " + crewSize + " / " + GameObject.Find("GameManager").GetComponent<GameManager>().crewMax;
    }
}
