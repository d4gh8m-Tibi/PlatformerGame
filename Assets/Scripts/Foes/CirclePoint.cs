using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePoint : MonoBehaviour
{
    [SerializeField] private int OrderNumber;

    public int OrderNumberValue
    {
        get { return OrderNumber; }
    }
}
