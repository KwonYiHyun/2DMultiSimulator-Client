using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 des = Vector2.zero;

    void Start()
    {
        
    }

    public void Print(float x, float y)
    {
        Debug.Log("x = " + x + " / y = " + y);
        transform.position = new Vector2(x, y);
    }
}
