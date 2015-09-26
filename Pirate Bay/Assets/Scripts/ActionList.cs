using System.Collections.Generic;
public class ActionList {
    private Queue<Action> actionQueue;
    private bool runnning = false;

    public ActionList()
    {
        actionQueue = new Queue<Action>();
    }

    public void Add(Action action)
    {
        actionQueue.Enqueue(action);
    }

    public void Start()
    {
        runnning = true;
    }

    public void Pause()
    {
        runnning = false;
    }

    public void Work(float deltaTime)
    {
        Action action = actionQueue.Peek();
        action.Work(deltaTime);
        if(action.IsDone())
        {
            actionQueue.Dequeue();
        }
    }

    public bool IsDone()
    {
        return actionQueue.Count == 0;
    }

}
