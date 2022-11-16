using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public List<PlayerAction> Actions { get; set; }
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
            Actions = new List<PlayerAction>();
            Actions.Add(PlayerAction.Idle);
        }
    }

    private void Update()
    {
        
        CheckXAxis();
        CheckInput();
    }

    private void CheckXAxis()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (HorizontalInput != 0 && !Actions.Contains(PlayerAction.MoveHorizontal))
        {
            
            Actions.Add(PlayerAction.MoveHorizontal);
        }
        else
        {
            Actions.Remove(PlayerAction.MoveHorizontal);
        }
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && !Actions.Contains(PlayerAction.Jump))
        {
            Actions.Add(PlayerAction.Jump);
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Escape key was released");
            Actions.Add(PlayerAction.Menu);
        }
        else
        {
            if (!Actions.Contains(PlayerAction.Idle))
            {
                Actions.Add(PlayerAction.Idle);
            }
        }
    }



}
