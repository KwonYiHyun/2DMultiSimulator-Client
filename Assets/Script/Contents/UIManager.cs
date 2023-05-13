using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text frame;

    void Start()
    {
        
    }

    void Update()
    {
        frame.SetText(NetworkManager.Instance.dsec + "");
    }

    public void SendTo()
    {
        StartCoroutine("Send");
    }

    IEnumerator Send()
    {
        while (true)
        {
            System.Random rand = new System.Random();

            double minX = -16;
            double maxX = 16;
            double minY = -8;
            double maxY = 8;
            
            double rangeX = maxX - minX;
            double rangeY = maxY - minY;
            
            double sample = rand.NextDouble();
            double scaled = (sample * rangeX) + minX;
            double x = scaled;

            sample = rand.NextDouble();
            scaled = (sample * rangeY) + minY;
            double y = scaled;

            x = Math.Truncate(x * 10) / 10;
            y = Math.Truncate(y * 10) / 10;

            PositionInfo pos = new PositionInfo();
            pos.posX = (float)x;
            pos.posY = (float)y;

            C_Move movePacket = new C_Move();
            movePacket.positionInfo = pos;

            NetworkManager.Instance.sNow();
            NetworkManager.Instance.Send(movePacket.Serialize());

            int val = rand.Next(2, 5);

            yield return new WaitForSeconds(val);
        }
    }
}
