using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float jumpForce = 7f;
    [SerializeField]
    private LayerMask jumpableGround;

    private BoxCollider2D boxCollider2D;
    private PlayerInput playerInput;
    private GameController gameController;
    public int StarCounter { get; private set; } = 0;
    
    private Animator animator;
    private PlayerAction playerActionState = PlayerAction.Idle;

    private void Start()
    {
        SetRigidBody(GetComponent<Rigidbody2D>());
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerInput = PlayerInput.Instance;
        gameController = GameController.instance;
        boxCollider2D.tag = "Player";
    }

    private void Awake()
    {
       
    }

    private void Update()
    {
        ExecutePlayerInputs(playerInput.Actions);
        UpdateAnimationState();
    }

    private void ExecutePlayerInputs(List<PlayerAction> actions)
    {
        if(actions.Contains(PlayerAction.Jump))
        {
            Jump();
            actions.Remove(PlayerAction.Jump);
        }
        else if(actions.Contains(PlayerAction.Move))
        {
            Move(playerInput.HorizontalInput);
            actions.Remove(PlayerAction.Move);

        }
        
        if(actions.Contains(PlayerAction.Menu))
        {
            //openmenu
            gameController.SetGameSpeedForMenu();
            actions.Remove(PlayerAction.Menu);
        }
    }

    public void Move(float directionInput)
    {
        rigidBody.velocity = new Vector2(directionInput * movementSpeed, rigidBody.velocity.y);
    }

    public void Jump()
    {
        if(IsGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
        }
    }

    public void SetRigidBody(Rigidbody2D rigidbody)
    {
        rigidBody = rigidbody;
    }

    public void SetCollider(BoxCollider2D boxCollider2D)
    {
        this.boxCollider2D = boxCollider2D;
    }

    public bool IsGrounded
    {
        get
        {
            return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        }        
    }

    public void UpdateAnimationState()
    {
        if (playerInput.HorizontalInput > 0f)
        {
            playerActionState = PlayerAction.Move;
        }
        else if (playerInput.HorizontalInput < 0f)
        {
            playerActionState = PlayerAction.Move;
        }
        else
        {
            playerActionState = PlayerAction.Idle;
        }

        if (rigidBody.velocity.y > .1f)
        {
            playerActionState = PlayerAction.Jump;
        }
      
        animator.SetInteger("state", (int)playerActionState);
    }

    public void IncrementStars()
    {
        StarCounter++;
        Debug.Log(StarCounter);
    }
}
