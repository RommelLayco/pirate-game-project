using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrewGenerator : MonoBehaviour
{

    public GameObject crewMemberOriginal;

    public List<CrewMember> GenerateCrewList()
    {
        List<CrewMember> crewList = new List<CrewMember>();
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
