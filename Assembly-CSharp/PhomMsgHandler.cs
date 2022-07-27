using System;

// Token: 0x020000AB RID: 171
public class PhomMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000566 RID: 1382 RVA: 0x0003298C File Offset: 0x00030D8C
	public static void onHandler()
	{
		BoardScr.numPlayer = 4;
		BoardListOnScr.type = BoardListOnScr.STYLE_4PLAYER;
		RoomListOnScr.title = T.nameCasinoOngame[1];
		CasinoMsgHandler.curScr = PBoardScr.gI();
		CasinoMsgHandler.me.miniGameMessageHandler = PhomMsgHandler.instance;
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x000329C4 File Offset: 0x00030DC4
	public void onMessage(Message msg)
	{
		try
		{
			sbyte roomID = msg.reader().readByte();
			sbyte boardID = msg.reader().readByte();
			if (BoardScr.setR_B(roomID, boardID))
			{
				sbyte command = msg.command;
				switch (command)
				{
				case 62:
				{
					int interval = (int)msg.reader().readByte();
					int curPlayer = msg.reader().readInt();
					int firPlayer = msg.reader().readInt();
					int firHa = msg.reader().readInt();
					int[][] array = new int[4][];
					int[][] array2 = new int[4][];
					for (int i = 0; i < 4; i++)
					{
						array[i] = new int[4];
						array2[i] = new int[3];
					}
					for (int j = 0; j < 4; j++)
					{
						for (int k = 0; k < 4; k++)
						{
							array[j][k] = -1;
							if (k < 3)
							{
								array2[j][k] = -1;
							}
						}
					}
					int num = 0;
					int num2 = 0;
					for (int l = 0; l < 4; l++)
					{
						for (int m = 0; m < 3; m++)
						{
							int num3 = (int)msg.reader().readByte();
							if (num3 == -2)
							{
								break;
							}
							if (num3 == -1)
							{
								break;
							}
							array2[l][m] = num3;
						}
					}
					while (msg.reader().available() > 0)
					{
						int num4 = (int)msg.reader().readByte();
						if (num < 3 && num4 == -1)
						{
							num++;
							num2 = 0;
						}
						else
						{
							array[num][num2] = num4;
							if (num2 < 3)
							{
								num2++;
							}
						}
					}
					PBoardScr.gI().onPlaying(interval, curPlayer, firPlayer, array, array2, firHa);
					break;
				}
				case 63:
				{
					int cardGet = (int)msg.reader().readByte();
					Canvas.endDlg();
					PBoardScr.gI().onGetCard(cardGet);
					break;
				}
				case 64:
				{
					int firHa2 = msg.reader().readInt();
					bool flag = msg.reader().readBoolean();
					int numEat = 0;
					if (flag)
					{
						numEat = (int)msg.reader().readByte();
					}
					sbyte nCard = msg.reader().readByte();
					Canvas.endDlg();
					PBoardScr.gI().onEatCard(flag, numEat, firHa2, nCard);
					break;
				}
				case 65:
				{
					int curID = msg.reader().readInt();
					bool flag2 = msg.reader().readBoolean();
					bool isU = msg.reader().readBoolean();
					int[] array3 = new int[4];
					int[] array4 = new int[12];
					if (flag2)
					{
						for (int n = 0; n < 4; n++)
						{
							array3[n] = msg.reader().readInt();
						}
						for (int num5 = 0; num5 < 12; num5++)
						{
							array4[num5] = -1;
						}
						int num6 = 0;
						while (msg.reader().available() > 0)
						{
							array4[num6] = (int)msg.reader().readByte();
							num6++;
						}
					}
					Canvas.endDlg();
					PBoardScr.gI().onHaPhom(flag2, array4, isU, array3, curID);
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
							{
								int curPlayer2 = msg.reader().readInt();
								int firHa3 = msg.reader().readInt();
								PBoardScr.gI().onSkipPlayer(curPlayer2, firHa3);
								break;
							}
							case 51:
							{
								int[] array5 = new int[4];
								for (int num7 = 0; num7 < 4; num7++)
								{
									array5[num7] = msg.reader().readInt();
								}
								int[] array6 = new int[4];
								for (int num8 = 0; num8 < 4; num8++)
								{
									array6[num8] = msg.reader().readInt();
								}
								int[][] array7 = new int[4][];
								for (int num9 = 0; num9 < 4; num9++)
								{
									array7[num9] = new int[11];
								}
								for (int num10 = 0; num10 < 4; num10++)
								{
									for (int num11 = 0; num11 < 11; num11++)
									{
										array7[num10][num11] = -1;
									}
								}
								int num12 = 0;
								int num13 = 0;
								while (msg.reader().available() > 0)
								{
									sbyte b = msg.reader().readByte();
									if ((int)b == -1)
									{
										if (num12 < 3)
										{
											num12++;
										}
										num13 = 0;
									}
									else
									{
										array7[num12][num13] = (int)b;
										if (num13 < 10)
										{
											num13++;
										}
									}
								}
								Canvas.endDlg();
								PBoardScr.gI().onFinish(array6, array7, array5);
								break;
							}
							}
						}
						else
						{
							int currentP = msg.reader().readInt();
							int firstP = msg.reader().readInt();
							int num14 = (int)msg.reader().readByte();
							sbyte numberCard = 0;
							if (num14 != -1)
							{
								numberCard = msg.reader().readByte();
							}
							Canvas.endDlg();
							PBoardScr.gI().onFire(currentP, firstP, num14, numberCard);
						}
					}
					else
					{
						sbyte interval2 = msg.reader().readByte();
						MyVector myVector = new MyVector();
						for (int num15 = 0; num15 < 9; num15++)
						{
							sbyte id = msg.reader().readByte();
							myVector.addElement(new Card(id));
						}
						int firstMove = msg.reader().readInt();
						int firHa4 = msg.reader().readInt();
						Canvas.endDlg();
						BoardScr.me.resetReady();
						PBoardScr.gI().start(interval2, myVector, firstMove, firHa4);
					}
					break;
				case 67:
				{
					bool flag3 = msg.reader().readBoolean();
					sbyte card = -1;
					if (flag3)
					{
						card = msg.reader().readByte();
					}
					Canvas.endDlg();
					PBoardScr.gI().onAddCardToPhom(flag3, card);
					break;
				}
				case 68:
				{
					sbyte isReset = msg.reader().readByte();
					Canvas.endDlg();
					PBoardScr.gI().onResetPhomEat(isReset);
					break;
				}
				case 69:
				{
					int winID = msg.reader().readInt();
					int[] array8 = new int[4];
					for (int num16 = 0; num16 < 4; num16++)
					{
						array8[num16] = msg.reader().readInt();
					}
					PBoardScr.gI().onDenBai(winID, array8);
					break;
				}
				case 70:
				{
					int orderMoney = msg.reader().readInt();
					PBoardScr.gI().onOnceWin(orderMoney);
					break;
				}
				}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0400078E RID: 1934
	public static PhomMsgHandler instance = new PhomMsgHandler();
}
