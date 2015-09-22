using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    public int columns = 7;
    public int rows = 7;

    public int x_rooms = 2;
    public int y_rooms = 2;

    public GameObject exit;
    public GameObject floorTiles;
    public GameObject wallTiles;
    public GameObject enemyTiles;
    public GameObject MainTreasureTile;

    private Transform boardHolder;

    private List <Vector3> gridPositions = new List <Vector3>();
    private List<Transform> rooms = new List<Transform>();

    void InitialseRooms()
    {
        rooms.Clear();

        for (int x = 1; x < x_rooms + 1; x++)
        {
            for (int y = 1; y < y_rooms + 1; y++)
            {

            }
        }
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
                
            }
        }
    }

    void BoardSetUp()
    {
        boardHolder = new GameObject("board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles;

                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    if (y == rows && x == columns / 2 )
                    {
                        toInstantiate = exit;
                    }
                    else
                    {
                       toInstantiate = wallTiles;
                    }
                    
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
                    
            }
        }
    }

    //random position function to spawn enemy will change to room
    //currently random spawns on a floor tile
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    //Function to spawn enemy
    void LayoutObjectAtRandom(GameObject tile)
    {
        Vector3 randomPosition = RandomPosition();

        Instantiate(tile, randomPosition, Quaternion.identity);
    }

    public void SetUpScene()
    {
        BoardSetUp();
        InitialiseList();

        //spawn enemy
        LayoutObjectAtRandom(enemyTiles);
        LayoutObjectAtRandom(MainTreasureTile);

        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}
