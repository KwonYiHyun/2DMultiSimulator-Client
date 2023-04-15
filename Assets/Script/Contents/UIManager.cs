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
        pos.posX = int.Parse(input_posX.text);
        pos.posY = int.Parse(input_posY.text);

        C_Move movePacket = new C_Move();
        movePacket.positionInfo = pos;
        movePacket.speed = float.Parse(input_speed.text);

        NetworkManager.Instance.Send(movePacket.Serialize());

        GameObject player = GameObject.Find("MyPlayer(Clone)");
        // player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(pos.posX, pos.posY), 10 * Time.deltaTime);
        player.transform.position = Vector2.Lerp(player.transform.position, new Vector2(pos.posX, pos.posY), Time.deltaTime);
    }
}
