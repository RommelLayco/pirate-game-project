using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaptainScript : MonoBehaviour {
    private GameManager manager;
    private Text captainInfo;
    private Text captainName;
    private Text newText;

    void Awake () {
        manager = GameManager.getInstance();
        captainInfo = GameObject.Find("CaptainData").GetComponent<Text>();
        captainName = GameObject.Find("CaptainName").GetComponentInChildren<InputField>().GetComponentInChildren<Text>();
        newText = GameObject.Find("InputText").GetComponent<Text>();

        setCaptainInfo();
    }

    void Update() {
        setCaptainInfo();
    }

    public void setCaptainName() {
        string newName = newText.text;
        if (newName.Length != 0) {
            manager.captainName = newName;
        }
        GameObject.Find("CaptainName").GetComponentInChildren<InputField>().text = "";
        setCaptainInfo();
    }

    private void setCaptainInfo() {
        captainName.text = manager.captainName;
        newText.text = manager.captainName;
        captainInfo.text = "\n" + manager.gold + "\n" + manager.crewSize + "\n" + manager.notoriety;
    }

    public void onClickSave() {
        Debug.Log("Game should be saved here");
    }
}
