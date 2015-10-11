﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

public class RoomBuilder : MonoBehaviour
{

    //tiles to generate board
    public GameObject floor;
    public GameObject wall;
    public GameObject gold;
    public GameObject treasure;

    private GameObject roomHolder;
    private List<Vector3> placeablePositions = new List<Vector3>();
    private List<GameObject> walltiles = new List<GameObject>();




    //initalise list of  vector positions for placable treasure
    void InitialiseList(int columns, int rows)
    {
        placeablePositions.Clear();

        for (int x = 0; x < columns - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                placeablePositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    //create a room columns + 2 by rows + 2
    //add two due to walls that surround room
    GameObject RoomSetup(int columns, int rows)
    {
        //Instantiate Board and set boardHolder to its transform.
        roomHolder = new GameObject("Room");

        //clear list
        walltiles.Clear();

        //position coordinates
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floor;

                //Check if Edge of room to place walls
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wall;
                }

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to roomHolder.
                instance.transform.SetParent(roomHolder.transform);

                //Store wall tiles on if wall
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    walltiles.Add(instance);
                }

            } //close inner for loop "Y"
        } // close outer for loop "X"


        return roomHolder;
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public Room BuildRoom(int columns, int rows)
    {

        //Initialse List of vector positions
        InitialiseList(columns, rows);


        //Creates the outer walls and floor of the room.
        GameObject createdRoom = RoomSetup(columns, rows);

        //place gold
        if (RoomHasGold())
        {
            SpawnGold();
        }


        Room room = new Room(createdRoom, columns, rows, placeablePositions, walltiles);

        return room;

    }

    //Method to decide whether there should be any treasure in the room
    bool RoomHasGold()
    {
        //We will place treaure in a room 75% of the time
        int x = Random.Range(0, 4);
        if (x < 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SpawnGold()
    {
        //Decide on amount to place between 1 and 4
        int amount = Random.Range(1, 5);

        for (int i = 0; i < amount; i++)
        {
            //only do it if there is tiles left to place
            if (i < placeablePositions.Count)
            {
                //select a random vector position to place treasure.
                int randomIndex = Random.Range(0, placeablePositions.Count);

                Vector3 pos = placeablePositions[randomIndex];

                //Remove the entry at randomIndex from the list so that it can't be re-used.
                placeablePositions.RemoveAt(randomIndex);

                GameObject instance = Instantiate(gold, pos, Quaternion.identity) as GameObject;

                instance.transform.SetParent(roomHolder.transform);
            }

        }
    }

    void SpawnTreasure(int columns, int rows)
    {
        // place in the center of the room
        int middle = placeablePositions.Count / 2;


        Vector3 pos = placeablePositions[middle];


        //Remove the entry at randomIndex from the list so that it can't be re-used.
        placeablePositions.RemoveAt(middle);

        GameObject instance = Instantiate(treasure, pos, Quaternion.identity) as GameObject;

        instance.transform.SetParent(roomHolder.transform);


    }


    public void CreateDoor(Vector3 tilePosition, List<GameObject> tiles,
        GameObject floorTile)
    {

        GameObject tile = null;
        bool foundTile = false;
        //loop throught tiles list to find tile at given position
        for (int i = 0; i < tiles.Count; i++)
        {
            //check if desired tile
            if (tiles[i].transform.position == tilePosition)
            {
                tile = tiles[i];
                foundTile = true;
                break;
            }
        }


        //only do the following code if the tile has been found

        if (foundTile)
        {
            //remove tile from room
            Destroy(tile);



            //Create floor tile
            GameObject instance =
                       Instantiate(floorTile, tilePosition, Quaternion.identity) as GameObject;

            instance.transform.SetParent(roomHolder.transform);
        }

    }


}
