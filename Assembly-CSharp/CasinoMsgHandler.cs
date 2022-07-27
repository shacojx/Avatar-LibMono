using System;

// Token: 0x02000087 RID: 135
public class CasinoMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000451 RID: 1105 RVA: 0x00027ABA File Offset: 0x00025EBA
	public static void onHandler()
	{
		GlobalMessageHandler.gI().miniGameMessageHandler = CasinoMsgHandler.me;
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00027ACC File Offset: 0x00025ECC
	public void onMessage(Message msg)
	{
		try
		{
			sbyte command = msg.command;
			switch (command)
			{
			case 6:
			{
				MyVector myVector = new MyVector();
				while (msg.reader().available() > 0)
				{
					myVector.addElement(new RoomInfo
					{
						id = msg.reader().readByte(),
						roomFree = msg.reader().readByte(),
						roomWait = msg.reader().readByte(),
						lv = msg.reader().readByte()
					});
				}
				RoomListOnScr.gI().setRoomList(myVector);
				RoomListOnScr.gI().switchToMe();
				Canvas.load = -1;
				if (!onMainMenu.isOngame)
				{
					onMainMenu.isOngame = true;
					onMainMenu.initSize();
					Canvas.paint.initOngame();
				}
				Canvas.endDlg();
				break;
			}
			case 7:
			{
				MyVector myVector2 = new MyVector();
				sbyte roomID = msg.reader().readByte();
				while (msg.reader().available() > 0)
				{
					BoardInfo boardInfo = new BoardInfo();
					boardInfo.boardID = msg.reader().readByte();
					int num = (int)msg.reader().readUnsignedByte();
					boardInfo.nPlayer = (sbyte)(num % 16);
					boardInfo.maxPlayer = (sbyte)(num / 16);
					int num2 = (int)msg.reader().readUnsignedByte();
					boardInfo.isPass = ((num2 & 1) != 0);
					boardInfo.isPlaying = ((num2 & 2) != 0);
					boardInfo.money = msg.reader().readInt();
					boardInfo.strMoney = Canvas.getMoneys(boardInfo.money);
					myVector2.addElement(boardInfo);
				}
				BoardListOnScr.gI().roomID = roomID;
				BoardListOnScr.gI().setBoardList(myVector2);
				BoardListOnScr.gI().setCam();
				Canvas.load = -1;
				BoardListOnScr.gI().switchToMe();
				Canvas.endDlg();
				break;
			}
			case 8:
			{
				Canvas.load = 0;
				sbyte roomID2 = msg.reader().readByte();
				sbyte boardID = msg.reader().readByte();
				int num3 = msg.reader().readInt();
				int money = msg.reader().readInt();
				MyVector myVector3 = new MyVector();
				while (msg.reader().available() > 0)
				{
					Avatar avatar = new Avatar();
					avatar.IDDB = msg.reader().readInt();
					if (avatar.IDDB == -1)
					{
						avatar.setName(string.Empty);
					}
					else
					{
						if (avatar.IDDB == GameMidlet.avatar.IDDB)
						{
							avatar = GameMidlet.avatar;
						}
						avatar.setName(msg.reader().readUTF());
						avatar.setMoneyNew(msg.reader().readInt());
						sbyte b = msg.reader().readByte();
						for (int i = 0; i < (int)b; i++)
						{
							SeriPart seri = new SeriPart(msg.reader().readShort());
							if (avatar.IDDB != GameMidlet.avatar.IDDB)
							{
								avatar.addSeri(seri);
							}
						}
						int exp = msg.reader().readInt();
						avatar.setExp(exp);
						avatar.isReady = msg.reader().readBoolean();
						avatar.setExp(exp);
						avatar.setMoneyNew(avatar.getMoneyNew());
						avatar.idImg = msg.reader().readShort();
					}
					myVector3.addElement(avatar);
				}
				CasinoMsgHandler.curScr.setPlayers(roomID2, boardID, num3, money, myVector3);
				TLBoardScr.gI().isFirstMatch = true;
				BoardScr.disableReady = false;
				int num4 = myVector3.size();
				for (int j = 0; j < num4; j++)
				{
					Avatar avatar2 = (Avatar)myVector3.elementAt(j);
					if (avatar2.IDDB == num3)
					{
						avatar2.isReady = true;
					}
					if (avatar2.IDDB == GameMidlet.avatar.IDDB)
					{
						GameMidlet.avatar.setMoneyNew(avatar2.getMoneyNew());
					}
				}
				if ((int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_2PLAYER)
				{
					CasinoMsgHandler.curScr.loadMap(60);
				}
				else if ((int)BoardListOnScr.type == (int)BoardListOnScr.STYLE_4PLAYER)
				{
					CasinoMsgHandler.curScr.loadMap(61);
				}
				else
				{
					CasinoMsgHandler.curScr.loadMap(65);
				}
				CasinoMsgHandler.curScr.switchToMe();
				TLBoardScr.gI().setMode(false);
				Canvas.endDlg();
				Canvas.load = 1;
				break;
			}
			case 9:
			{
				sbyte roomID2 = msg.reader().readByte();
				sbyte boardID = msg.reader().readByte();
				int fromID = msg.reader().readInt();
				string text = msg.reader().readUTF();
				if (BoardScr.setR_B(roomID2, boardID))
				{
					BoardScr.showChat(fromID, text);
				}
				break;
			}
			default:
				if (command != 52)
				{
					if (command != 61)
					{
						this.miniGameMessageHandler.onMessage(msg);
					}
					else
					{
						sbyte b2 = msg.reader().readByte();
						if (b2 != 21)
						{
							if (b2 != 22)
							{
								if (b2 != 3)
								{
									if (b2 != 7)
									{
										break;
									}
									PhomMsgHandler.onHandler();
								}
								else
								{
									TienLenMsgHandler.onHandler();
								}
							}
							else
							{
								BaucuaMsgHandler.onHandler();
							}
						}
						else
						{
							DiamondMessageHandler.onHandler();
						}
						CasinoService.gI().requestRoomList();
					}
				}
				else
				{
					sbyte roomID2 = msg.reader().readByte();
					sbyte boardID = msg.reader().readByte();
					int num5 = msg.reader().readInt();
					int num6 = msg.reader().readInt();
					string text2 = msg.reader().readUTF();
					Avatar avatarByID = BoardScr.getAvatarByID(num5);
					if (num6 != 0 && avatarByID != null)
					{
						avatarByID.setMoneyNew(avatarByID.getMoneyNew() + num6);
						if (GameMidlet.avatar.IDDB == num5)
						{
							GameMidlet.avatar.setMoneyNew(avatarByID.getMoneyNew());
						}
						BoardScr.showChat(num5, text2);
						BoardScr.showFlyText(num5, num6);
					}
				}
				break;
			case 11:
			{
				sbyte roomID2 = msg.reader().readByte();
				sbyte boardID = msg.reader().readByte();
				int num7 = msg.reader().readInt();
				Canvas.currentDialog = null;
				if (BoardScr.setR_B(roomID2, boardID))
				{
					if (num7 == GameMidlet.avatar.IDDB)
					{
						Canvas.startOK(T.youAreKicked, new CasinoMsgHandler.IActionKick());
					}
					else
					{
						BoardScr.me.playerLeave(num7);
					}
				}
				break;
			}
			case 12:
			{
				Avatar avatar3 = new Avatar();
				int seat = (int)msg.reader().readByte();
				avatar3.IDDB = msg.reader().readInt();
				avatar3.setName(msg.reader().readUTF());
				avatar3.setMoneyNew(msg.reader().readInt());
				sbyte b3 = msg.reader().readByte();
				for (int k = 0; k < (int)b3; k++)
				{
					avatar3.addSeri(new SeriPart(msg.reader().readShort()));
				}
				avatar3.direct = Base.RIGHT;
				avatar3.setExp(msg.reader().readInt());
				avatar3.idImg = msg.reader().readShort();
				avatar3.isReady = false;
				TLBoardScr.gI().isFirstMatch = true;
				avatar3.isReady = false;
				CasinoMsgHandler.curScr.setAt(seat, avatar3);
				break;
			}
			case 14:
			{
				int leaveID = msg.reader().readInt();
				int owner = msg.reader().readInt();
				if (BoardScr.isStartGame && BoardScr.numPlayer == 2)
				{
					CasinoMsgHandler.curScr.closeBoard(T.opponentQuit);
				}
				TLBoardScr.gI().isFirstMatch = true;
				BoardScr.me.playerLeave(leaveID);
				BoardScr.setOwner(owner);
				break;
			}
			case 16:
			{
				int num8 = msg.reader().readInt();
				bool isReady = msg.reader().readBoolean();
				if (num8 == GameMidlet.avatar.IDDB)
				{
					Canvas.endDlg();
				}
				CasinoMsgHandler.curScr.setReady(num8, isReady);
				break;
			}
			case 19:
			{
				sbyte roomID2 = msg.reader().readByte();
				sbyte boardID = msg.reader().readByte();
				int money2 = msg.reader().readInt();
				if (BoardScr.setR_B(roomID2, boardID))
				{
					CasinoMsgHandler.curScr.setMoney(money2);
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x04000769 RID: 1897
	public static CasinoMsgHandler me = new CasinoMsgHandler();

	// Token: 0x0400076A RID: 1898
	public IMiniGameMsgHandler miniGameMessageHandler;

	// Token: 0x0400076B RID: 1899
	public static BoardScr curScr;

	// Token: 0x02000088 RID: 136
	private class IActionKick : IAction
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00028358 File Offset: 0x00026758
		public void perform()
		{
			Canvas.startWaitDlg();
			CasinoService.gI().requestBoardList(BoardScr.roomID);
		}
	}
}
