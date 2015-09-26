public abstract class Action {
    protected bool done = false;
    abstract public void Work();
    public bool IsDone()
    {
        return done;
    }

}
