
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Core;

public class Packets
{

}


public class S_Connect : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_Connect;
    public int id;
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] idPkt = BitConverter.GetBytes(id);
        byte[] idSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)idPkt.Length));
	
	short dataSize = (short)(packetType.Length + idPkt.Length + idSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(idSize, 0, buffer, offset, idSize.Length);
        offset += idSize.Length;
        Array.Copy(idPkt, 0, buffer, offset, idPkt.Length);
        offset += idPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short idSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        id = BitConverter.ToInt32(buffer, offset);
        offset += idSize;
        
	
    }
}


public class C_Connect : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.C_Connect;
    public string msg;
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] msgPkt = Encoding.UTF8.GetBytes(msg);
        byte[] msgSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)msgPkt.Length));
	
	short dataSize = (short)(packetType.Length + msgPkt.Length + msgSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(msgSize, 0, buffer, offset, msgSize.Length);
        offset += msgSize.Length;
        Array.Copy(msgPkt, 0, buffer, offset, msgPkt.Length);
        offset += msgPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short msgSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        msg = Encoding.UTF8.GetString(buffer, offset, msgSize);
        offset += msgSize;
        
	
    }
}


public class S_StartGame : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_StartGame;
    public string msg;
    public ObjectInfo objectInfo { get; set; } = new ObjectInfo();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] msgPkt = Encoding.UTF8.GetBytes(msg);
        byte[] msgSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)msgPkt.Length));
	byte[] ObjectInfoPkt = objectInfo.Serialize();
        byte[] ObjectInfoSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)ObjectInfoPkt.Length));
	
	short dataSize = (short)(packetType.Length + msgPkt.Length + msgSize.Length + ObjectInfoPkt.Length + ObjectInfoSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(msgSize, 0, buffer, offset, msgSize.Length);
        offset += msgSize.Length;
        Array.Copy(msgPkt, 0, buffer, offset, msgPkt.Length);
        offset += msgPkt.Length;

	Array.Copy(ObjectInfoSize, 0, buffer, offset, ObjectInfoSize.Length);
        offset += ObjectInfoSize.Length;
        Array.Copy(ObjectInfoPkt, 0, buffer, offset, ObjectInfoPkt.Length);
        offset += ObjectInfoPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short msgSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        msg = Encoding.UTF8.GetString(buffer, offset, msgSize);
        offset += msgSize;
        
	short objectInfoSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        ObjectInfo _objectInfo = new ObjectInfo();
        _objectInfo.DeSerialize(new ArraySegment<byte>(buffer, offset + 2, objectInfoSize - 2).ToArray());
        objectInfo = _objectInfo;
        offset += objectInfoSize;
        
	
    }
}


public class C_StartGame : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.C_StartGame;
    public string msg;
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] msgPkt = Encoding.UTF8.GetBytes(msg);
        byte[] msgSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)msgPkt.Length));
	
	short dataSize = (short)(packetType.Length + msgPkt.Length + msgSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(msgSize, 0, buffer, offset, msgSize.Length);
        offset += msgSize.Length;
        Array.Copy(msgPkt, 0, buffer, offset, msgPkt.Length);
        offset += msgPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short msgSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        msg = Encoding.UTF8.GetString(buffer, offset, msgSize);
        offset += msgSize;
        
	
    }
}


public class S_EnterGame : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_EnterGame;
    public ObjectInfo objectInfo { get; set; } = new ObjectInfo();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] ObjectInfoPkt = objectInfo.Serialize();
        byte[] ObjectInfoSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)ObjectInfoPkt.Length));
	
	short dataSize = (short)(packetType.Length + ObjectInfoPkt.Length + ObjectInfoSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(ObjectInfoSize, 0, buffer, offset, ObjectInfoSize.Length);
        offset += ObjectInfoSize.Length;
        Array.Copy(ObjectInfoPkt, 0, buffer, offset, ObjectInfoPkt.Length);
        offset += ObjectInfoPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short objectInfoSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        ObjectInfo _objectInfo = new ObjectInfo();
        _objectInfo.DeSerialize(new ArraySegment<byte>(buffer, offset + 2, objectInfoSize - 2).ToArray());
        objectInfo = _objectInfo;
        offset += objectInfoSize;
        
	
    }
}


public class C_EnterGame : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.C_EnterGame;
    public string msg;
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] msgPkt = Encoding.UTF8.GetBytes(msg);
        byte[] msgSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)msgPkt.Length));
	
	short dataSize = (short)(packetType.Length + msgPkt.Length + msgSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(msgSize, 0, buffer, offset, msgSize.Length);
        offset += msgSize.Length;
        Array.Copy(msgPkt, 0, buffer, offset, msgPkt.Length);
        offset += msgPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short msgSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        msg = Encoding.UTF8.GetString(buffer, offset, msgSize);
        offset += msgSize;
        
	
    }
}


