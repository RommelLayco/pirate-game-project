using UnityEngine;
using System.Collections;
using System;

public class ActionHideInfo : Action
{
    public override void Work(float deltaTime)
    {
        GameObject.Find("Battle Info").GetComponent<BattleText>().HideText();
        done = true;
    }
}
