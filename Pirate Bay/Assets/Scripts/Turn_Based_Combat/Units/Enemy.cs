using System.Collections.Generic;
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

    public Queue<Action> ActionAI(List<Combatant> targets, List<Combatant> allies)
    {
        if (ability.GetCD() <= 0)
        {
            ability.PutOnCD();
            if (ability.needsTarget)
            {               
                AbilityTargeted targetableAbility = ability as AbilityTargeted;
                targetableAbility.SetTarget(GetTargetable(targets)[0]);
                return targetableAbility.GetActions(this, allies, targets);
            }
            else
            {
                return ability.GetActions(this, allies, targets);
            }
        }
        else
        {
            AbilityTargeted basicAttack = new AbilityBasicAttack();
            basicAttack.SetTarget(GetTargetable(targets)[0]);
            return basicAttack.GetActions(this, allies, targets);
        }
    }

}
