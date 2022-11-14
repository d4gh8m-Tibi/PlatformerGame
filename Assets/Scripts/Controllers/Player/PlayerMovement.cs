using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    
    [SerializeField]
    private float movementSpeed = 7f;
    [SerializeField]
    private float jumpForce = 7f;

    private float jumpCooldown = 1.3f;
    private float jumpTimer = 0.0f;

    private bool canJump
    {
        get { return jumpTimer > jumpCooldown; }
    }

    private void Start()
    {
        SetRigidBody(GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        Move(Input.GetAxis("Horizontal"));

        Jump();
    }

    public void Move(float directionInput)
    {
        if (directionInput != 0)
        {
            var dirX = Input.GetAxis("Horizontal") > 0 ? 0.5f : -0.5f;
            rigidBody.velocity = new Vector2(dirX * 7f, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && canJump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
            jumpTimer = 0.0f;
        }
        else if(!canJump)
        {
            jumpTimer += Time.deltaTime;
        }
    }

    public void SetRigidBody(Rigidbody2D rigidbody)
    {
        this.rigidBody = rigidbody;
    }
}
