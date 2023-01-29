using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    // Start is called before the first frame update
    void Awake()
    {
        //if(playerInput == null)
        //{
        //    playerInput = new PlayerInput();
        //}    
        ////playerInput = new PlayerInput();
        ////onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        //onFoot.Jump.performed += ctx => motor.Jump();

        //onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    // Update is called once per frame
    void Update()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerInput = RebindManager.inputActions;
        onFoot = playerInput.OnFoot;
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
