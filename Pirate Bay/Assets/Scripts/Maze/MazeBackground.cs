using UnityEngine;
using System.Collections;

public class MazeBackground : MonoBehaviour {

    public GameObject floorBackground;
    public GameObject wall;

    private GameObject mazeHolder;

    public void BackgroundSetup(int columns, int rows)
    {
        //Instantiate Board and set boardHolder to its transform.
        mazeHolder = new GameObject("Maze");

      

        //position coordinates
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorBackground;

                //Check if Edge of room to place walls
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wall;
                }

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to roomHolder.
                instance.transform.SetParent(mazeHolder.transform);

            }
        }


        return;
    }
}
