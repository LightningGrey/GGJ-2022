using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Flags]
public enum PlayerState
{
    IDLE = 1,
    GROUNDED = 2,
    JUMPING = 4,
    ALIVE = 8,
}


public class PlayerControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D pCollider;

    [Header("Variables")] 
    private Vector2 moveVec = Vector2.zero;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    private const float gravity = -9.8f;

    private PlayerState state = PlayerState.IDLE;

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
        if (ctx.performed && HasFlag(PlayerState.GROUNDED))
        {
            UnsetFlag(PlayerState.GROUNDED);
            SetFlag(PlayerState.JUMPING);
            moveVec.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnAttack()
    {

    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(pCollider.bounds.center, pCollider.bounds.size, 0.0f, Vector2.down, 0.1f, groundLayer);
        
        if (hit.collider != null)
        {
            SetFlag(PlayerState.GROUNDED);
        }
        else
        {
            UnsetFlag(PlayerState.GROUNDED);
        }

    }

    void Movement()
    {
        if (HasFlag(PlayerState.GROUNDED) && !HasFlag(PlayerState.JUMPING))
        {
           moveVec.y = 0.0f;
        }

        moveVec.y += gravity * Time.fixedDeltaTime;

        rb.velocity = moveVec * speed;

        //unset flag when player lands
        if (!HasFlag(PlayerState.GROUNDED) && HasFlag(PlayerState.JUMPING))
        {
            UnsetFlag(PlayerState.JUMPING);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject == GameManager.Instance.p1)
            {
                GameManager.Instance.GameOver();
            }
        }
    }


    #region Enum flag functions

    public void SetFlag(PlayerState state)
    {
        this.state |= state;
    }

    public void UnsetFlag(PlayerState state)
    {
        this.state &= ~state;
    }

    public bool HasFlag(PlayerState state)
    {
        return (this.state & state) == state;
    }

    public void ToggleFlag(PlayerState state)
    {
        this.state ^= state;
    }

    #endregion

}
