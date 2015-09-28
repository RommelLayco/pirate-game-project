using UnityEngine;
using System.Collections;

public class EnemyRoom : RoomBuilder{

    public GameObject enemy;

    protected override GameObject PlaceHoles(int x, int y, GameObject toInstantiate)
    {
        //calculate x mid point
        int mid = columns / 2;
        mid = xStart + mid;

        //bottom needs a door
        if (x == mid && y == yStart - 1)
        {
            toInstantiate = floor;
        }
        else
        {
            toInstantiate = base.PlaceHoles(x, y, toInstantiate);
        }

        return toInstantiate;

    }

    protected override void SpawnThing()
    {
        //calculate both x and y mid points
        int xMid = columns / 2;
        xMid = xStart + xMid;

        int yMid = rows / 2;
        yMid = yStart + yMid;

        //spawn enemy
        Instantiate(enemy, new Vector3(xMid, yMid, 0f), Quaternion.identity);
    }
}
