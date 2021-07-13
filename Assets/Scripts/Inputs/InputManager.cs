﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    public Vector2 movementInput;

    //Input Detected
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            //Move thorugh variables in input system 
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable(); 
    }
}
