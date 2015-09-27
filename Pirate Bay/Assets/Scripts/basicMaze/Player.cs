using UnityEngine;
using System.Collections;

public class Player : MovingObject
{

    //Time to chnage Room
    public float changeRoomDelay = 1f;

    // Update is called once per frame
    void Update()
    {

        int horizontal = 0;
        int vertical = 0;


        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
        }

        //call Move
        Move(horizontal, vertical);

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
    }

    //chnages to connected room
    private void ChangeRoom()
    {
        

        //Load the next room
        Application.LoadLevel(1);
          
    }
}
