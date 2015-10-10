using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
This will be a singleton GameObject used by the CombatManager to generate a random
list of 1~5 enemies from existing enemy types.
*/
public class EnemyGenerator : MonoBehaviour {

    public Snake snake;
    public Maneater maneater;

    private List<Enemy> enemyTypes;
    
    void Start()
    {
        // Adds existing enemy types. Any expansion to the enemy base will need to
        // be registered here.
        enemyTypes = new List<Enemy>();
        enemyTypes.Add(snake);
        enemyTypes.Add(maneater);
    } 

    // Called by the CombatManager to get a list of enemy GameObjects to be instantiated.
    public List<Enemy> GenerateEnemyList()
    {
        List<Enemy> enemyList = new List<Enemy>();
        while (true)
        {
            // Add a random enemy type to the list
            enemyList.Add(enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count - 1)]);

            // Stop if max enemies reached or by a certain probability
            if (enemyList.Count == 5 || UnityEngine.Random.value <= 0.1)
            {
                break;
            }
        }
        return enemyList;
    }
}
