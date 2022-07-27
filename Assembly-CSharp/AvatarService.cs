using System;
using System.IO;

// Token: 0x02000084 RID: 132
public class AvatarService
{
	// Token: 0x06000430 RID: 1072 RVA: 0x00026F98 File Offset: 0x00025398
	public static AvatarService gI()
	{
		if (AvatarService.instance == null)
		{
			AvatarService.instance = new AvatarService();
		}
		return AvatarService.instance;
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00026FB3 File Offset: 0x000253B3
	public void send(Message m)
	{
		Session_ME.gI().sendMessage(m);
		m.cleanup();
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00026FC8 File Offset: 0x000253C8
	public void getBigData()
	{
		Message m = new Message(-11);
		this.send(m);
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00026FE4 File Offset: 0x000253E4
	public void getBigImage(short id)
	{
		Message message = new Message(-14);
		try
		{
			message.writer().writeShort(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		Canvas.startWaitDlg(T.getData);
		MsgDlg.isBack = false;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00027040 File Offset: 0x00025440
	public void getImageData()
	{
		Message m = new Message(-15);
		this.send(m);
		MsgDlg.isBack = false;
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00027064 File Offset: 0x00025464
	public void getAvatarPart()
	{
		Message m = new Message(-16);
		this.send(m);
		MsgDlg.isBack = false;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00027088 File Offset: 0x00025488
	public void getItemInfo()
	{
		Message m = new Message(-37);
		this.send(m);
		MsgDlg.isBack = false;
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x000270AC File Offset: 0x000254AC
	public void getMapItemType()
	{
		Message m = new Message(-40);
		this.send(m);
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x000270C8 File Offset: 0x000254C8
	public void getMapItem()
	{
		Message m = new Message(-41);
		this.send(m);
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x000270E4 File Offset: 0x000254E4
	public void doFeel(int focusFeel)
	{
		if (GameMidlet.CLIENT_TYPE != 9 && GameMidlet.CLIENT_TYPE != 11)
		{
			return;
		}
		Message message = new Message(57);
		try
		{
			message.writer().writeByte(focusFeel);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00027148 File Offset: 0x00025548
	public void doBuyItem(int part, int typeBuy)
	{
		Message message = new Message(-24);
		try
		{
			message.writer().writeShort((short)part);
			message.writer().writeByte((sbyte)typeBuy);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x000271A0 File Offset: 0x000255A0
	public void doGetTileInfo()
	{
		Message m = new Message(-43);
		this.send(m);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x000271BC File Offset: 0x000255BC
	public void doCreateHome(short[] map, int type)
	{
		Message message = new Message(-46);
		try
		{
			message.writer().writeShort((short)type);
			message.writer().writeShort((short)map.Length);
			for (int i = 0; i < map.Length; i++)
			{
				message.writer().writeByte((sbyte)map[i]);
			}
			message.writer().writeShort(0);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00027244 File Offset: 0x00025644
	public void doBuyItemHouse(MapItem map)
	{
		Message message = new Message(-74);
		try
		{
			message.writer().writeShort(map.typeID);
			message.writer().writeByte((sbyte)(map.x / 24));
			message.writer().writeByte((sbyte)(map.y / 24));
			message.writer().writeByte((sbyte)map.type);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x000272D0 File Offset: 0x000256D0
	public void doJoinHouse(int iddb)
	{
		Canvas.startWaitDlg();
		Message message = new Message(-65);
		try
		{
			message.writer().writeInt(iddb);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00027320 File Offset: 0x00025720
	public void dodelItem(MapItem pos)
	{
		Message message = new Message(-66);
		try
		{
			message.writer().writeShort(pos.typeID);
			message.writer().writeByte((sbyte)(pos.x / 24));
			message.writer().writeByte((sbyte)(pos.y / 24));
			message.writer().writeByte(pos.dir);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x000273AC File Offset: 0x000257AC
	public void getTypeHouse(int type)
	{
		Message message = new Message(-67);
		try
		{
			message.writer().writeByte(type);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x000273F8 File Offset: 0x000257F8
	public void doKickOutHome(int iddb)
	{
		Message message = new Message(-69);
		try
		{
			message.writer().writeInt(iddb);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00027444 File Offset: 0x00025844
	public void doRequestExpicePet(int idUser)
	{
		Message message = new Message(-70);
		try
		{
			message.writer().writeInt(idUser);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00027490 File Offset: 0x00025890
	public void doSortItem(int anchor, int x, int y, int x2, int y2, int dir)
	{
		Message message = new Message(-71);
		try
		{
			message.writer().writeShort((short)anchor);
			message.writer().writeByte((sbyte)x);
			message.writer().writeByte((sbyte)y);
			message.writer().writeByte((sbyte)x2);
			message.writer().writeByte((sbyte)y2);
			message.writer().writeByte((sbyte)dir);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00027520 File Offset: 0x00025920
	public void doGetTileMap()
	{
		Message m = new Message(-73);
		this.send(m);
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0002753C File Offset: 0x0002593C
	public void doSetPassMyHouse(string text, int idUser, int type)
	{
		Message message = new Message(-75);
		try
		{
			message.writer().writeByte((sbyte)type);
			message.writer().writeUTF(text);
			if (type == 1)
			{
				message.writer().writeInt(idUser);
			}
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x000275A8 File Offset: 0x000259A8
	public void doSMSServerLoad(string link)
	{
		Message message = new Message(-76);
		try
		{
			message.writer().writeUTF(link);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x000275F4 File Offset: 0x000259F4
	public void doGetImgIcon(short id)
	{
		Message message = new Message(-80);
		try
		{
			message.writer().writeShort(id);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00027640 File Offset: 0x00025A40
	public void doRequestEffectData(short id3)
	{
		Message message = new Message(-84);
		try
		{
			message.writer().writeByte((int)id3);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0002768C File Offset: 0x00025A8C
	public void doJoinHouse4(int id)
	{
		Message message = new Message(-104);
		try
		{
			message.writer().writeInt(id);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x04000765 RID: 1893
	protected static AvatarService instance;
}
