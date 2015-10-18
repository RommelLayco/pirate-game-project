using UnityEngine;
using System.Collections;

public class LoadButton : MonoBehaviour
{
    public void onClickLoad()
    {
        SaverLoader.LoadFromFile("SaveGame001.xml");
        Application.LoadLevel("Ship");
    }
}
