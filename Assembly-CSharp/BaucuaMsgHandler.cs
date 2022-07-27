using System;

// Token: 0x02000085 RID: 133
public class BaucuaMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x0600044B RID: 1099 RVA: 0x000276E0 File Offset: 0x00025AE0
	public static void init()
	{
		BaucuaMsgHandler.me = new BaucuaMsgHandler();
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x000276EC File Offset: 0x00025AEC
	public static void onHandler()
	{
		BoardScr.numPlayer = 5;
		BoardListOnScr.type = BoardListOnScr.STYLE_5PLAYER;
		if (onMainMenu.isOngame)
		{
			RoomListOnScr.setName(3, BCBoardScr.gI());
		}
		else
		{
			RoomListOnScr.setName((Canvas.iOpenOngame != 0) ? 1 : 3, BCBoardScr.gI());
		}
		if (BaucuaMsgHandler.me == null)
		{
			BaucuaMsgHandler.me = new BaucuaMsgHandler();
		}
		CasinoMsgHandler.me.miniGameMessageHandler = BaucuaMsgHandler.me;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00027764 File Offset: 0x00025B64
	public void onMessage(Message msg)
	{
		sbyte b = msg.reader().readByte();
		sbyte b2 = msg.reader().readByte();
		if (!BoardScr.setR_B(b, b2))
		{
			return;
		}
		sbyte command = msg.command;
		switch (command)
		{
		case 60:
		{
			sbyte fromUse = msg.reader().readByte();
			sbyte toUse = msg.reader().readByte();
			int moneyValue = msg.reader().readInt();
			BCBoardScr.me.onSetPlayer(fromUse, toUse, moneyValue);
			break;
		}
		default:
			if (command != 20)
			{
				if (command != 21)
				{
					switch (command)
					{
					case 49:
						break;
					default:
						if (command != 37)
						{
							if (command == 100)
							{
								sbyte seatI = msg.reader().readByte();
								BCBoardScr.me.onSetTurn(seatI);
							}
						}
						else
						{
							sbyte[] array = new sbyte[3];
							for (int i = 0; i < 3; i++)
							{
								array[i] = msg.reader().readByte();
							}
							BCBoardScr.me.onResult(array);
							BCBoardScr.me.setCmdWaiting();
						}
						break;
					case 51:
					{
						int[] array2 = new int[BoardScr.avatarInfos.size()];
						for (int j = 0; j < array2.Length; j++)
						{
							array2[j] = msg.reader().readInt();
						}
						BCBoardScr.me.onFinish(array2);
						break;
					}
					}
				}
				else
				{
					sbyte b3 = msg.reader().readByte();
					if ((int)b3 == -1)
					{
						BCBoardScr.me.setBotCmd();
						BCBoardScr.me.canpointer = false;
					}
					else if ((int)b3 != -1)
					{
						for (int k = 0; k < 6; k++)
						{
							BCBoardScr.me.moneySV[(int)b3][k] = msg.reader().readByte();
						}
						BCBoardScr.me.onMove(b3);
					}
				}
			}
			else
			{
				sbyte b4 = msg.reader().readByte();
				BCBoardScr.me.saveTime = b4;
				BCBoardScr.me.onStartGame(b, b2, b4);
			}
			break;
		case 62:
		{
			sbyte b4 = msg.reader().readByte();
			BCBoardScr.me.saveTime = b4;
			for (int l = 0; l < BoardScr.avatarInfos.size(); l++)
			{
				for (int m = 0; m < 6; m++)
				{
					BCBoardScr.me.moneySV[l][m] = msg.reader().readByte();
				}
			}
			BCBoardScr.me.onPlaying(b4);
			break;
		}
		case 65:
		{
			sbyte b5 = msg.reader().readByte();
			sbyte b6 = msg.reader().readByte();
			sbyte b7 = msg.reader().readByte();
			sbyte b8 = msg.reader().readByte();
			if ((int)b6 != (int)b7 && (int)b8 > 0)
			{
				BCBoardScr.me.moneySV[(int)b5][(int)b7] = b8;
				BCBoardScr.me.onHaphom(b5, b6, b7);
			}
			break;
		}
		}
	}

	// Token: 0x04000766 RID: 1894
	public static BaucuaMsgHandler me;
}
