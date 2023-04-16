using System;
using System.Collections.Generic;
using System.Text;
using Core;

public class PacketManager
{
    public static Dictionary<PacketType, Action<ServerSession, IPacket>> action = new Dictionary<PacketType, Action<ServerSession, IPacket>>();
    public static Dictionary<PacketType, Func<ServerSession, byte[], IPacket>> packetTypes = new Dictionary<PacketType, Func<ServerSession, byte[], IPacket>>();
    public static PacketHandler packetHandler = new PacketHandler();

    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance { get { return _instance; } }

    static PacketManager()
    {
        action.Add(PacketType.S_Connect, packetHandler.S_ConnectAction);
        packetTypes.Add(PacketType.S_Connect, MakePacket<S_Connect>);

		action.Add(PacketType.S_StartGame, packetHandler.S_StartGameAction);
        packetTypes.Add(PacketType.S_StartGame, MakePacket<S_StartGame>);

		action.Add(PacketType.S_EnterGame, packetHandler.S_EnterGameAction);
        packetTypes.Add(PacketType.S_EnterGame, MakePacket<S_EnterGame>);

		action.Add(PacketType.S_LeaveGame, packetHandler.S_LeaveGameAction);
        packetTypes.Add(PacketType.S_LeaveGame, MakePacket<S_LeaveGame>);

		action.Add(PacketType.S_Spawn, packetHandler.S_SpawnAction);
        packetTypes.Add(PacketType.S_Spawn, MakePacket<S_Spawn>);

		action.Add(PacketType.S_Despawn, packetHandler.S_DespawnAction);
        packetTypes.Add(PacketType.S_Despawn, MakePacket<S_Despawn>);

		action.Add(PacketType.S_Move, packetHandler.S_MoveAction);
        packetTypes.Add(PacketType.S_Move, MakePacket<S_Move>);

		action.Add(PacketType.S_Hit, packetHandler.S_HitAction);
        packetTypes.Add(PacketType.S_Hit, MakePacket<S_Hit>);

		
    }

    static T MakePacket<T>(Session session, byte[] buffer) where T : IPacket, new()
    {
        T pkt = new T();
        pkt.DeSerialize(buffer);
        return pkt;
    }

    public void HandlePacket(ServerSession session, IPacket type)
    {
        Action<ServerSession, IPacket> _action = null;
        if (action.TryGetValue((PacketType)type.Protocol, out _action))
        {
            _action.Invoke(session, type);
        }
    }
}