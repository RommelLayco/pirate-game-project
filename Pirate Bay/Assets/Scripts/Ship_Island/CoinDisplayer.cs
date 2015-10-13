using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinDisplayer : MonoBehaviour {
    public Text coinText;
    GameManager manager;

    void Awake () {
        manager = GameManager.getInstance();
	}
	
	void Update () {
        coinText.text = manager.gold.ToString();
	}
}
