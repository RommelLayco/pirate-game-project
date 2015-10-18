using UnityEngine;
using System.Collections;
using System;

public class ActionShakeBuff : Action {
    private BuffIcon b;
    private float time;

	public ActionShakeBuff(BuffIcon b, float time)
    {
        this.b = b;
        this.time = time;
    }

    public override void Work(float deltaTime)
    {
        b.ShakeIcon(time);
        done = true;
    }
}
