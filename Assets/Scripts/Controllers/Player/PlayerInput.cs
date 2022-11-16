using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public PlayerAction Action { get; private set; }
    public float HorizontalInput { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        CheckInput();
        CheckXAxis();
    }

    private void CheckXAxis()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Action = PlayerAction.Jump;
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Escape key was released");
            Action = PlayerAction.Menu;
        }
    }



}
