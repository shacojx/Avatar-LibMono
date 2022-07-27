using System;

// Token: 0x0200008A RID: 138
public class DiamondMessageHandler : IMiniGameMsgHandler
{
	// Token: 0x0600047C RID: 1148 RVA: 0x0002922C File Offset: 0x0002762C
	public static void onHandler()
	{
		BoardScr.numPlayer = 2;
		BoardListOnScr.type = BoardListOnScr.STYLE_2PLAYER;
		if (onMainMenu.isOngame)
		{
			RoomListOnScr.setName(2, DiamondScr.gI());
		}
		else
		{
			RoomListOnScr.setName((Canvas.iOpenOngame != 0) ? 0 : 2, DiamondScr.gI());
		}
		CasinoMsgHandler.me.miniGameMessageHandler = DiamondMessageHandler.instance;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0002928D File Offset: 0x0002768D
	public void onConnectOK()
	{
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0002928F File Offset: 0x0002768F
	public void onConnectionFail()
	{
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00029291 File Offset: 0x00027691
	public void onDisconnected()
	{
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00029294 File Offset: 0x00027694
	public void onMessage(Message msg)
	{
		Out.println("msg: " + msg.command);
		sbyte roomID = msg.reader().readByte();
		sbyte boardID = msg.reader().readByte();
		if (!BoardScr.setR_B(roomID, boardID))
		{
			return;
		}
		sbyte command = msg.command;
		switch (command)
		{
		case 20:
		{
			sbyte b = msg.reader().readByte();
			int whoMoveFirst = msg.reader().readInt();
			sbyte[][] array = new sbyte[8][];
			for (int i = 0; i < 8; i++)
			{
				array[i] = new sbyte[8];
			}
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					array[j][k] = msg.reader().readByte();
				}
			}
			for (int l = 0; l < 2; l++)
			{
				int id = msg.reader().readInt();
				Avatar avatarByID = BoardScr.getAvatarByID(id);
				avatarByID.defence = msg.reader().readShort();
				avatarByID.plusHP = (avatarByID.plusMP = 0);
				avatarByID.hp = (avatarByID.maxHP = msg.reader().readShort());
				avatarByID.mp = msg.reader().readShort();
				avatarByID.maxMP = msg.reader().readShort();
				avatarByID.v *= 2;
				avatarByID.setFeel(4);
			}
			DiamondScr.gI().start(whoMoveFirst, (int)b, array);
			break;
		}
		case 21:
		{
			int whoMove = msg.reader().readInt();
			sbyte b2 = msg.reader().readByte();
			sbyte b3 = msg.reader().readByte();
			DiamondScr.gI().move(whoMove, (int)b2, (int)b3);
			break;
		}
		default:
			switch (command)
			{
			case 49:
			{
				int whoMove2 = msg.reader().readInt();
				DiamondScr.gI().onSkip(whoMove2);
				break;
			}
			default:
				if (command != 64)
				{
					if (command == 71)
					{
						sbyte[][] array2 = new sbyte[8][];
						for (int m = 0; m < 8; m++)
						{
							array2[m] = new sbyte[8];
						}
						for (int n = 0; n < 8; n++)
						{
							for (int num = 0; num < 8; num++)
							{
								array2[n][num] = msg.reader().readByte();
							}
						}
						DiamondScr.gI().onData(array2);
					}
				}
				else
				{
					sbyte b4 = msg.reader().readByte();
					sbyte[] array3 = new sbyte[(int)b4];
					AvPosition[] array4 = new AvPosition[(int)b4];
					for (int num2 = 0; num2 < (int)b4; num2++)
					{
						array4[num2] = new AvPosition();
						array3[num2] = msg.reader().readByte();
						array4[num2].anchor = (int)msg.reader().readByte();
						array4[num2].depth = msg.reader().readByte();
					}
					sbyte countCombo = msg.reader().readByte();
					sbyte b5 = msg.reader().readByte();
					MyVector myVector = new MyVector();
					for (int num3 = 0; num3 < (int)b5; num3++)
					{
						string o = msg.reader().readUTF();
						myVector.addElement(o);
					}
					for (int num4 = 0; num4 < 2; num4++)
					{
						int id2 = msg.reader().readInt();
						Avatar avatarByID2 = BoardScr.getAvatarByID(id2);
						avatarByID2.fight = msg.reader().readByte();
						avatarByID2.defence = msg.reader().readShort();
						avatarByID2.plusHP = msg.reader().readShort() - avatarByID2.hp;
						avatarByID2.plusMP = msg.reader().readShort() - avatarByID2.mp;
						avatarByID2.isNo = msg.reader().readBoolean();
						if (avatarByID2.isNo)
						{
							DiamondScr.gI().isNo = true;
						}
					}
					DiamondScr.gI().onCreateCell(array3, array4, countCombo, myVector);
				}
				break;
			case 51:
			{
				MyVector myVector2 = new MyVector();
				for (int num5 = 0; num5 < 2; num5++)
				{
					int id3 = msg.reader().readInt();
					int num6 = msg.reader().readInt();
					Avatar avatarByID3 = BoardScr.getAvatarByID(id3);
					avatarByID3.v /= 2;
					avatarByID3.action = 0;
					avatarByID3.setMoneyNew(avatarByID3.getMoneyNew() + num6);
					if (num6 != 0)
					{
						Canvas.addFlyText(num6, avatarByID3.x, avatarByID3.y, -1, 30);
						string text = avatarByID3.name + ": ";
						if (num6 > 0)
						{
							DiamondScr.gI().idWin = avatarByID3.IDDB;
							string text2 = text;
							text = string.Concat(new object[]
							{
								text2,
								T.win,
								"   +",
								num6,
								T.xu
							});
						}
						else
						{
							string text2 = text;
							text = string.Concat(new object[]
							{
								text2,
								T.lose,
								"  ",
								num6,
								T.xu
							});
						}
						myVector2.addElement("  ");
						myVector2.addElement(text);
					}
				}
				DiamondScr.gI().onFinish(myVector2);
				break;
			}
			}
			break;
		case 24:
		{
			int whoMove3 = msg.reader().readInt();
			sbyte[][] array5 = new sbyte[8][];
			for (int num7 = 0; num7 < 8; num7++)
			{
				array5[num7] = new sbyte[8];
			}
			for (int num8 = 0; num8 < 8; num8++)
			{
				for (int num9 = 0; num9 < 8; num9++)
				{
					array5[num8][num9] = msg.reader().readByte();
				}
			}
			DiamondScr.gI().onOutPath(whoMove3, array5);
			break;
		}
		}
	}

	// Token: 0x0400076D RID: 1901
	private static DiamondMessageHandler instance = new DiamondMessageHandler();
}
