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

    void Update()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Ended)
            {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if(contained)
                    TargetMe();
            }

        }

    }

    void TargetMe()
    {
        if(!IsDead())
        { 
            GameObject obj = GameObject.Find("CombatManager");
            obj.GetComponent<CombatManager>().SelectTarget(gameObject);
        }
    }

    public void SetSelectionRing()
    {
        selectionRing = Instantiate(GameObject.Find("EnemySelectionRing")) as GameObject;
        float height = (this.GetComponent<BoxCollider>().size.y) * this.transform.localScale.y / 2.0f;
        selectionRing.transform.position = this.transform.position + new Vector3(0.0f, -height, 0.0f);
        selectionRing.transform.parent = this.gameObject.transform;

    }

    
}
