using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
This will be a singleton GameObject used by the CombatManager to generate a random
list of 1~5 enemies from existing enemy types.
*/
public class CrewGenerator : MonoBehaviour
{

    public GameObject crewMemberOriginal;
    // Called by the CombatManager to get a list of enemy GameObjects to be instantiated.
    public List<CrewMember> GenerateCrewList()
    {
        List<CrewMember> crewList = new List<CrewMember>();
        Debug.Log(GameManager.getInstance().explorers.Count);
        foreach (CrewMemberData data in GameManager.getInstance().explorers)
        {
            GameObject g = Instantiate(crewMemberOriginal);
            CrewMember cm = g.GetComponent<CrewMember>();
            cm.CreateFromData(data);
            crewList.Add(cm);
        }
        return crewList;
    }
}
