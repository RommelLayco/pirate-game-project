using UnityEngine;
using System.Collections.Generic;

public class BuffIconProvider : MonoBehaviour
{
    public Sprite tauntSprite;
    public Sprite poisonSprite;
    public static Dictionary<string, Sprite> dict = new Dictionary<string,Sprite>();

	// Use this for initialization
	void Start ()
    {
        dict.Add("Taunt", tauntSprite);
        dict.Add("Poison", poisonSprite);
    }
}
