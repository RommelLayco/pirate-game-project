using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;


public class MazeBuilder : MonoBehaviour
{
    public GameObject player;

    public int min_x_room_size = 5;
    public int max_x_room_size = 10;
    public int min_y_room_size = 5;
    public int max_y_room_size = 10;

    public GameObject floorTile;

    private int min_hallway_size = 10;

    //need to replace with the game manager
    private int level = 3;


    private RoomBuilder roombuilder;

    //list of vectors to place rooms

    Vector3[,] roomPos;
    Room[,] rooms;

    // Use this for initialization
    void Awake()
    {

        //Get a component reference to the attached BoardManager script
        roombuilder = GetComponent<RoomBuilder>();

        //Get a list of positions of where to place a room
        InitalseRoomPos();


        //spawn player
        SpawnPlayer();


        // place the rooms in the maze
        PlaceRooms();

        GenerateHallways(rooms[1, 2], rooms[2, 2]);


        //create the hallways
        // AddHallways();
    }

    //method to calcuate placeable size of maze for 
    // the rooms
    int CalcSize()
    {
        //calculate maz size of room plus min 
        // corridor length take the bigger of the x  or y
        int x_size = max_x_room_size + min_hallway_size;
        int y_size = max_y_room_size + min_hallway_size;

        int size = (x_size > y_size) ? x_size : y_size;

        return size;
    }

    void SpawnPlayer()
    {
        //spawn player
        Vector3 pos = roomPos[1, 2];

        pos.x = pos.x + 5;
        pos.y = pos.y + 5;

        //spawn player
        player.transform.position = pos;
    }

    //Initalise vector positions of where to place rooms
    void InitalseRoomPos()
    {

        //get size
        int size = CalcSize();



        //initalise 2d array
        roomPos = new Vector3[level + 1, level + 1];
        //initalse 2d array to store room
        rooms = new Room[level + 1, level + 1];

        int xVector = 0;
        int yVector = 0;
        for (int x = 0; x < level + 1; x++)
        {
            //reset y to 0
            yVector = 0;
            for (int y = 0; y < level + 1; y++)
            {
                roomPos[x, y] = new Vector3(xVector, yVector, 0f);
                yVector += size;
            }
            xVector += size;
        }

        /*int xVector = 0;
        int yVector = 0;

        //note amount of rooms = (level + 1) ^ 2
        for (int x = 0; x < level + 1; x++)
        {
            //reset y to 0
            yVector = 0;
            for (int y = 0; y < level + 1; y++)
            {
                roomPos.Add(new Vector3(xVector, yVector, 0f));
                yVector += roomSize;
            }
            xVector += roomSize;
        }*/
    }


    void GenerateRoom(int x, int y)
    {
        Room room;




        room = roombuilder.BuildRoom(14, 14);

        //place rooms in their correct position
        Vector3 pos = roomPos[x, y];
        pos.x += 3;
        pos.y += 3;

        room.room.transform.position = pos;

        room.shift = roomPos[x, y];

        //store in room array
        rooms[x, y] = room;
    }

    void GenerateRoomRec(int x, int y)
    {


        GenerateRoom(x, y);
        if (Random.Range(1, 101) > 0)
        {

            if ((x + 1) < (level + 1) && rooms[x + 1, y] == null)
            {

                GenerateRoomRec(x + 1, y);
            }

            /*if ((y + 1) < (level + 1) && rooms[x, y + 1] == null)
            {
                GenerateRoomRec(x, y + 1);
            }*/

        }
    }

    void PlaceRooms()
    {

        GenerateRoomRec(1, 2);

        /* //place rooms in the grid
         for (int x = 0; x < level + 1; x++)
         {
             for (int y = 0; y < level + 1; y++)
             {

                 //randomly choose to have room in grid position 60% chance of 
                 //a room being in roomPos grid
                 int chance = Random.Range(1, 101);
                 //ensure that there is always at least 2 rooms
                 if (chance < 55 || (x == 0 && y == 0) || (x == level && y == level))
                 {
                     //randomly chose size of the room
                     int cols = Random.Range(min_x_room_size, max_x_room_size + 1);
                     int rows = Random.Range(min_y_room_size, max_y_room_size + 1);

                     room = roombuilder.BuildRoom(cols, rows);

                     //add room to 2d array
                     rooms[x, y] = room;

                     //place rooms in their correct position
                     room.room.transform.position = roomPos[x, y];
                     room.shift = roomPos[x, y];
                 }
                 else
                 {
                     //set the vector position stored at the array is null
                     roomPos[x, y] = new Vector3(-1, -1, -1f);

                 }
             } // end inner for loop "Y"
         } // end outer for loop "X"
         */
    }

    //method to connect two rooms together
    void GenerateHallways(Room current, Room neighbor)
    {
        int size = CalcSize();
        int[] c = current.getGridPos(size);
        int[] n = neighbor.getGridPos(size);

        //check if right
        if (n[0] == c[0]+1)
        {
            //delete the left wall tile on neighbor
            Vector3 nDoorPos = neighbor.shift + new Vector3(2f, 10f, 0f);
            roombuilder.CreateDoor(nDoorPos, neighbor, floorTile);

            //delete the right wall on current
            Vector3 cDoorPos = current.shift + new Vector3(17f, 10f, 0f);
            roombuilder.CreateDoor(cDoorPos, current, floorTile);

            //create door to connect the two rooms
            roombuilder.Hpath(cDoorPos, nDoorPos);
        }
        //check if above
    }
}

    //method to connect rooms
    /* void AddHallways()
     {
         //help hallway


         int size = CalcSize();


         //always connect up and to the right if possible
         for (int x = 0; x < level - 1; x++)
         {
             for(int y = 0; y < level - 1; y++)
             {
                 //Check if a room exist at the current positions
                 if(roomPos[x,y].x != -1) //note a negative number indicates a room was no placed
                 {

                     //get right neigbor location
                     int[] nPos = rightNeighbor(x, y);

                     if (nPos[0] != -1)
                     {

                         int l = CalcHallwayLength(x, y, nPos[0], nPos[1], true);
                         Debug.Log(l);
                         Room hallway = roombuilder.BuildRoom(l, 2);
                     }

                 }

             }// end inner for loop "Y"
         } // end outer for loop "X"
     }*/
     




/*	
	
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
    */
