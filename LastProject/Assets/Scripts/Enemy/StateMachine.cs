using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public void Initialise()
    {
        patrolState = new PatrolState();
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