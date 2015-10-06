using UnityEngine;
using System.Collections;

public class IslandController : MonoBehaviour {

    void OnMouseUp() {
        //Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
		GameObject.Find ("GameManager").GetComponent <GameManager>().targetLocation.x = gameObject.transform.position.x;
		GameObject.Find ("GameManager").GetComponent <GameManager>().targetLocation.y = gameObject.transform.position.y -1;
    }

}
