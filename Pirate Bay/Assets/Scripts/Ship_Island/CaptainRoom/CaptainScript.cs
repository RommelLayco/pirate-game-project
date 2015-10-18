using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System;
using System.Collections.Generic;

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
        //Setting the captain's name
        string newName = newText.text;
        if (newName.Length != 0) {
            manager.captainName = newName;
        }
        GameObject.Find("CaptainName").GetComponentInChildren<InputField>().text = "";
        setCaptainInfo();
    }

    private void setCaptainInfo() {
        //Setting up the display of the captain's information
        captainName.text = manager.captainName;
        newText.text = manager.captainName;
        captainInfo.text = "\n" + manager.gold + "\n" + manager.crewSize + "\n" + manager.notoriety;
    }

    public void onClickSave()
    {
        //Saving the game
        Debug.Log("Game should be saved here");
        SaverLoader.SaveToFile("SaveGame001.xml");
    }
}
