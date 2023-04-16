using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDemoScene : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        player.transform.position = Vector2.Lerp(player.transform.position, new Vector2(5, 5), Time.deltaTime);
    }
}
