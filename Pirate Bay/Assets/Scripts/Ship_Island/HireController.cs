using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HireController : MonoBehaviour {
    public Text capacityInfo;
    private int crewSize;

    void Awake() {
        GameManager g = GameObject.Find("GameManager").GetComponent<GameManager>();
        setInfoText();
    }

    void Update() {
        crewSize = GameObject.Find("GameManager").GetComponent<GameManager>().crewSize;
        setInfoText();
    }

    public void onClickHire() {
        GameObject.Find("GameManager").GetComponent<GameManager>().crewSize = crewSize + 1;
    }

    public void onClickFire() {
        GameObject.Find("GameManager").GetComponent<GameManager>().crewSize = crewSize - 1;
    }

    private void setInfoText() {
        capacityInfo.text = "Capacity: " + crewSize + " / " + GameObject.Find("GameManager").GetComponent<GameManager>().crewMax;
    }
}