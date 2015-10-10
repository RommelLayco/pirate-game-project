using System.Collections.Generic;
public class ActionList {
    private Queue<Action> actionQueue;
    private bool running = false;

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
        running = true;
    }

    public void Pause()
    {
        running = false;
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
