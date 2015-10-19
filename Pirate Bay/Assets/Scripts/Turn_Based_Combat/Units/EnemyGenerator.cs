using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
    This will be a singleton GameObject used by the CombatManager to generate a random
    list of enemies from existing enemy types.
*/
public class EnemyGenerator : MonoBehaviour {

    public GameObject snake;
    public GameObject maneater;
    public GameObject enemyPirate;
    public GameObject giantCrab;

    public List<GameObject> enemyTypes = new List<GameObject>();
    public enum EnemyType { Snake, Maneater, EnemyPirate, GiantCrab };

    // Called by the CombatManager to get a list of enemy GameObjects to be instantiated.
    // The min/max number and types of enemies generated can be customized.
    public List<GameObject> GenerateEnemyList(HashSet<EnemyType> types, int minEnemies, int maxEnemies)
    {
        List<GameObject> enemyTypes = new List<GameObject>();
        if (types == null || types.Count == 0) // If types is null, default to all enemy types
        {
            enemyTypes.Add(snake);
            enemyTypes.Add(maneater);
            enemyTypes.Add(enemyPirate);
            enemyTypes.Add(giantCrab);
        }
        else {
            foreach (EnemyType t in types)
            {
                if (t == EnemyType.Snake)
                    enemyTypes.Add(snake);
                if (t == EnemyType.Maneater)
                    enemyTypes.Add(maneater);
                if (t == EnemyType.EnemyPirate)
                    enemyTypes.Add(enemyPirate);
                if (t == EnemyType.GiantCrab)
                    enemyTypes.Add(giantCrab);
            }
        } 

        // Randomly generates a number of enemies between the min and max values.
        List<GameObject> enemyList = new List<GameObject>();
        int number = UnityEngine.Random.Range(minEnemies, maxEnemies + 1);
        for(int i = 0; i < number; i++)
        {
            int index = UnityEngine.Random.Range(0, enemyTypes.Count);
            enemyList.Add(enemyTypes[index]);
        }
       
        return enemyList;
    }
}
