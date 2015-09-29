using UnityEngine;

public class ActionWaitForInput : Action
{
    public override void Work(float deltaTime)
    {
        if (Input.GetButtonUp("Submit"))
        {
            done = true;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Ended)
                done = true;
        }
    }
}
