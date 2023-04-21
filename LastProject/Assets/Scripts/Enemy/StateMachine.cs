using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState activeState;
    private PatrolState patrolState;
    private ChaseState chaseState;
    private RetreatState retreatState;
    private BaseState previousState;

    Enemy enemy;
     
    double decision; // Decide what the ai do and how fast they should move , Look at fuzzy logic for more explaination
    //private bool playerIsFound;
    public void Initialise()
    {
        enemy = GetComponent<Enemy>();

        chaseState = new ChaseState();
        patrolState = new PatrolState();
        retreatState = new RetreatState();
        ChangeState(patrolState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        decision = enemy.GetFuzzyLogic().initiateFuzzy(enemy.health);

        if (enemy.GetComponent<FieldOfView>().canSeePlayer)
        {
            MakeDecision(decision);
        }
        else
        {
            if (decision < 0)
            {
                //Debug.Log("Heal up");
                ChangeState(retreatState);
                if (enemy.health == 100)
                {
                    //Debug.Log("Health is full, resume previous behavior");
                    ChangeState(previousState);
                }
            }
            else
            {
                //Debug.Log("Patrol the area");
                ChangeState(patrolState);
            }
        }

        if (activeState != null)
        {
            activeState.Perform(decision);
        }
    }

    public void ChangeState(BaseState newState)
    {
        if(activeState != null)
        {
            previousState = activeState;
            activeState.Exit();
        }

        activeState = newState;

        if(activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }

    void MakeDecision(double decision)
    {
        if (decision > 0)
        {
            //Debug.Log("Chase player if player is found");
            ChangeState(chaseState);
        }
        else if (decision < 0)
        {
            //Debug.Log("Heal up");
            ChangeState(retreatState);
        }
        else
        {
            //Debug.Log("Keep doing what you're doing");
            ChangeState(previousState);
        }
    }
}
