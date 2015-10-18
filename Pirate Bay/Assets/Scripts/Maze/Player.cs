using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MovingObject {

    //Time to chnage Room
    public float changeRoomDelay = 1f;
    public Text goldText;

    override protected void Start() {
        base.Start();
        goldText.text = gold.ToString() ;

    }

    // Update is called once per frame
    void Update() {


        //note the following code will be grey and not work 
        //depending on the build settings platform
        //for now it is set to pc hence the mobile will be grey and won't work
        //changing to android will enable the touch control
        //using pc for testing

        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        //only get position on mouse release
        if (Input.GetMouseButtonUp(0)) {
            Vector3 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Move(goal);
        }


        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            
            // Get location of touch
            Vector2 touchPosition = Input.GetTouch(0).position;

            //turn into a vector3 to move
            Vector3 goal = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0.0f));

            Move(goal);
        }
        
#endif



        GetComponent<Animator>().SetBool("moving", moving);
    }

    //Trigger exit doors, enemies and treasure.
    private void OnTriggerEnter2D(Collider2D other) {
        //for testif purpose on unity will be on trigger
        //need to change to change to a scene where we can shake
        //to open treasure chest
        if (other.tag == "Main Treasure") {

            GameManager.getInstance().notoriety++;
            // no longer in maze
            GameManager.getInstance().inMaze = false;

            //clear list of collected gold
            GameManager.getInstance().collectedgold.Clear();

            //transfer collected gold
            GameManager.getInstance().gold += gold;
            foreach (CrewMemberData d in GameManager.getInstance().explorers) {
                d.setHealth(100);
            }

            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke("ChangeScene", changeRoomDelay);
        }

        else if (other.tag == "Gold")
        {
            //chose an amount to increase by
            int amount = Random.Range(1, 6) * GameManager.getInstance().islandLevel;
            gold += amount;
            goldText.text = gold.ToString();

            other.gameObject.SetActive(false);

            //add to collect list
            collectedGold.Add(other.gameObject.transform.position);
        }
    }

    //chnages to connected room
    private void ChangeScene() {

        //Load the next room
        Application.LoadLevel("loot");

    }
}
