using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public override void Enter()
    {
        enemy.setColor(Color.green);
    }

    public override void Perform(double speed)
    {
        FindHealZone(speed);
    }

    public override void Exit()
    {
    }

    void FindHealZone(double speed)
    {
        if (speed < 0)
        {
            speed *= -1f;
        }
        if (!enemy.beingSlowed && !enemy.currentlyFrozen) { enemy.Agent.speed = (float)speed; };
        enemy.Agent.SetDestination(enemy.healZonePosition);
    }
}
