using System.Collections.Generic;

// Specialized implementation for a list of Actions. Maintains a queue of actions that gets
// executed in their order. An action will repeat in each frame until it is done.
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
