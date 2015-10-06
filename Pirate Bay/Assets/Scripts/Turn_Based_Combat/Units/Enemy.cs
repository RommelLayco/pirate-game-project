using UnityEngine;

public class Enemy : Combatant{

    public Enemy()
    {
        spd = 2.0f;
    }

    void OnMouseDown()
    {
        TargetMe();
    }

    /*void OnMouseOver()
    {
        if (targetable && selectionRing == null)
            SetSelectionRing();
    }

    void OnMouseExit()
    {
        if (targetable)
            UnsetSelectionRing();
    }*/

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
