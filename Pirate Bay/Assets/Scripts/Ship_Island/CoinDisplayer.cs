using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinDisplayer : MonoBehaviour {

    public Text coinText;
    GameManager manager;
	// Use this for initialization
	void Start () {
        manager = GameManager.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
        coinText.text = manager.gold.ToString();
	}
}
