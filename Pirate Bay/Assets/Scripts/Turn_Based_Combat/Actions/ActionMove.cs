using UnityEngine;

public class ActionMove : Action
{
    private GameObject obj = null;
    private Vector3 target;
    private float speed = 5;
    private float threshold;
    //target is in worldspace
    public ActionMove(GameObject obj, Vector3 target, float threshold = 0)
    {
        this.obj = obj;
        this.target = target;
        if (obj == null)
            done = true;
        this.threshold = threshold;
    }
    public override void Work(float deltaTime)
    {
        this.obj.GetComponent<Animator>().SetBool("moving", true);
        //obj.transform.Translate(Vector3.MoveTowards(obj.transform.position, target, deltaTime*1.0f));
        Vector3 diff = target - obj.transform.position;
        if (diff.magnitude < threshold || (threshold == 0 && diff.magnitude < 0.2f))
        {
            if(threshold == 0)
                obj.transform.Translate(diff);
            done = true;
            this.obj.GetComponent<Animator>().SetBool("moving", false);
            return;
        }
        diff.Normalize();
        obj.transform.Translate(diff*deltaTime*speed);

    }
}
