using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    public void onClickNewGame()
    {
        GameManager.getInstance().NewGame();
        Application.LoadLevel("Ship");
    }
    public void onClickLoad()
    {
        if (SaverLoader.LoadFromFile("SaveGame001.xml"))
            Application.LoadLevel("Ship");
    }
    public void onClickHelp() {
        Application.LoadLevel("Help");
    }
    public void onClickStory()
    {
        Application.LoadLevel("Story");
    }
}
