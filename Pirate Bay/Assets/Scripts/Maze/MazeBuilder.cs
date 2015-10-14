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
    public GameObject finalTreasure;

    private int min_hallway_size = 10;
    private int number_of_rooms = 0;

    //ensure there is at least a min number of rooms for the level
    private int hasMinRooms = 0;
    private bool only1 = true;
    private bool placeTreaure = false;

    //need to replace with the game manager
    private int level = 3;


    private RoomBuilder roombuilder;

    //list of vectors to place rooms

    Vector3[,] roomPos;
    Room[,] rooms;

    List<Vector3> existingRooms = new List<Vector3>();

    // Use this for initialization
    void Awake()
    {
        int originalSeed = Random.seed;
        level = GameManager.getInstance().islandLevel;
        if (!GameManager.getInstance().inMaze)
        {
            //set seed
            GameManager.getInstance().seed = Random.seed;
            Debug.Log("Generating first time, seed: " +GameManager.getInstance().seed);
        }
        Random.seed = GameManager.getInstance().seed;
        //Get a component reference to the attached BoardManager script
        roombuilder = GetComponent<RoomBuilder>();

        //Get a list of positions of where to place a room
        InitaliseRoomPos();

        hasMinRooms = 0;


        // place the rooms in the maze
        //random choose starting co ordinate
        
        int x = Random.Range(0, level + 1);
        int y = Random.Range(0, level + 1);
        PlaceRooms(x, y);
        //spawn player in starting point
        SpawnPlayer(x, y);
        SpawnTreasure(x, y);

        if (GameManager.getInstance().inMaze)
        {
            reload();
            //Reset the seed to random so different encounters
            
        }
        else
        {
            GameManager.getInstance().inMaze = true;
        }
        Debug.Log("Setting seed to: " + originalSeed);
        Random.seed = originalSeed;

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
    void reload()
    {
        //return player to where he left of.
        player.transform.position = GameManager.getInstance().playerPos;

    }

    void SpawnPlayer(int x, int y)
    {
        //spawn player
        Vector3 pos = roomPos[x, y];

        pos.x = pos.x + 5;
        pos.y = pos.y + 5;

        //spawn player
        player.transform.position = pos;
    }

    //spawn treaure
    void SpawnTreasure(int x, int y)
    {
        int maxdiff = 0;
        int[] finalPos = new int[2] { x, y };

        for (int xx = 0; xx < level + 1; xx++)
        {
            for (int yy = 0; yy < level + 1; yy++)
            {
                if(rooms[xx,yy] != null)
                {
                    int dif = Math.Abs(xx - x) + Math.Abs(yy - y);
                    if(dif > maxdiff)
                    {
                        maxdiff = dif;
                        finalPos[0] = xx;
                        finalPos[1] = yy;
                    }
                }
            } 
        } // end for loop X"

        //spawn treasure
        Vector3 pos = roomPos[finalPos[0], finalPos[1]];
        int size = CalcSize();
        pos += new Vector3(size / 2, size / 2, 0f);

        GameObject treasure = Instantiate(finalTreasure, pos, Quaternion.identity) as GameObject;

    }



    //Initalise vector positions of where to place rooms
    void InitaliseRoomPos()
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

    }


    void GenerateRoom(int x, int y)
    {
        Room room;

        room = roombuilder.BuildRoom(14, 14, placeTreaure);

        //place rooms in their correct position
        Vector3 pos = roomPos[x, y];
        pos.x += 3;
        pos.y += 3;

        room.room.transform.position = pos;

        room.shift = roomPos[x, y];

        //store in room array
        rooms[x, y] = room;

        //store coordinate of a spawned room
        Vector3 co = new Vector3(x, y, 0f);
        existingRooms.Add(co);

        number_of_rooms++;

    }


    void PlaceRooms(int x, int y)
    {
        //randomly choose a starting position
        //int x = Random.Range(0, level + 1);
        //int y = Random.Range(0, level + 1);
        if (rooms[x, y] == null)
        {
            GenerateRoom(x, y);
            hasMinRooms++;
        }

        //40 chance to not have neighbors
        if (Random.Range(1, 101) < 41 && hasMinRooms > level + 1)
        {
            return;
        }

        double max = Math.Pow(level + 1, 2);
        double chance = ((max - number_of_rooms) / max) * 100;

        //place room on right with 61% chance
        if (x + 1 < level + 1 && rooms[x + 1, y] == null && Random.Range(1, 101) < chance)
        {
            hasMinRooms += 1;
            GenerateRoom(x + 1, y);
            GenerateHallway(rooms[x, y], rooms[x + 1, y]);
            PlaceRooms(x + 1, y);
        }

        //place a room to the left
        if ((x - 1) > -1 && rooms[x - 1, y] == null && Random.Range(1, 101) < chance)
        {
            hasMinRooms += 1;
            GenerateRoom(x - 1, y);
            GenerateHallway(rooms[x - 1, y], rooms[x, y]);
            PlaceRooms(x - 1, y);
        }

        //place room on top
        if (y + 1 < level + 1 && rooms[x, y + 1] == null && Random.Range(1, 101) < chance)
        {
            hasMinRooms += 1;
            GenerateRoom(x, y + 1);
            GenerateHallway(rooms[x, y], rooms[x, y + 1]);
            PlaceRooms(x, y + 1);
        }

        //place room on the bottom
        if (y - 1 > -1 && rooms[x, y - 1] == null && Random.Range(1, 101) < chance)
        {
            hasMinRooms += 1;
            GenerateRoom(x, y - 1);
            GenerateHallway(rooms[x, y - 1], rooms[x, y]);
            PlaceRooms(x, y - 1);
        }

        //ensure we spawn 1 more room if we fail all if cases if first round
        if (hasMinRooms == 1)
        {
            only1 = false;
            if (x + 1 < level + 1 && rooms[x + 1, y] == null)
            {
                GenerateRoom(x + 1, y);
                GenerateHallway(rooms[x, y], rooms[x + 1, y]);
                PlaceRooms(x + 1, y);
            }
            else if((x - 1) > -1 && rooms[x - 1, y] == null)
            {
                GenerateRoom(x - 1, y);
                GenerateHallway(rooms[x - 1, y], rooms[x, y]);
                PlaceRooms(x - 1, y);
            }
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
        else if (n[1] > c[1])
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


