using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.speed = 7;
        enemy.setColor(Color.green);
    }

    public override void Perform()
    {
        FindHealZone();
    }

    public override void Exit()
    {
    }

    void FindHealZone()
    {
        enemy.Agent.SetDestination(enemy.healZonePosition);
    }
}
