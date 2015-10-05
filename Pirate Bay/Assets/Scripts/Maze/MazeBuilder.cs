using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MazeBuilder : MonoBehaviour {

   
    public int min_x_room_size = 5;
    public int max_x_room_size = 10;
    public int min_y_room_size = 5;
    public int max_y_room_size = 10;
    public int min_spacing = 8;
    

    private RoomBuilder roombuilder;
    private MazeBackground mazeBackground;

    //will be replaced in the gamemanger
    //used to specify size and number of rooms in the island
    public int islandLevel = 1;
    private int max_number_of_rooms = 1;

    private List<Room> rooms;

	// Use this for initialization
	void Awake () {
        //Get a component reference to the attached BoardManager script
        roombuilder = GetComponent<RoomBuilder>();
        mazeBackground = GetComponent<MazeBackground>();
       

        rooms = new List<Room>();

        //create the island background;
        CreateBackground();

        //build the room
        //InitMaze();

        //create the hallways
       // CreateHallWays();
    }


    //this functio calculates the size of the island
    void CreateBackground()
    {
        //number of rooms x and y is the level + 1;
        int size = 1 + islandLevel;
        max_number_of_rooms = (size) ^ 2;

        //calculate the amount of tiles needed for spacing between rooms
        int spaceing = size * min_spacing; 

        //calculate amount of tiles needed for rooms
        int xdir = (max_x_room_size + 2) * size + spaceing;

        int ydir = (max_y_room_size + 2) * size + spaceing;

        //build the background
        mazeBackground.BackgroundSetup(xdir, ydir);
        
        
    }


	// Update is called once per frame
	void InitMaze () {

      GameObject floorTile = roombuilder.floor;
      Room roomInfo;

        for (int i = 0; i < max_number_of_rooms; i++)
        {
           // int x = Random.Range(min_x_room_size, max_x_room_size + 1);
           // int y = Random.Range(min_y_room_size, max_y_room_size + 1);
      
         
            
          
           if(i != max_number_of_rooms - 1)
            {

                roomInfo = roombuilder.BuildRoom(8, 8, false);
            }
            else
            {
                roomInfo = roombuilder.BuildRoom(8, 8, true);
            }
            

            //spawn treasure 
            

           
            //shift rooms to their positions and create doors

            // note need to add door first in this implem else the vector positions of
            //the tile you want to delete will have been moved
            if (i == 0)
            {
                

                //create door at top middle
                roombuilder.CreateDoor(new Vector3(3, 8, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(4, 8, 0f), roomInfo.tiles, floorTile);
                
            }
            else if (i == 1)
            {
                //create door at bottom middle 
                roombuilder.CreateDoor(new Vector3(3, -1, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(4, -1, 0f), roomInfo.tiles, floorTile);

                //create door at bottom middle 
                roombuilder.CreateDoor(new Vector3(8, 3, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(8, 4, 0f), roomInfo.tiles, floorTile);

                //shift 20 up
                roomInfo.room.transform.position = new Vector3(0, 20, 0f);
            }
            else if (i == 2)
            {
                //create door at bottom middle 
                roombuilder.CreateDoor(new Vector3(3, -1, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(4, -1, 0f), roomInfo.tiles, floorTile);

                //create door at left middle 
                roombuilder.CreateDoor(new Vector3(-1, 3, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(-1, 4, 0f), roomInfo.tiles, floorTile);

                //shift 20 up and to the right
                roomInfo.room.transform.position = new Vector3(20, 20, 0f);
            }
            else if ( i == 3)
            {
                
                //create door at top middle
                roombuilder.CreateDoor(new Vector3(3, 8, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(4, 8, 0f), roomInfo.tiles, floorTile);

                //shift 5 to the left
                roomInfo.room.transform.position = new Vector3(20, 0, 0f);

            }
            rooms.Add(roomInfo);
        }
        
    } // End init maze

    void CreateHallWays()
    {
        GameObject floorTile = roombuilder.floor;

        for (int i = 0; i < 3; i++)
        {
            if(i != 1)
            {
                Room roomInfo = roombuilder.BuildRoom(2, 9, false);

                if(i == 0)
                {
                    //create door at bottom
                    roombuilder.CreateDoor(new Vector3(0, -1, 0f), roomInfo.tiles, floorTile);
                    roombuilder.CreateDoor(new Vector3(1, -1, 0f), roomInfo.tiles, floorTile);

                    //create door at top
                    roombuilder.CreateDoor(new Vector3(0, 9, 0f), roomInfo.tiles, floorTile);
                    roombuilder.CreateDoor(new Vector3(1, 9, 0f), roomInfo.tiles, floorTile);

                    //shift in between bottom left and top left room
                    roomInfo.room.transform.position = new Vector3(3, 9, 0f);
                }
                else if (i == 2)
                {

                    //create door at bottom
                    roombuilder.CreateDoor(new Vector3(0, -1, 0f), roomInfo.tiles, floorTile);
                    roombuilder.CreateDoor(new Vector3(1, -1, 0f), roomInfo.tiles, floorTile);

                    //create door at top
                    roombuilder.CreateDoor(new Vector3(0, 9, 0f), roomInfo.tiles, floorTile);
                    roombuilder.CreateDoor(new Vector3(1, 9, 0f), roomInfo.tiles, floorTile);

                    //shift in between bottom left and top left room
                    roomInfo.room.transform.position = new Vector3(23, 9, 0f);
                }
            }

            else if (i == 1)
            {
                Room roomInfo = roombuilder.BuildRoom(9, 2, false);

                //create door at left middle
                roombuilder.CreateDoor(new Vector3(-1, 0, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(-1, 1, 0f), roomInfo.tiles, floorTile);

                //create door at right middle
                roombuilder.CreateDoor(new Vector3(9, 0, 0f), roomInfo.tiles, floorTile);
                roombuilder.CreateDoor(new Vector3(9, 1, 0f), roomInfo.tiles, floorTile);

                //shift in between bottom left and top left room
                roomInfo.room.transform.position = new Vector3(10, 23, 0f);
            }
           
        }
    }

}
