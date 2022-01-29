using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("Variables")] 
    private Vector2 moveVec = Vector2.zero;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    private const float gravity = -9.8f;

    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        Movement();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveVec.x = ctx.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            moveVec.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnAttack()
    {

    }

    void GroundCheck()
    {
        //Physics2D.OverlapArea()
    }

    void Movement()
    {

        rb.velocity = Vector2.ClampMagnitude(moveVec * speed, 5.0f);
    }

}
