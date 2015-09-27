using UnityEngine;

public class ActionWaitForInput : Action
{
    public override void Work(float deltaTime)
    {
        if (Input.GetButtonDown("Submit"))
        {
            done = true;
        }
    }
}
