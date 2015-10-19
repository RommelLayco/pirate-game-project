using UnityEngine;
using System.Collections;

// The basic attack action.
public class ActionAttack : Action {

    private Combatant _attacker;
    private Combatant _receiver;
    private float timespent = 0;
    private float scale;

    public ActionAttack(Combatant attacker, Combatant receiver, float scale = 1.0f)
    {
        _attacker = attacker;
        _receiver = receiver;
        this.scale = scale;
    }

    override public void Work(float deltaTime)
    {
        _attacker.GetComponent<Animator>().SetBool("attacking", true);
        
        timespent += deltaTime;
        if (timespent > _attacker.GetComponent<Animator>().speed)
        {
            _attacker.GetComponent<Animator>().SetBool("attacking", false);
            float damage = _attacker.Attack(_receiver) * scale;
            _receiver.TakeDamage(damage);
            _receiver.ShowDamage(damage);
            done = true;
        }
    }
}
