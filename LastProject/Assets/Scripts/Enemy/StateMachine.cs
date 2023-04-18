using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public ChaseState chaseState;
    public RetreatState retreatState;

    //private bool playerIsFound;
    public void Initialise()
    {
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
        if(activeState != null)
        {
            activeState.Perform();
        }
        if (transform.GetComponent<FieldOfView>().canSeePlayer)
        {
            ChangeState(chaseState);
        }
        else if(transform.GetComponent<Enemy>().health < 50)
        {
            ChangeState(retreatState);
            if(transform.GetComponent<Enemy>().health == 100)
            {
                ChangeState(chaseState);
            }
        }
        else
        {
            ChangeState(patrolState);
        }
    }

    public void ChangeState(BaseState newState)
    {
        //First check if the enemy is in a state 
        if(activeState != null)
        {
            //Exit current state to prepare for new state
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
}
