using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomBuilder : MonoBehaviour
{

   

    //set room size in Maze builder
    protected int columns = 5;
    protected int rows = 5;

    //positions on  where to start placing tiles
    protected int xStart;
    protected int yStart;
    protected int xFin;
    protected int yFin;

    //tiles to generate board
    public GameObject floor;
    public GameObject wall;



    private Transform boardHolder;

    //Sets up the outer walls and floor(background) of the room.
    //x_shift and y _shift move the room by that many units
    void RoomSetup(int x_shift, int y_shift)
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        xStart = x_shift;
        xFin = columns + x_shift;

        yStart = y_shift;
        yFin = rows + y_shift;

        //position coordinates
        for (int x = xStart -1; x < xFin + 1; x++)
        {
            for (int y = yStart - 1; y < yFin + 1; y++)
            {
                GameObject toInstantiate = floor;
                
                //Check if Edge of room to place walls
                if (x == xStart - 1|| x == xFin ||  y == yStart - 1|| y == yFin)
                {
                    toInstantiate = wall;

                    toInstantiate = PlaceHoles(x, y, toInstantiate);
                    
                }
                



                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void BuildRoom(int x_shift, int y_shift)
    {
        //Adjust vector co ordinates
        //InitaliseShift(x_shift, x_shift);

        //Creates the outer walls and floor of the room.
        RoomSetup(x_shift,y_shift);

        SpawnThing();


    }

    //Initalise start and finsh values for x and y
    private void InitaliseShift(int x_shift, int y_shift)
    {

       

        //calculate horizontal and vertical min and max
        xStart = x_shift;
        xFin = columns + x_shift;

        yStart = y_shift;
        yFin = rows + y_shift;

        Debug.Log(yStart);
    }

    //Hook Method to places holes in rooms to connect to other rooms
    protected virtual GameObject PlaceHoles(int x, int y, GameObject toInstantiate)
    {
        //calculate x mid
        int mid = columns / 2;
        mid = xStart + mid;
        if(x == mid && y == yFin)
        {
            toInstantiate = floor;
        }

        return toInstantiate;
    }

    protected virtual void SpawnThing()
    {
        return;
    }
}
