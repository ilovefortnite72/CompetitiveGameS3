using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    private Vector2 lastMoveDirection;
    public MeleeAttack meleeAttack;

    Vector2 movementInput;
    Rigidbody2D rb;
    public Animator anim;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool canMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Update()
    {
        UpdateAnims();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();

        }
        
    }

    private void HandleMovement()
    {
        if (movementInput != Vector2.zero)
        {

            bool canMove = TryMove(movementInput);

            if (!canMove)
            {
                canMove = TryMove(new Vector2(movementInput.x, 0));
                if (!canMove)
                {
                    canMove = TryMove(new Vector2(0, movementInput.y));
                }
            }

        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if(movementInput.x == 0 && movementInput.y ==0)
        {
            lastMoveDirection = movementInput;
        }

    }


    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        { 
            return false;
        }
        
    }


    void UpdateAnims()
    {
        anim.SetFloat("moveX", movementInput.x);
        anim.SetFloat("moveY", movementInput.y);
        anim.SetFloat("moveSpeed", movementInput.sqrMagnitude);
    }

    void OnAttack()
    {
        anim.SetTrigger("MeleeAttack1");
        LockMove();
        if (movementInput.x < 0)
        {
            meleeAttack.AttackLeft();
            UnlockMove();
        }
        else if (movementInput.x > 0)
        {
            meleeAttack.AttackRight();
            UnlockMove();
        }

    }


    public void LockMove()
    {
            canMove = false;
    }

    public void UnlockMove()
    {
            canMove = true;
    }


}
