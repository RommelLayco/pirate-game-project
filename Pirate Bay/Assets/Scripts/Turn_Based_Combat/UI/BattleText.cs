using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BattleText : MonoBehaviour {

    void Start()
    {
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        this.transform.position = new Vector3(0.0f, -4.0f);
        GetComponent<Text>().enabled = false;
    }

    void FixedUpdate()
    {

    }

    public void ShowText(String text)
    {
        GetComponent<Text>().text = text;
        GetComponent<Text>().enabled = true;       
    }

    public void HideText()
    {
        GetComponent<Text>().enabled = false;
    }
}
