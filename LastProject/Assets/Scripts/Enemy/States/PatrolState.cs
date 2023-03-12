using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    float waitTimer;
    int randomChoice;
    int previousChoice;
    public override void Enter()
    {
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
    }

    void PatrolCycle()
    {
        enemy.Agent.speed = 3;
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > 2)
            {
                while(previousChoice == randomChoice)
                {
                    randomChoice = Random.Range(0, enemy.path.waypoints.Count);
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[randomChoice].position);
                waitTimer = 0;
                previousChoice = randomChoice;
            }
        }
    }

}
