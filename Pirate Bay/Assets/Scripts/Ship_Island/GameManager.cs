using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    //IslandView Data
    public Vector3 targetLocation;
    public Vector3 currentLocation;

    //BunkRoom
    public int bunkLevel;
    public int[] bunkLevels = { 1, 2, 3, 4, 5};
    public int[] bunkCosts = { 100, 200, 300, 400, 500};
    public int[] bunkCapacities = { 2, 4, 6, 8, 10};

    void Awake() {
        //if we don't have an GameManager set yet
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

        bunkLevel = 1;
        /*
        bunkLevels = { 1, 2, 3, 4, 5 };
        bunkCosts = { 100, 200, 300, 400, 500 };
        bunkCapacities = { 2, 4, 6, 8, 10 };
        */

    }
}
