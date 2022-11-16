using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    
    [SerializeField]
    private float movementSpeed = 7f;
    [SerializeField]
    private float jumpForce = 7f;
    [SerializeField]
    private LayerMask jumpableGround;

    private BoxCollider2D coll;
    private float jumpCooldown = 1.3f;
    private float jumpTimer = 0.0f;
    private PlayerInput playerInput;
    private void Start()
    {
        SetRigidBody(GetComponent<Rigidbody2D>());
        coll = GetComponent<BoxCollider2D>();
    }

    private void Awake()
    {
        playerInput = PlayerInput.Instance;
    }

    private void Update()
    {
        ExecutePlayerInputs(playerInput.Actions);
    }

    private void ExecutePlayerInputs(List<PlayerAction> actions)
    {
        if(actions.Contains(PlayerAction.Jump))
        {
            Jump();
            actions.Remove(PlayerAction.Jump);
        }
        else if(actions.Contains(PlayerAction.MoveHorizontal))
        {
            Move(playerInput.HorizontalInput);
            actions.Remove(PlayerAction.MoveHorizontal);
        }
        else if(actions.Contains(PlayerAction.Menu))
        {
            //openmenu
        }
    }

    public void Move(float directionInput)
    {
        if (directionInput != 0)
        {
            var dirX = directionInput > 0 ? 0.5f : -0.5f;
            rigidBody.velocity = new Vector2(dirX * 7f, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
            jumpTimer = 0.0f;
        }
    }

    public void SetRigidBody(Rigidbody2D rigidbody)
    {
        this.rigidBody = rigidbody;
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
