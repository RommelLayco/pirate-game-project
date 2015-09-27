using UnityEngine;
using System.Collections;

//This class inherits from boardManager and spawns either
//an enemy tile or treasure tile

public class SpawnManager : BoardManager {

    public GameObject thingTile;

    protected override void SpawnThing ()
    {
        //want to place in center square
        int index = gridPositions.Count;
        index = index / 2;

        //position vector
        Vector3 center = gridPositions[index];

        Instantiate(thingTile, center, Quaternion.identity);
    }

    
}
