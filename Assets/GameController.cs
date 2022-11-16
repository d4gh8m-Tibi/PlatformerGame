using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameController instance;
    [SerializeField]
    private PlayerInput PlayerInput;
    private void Awake()
    {
        instance = this;
    }
}
