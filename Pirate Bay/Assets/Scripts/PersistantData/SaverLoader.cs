using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System;
using System.Collections.Generic;

public class SaverLoader
{

    public static void SaveToFile(string fileName)
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
        XmlElement crew = doc.CreateElement("Crew");
        root.AppendChild(crew);
        foreach (CrewMemberData data in manager.crewMembers)
        {
            XmlElement crewData = doc.CreateElement("CrewMember");
            crew.AppendChild(crewData);

            e = doc.CreateElement("Name");
            e.InnerText = data.getName();
            crewData.AppendChild(e);

            e = doc.CreateElement("Class");
            e.InnerText = CrewMemberData.getStringFromType(data.getCrewClass());
            crewData.AppendChild(e);

            e = doc.CreateElement("Level");
            e.InnerText = data.getLevel().ToString();
            crewData.AppendChild(e);

            e = doc.CreateElement("Atk");
            e.InnerText = data.getBaseAttack().ToString();
            crewData.AppendChild(e);

            e = doc.CreateElement("Def");
            e.InnerText = data.getBaseDefense().ToString();
            crewData.AppendChild(e);

            e = doc.CreateElement("Spd");
            e.InnerText = data.getSpeed().ToString();
            crewData.AppendChild(e);

            e = doc.CreateElement("Exp");
            e.InnerText = data.getExp().ToString();
            crewData.AppendChild(e);
        }

        //Armour
        XmlElement armoury = doc.CreateElement("Armoury");
        root.AppendChild(armoury);
        foreach (Armour armour in manager.armoury)
        {
            e = doc.CreateElement("Armour");
            e.SetAttribute("Strength", armour.getStrength().ToString());
            if (armour.getCrewMember() == null)
            {
                e.SetAttribute("CrewIndex", "-1");
            }
            else
            {
                int index = manager.crewMembers.IndexOf(armour.getCrewMember());
                e.SetAttribute("CrewIndex", index.ToString());
            }
            e.InnerText = armour.getName();
            armoury.AppendChild(e);
        }
        //Weapons
        XmlElement weapons = doc.CreateElement("Weapons");
        root.AppendChild(weapons);
        foreach (Weapon weapon in manager.weapons)
        {
            e = doc.CreateElement("Weapon");
            e.SetAttribute("Strength", weapon.getStrength().ToString());
            if (weapon.getCrewMember() == null)
            {
                e.SetAttribute("CrewIndex", "-1");
            }
            else
            {
                int index = manager.crewMembers.IndexOf(weapon.getCrewMember());
                e.SetAttribute("CrewIndex", index.ToString());
            }
            e.InnerText = weapon.getName();
            weapons.AppendChild(e);
        }

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
        XmlElement islands = doc.CreateElement("IslandsCleared");
        root.AppendChild(islands);
        foreach (KeyValuePair<Vector3, bool> pair in manager.IslandClearedStatus)
        {
            if (pair.Value)
            {
                e = doc.CreateElement("Position");
                e.SetAttribute("x", pair.Key.x.ToString());
                e.SetAttribute("y", pair.Key.y.ToString());
                e.SetAttribute("z", pair.Key.z.ToString());
                islands.AppendChild(e);
            }
        }

        //CurrentPos
        Vector3 pos = manager.currentLocation;
        e = doc.CreateElement("CurrentPos");
        e.SetAttribute("x", pos.x.ToString());
        e.SetAttribute("y", pos.y.ToString());
        e.SetAttribute("z", pos.z.ToString());
        root.AppendChild(e);

