public abstract class Action {
    protected bool done = false;
    abstract public void Work(float deltaTime);
    public bool IsDone()
    {
        return done;
    }

}
