using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    public void onClickLoad()
    {
        if(SaverLoader.LoadFromFile("SaveGame001.xml"))
            Application.LoadLevel("Ship");
    }
}
