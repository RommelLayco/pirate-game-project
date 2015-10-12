using UnityEngine;
using System.Collections;

public class IslandController : MonoBehaviour {
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
    }
    void OnMouseUp() {
        //Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
        manager.targetLocation.x = gameObject.transform.position.x;
        manager.targetLocation.y = gameObject.transform.position.y - 1;
    }

}
