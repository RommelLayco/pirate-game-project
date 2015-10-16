using UnityEngine;

public class ActionMove : Action
{
    private GameObject obj = null;
    private Vector3 target;
    private float speed = 5;

    //target is in worldspace
    public ActionMove(GameObject obj, Vector3 target)
    {
        this.obj = obj;
        this.target = target;
        if (obj == null)
            done = true;
    }
    public override void Work(float deltaTime)
    {
        this.obj.GetComponent<Animator>().SetBool("moving", true);
        //obj.transform.Translate(Vector3.MoveTowards(obj.transform.position, target, deltaTime*1.0f));
        Vector3 diff = target - obj.transform.position;
        if (diff.magnitude < 1.0f)
        {
            //obj.transform.Translate(diff);
            done = true;
            this.obj.GetComponent<Animator>().SetBool("moving", false);
            return;
        }
        diff.Normalize();
        obj.transform.Translate(diff*deltaTime*speed);

    }
}
