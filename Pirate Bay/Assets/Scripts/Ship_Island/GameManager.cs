using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;


    public Vector3 targetLocation;
    public Vector3 currentLocation;


    void Awake() {
        //if we don't have an [_instance] set yet
        if (!_instance) {
            _instance = this;
            //otherwise, if we do, kill this thing
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start() {
        targetLocation = new Vector3(-500, -500, -500);
        currentLocation = new Vector3(-500, -500, -500);
    }


}
