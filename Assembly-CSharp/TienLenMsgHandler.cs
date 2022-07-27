using System;

// Token: 0x020000B1 RID: 177
public class TienLenMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000580 RID: 1408 RVA: 0x000349DE File Offset: 0x00032DDE
	public static void onHandler()
	{
		BoardScr.numPlayer = 4;
		BoardListOnScr.type = BoardListOnScr.STYLE_4PLAYER;
		RoomListOnScr.title = T.nameCasinoOngame[0];
		CasinoMsgHandler.curScr = TLBoardScr.gI();
		CasinoMsgHandler.me.miniGameMessageHandler = TienLenMsgHandler.instance;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00034A18 File Offset: 0x00032E18
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
				case 49:
				{
					int whoSkip = msg.reader().readInt();
					int nextMove = msg.reader().readInt();
					bool isClearCurrentCards = msg.reader().readBoolean();
					TLBoardScr.gI().skip(whoSkip, nextMove, isClearCurrentCards);
					break;
				}
				case 50:
					TLBoardScr.gI().isFirstMatch = false;
					TLBoardScr.gI().stopGame();
					if (msg.reader().available() > 0)
					{
						int whoShow = msg.reader().readInt();
						sbyte b = msg.reader().readByte();
						sbyte[] array = new sbyte[(int)b];
						for (int i = 0; i < (int)b; i++)
						{
							array[i] = msg.reader().readByte();
						}
						TLBoardScr.gI().showCards(whoShow, array);
					}
					break;
				case 51:
				{
					int whoFinish = msg.reader().readInt();
					sbyte finishPosition = msg.reader().readByte();
					int dMoney = msg.reader().readInt();
					int dExp = msg.reader().readInt();
					TLBoardScr.gI().finish(whoFinish, finishPosition, dMoney, dExp);
					break;
				}
				default:
					if (command != 20)
					{
						if (command == 21)
						{
							int whoMove = msg.reader().readInt();
							sbyte b2 = msg.reader().readByte();
							sbyte[] array2 = new sbyte[(int)b2];
							for (int j = 0; j < (int)b2; j++)
							{
								array2[j] = msg.reader().readByte();
							}
							int nextMove2 = msg.reader().readInt();
							BoardScr.disableReady = true;
							TLBoardScr.gI().move(whoMove, array2, nextMove2);
							BoardScr.me.setPosPlaying();
						}
					}
					else
					{
						sbyte interval = msg.reader().readByte();
						MyVector myVector = new MyVector();
						for (int k = 0; k < 13; k++)
						{
							myVector.addElement(new Card(msg.reader().readByte()));
						}
						int whoMoveFirst = msg.reader().readInt();
						Canvas.endDlg();
						BoardScr.me.resetReady();
						TLBoardScr.gI().start(whoMoveFirst, interval, myVector);
						CasinoService.gI().forceFinish();
					}
					break;
				case 53:
				{
					int num = msg.reader().readInt();
					sbyte[] array3 = new sbyte[13];
					try
					{
						for (int l = 0; l < 13; l++)
						{
							array3[l] = msg.reader().readByte();
						}
					}
					catch (Exception e)
					{
						Out.logError(e);
						array3 = null;
					}
					Canvas.endDlg();
					TLBoardScr.gI().stopGame();
					if (array3 != null)
					{
						TLBoardScr.gI().showCards(num, array3);
					}
					BoardScr.showChat(num, T.forceFinish);
					break;
				}
				case 54:
				{
					string info = msg.reader().readUTF();
					TLBoardScr.gI().moveError(info);
					break;
				}
				}
			}
		}
		catch (Exception e2)
		{
			Out.logError(e2);
		}
	}

	// Token: 0x040007BA RID: 1978
	public static TienLenMsgHandler instance = new TienLenMsgHandler();
}
