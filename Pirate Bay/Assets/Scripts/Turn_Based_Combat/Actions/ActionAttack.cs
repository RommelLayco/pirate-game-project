﻿using UnityEngine;
using System.Collections;
using System;

public class ActionAttack : Action {

    private Combatant attacker;
    private Combatant target;
    private enum State {MovingToTarget, AttackingTarget, MovingBack};
    private State currentState;
    private Vector3 originalPos;

    private static float moveSpeed = 10;

    public ActionAttack(Combatant attacker, Combatant target)
    {
        this.attacker = attacker;
        this.target = target;
        originalPos = attacker.transform.position;
    }

    override public void Work(float deltaTime)
    {
        switch (currentState)
        {
            case State.MovingToTarget: MoveToTarget(deltaTime); break;
            case State.AttackingTarget: AttackTarget(); break;
            case State.MovingBack: MoveBack(deltaTime); break;
        }
    }

    void MoveToTarget(float deltaTime)
    {
        Vector3 targetPos = target.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
        if (Move(attacker.gameObject, targetPos, moveSpeed, deltaTime))
            currentState = State.AttackingTarget;
    }

    void AttackTarget()
    {
        attacker.DoBasicAttackOn(target);
        currentState = State.MovingBack;
    }

    void MoveBack(float deltaTime)
    {
        if (Move(attacker.gameObject, originalPos, moveSpeed, deltaTime))
            done = true;
    }

}
