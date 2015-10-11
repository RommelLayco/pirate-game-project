using UnityEngine;

public abstract class Enemy : Combatant{

    void OnMouseDown()
    {
        TargetMe();
    }

    void Update()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Ended)
            {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if (contained)
                    TargetMe();
            }

        }
    }
    
}
