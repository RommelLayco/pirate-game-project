using UnityEngine;
using System.Collections;
using System;

public class ActionInfo : Action
{
    private String info;

    public ActionInfo(String info)
    {
        this.info = info;
    }

    public override void Work(float deltaTime)
    {
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText(info);
        done = true;
    }
}
