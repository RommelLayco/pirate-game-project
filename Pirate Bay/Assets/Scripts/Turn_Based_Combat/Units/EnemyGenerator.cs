﻿using UnityEngine;
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

    private List<GameObject> enemyTypes = new List<GameObject>();
    public enum EnemyType { Snake, Maneater, EnemyPirate };

    // Called by the CombatManager to get a list of enemy GameObjects to be instantiated.
    public List<GameObject> GenerateEnemyList(HashSet<EnemyType> types)
    {
        List<GameObject> enemyTypes = new List<GameObject>();
        if (types == null || types.Count == 0)
        {
            enemyTypes.Add(snake);
            enemyTypes.Add(maneater);
            enemyTypes.Add(enemyPirate);
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
            }
        } 

        List<GameObject> enemyList = new List<GameObject>();
        //int number = UnityEngine.Random.Range(1, 6);
        int number = UnityEngine.Random.Range(1, GameManager.getInstance().islandLevel + 1);
        for(int i = 0; i < number; i++)
        {
            int index = UnityEngine.Random.Range(0, enemyTypes.Count);
            enemyList.Add(enemyTypes[index]);
        }
       
        return enemyList;
    }
}
