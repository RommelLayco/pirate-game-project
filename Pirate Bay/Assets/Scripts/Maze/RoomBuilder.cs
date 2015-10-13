using UnityEngine;
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
    public GameObject obstacle;

    private GameObject roomHolder;
    private GameObject hallWayHolder;
    private List<Vector3> placeablePositions;
    private List<GameObject> walltiles;




    //initalise list of  vector positions for placable treasure
    void InitialiseList(int columns, int rows)
    {
        placeablePositions = new List<Vector3>();

        //positions are 1 in from the walls
        for (int x = 1; x < columns - 2; x++)
        {
            for (int y = 0; y < rows - 2; y++)
            {
                placeablePositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    //create a room columns + 2 by rows + 2
    //add two due to walls that surround room
    GameObject RoomSetup(int columns, int rows)
    {
        //Instantiate room and set roomHolder to its transform.
        roomHolder = new GameObject("Room");

        //clear list
        walltiles = new List<GameObject>();

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
    public Room BuildRoom(int columns, int rows, bool placeTreasure)
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

        //spawn treasure
        if (placeTreasure)
        {
            
        }

        SpawnObstacle();

        Room room = new Room(createdRoom, columns + 2, rows + 2, placeablePositions, walltiles);

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
                //select a random vector position to place gold.
                int randomIndex = Random.Range(0, placeablePositions.Count);

                Vector3 pos = placeablePositions[randomIndex];

                //Remove the entry at randomIndex from the list so that it can't be re-used.
                placeablePositions.RemoveAt(randomIndex);

                GameObject instance = Instantiate(gold, pos, Quaternion.identity) as GameObject;

                instance.transform.SetParent(roomHolder.transform);
            }

        }
    }

    

    //spawn obstacle
    void SpawnObstacle()

    {
        //Decide on amount to place between 1 and 4
        int amount = Random.Range(1, 5);

        for (int i = 0; i < amount; i++)
        {
            //only do it if there is tiles left to place
            if (i < placeablePositions.Count)
            {
                //select a random vector position to place gold.
                int randomIndex = Random.Range(0, placeablePositions.Count);

                Vector3 pos = placeablePositions[randomIndex];

                //Remove the entry at randomIndex from the list so that it can't be re-used.
                placeablePositions.RemoveAt(randomIndex);

                GameObject instance = Instantiate(obstacle, pos, Quaternion.identity) as GameObject;

                instance.transform.SetParent(roomHolder.transform);
            }

        }
    }


    public void CreateDoor(Vector3 tilePosition, Room r, GameObject floorTile)
    // Vector3 shift,
    //List<GameObject> tiles, GameObject floorTile
    {

        GameObject tile = null;
        bool foundTile = false;
        //loop throught tiles list to find tile at given position
        for (int i = 0; i < r._walltiles.Count; i++)
        {
            //check if desired tile
            if (r._walltiles[i].transform.position == tilePosition)
            {
                tile = r._walltiles[i];
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

    public void Hpath(Vector3 cPos, Vector3 nPos)
    {

        //store in a holder to organise
        hallWayHolder = new GameObject("Hallway");

        for (int i = (int)cPos.x + 1; i < (int)nPos.x; i++)
        {
            Vector3 topWall = new Vector3(i, cPos.y + 1, 0f);
            Vector3 floorPos = new Vector3(i, cPos.y, 0f);
            Vector3 bottomWall = new Vector3(i, cPos.y - 1, 0f);

            GameObject instance1 =
                       Instantiate(wall, topWall, Quaternion.identity) as GameObject;

            GameObject instance2 =
                       Instantiate(floor, floorPos, Quaternion.identity) as GameObject;

            GameObject instance3 =
                       Instantiate(wall, bottomWall, Quaternion.identity) as GameObject;


            //Set the parent of our newly instantiated objects instance to roomHolder.
            instance1.transform.SetParent(hallWayHolder.transform);
            instance2.transform.SetParent(hallWayHolder.transform);
            instance3.transform.SetParent(hallWayHolder.transform);

        }
    }

    public void Vpath(Vector3 cPos, Vector3 nPos)
    {

        //store in a holder to organise
        hallWayHolder = new GameObject("Hallway");

        for (int i = (int)cPos.y + 1; i < (int)nPos.y; i++)
        {
            Vector3 leftWall = new Vector3(cPos.x - 1, i, 0f);
            Vector3 floorPos = new Vector3(cPos.x, i, 0f);
            Vector3 rightWall = new Vector3(cPos.x + 1, i, 0f);

            GameObject instance1 =
                       Instantiate(wall, leftWall, Quaternion.identity) as GameObject;

            GameObject instance2 =
                       Instantiate(floor, floorPos, Quaternion.identity) as GameObject;

            GameObject instance3 =
                       Instantiate(wall, rightWall, Quaternion.identity) as GameObject;


            //Set the parent of our newly instantiated objects instance to roomHolder.
            instance1.transform.SetParent(hallWayHolder.transform);
            instance2.transform.SetParent(hallWayHolder.transform);
            instance3.transform.SetParent(hallWayHolder.transform);

        }
    }
}
