using UnityEngine;
using System.Collections.Generic;

public class BuffIconProvider : MonoBehaviour
{
    public Sprite tauntSprite;
    public Sprite poisonSprite;
    public static Dictionary<string, Sprite> dict = new Dictionary<string,Sprite>();

	// Use this for initialization
	void Awake ()
    {
        if(!dict.ContainsKey("Taunt"))
            dict.Add("Taunt", tauntSprite);
        if (!dict.ContainsKey("Poison"))
            dict.Add("Poison", poisonSprite);
    }
}
