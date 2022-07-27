using System;

// Token: 0x020000A8 RID: 168
public class ParkMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000549 RID: 1353 RVA: 0x00031C64 File Offset: 0x00030064
	public static void onHandler()
	{
		if (ParkMsgHandler.instance == null)
		{
			ParkMsgHandler.instance = new ParkMsgHandler();
		}
		GlobalMessageHandler.gI().miniGameMessageHandler = ParkMsgHandler.instance;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00031C8C File Offset: 0x0003008C
	public void onMessage(Message msg)
	{
		try
		{
			sbyte command = msg.command;
			switch (command)
			{
			case 82:
			{
				int idUser = msg.reader().readInt();
				FishingScr.gI().onQuanCau(idUser);
				break;
			}
			default:
				switch (command)
				{
				case 51:
					ParkMsgHandler.playerJoinBoard(msg);
					break;
				default:
					if (command != -69)
					{
						if (command != -68)
						{
							if (command != 78)
							{
							}
						}
						else
						{
							sbyte type = msg.reader().readByte();
							int idUser2 = msg.reader().readInt();
							MapScr.gI().onInviteToMyHome(type, idUser2);
						}
					}
					else
					{
						Canvas.startOK("Bạn bị chủ nhà đuổi.", new ParkMsgHandler.IActionKickOutHome());
					}
					break;
				case 53:
				{
					int id = msg.reader().readInt();
					MapScr.gI().onPlayerLeave(id);
					break;
				}
				case 54:
					GlobalMessageHandler.readMove(msg);
					break;
				case 55:
					GlobalMessageHandler.readChat(msg);
					break;
				case 57:
				{
					int idUser3 = msg.reader().readInt();
					sbyte idFeel = msg.reader().readByte();
					MapScr.gI().onFeel(idUser3, idFeel);
					break;
				}
				case 58:
				{
					int idFrom = msg.reader().readInt();
					int idTo = msg.reader().readInt();
					int num = (int)msg.reader().readShort();
					string des = string.Empty;
					if (num == -1)
					{
						des = msg.reader().readUTF();
					}
					int curMoney = msg.reader().readInt();
					int typeBuy = (int)msg.reader().readByte();
					int xu = msg.reader().readInt();
					int luong = msg.reader().readInt();
					int luongK = msg.reader().readInt();
					MapScr.gI().onGiftGiving(idFrom, idTo, num, des, curMoney, typeBuy, xu, luong, luongK);
					break;
				}
				case 59:
				{
					int idFrom2 = msg.reader().readInt();
					int idTo2 = msg.reader().readInt();
					int num2 = (int)msg.reader().readShort();
					string text = string.Empty;
					int time = 0;
					if (num2 == -1)
					{
						text = msg.reader().readUTF();
					}
					else
					{
						time = (int)msg.reader().readShort();
					}
					MapScr.gI().onGivingDefferent(idFrom2, idTo2, num2, text, time);
					break;
				}
				case 60:
				{
					sbyte b = msg.reader().readByte();
					int[] array = new int[(int)b];
					for (int i = 0; i < (int)b; i++)
					{
						array[i] = (int)msg.reader().readByte();
					}
					MapScr.gI().onParkList(array);
					Canvas.endDlg();
					break;
				}
				}
				break;
			case 84:
			{
				int idUser4 = msg.reader().readInt();
				int idFish = (int)msg.reader().readShort();
				FishingScr.gI().onFinish(idUser4, idFish);
				break;
			}
			case 85:
			{
				int idUser5 = msg.reader().readInt();
				FishingScr.gI().onCauCaXong(idUser5);
				break;
			}
			case 86:
			{
				bool flag = msg.reader().readBoolean();
				string error = string.Empty;
				if (!flag)
				{
					error = msg.reader().readUTF();
				}
				FishingScr.gI().onStartFishing(flag, error);
				break;
			}
			case 87:
			{
				int idUser6 = msg.reader().readInt();
				int status = (int)msg.reader().readByte();
				FishingScr.gI().onStatus(idUser6, status);
				break;
			}
			case 88:
			{
				int idUser7 = msg.reader().readInt();
				sbyte lv = msg.reader().readByte();
				sbyte perLv = msg.reader().readByte();
				int numFish = msg.reader().readInt();
				short idPart = msg.reader().readShort();
				FishingScr.gI().onInfo(idUser7, lv, perLv, numFish, idPart);
				break;
			}
			case 91:
			{
				int idUser8 = msg.reader().readInt();
				int idFish2 = (int)msg.reader().readShort();
				short timeDelay = msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				sbyte[][] array2 = new sbyte[(int)b2][];
				for (int j = 0; j < (int)b2; j++)
				{
					short num3 = msg.reader().readShort();
					array2[j] = new sbyte[(int)num3];
					msg.reader().read(ref array2[j]);
				}
				FishingScr.gI().onCaCanCau(idUser8, idFish2, timeDelay, array2);
				break;
			}
			case 92:
			{
				bool flag2 = msg.reader().readBoolean();
				if (flag2)
				{
					short num4 = msg.reader().readShort();
				}
				break;
			}
			case 93:
			{
				int userIDBoy = msg.reader().readInt();
				int userIDGirl = msg.reader().readInt();
				MapScr.gI().onWeddingStart(userIDBoy, userIDGirl);
				break;
			}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00032194 File Offset: 0x00030594
	public static void playerJoinBoard(Message msg)
	{
		Avatar avatar = new Avatar();
		avatar.IDDB = msg.reader().readInt();
		avatar.setName(msg.reader().readUTF());
		sbyte b = msg.reader().readByte();
		for (int i = 0; i < (int)b; i++)
		{
			avatar.addSeri(new SeriPart(msg.reader().readShort()));
		}
		avatar.x = (int)msg.reader().readShort();
		avatar.y = (int)msg.reader().readShort();
		avatar.blogNews = msg.reader().readByte();
		avatar.hungerPet = (short)((sbyte)(100 - (int)msg.reader().readByte()));
		avatar.idImg = msg.reader().readShort();
		avatar.idWedding = msg.reader().readShort();
		MapScr.gI().onPlayerJoinPark(avatar);
	}

	// Token: 0x0400078C RID: 1932
	public static ParkMsgHandler instance;

	// Token: 0x020000A9 RID: 169
	private class IActionKickOutHome : IAction
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0003227D File Offset: 0x0003067D
		public void perform()
		{
			ParkService.gI().doJoinPark(21, 0);
		}
	}
}
