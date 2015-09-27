using UnityEngine;

public class Enemy : Combatant{
    public Enemy()
    {
        spd = 2.0f;
    }

    void OnMouseDown()
    {
        GameObject obj = GameObject.Find("CombatManager");
        obj.GetComponent<CombatManager>().SelectTarget(gameObject);
    }
    
}
