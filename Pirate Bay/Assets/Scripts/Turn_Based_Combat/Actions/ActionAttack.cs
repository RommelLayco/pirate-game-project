﻿using UnityEngine;
using System.Collections;

public class ActionAttack : Action {

    private Combatant _attacker;
    private Combatant _receiver;
    private float timespent = 0;

    public ActionAttack(Combatant attacker, Combatant receiver)
    {
        _attacker = attacker;
        _receiver = receiver;
    }

    override public void Work(float deltaTime)
    {
        _attacker.GetComponent<Animator>().SetBool("attacking", true);
        
        timespent += deltaTime;
        if (timespent > _attacker.GetComponent<Animator>().speed)
        {
            _attacker.GetComponent<Animator>().SetBool("attacking", false);
            float damage = _attacker.Attack(_receiver);
            _receiver.TakeDamage(damage);
            _receiver.ShowDamage(damage);
            done = true;
        }
    }
}
