using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
	public Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

	public static GameObjectType GetObjectTypeById(int id)
	{
		int type = (id >> 24) & 0x7F;
		return (GameObjectType)type;
	}

	public void Add(ObjectInfo info, bool myPlayer = false)
	{
		GameObjectType objectType = GetObjectTypeById(info.objectId);

		if (objectType == GameObjectType.Player)
		{
			if (myPlayer)
			{
				/*
				GameObject go = Managers.Resource.Instantiate("Creature/MyPlayer");
				go.name = info.Name;
				_objects.Add(info.ObjectId, go);

				MyPlayer = go.GetComponent<MyPlayerController>();
				MyPlayer.Id = info.ObjectId;
				MyPlayer.PosInfo = info.PosInfo;
				MyPlayer.SyncPos();
				*/

				GameObject go = GameObject.Instantiate(NetworkManager.Instance.myPlayer);
				go.transform.position = Vector3.zero;
				_objects.Add(info.objectId, go);
			}
			else
			{
				/*
				GameObject go = Managers.Resource.Instantiate("Creature/Player");
				go.name = info.Name;
				_objects.Add(info.ObjectId, go);

				PlayerController pc = go.GetComponent<PlayerController>();
				pc.Id = info.ObjectId;
				pc.PosInfo = info.PosInfo;
				pc.SyncPos();
				*/

				GameObject go = GameObject.Instantiate(NetworkManager.Instance.otherPlayer);
				go.transform.position = new Vector2(info.positionInfo.posX, info.positionInfo.posY);
				go.GetComponent<Player>().des = new Vector2(info.positionInfo.posX, info.positionInfo.posY);
				_objects.Add(info.objectId, go);
			}
		}
		else if (objectType == GameObjectType.Monster)
		{

		}
		else if (objectType == GameObjectType.Projectile)
		{
			GameObject go = GameObject.Instantiate(NetworkManager.Instance.projectile);
			go.transform.position = new Vector2(info.positionInfo.posX, info.positionInfo.posY);
			_objects.Add(info.objectId, go);
		}
	}

	public void Remove(int id)
	{
		GameObject go = FindById(id);
		if (go == null)
			return;

		_objects.Remove(id);
		// Managers.Resource.Destroy(go);
	}

	public void RemoveMyPlayer()
	{
		/*
		if (MyPlayer == null)
			return;

		Remove(MyPlayer.Id);
		MyPlayer = null;
		*/
	}

	public GameObject FindById(int id)
	{
		GameObject go = null;
		_objects.TryGetValue(id, out go);
		return go;
	}

	public GameObject Find(Vector3Int cellPos)
	{
		/*
		foreach (GameObject obj in _objects.Values)
		{
			CreatureController cc = obj.GetComponent<CreatureController>();
			if (cc == null)
				continue;

			if (cc.CellPos == cellPos)
				return obj;
		}
		*/

		return null;
	}

	public GameObject Find(Func<GameObject, bool> condition)
	{
		foreach (GameObject obj in _objects.Values)
		{
			if (condition.Invoke(obj))
				return obj;
		}

		return null;
	}

	public void Clear()
	{
		/*
		foreach (GameObject obj in _objects.Values)
			Managers.Resource.Destroy(obj);
		*/
		_objects.Clear();
	}
}
