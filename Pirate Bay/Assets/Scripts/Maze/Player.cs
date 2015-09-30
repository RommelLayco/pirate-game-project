using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject
{

    //Time to chnage Room
    public float changeRoomDelay = 1f;
    public Text goldText;

    private int gold = 0;
    

    // Update is called once per frame
    void Update()
    {

       
   
       
        if (Input.GetMouseButton(0))
        {
            Vector3 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Move(goal);
        }

        
       

    }

    //Trigger exit doors, enemies and treasure.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Exit")
        {
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke("ChangeRoom", changeRoomDelay);
        }
        else if (other.tag == "Gold")
        {
            gold++;
            goldText.text = "Total gold: "+ gold;
            other.gameObject.SetActive(false);
        }
    }

    //chnages to connected room
    private void ChangeRoom()
    {
        

        //Load the next room
        Application.LoadLevel(1);
          
    }
}
