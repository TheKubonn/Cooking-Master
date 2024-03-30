using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    // Getting the Movement Vector from Player class in order to use it in our player script
    public Vector2 GetMovementVectorNormalized()
    {
        // Making a Vector2 which will handle input
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        // We are making inputVector normalized so that movement is smooth when pressing W & D for example
        inputVector = inputVector.normalized;

        return inputVector;
    }
}