using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public override void Enter()
    {
    }

    public override void Perform()
    {
        ChasePlayer();
    }

    public override void Exit()
    {
    }

    void ChasePlayer()
    {
        enemy.Agent.speed = 7;
        enemy.Agent.SetDestination(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
    }
}
