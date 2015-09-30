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
        //for testif purpose on unity will be on trigger
        //need to change to change to a scene where we can shake
        //to open treasure chest
        if (other.tag == "MainTreasure")
        {
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke("ChangeScene", changeRoomDelay);
        }

        else if (other.tag == "Gold")
        {
            gold++;
            goldText.text = "Total gold: "+ gold;
            other.gameObject.SetActive(false);
        }
    }

    //chnages to connected room
    private void ChangeScene()
    {
        

        //Load the next room
        Application.LoadLevel("Ship");
          
    }
}
