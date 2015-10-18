using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class CaptainScript : MonoBehaviour {
    private GameManager manager;
    private Text captainInfo;
    private Text captainName;
    private Text newText;

    void Awake () {
        manager = GameManager.getInstance();
        captainInfo = GameObject.Find("CaptainData").GetComponent<Text>();
        captainName = GameObject.Find("CaptainName").GetComponentInChildren<InputField>().GetComponentInChildren<Text>();
        newText = GameObject.Find("InputText").GetComponent<Text>();

        setCaptainInfo();
    }

    void Update() {
        setCaptainInfo();
    }

    public void setCaptainName() {
        //Setting the captain's name
        string newName = newText.text;
        if (newName.Length != 0) {
            manager.captainName = newName;
        }
        GameObject.Find("CaptainName").GetComponentInChildren<InputField>().text = "";
        setCaptainInfo();
    }

    private void setCaptainInfo() {
        //Setting up the display of the captain's information
        captainName.text = manager.captainName;
        newText.text = manager.captainName;
        captainInfo.text = "\n" + manager.gold + "\n" + manager.crewSize + "\n" + manager.notoriety;
    }

    public void onClickSave() {
        //Saving the game
        Debug.Log("Game should be saved here");
        SaveToFile("SaveGame001.xml");
    }

    public void SaveToFile(string fileName)
    {
        GameManager manager = GameManager.getInstance();
        string path = Application.persistentDataPath + "/" + fileName;
        Debug.Log("Saving to " + path);
        XmlDocument doc = new XmlDocument();

        XmlElement root = doc.CreateElement("SaveData");
        doc.AppendChild(root);

        //Captains Name
        XmlElement e = doc.CreateElement("CaptainName");
        e.InnerText = manager.captainName;
        root.AppendChild(e);

        //Notoriety
        e = doc.CreateElement("Notoriety");
        e.InnerText = manager.notoriety.ToString();
        root.AppendChild(e);

        //Gold
        e = doc.CreateElement("Gold");
        e.InnerText = manager.gold.ToString();
        root.AppendChild(e);

        //BunkLevel
        e = doc.CreateElement("BunkLevel");
        e.InnerText = manager.bunkLevel.ToString();
        root.AppendChild(e);

        //SailsLevel
        e = doc.CreateElement("SailsLevel");
        e.InnerText = manager.sailsLevel.ToString();
        root.AppendChild(e);

        //CannonLevel
        e = doc.CreateElement("CannonLevel");
        e.InnerText = manager.cannonLevel.ToString();
        root.AppendChild(e);

        //HullLevel
        e = doc.CreateElement("HullLevel");
        e.InnerText = manager.hullLevel.ToString();
        root.AppendChild(e);

        //Crew

        //Armour

        //Weapons

        //Rivalries
        e = doc.CreateElement("RedRivalry");
        e.InnerText = manager.redRivalry.ToString();
        root.AppendChild(e);

        e = doc.CreateElement("WhiteRivalry");
        e.InnerText = manager.whiteRivalry.ToString();
        root.AppendChild(e);

        e = doc.CreateElement("BlueRivalry");
        e.InnerText = manager.blueRivalry.ToString();
        root.AppendChild(e);

        //ClearedIslands


        string s = doc.OuterXml;
        Debug.Log(s);
    }
}
