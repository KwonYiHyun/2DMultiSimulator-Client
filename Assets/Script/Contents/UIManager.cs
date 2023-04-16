using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_InputField input_posX;
    public TMP_InputField input_posY;
    public TMP_InputField input_speed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SendTo()
    {
        PositionInfo pos = new PositionInfo();
        pos.posX = float.Parse(input_posX.text);
        pos.posY = float.Parse(input_posY.text);

        C_Move movePacket = new C_Move();
        movePacket.positionInfo = pos;
        movePacket.speed = float.Parse(input_speed.text);

        NetworkManager.Instance.Send(movePacket.Serialize());

        // GameObject player = GameObject.Find("MyPlayer(Clone)");

        // player.GetComponent<Player>().des = new Vector2(movePacket.positionInfo.posX, movePacket.positionInfo.posY);
    }
}
