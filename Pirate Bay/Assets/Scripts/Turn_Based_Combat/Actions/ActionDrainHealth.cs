using UnityEngine;
using System.Collections;

// The drain health action used in the Devour ability. The attacker gains health equal to damage dealt.
public class ActionDrainHealth : Action {

    private Combatant attacker;
    private Combatant target;
    private float timespent = 0;

    public ActionDrainHealth(Combatant attacker, Combatant target)
    {
        this.attacker = attacker;
        this.target = target;
    }

    public override void Work(float deltaTime)
    {
        attacker.GetComponent<Animator>().SetBool("attacking", true);

        timespent += deltaTime;
        if (timespent > attacker.GetComponent<Animator>().speed)
        {
            attacker.GetComponent<Animator>().SetBool("attacking", false);
            float damage = attacker.Attack(target);
            target.TakeDamage(damage);
            target.ShowDamage(damage);
            attacker.GainHealth(damage);
            attacker.ShowHeal(damage);
            done = true;
        }
    }

}
