using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;
using UnityEngine;

public class PacketHandler
{
    public void S_ConnectAction(Session session, IPacket packet)
    {
		
    }

	public void S_StartGameAction(Session session, IPacket packet)
	{

	}

	public void S_EnterGameAction(Session session, IPacket packet)
	{
		S_EnterGame enterPacket = packet as S_EnterGame;
		NetworkManager.Instance.objManager.Add(enterPacket.objectInfo, myPlayer: true);
	}

	public void S_LeaveGameAction(Session session, IPacket packet)
	{

	}

	public void S_SpawnAction(Session session, IPacket packet)
	{
		S_Spawn spawnPacket = packet as S_Spawn;
        foreach (ObjectInfo obj in spawnPacket.objectInfoList)
        {
			NetworkManager.Instance.objManager.Add(obj, myPlayer: false);
        }
	}

	public void S_DespawnAction(Session session, IPacket packet)
	{

	}

	public void S_MoveAction(Session session, IPacket packet)
	{
		S_Move movePacket = packet as S_Move;

		GameObject go = NetworkManager.Instance.objManager.FindById(movePacket.objectId);
		if (go == null)
			return;

		//go.GetComponent<Player>().des = new Vector2(movePacket.positionInfo.posX, movePacket.positionInfo.posY);

		NetworkManager.Instance.eNow();
		NetworkManager.Instance.dsec = NetworkManager.Instance.esec - NetworkManager.Instance.ssec;
		NetworkManager.Instance.ssec = NetworkManager.Instance.esec;

		go.GetComponent<Player>().Print(movePacket.positionInfo.posX, movePacket.positionInfo.posY);
	}

	public void S_HitAction(Session session, IPacket packet)
	{
		NetworkManager.Instance.OnHit();

		S_Hit hitPacket = packet as S_Hit;

		GameObject go = NetworkManager.Instance.objManager.FindById(hitPacket.objectInfo.objectId);
		if (go == null)
			return;

		NetworkManager.Instance.OnHit();
	}
}
