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
        PlaceRooms(1,1);

       


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
            //place a room on the right
            if ((x + 1) < (level + 1) && rooms[x + 1, y] == null)
            {

                GenerateRoomRec(x + 1, y);
                //connect the two rooms
                GenerateHallway(rooms[x, y], rooms[x + 1, y]);
            }

                //place a room above
                if ((y + 1) < (level + 1) && rooms[x, y + 1] == null)
            {
                GenerateRoomRec(x, y + 1);
                GenerateHallway(rooms[x, y], rooms[x, y + 1]);
            }
           
        }
    }



    void PlaceRooms(int x, int y)
    {

        //randomly choose a starting position
        //int x = Random.Range(0, level + 1);
        //int y = Random.Range(0, level + 1);
        if(rooms[x,y] == null)
        {
            GenerateRoom(x, y);
        }
        
        //place room on right
        if (x + 1 < level + 1 && rooms[x + 1,y] == null)
        {
            GenerateRoom(x + 1, y);
            GenerateHallway(rooms[x, y], rooms[x + 1, y]);
        }

        //place a room to the left
        if ((x - 1) > -1 && rooms[x - 1, y] == null)
        {
            GenerateRoom(x - 1, y);
            GenerateHallway(rooms[x - 1, y], rooms[x, y]);
        }

        //place room on top
        if (y + 1 < level + 1 && rooms[x , y + 1] == null)
        {
            GenerateRoom(x , y + 1);
            GenerateHallway(rooms[x, y], rooms[x, y + 1]);
        }

        //place room on the bottom
        if (y - 1 > - 1 && rooms[x, y - 1] == null)
        {
            GenerateRoom(x, y - 1);
            GenerateHallway(rooms[x, y - 1], rooms[x, y]);
        }



    }

    //method to connect two rooms together
    void GenerateHallway(Room current, Room neighbor)
    {
        int size = CalcSize();
        int[] c = current.getGridPos(size);
        int[] n = neighbor.getGridPos(size);

        //check if right
        if (n[0] > c[0])
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
        else if(n[1] > c[1])
        {
            //delete the bottom wall tile on neighbor
            Vector3 nDoorPos = neighbor.shift + new Vector3(10f, 2f, 0f);
            roombuilder.CreateDoor(nDoorPos, neighbor, floorTile);

            //delete the top wall on current
            Vector3 cDoorPos = current.shift + new Vector3(10f, 17f, 0f);
            roombuilder.CreateDoor(cDoorPos, current, floorTile);

            //create door to connect the two rooms
            roombuilder.Vpath(cDoorPos, nDoorPos);
        }
    }

    
}

    
