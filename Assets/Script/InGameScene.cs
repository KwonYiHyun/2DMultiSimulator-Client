using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : MonoBehaviour
{
    void Start()
    {
        C_StartGame pkt = new C_StartGame();
        pkt.msg = "200";
        NetworkManager.Instance.Send(pkt.Serialize());
    }

    void Update()
    {
        
    }
}
