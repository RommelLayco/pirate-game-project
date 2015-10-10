using UnityEngine;

public abstract class Action {

    protected bool done = false;
    public abstract void Work(float deltaTime);

    public bool IsDone()
    {
        return done;
    }

    // Moves the GameObject towards its target. Returns true when reached the target.
    protected bool Move(GameObject obj, Vector3 target, float speed, float deltaTime)
    {
        Vector3 diff = target - obj.transform.position;
        if (diff.magnitude < 1.0f)
        {
            obj.transform.Translate(diff);
            return true;
        }
        else
        {
            diff.Normalize();
            obj.transform.Translate(diff * deltaTime * speed);
            return false;
        }      
    }

}
