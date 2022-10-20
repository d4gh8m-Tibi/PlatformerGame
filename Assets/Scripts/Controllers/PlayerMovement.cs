using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(dirX * 7f, rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 7f, 0); 
        }
    }
}
