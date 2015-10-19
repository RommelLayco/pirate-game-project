using UnityEngine;
using System.Collections;
using System;

// The action used to create a slight delay for playback effects.
public class ActionPauseForFrames : Action
{
    private int framesToPause;
    private int framesPaused;

    public ActionPauseForFrames(int framesToPause)
    {
        this.framesToPause = framesToPause;
        this.framesPaused = 0;
    }

    public override void Work(float deltaTime)
    {
        framesPaused++;
        if (framesPaused >= framesToPause)
        {
            done = true;
        }
    }
}
