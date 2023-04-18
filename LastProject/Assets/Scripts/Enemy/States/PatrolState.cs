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
        enemy.Agent.speed = 3;
        enemy.setColor(Color.black);
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
        if (enemy.Agent.remainingDistance < 1.5f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 2)
            {
                while (previousChoice == randomChoice)
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