public class S_LeaveGame : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_LeaveGame;
    public int objectId;
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] objectIdPkt = BitConverter.GetBytes(objectId);
        byte[] objectIdSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)objectIdPkt.Length));
	
	short dataSize = (short)(packetType.Length + objectIdPkt.Length + objectIdSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(objectIdSize, 0, buffer, offset, objectIdSize.Length);
        offset += objectIdSize.Length;
        Array.Copy(objectIdPkt, 0, buffer, offset, objectIdPkt.Length);
        offset += objectIdPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short objectIdSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        objectId = BitConverter.ToInt32(buffer, offset);
        offset += objectIdSize;
        
	
    }
}


public class S_Spawn : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_Spawn;
    public List<ObjectInfo> objectInfoList = new List<ObjectInfo>();
    public List<int> arrrList = new List<int>();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte [] ObjectInfoCountSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)objectInfoList.Count));
	byte [] arrrCountSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)arrrList.Count));
	
	short dataSize = (short)(packetType.Length + ObjectInfoCountSize.Length + (objectInfoList[0].typeSizeSum + 2) * objectInfoList.Count + arrrCountSize.Length + sizeof(int) * arrrList.Count);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(ObjectInfoCountSize, 0, buffer, offset, ObjectInfoCountSize.Length);
        offset += ObjectInfoCountSize.Length;

        foreach (var item in objectInfoList)
        {
            byte[] ObjectInfoPkt = item.Serialize();
            byte[] ObjectInfoSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)ObjectInfoPkt.Length));

            Array.Copy(ObjectInfoSize, 0, buffer, offset, ObjectInfoSize.Length);
            offset += ObjectInfoSize.Length;

            Array.Copy(ObjectInfoPkt, 0, buffer, offset, ObjectInfoPkt.Length);
            offset += ObjectInfoPkt.Length;
        }

	Array.Copy(arrrCountSize, 0, buffer, offset, arrrCountSize.Length);
        offset += arrrCountSize.Length;

        foreach (var item in arrrList)
        {
            byte[] arrrPkt = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)item));
            byte[] arrrSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)arrrPkt.Length));

            Array.Copy(arrrSize, 0, buffer, offset, arrrSize.Length);
            offset += arrrSize.Length;

            Array.Copy(arrrPkt, 0, buffer, offset, arrrPkt.Length);
            offset += arrrPkt.Length;
        }

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short objectInfoCountSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);

        for (int i = 0; i < objectInfoCountSize; i++)
        {
            short objectInfoSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
            offset += sizeof(short);
            ObjectInfo objectInfo = new ObjectInfo();

            objectInfo.DeSerialize(new ArraySegment<byte>(buffer, offset + 2, objectInfoSize - 2).ToArray());
            offset += objectInfoSize;
            
            objectInfoList.Add(objectInfo);
        }

        short arrrCountSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);

        for (int i = 0; i < arrrCountSize; i++)
        {
            short itemSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
            offset += sizeof(short);

            short item = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
            offset += itemSize;

            arrrList.Add(item);
        }

        
    }
}


public class S_Despawn : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_Despawn;
    public List<int> objectIdList = new List<int>();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte [] objectIdCountSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)objectIdList.Count));
	
	short dataSize = (short)(packetType.Length + objectIdCountSize.Length + sizeof(int) * objectIdList.Count);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(objectIdCountSize, 0, buffer, offset, objectIdCountSize.Length);
        offset += objectIdCountSize.Length;

        foreach (var item in objectIdList)
        {
            byte[] objectIdPkt = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)item));
            byte[] objectIdSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)objectIdPkt.Length));

            Array.Copy(objectIdSize, 0, buffer, offset, objectIdSize.Length);
            offset += objectIdSize.Length;

            Array.Copy(objectIdPkt, 0, buffer, offset, objectIdPkt.Length);
            offset += objectIdPkt.Length;
        }

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short objectIdCountSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);

        for (int i = 0; i < objectIdCountSize; i++)
        {
            short itemSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
            offset += sizeof(short);

            short item = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
            offset += itemSize;

            objectIdList.Add(item);
        }

        
    }
}


public class S_Move : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.S_Move;
    public int objectId;
    public PositionInfo positionInfo { get; set; } = new PositionInfo();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] objectIdPkt = BitConverter.GetBytes(objectId);
        byte[] objectIdSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)objectIdPkt.Length));
	byte[] PositionInfoPkt = positionInfo.Serialize();
        byte[] PositionInfoSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)PositionInfoPkt.Length));
	
	short dataSize = (short)(packetType.Length + objectIdPkt.Length + objectIdSize.Length + PositionInfoPkt.Length + PositionInfoSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(objectIdSize, 0, buffer, offset, objectIdSize.Length);
        offset += objectIdSize.Length;
        Array.Copy(objectIdPkt, 0, buffer, offset, objectIdPkt.Length);
        offset += objectIdPkt.Length;

	Array.Copy(PositionInfoSize, 0, buffer, offset, PositionInfoSize.Length);
        offset += PositionInfoSize.Length;
        Array.Copy(PositionInfoPkt, 0, buffer, offset, PositionInfoPkt.Length);
        offset += PositionInfoPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short objectIdSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        objectId = BitConverter.ToInt32(buffer, offset);
        offset += objectIdSize;
        
	short positionInfoSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        PositionInfo _positionInfo = new PositionInfo();
        _positionInfo.DeSerialize(new ArraySegment<byte>(buffer, offset + 2, positionInfoSize - 2).ToArray());
        positionInfo = _positionInfo;
        offset += positionInfoSize;
        
	
    }
}


