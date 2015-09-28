using UnityEngine;
using System.Collections;

public class MapControllerScript : MonoBehaviour {
    public BoxCollider2D topLeft;
    public BoxCollider2D topMiddle;
    public BoxCollider2D topRight;
    public BoxCollider2D botLeft;
    public BoxCollider2D botMiddlge;
    public BoxCollider2D botRight;
    public BoxCollider2D backButton;

    void Start() {
        Screen.orientation = ScreenOrientation.Landscape;
        Debug.Log("MapController Start");
    }

    void Update() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Ended) {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (backButton == Physics2D.OverlapPoint(touchPos)) {

                    // if topLeft.bounds.contains(wp){
                    backClicked();
                } else if (topLeft == Physics2D.OverlapPoint(touchPos)) {
                    islandClicked();
                }

            }
        }
    }

    private void islandClicked() {
        //Application.LoadLevel("Ship");
        Debug.Log("Island clicked");
    }
    private void backClicked() {
        Debug.Log("Back button clicked");
        //Application.LoadLevel("Ship");
    }
}