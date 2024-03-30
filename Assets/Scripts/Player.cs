using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    // Setting up moveSpeed float
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update()
    {
        // Assigning inputVector with the value coming from a GameInput script from function GetMovementVectorNormalized
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        // Making a Vector3 which is direction of movement and moving only our player in x and z
        // so we take it from our input vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * (Time.deltaTime * moveSpeed);

        isWalking = moveDir != Vector3.zero;
        // Making character smoothly turn with function Slerp
        const float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}