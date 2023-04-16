using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 des = Vector2.zero;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, des, 0.04f);
    }
}
