using UnityEngine;
using System.Collections;
using System;

public class ActionDoubleStrike : Action {

    private Combatant attacker;
    private Combatant target;
    private enum State { MovingToTarget, AttackingFirstTime, AttackingSecondTime, MovingBack };
    private State currentState;
    private Vector3 originalPos;
    private int framesSinceFirstAttack;

    private static float moveSpeed = 10;
    private static int framesBetweenAttacks = 60;

    public ActionDoubleStrike(Combatant attacker, Combatant target)
    {
        this.attacker = attacker;
        this.target = target;
        originalPos = attacker.transform.position;
        framesSinceFirstAttack = 0;
    }

    override public void Work(float deltaTime)
    {
        switch (currentState)
        {
            case State.MovingToTarget: MoveToTarget(deltaTime); break;
            case State.AttackingFirstTime: AttackFirstTime(); break;
            case State.AttackingSecondTime: AttackSecondTime(); break;
            case State.MovingBack: MoveBack(deltaTime); break;
        }
    }

    void MoveToTarget(float deltaTime)
    {
        Vector3 targetPos = target.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
        if (Move(attacker.gameObject, targetPos, moveSpeed, deltaTime))
            currentState = State.AttackingFirstTime;
    }

    void AttackFirstTime()
    {
        attacker.DoBasicAttackOn(target);
        currentState = State.AttackingSecondTime;
    }

    void AttackSecondTime()
    {
        framesSinceFirstAttack++;
        if (framesSinceFirstAttack >= framesBetweenAttacks)
        {
            attacker.DoBasicAttackOn(target);
            currentState = State.MovingBack;
            currentState = State.MovingBack;
        }
    }

    void MoveBack(float deltaTime)
    {
        if (Move(attacker.gameObject, originalPos, moveSpeed, deltaTime))
            done = true;
    }

}
