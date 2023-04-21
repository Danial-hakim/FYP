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
        enemy.setColor(Color.black);
    }

    public override void Perform(double speed)
    {
        PatrolCycle(speed);
    }

    public override void Exit()
    {
    }

    void PatrolCycle(double speed)
    {
        speed = 5;
        if (!enemy.beingSlowed && !enemy.currentlyFrozen) { enemy.Agent.speed = (float)speed; };
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
