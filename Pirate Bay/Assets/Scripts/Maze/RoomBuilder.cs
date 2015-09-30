using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomBuilder : MonoBehaviour
{
    
    //tiles to generate board
    public GameObject floor;
    public GameObject wall;
    
    private GameObject roomHolder;

    //Sets up the outer walls and floor(background) of the room.
    //x_shift and y _shift move the room by that many units
    GameObject RoomSetup(int columns, int rows)
    {
        //Instantiate Board and set boardHolder to its transform.
        roomHolder = new GameObject("Room");

        //position coordinates
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = - 1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floor;
                
                //Check if Edge of room to place walls
                if (x == - 1|| x == columns ||  y == -1|| y == rows)
                {
                    toInstantiate = wall;   
                }

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(roomHolder.transform);

                
            }
        }

        
       return roomHolder;
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public Room BuildRoom(int columns, int rows)
    {
        //Adjust vector co ordinates
        //InitaliseShift(x_shift, x_shift);

        //Creates the outer walls and floor of the room.
        GameObject room =  RoomSetup(columns, rows);

        room.AddComponent<BoxCollider2D>();

        Room roomInfo = new Room (room, columns, rows);

        return roomInfo;

       


    }

   

    protected virtual void SpawnThing()
    {
        return;
    }
}
