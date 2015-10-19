// The super class for all actions. An action is a basic unit of computation that occurs in the combat.
public abstract class Action {

    protected bool done = false;

    abstract public void Work(float deltaTime);

    // An action is complete only when the implemented subclasses set done to true
    public bool IsDone()
    {
        return done;
    }

}
