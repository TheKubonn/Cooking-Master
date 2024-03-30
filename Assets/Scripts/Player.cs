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
        
        
        // Making player collision detection by Ray casting and checking if we can move
        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 headLocation = transform.position + Vector3.up * playerHeight;
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);
        bool canMove = !Physics.CapsuleCast(transform.position, headLocation, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir
            // First attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, headLocation, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the x direction
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X
                // Attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, headLocation, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }
        
        if (canMove)
        {
            // Can move towards moveDir
            transform.position += moveDir * moveDistance;
        }
        
        
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