public class C_Move : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.C_Move;
    public float speed;
    public PositionInfo positionInfo { get; set; } = new PositionInfo();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] speedPkt = BitConverter.GetBytes(speed);
        byte[] speedSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)speedPkt.Length));
	byte[] PositionInfoPkt = positionInfo.Serialize();
        byte[] PositionInfoSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)PositionInfoPkt.Length));
	
	short dataSize = (short)(packetType.Length + speedPkt.Length + speedSize.Length + PositionInfoPkt.Length + PositionInfoSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(speedSize, 0, buffer, offset, speedSize.Length);
        offset += speedSize.Length;
        Array.Copy(speedPkt, 0, buffer, offset, speedPkt.Length);
        offset += speedPkt.Length;

	Array.Copy(PositionInfoSize, 0, buffer, offset, PositionInfoSize.Length);
        offset += PositionInfoSize.Length;
        Array.Copy(PositionInfoPkt, 0, buffer, offset, PositionInfoPkt.Length);
        offset += PositionInfoPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short speedSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        speed = BitConverter.ToSingle(buffer, offset);
        offset += speedSize;
        
	short positionInfoSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        PositionInfo _positionInfo = new PositionInfo();
        _positionInfo.DeSerialize(new ArraySegment<byte>(buffer, offset + 2, positionInfoSize - 2).ToArray());
        positionInfo = _positionInfo;
        offset += positionInfoSize;
        
	
    }
}


public class PositionInfo : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.PositionInfo;
    public float posX;
    public float posY;
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] posXPkt = BitConverter.GetBytes(posX);
        byte[] posXSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)posXPkt.Length));
	byte[] posYPkt = BitConverter.GetBytes(posY);
        byte[] posYSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)posYPkt.Length));
	
	short dataSize = (short)(packetType.Length + posXPkt.Length + posXSize.Length + posYPkt.Length + posYSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(posXSize, 0, buffer, offset, posXSize.Length);
        offset += posXSize.Length;
        Array.Copy(posXPkt, 0, buffer, offset, posXPkt.Length);
        offset += posXPkt.Length;

	Array.Copy(posYSize, 0, buffer, offset, posYSize.Length);
        offset += posYSize.Length;
        Array.Copy(posYPkt, 0, buffer, offset, posYPkt.Length);
        offset += posYPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short posXSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        posX = BitConverter.ToSingle(buffer, offset);
        offset += posXSize;
        
	short posYSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        posY = BitConverter.ToSingle(buffer, offset);
        offset += posYSize;
        
	
    }
}


public class ObjectInfo : IPacket
{
    public short Protocol { get; set; } = (short)PacketType.ObjectInfo;
    public int objectId;
    public PositionInfo positionInfo { get; set; } = new PositionInfo();
    
    public int offset = 0;
    public int typeSizeSum
    {
        get
        {
            if(offset == 0)
                Serialize();
            return offset;
        }
    }

    public byte[] Serialize()
    {
        byte[] packetType = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Protocol));
        
        byte[] objectIdPkt = BitConverter.GetBytes(objectId);
        byte[] objectIdSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)objectIdPkt.Length));
	byte[] PositionInfoPkt = positionInfo.Serialize();
        byte[] PositionInfoSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)PositionInfoPkt.Length));
	
	short dataSize = (short)(packetType.Length + objectIdPkt.Length + objectIdSize.Length + PositionInfoPkt.Length + PositionInfoSize.Length);
        byte[] header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataSize));

        byte[] buffer = new byte[2 + dataSize];

        offset = 0;

        Array.Copy(header, 0, buffer, offset, header.Length);
        offset += header.Length;

        Array.Copy(packetType, 0, buffer, offset, packetType.Length);
        offset += packetType.Length;
        
        Array.Copy(objectIdSize, 0, buffer, offset, objectIdSize.Length);
        offset += objectIdSize.Length;
        Array.Copy(objectIdPkt, 0, buffer, offset, objectIdPkt.Length);
        offset += objectIdPkt.Length;

	Array.Copy(PositionInfoSize, 0, buffer, offset, PositionInfoSize.Length);
        offset += PositionInfoSize.Length;
        Array.Copy(PositionInfoPkt, 0, buffer, offset, PositionInfoPkt.Length);
        offset += PositionInfoPkt.Length;

	
        return buffer;
    }

    public void DeSerialize(byte[] buffer)
    {
        offset = 2;

        short objectIdSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        objectId = BitConverter.ToInt32(buffer, offset);
        offset += objectIdSize;
        
	short positionInfoSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, offset));
        offset += sizeof(short);
        
        PositionInfo _positionInfo = new PositionInfo();
        _positionInfo.DeSerialize(new ArraySegment<byte>(buffer, offset + 2, positionInfoSize - 2).ToArray());
        positionInfo = _positionInfo;
        offset += positionInfoSize;
        
	
    }
}


