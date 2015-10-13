using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {

    private Combatant owner;
    private int frameCount;
    private static int durationFrames = 60;
    private bool showing;

    void Start()
    {
        this.transform.SetParent(GameObject.Find("Canvas").transform);
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        GetComponent<Text>().enabled = false;
        frameCount = 0;
        showing = false;
    }

    void FixedUpdate()
    {
        if (showing)
        {
            frameCount++;
            if (frameCount >= durationFrames)
            {
                GetComponent<Text>().enabled = false;
                showing = false;
                frameCount = 0;
            }
        }
    }

	public void SetOwner(Combatant owner)
    {
        this.owner = owner;
    }

    public void ShowText(float value)
    {
        GetComponent<Text>().text = Math.Round(value).ToString();
        this.transform.position = owner.transform.position + new Vector3(0.0f, 2.0f);
        GetComponent<Text>().enabled = true;
        showing = true;
        frameCount = 0;
    }

}
