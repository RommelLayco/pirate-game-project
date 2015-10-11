using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
This will be a singleton GameObject used by the CombatManager to generate a random
list of 1~5 enemies from existing enemy types.
*/
public class EnemyGenerator : MonoBehaviour {

    public GameObject snake;
    public GameObject maneater;
    public GameObject enemyPirate;

    private List<GameObject> enemyTypes;

    void Start()
    {
        // Adds existing enemy types. Any expansion to the enemy base will need to
        // be registered here.
        enemyTypes = new List<GameObject>();
        enemyTypes.Add(snake);
        enemyTypes.Add(maneater);
        enemyTypes.Add(enemyPirate);
    } 

    // Called by the CombatManager to get a list of enemy GameObjects to be instantiated.
    public List<GameObject> GenerateEnemyList()
    {
        List<GameObject> enemyList = new List<GameObject>();
        while (true)
        {
            // Add a random enemy type to the list
            enemyList.Add(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)]);

            // Stop if max enemies reached or by a certain probability
            if (enemyList.Count == 5 || UnityEngine.Random.value <= 0.1)
            {
                break;
            }
        }
        return enemyList;
    }
}