        string s = doc.OuterXml;
        Debug.Log(s);
        doc.Save(path);
    }
    //returns succesful or not
    public static bool LoadFromFile(string fileName)
    {
        Debug.Log("Loading " + fileName);
        GameManager manager = GameManager.getInstance();
        string path = Application.persistentDataPath + "/" + fileName;
        if(!System.IO.File.Exists(path))
        {
            Debug.Log("No file found");
            return false;
        }
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        XmlNode root = doc.FirstChild;

        foreach (XmlNode n in root.ChildNodes)
        {
            switch (n.Name)
            {
                case "CaptainName": manager.captainName = n.InnerText; break;
                case "Notoriety": manager.notoriety = Int32.Parse(n.InnerText); break;
                case "Gold": manager.gold = Int32.Parse(n.InnerText); break;
                case "BunkLevel": manager.bunkLevel = Int32.Parse(n.InnerText); break;
                case "CannonLevel": manager.cannonLevel = Int32.Parse(n.InnerText); break;
                case "SailsLevel": manager.sailsLevel = Int32.Parse(n.InnerText); break;
                case "HullLevel": manager.hullLevel = Int32.Parse(n.InnerText); break;
                case "RedRivalry": manager.redRivalry = Int32.Parse(n.InnerText); break;
                case "WhiteRivalry": manager.whiteRivalry = Int32.Parse(n.InnerText); break;
                case "BlueRivalry": manager.blueRivalry = Int32.Parse(n.InnerText); break;
                case "Crew": LoadCrew(n); break;
                case "Armoury": LoadArmoury(n); break;
                case "Weapons": LoadWeapons(n); break;
                case "IslandsCleared": LoadIslands(n); break;
                case "CurrentPos": manager.currentLocation = LoadVector(n); break;
            }
        }
        return true;
    }

    private static CrewMemberData LoadCrewMember(XmlNode node)
    {
        string cName = "";
        CrewMemberData.CrewClass cClass = CrewMemberData.CrewClass.None;
        int atk = 0;
        int def = 0;
        int spd = 0;
        int level = 0;
        int exp = 0;
        foreach (XmlNode nn in node.ChildNodes)
        {
            switch (nn.Name)
            {
                case "Name": cName = nn.InnerText; break;
                case "Class": cClass = CrewMemberData.getTypeFromString(nn.InnerText); break;
                case "Atk": atk = Int32.Parse(nn.InnerText); break;
                case "Def": def = Int32.Parse(nn.InnerText); break;
                case "Spd": spd = Int32.Parse(nn.InnerText); break;
                case "Level": level = Int32.Parse(nn.InnerText); break;
                case "Exp": exp = Int32.Parse(nn.InnerText); break;
            }
        }
        CrewMemberData data = new CrewMemberData(cName, atk, def, spd, 100, null, null);
        data.setCrewClass(cClass);
        data.setExp(exp);
        data.setLevel(level);
        return data;
    }

    private static void LoadCrew(XmlNode node)
    {
        Debug.Log("Load Crew");
        GameManager manager = GameManager.getInstance();
        manager.crewMembers = new List<CrewMemberData>();
        foreach (XmlNode nn in node.ChildNodes)
        {
            manager.crewMembers.Add(LoadCrewMember(nn));
        }
        manager.crewSize = manager.crewMembers.Count;
        manager.crewIndex = 0;
    }

    private static Armour LoadArmour(XmlNode node)
    {
        GameManager manager = GameManager.getInstance();
        int str = Int32.Parse(node.Attributes["Strength"].InnerText);
        string aName = node.InnerText;
        int index = Int32.Parse(node.Attributes["CrewIndex"].InnerText);
        CrewMemberData cmd = null;

        if (index >= 0 && index < manager.crewMembers.Count)
        {
            cmd = manager.crewMembers[index];

        }
        Armour armour = new Armour(str, aName, cmd);
        return armour;
    }
    private static void LoadArmoury(XmlNode node)
    {
        Debug.Log("Load Armoury");
        GameManager manager = GameManager.getInstance();
        manager.armoury = new List<Armour>();
        foreach (XmlNode nn in node.ChildNodes)
        {
            Armour a = LoadArmour(nn);
            manager.armoury.Add(a);
            //make association bidirectional
            if (a.getCrewMember() != null)
            {
                a.getCrewMember().setArmour(a);
            }
        }
    }
    private static Weapon LoadWeapon(XmlNode node)
    {
        GameManager manager = GameManager.getInstance();
        int str = Int32.Parse(node.Attributes["Strength"].InnerText);
        string aName = node.InnerText;
        int index = Int32.Parse(node.Attributes["CrewIndex"].InnerText);
        CrewMemberData cmd = null;

        if (index >= 0 && index < manager.crewMembers.Count)
        {
            cmd = manager.crewMembers[index];

        }
        Weapon weapon = new Weapon(str, aName, cmd);
        return weapon;
    }
    private static void LoadWeapons(XmlNode node)
    {
        Debug.Log("Load Weapons");
        GameManager manager = GameManager.getInstance();
        manager.weapons = new List<Weapon>();
        foreach (XmlNode nn in node.ChildNodes)
        {
            Weapon w = LoadWeapon(nn);
            manager.weapons.Add(w);
            //make association bidirectional
            if (w.getCrewMember() != null)
            {
                w.getCrewMember().setWeapon(w);
            }
        }
    }
    private static void LoadIslands(XmlNode node)
    {
        GameManager manager = GameManager.getInstance();
        manager.IslandClearedStatus = new List<KeyValuePair<Vector3, bool>>();

        foreach (XmlNode nn in node.ChildNodes)
        {
            Vector3 pos = LoadVector(nn);
            manager.IslandClearedStatus.Add(new KeyValuePair<Vector3, bool>(pos, true));
        }
    }
    private static Vector3 LoadVector(XmlNode nn)
    {
        Vector3 pos = new Vector3();
        pos.x = float.Parse(nn.Attributes["x"].InnerText);
        pos.y = float.Parse(nn.Attributes["y"].InnerText);
        pos.z = float.Parse(nn.Attributes["z"].InnerText);
        return pos;
    }
}
