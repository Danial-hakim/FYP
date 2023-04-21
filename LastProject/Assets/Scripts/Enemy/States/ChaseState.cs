using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public override void Enter()
    {
        enemy.setColor(Color.red);
    }

    public override void Perform(double speed)
    {
        ChasePlayer(speed);
    }

    public override void Exit()
    {
    }

    void ChasePlayer(double speed)
    {
        speed *= 1.5f;
        if(speed > 12.5f)
        {
            speed = 12.5f;
        }
        if (!enemy.beingSlowed && !enemy.currentlyFrozen) { enemy.Agent.speed = (float)speed; };
        enemy.Agent.SetDestination(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
    }
}
