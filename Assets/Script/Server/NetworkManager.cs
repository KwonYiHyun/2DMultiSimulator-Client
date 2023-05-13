using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }

    public GameObject myPlayer;
    public GameObject otherPlayer;
    public GameObject projectile;

    public ObjectManager objManager = new ObjectManager();

    public int ssec = 0, esec = 0, dsec = 0;
    public int playerId;

    private void Awake()
    {
        if(_instance == null){
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public async void Send(ArraySegment<byte> sendBuff)
    {
        await SessionManager.Instance.sessions[0].SendAsync(sendBuff);
    }

    void Start()
    {
        Connect();
    }

    void Update()
    {
        List<IPacket> list = PacketQueue.Instance.PopAll();
        foreach (IPacket packet in list)
        {
            PacketManager.Instance.HandlePacket(SessionManager.Instance.sessions[0], packet);
        }
    }

    public async void Connect()
    {
        /*
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
        */
        IPAddress ipA = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ipA, 7777);
        Connector connector = new Connector();

        connector.init(endPoint);

        await connector.ConnectAsync();

        SceneManager.LoadScene(1);
    }

    public void OnHit()
    {
        Debug.Log("Hit");
    }

    public int sNow()
    {
        return ssec = Environment.TickCount;
    }

    public int eNow()
    {
        return esec = Environment.TickCount;
    }

    public void LeavePlayer(GameObject obj)
    {
        Destroy(obj);
    }
}
