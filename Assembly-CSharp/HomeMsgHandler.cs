using System;

// Token: 0x020000A6 RID: 166
public class HomeMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000543 RID: 1347 RVA: 0x000317D8 File Offset: 0x0002FBD8
	public static void onHandler()
	{
		GlobalMessageHandler.gI().miniGameMessageHandler = HomeMsgHandler.instance;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x000317EC File Offset: 0x0002FBEC
	public void onMessage(Message msg)
	{
		try
		{
			sbyte command = msg.command;
			switch (command + 67)
			{
			case 0:
			{
				int num = (int)msg.reader().readByte();
				int houseType = -1;
				short verTile = 0;
				MyVector myVector = null;
				if (num == 0)
				{
					verTile = msg.reader().readShort();
					houseType = (int)msg.reader().readByte();
				}
				else
				{
					myVector = new MyVector();
					short num2 = msg.reader().readShort();
					for (int i = 0; i < (int)num2; i++)
					{
						myVector.addElement(new Avatar
						{
							IDDB = msg.reader().readInt(),
							typeHome = msg.reader().readByte()
						});
					}
				}
				HouseScr.gI().onGetTypeHouse(num, houseType, verTile, myVector);
				break;
			}
			case 1:
			{
				MapItem mapItem = new MapItem();
				mapItem.typeID = msg.reader().readShort();
				mapItem.x = (int)msg.reader().readByte();
				mapItem.y = (int)msg.reader().readByte();
				HouseScr.gI().onRemoveItem(mapItem);
				break;
			}
			case 2:
			{
				sbyte houseType2 = msg.reader().readByte();
				int idUser = msg.reader().readInt();
				short num3 = msg.reader().readShort();
				short[] array = new short[(int)num3];
				for (int j = 0; j < (int)num3; j++)
				{
					array[j] = (short)msg.reader().readByte();
				}
				sbyte w = msg.reader().readByte();
				MyVector myVector2 = new MyVector();
				short num4 = msg.reader().readShort();
				for (int k = 0; k < (int)num4; k++)
				{
					MapItem mapItem2 = new MapItem();
					mapItem2.typeID = msg.reader().readShort();
					mapItem2.x = (mapItem2.xTo = (int)msg.reader().readByte() * 24);
					mapItem2.y = (mapItem2.yTo = (int)msg.reader().readByte() * 24);
					mapItem2.dir = msg.reader().readByte();
					myVector2.addElement(mapItem2);
				}
				MyVector listPlayer = GlobalMessageHandler.readListPlayer(msg);
				ParkMsgHandler.onHandler();
				HouseScr.gI().onJoin(houseType2, idUser, array, w, myVector2, listPlayer);
				break;
			}
			default:
				switch (command)
				{
				case 51:
					ParkMsgHandler.playerJoinBoard(msg);
					break;
				default:
					switch (command + 75)
					{
					case 0:
					{
						int idUser2 = msg.reader().readInt();
						ipKeyboard.openKeyBoard(T.setPass, ipKeyboard.PASS, string.Empty, new HomeMsgHandler.IActionSetPass(idUser2), false);
						break;
					}
					default:
						switch (command + 46)
						{
						case 0:
						{
							short type = msg.reader().readShort();
							string str = msg.reader().readUTF();
							HouseScr.gI().onCreateHome(type, str);
							break;
						}
						default:
							if (command != 76)
							{
								if (command == 77)
								{
									GlobalMessageHandler.readChat(msg);
								}
							}
							else
							{
								GlobalMessageHandler.readMove(msg);
							}
							break;
						case 3:
						{
							short num5 = msg.reader().readShort();
							Tile[] array2 = new Tile[(int)num5];
							for (int l = 0; l < (int)num5; l++)
							{
								array2[l] = new Tile();
								array2[l].name = msg.reader().readUTF();
								array2[l].priceXu = msg.reader().readInt();
								array2[l].priceLuong = msg.reader().readInt();
							}
							HouseScr.gI().onGetTileInfo(array2);
							break;
						}
						}
						break;
					case 2:
					{
						int wNum = (int)msg.reader().readShort();
						int num6 = msg.reader().readInt();
						sbyte[] array3 = new sbyte[num6];
						msg.reader().read(ref array3);
						HouseScr.gI().saveTileMap(array3, wNum);
						break;
					}
					}
					break;
				case 54:
					GlobalMessageHandler.readMove(msg);
					break;
				case 55:
					GlobalMessageHandler.readChat(msg);
					break;
				}
				break;
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0400078A RID: 1930
	private static HomeMsgHandler instance = new HomeMsgHandler();

	// Token: 0x020000A7 RID: 167
	private class IActionSetPass : IKbAction
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00031C34 File Offset: 0x00030034
		public IActionSetPass(int idUser)
		{
			this.idUser5 = idUser;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00031C43 File Offset: 0x00030043
		public void perform(string text)
		{
			AvatarService.gI().doSetPassMyHouse(text, this.idUser5, 1);
			Canvas.endDlg();
		}

		// Token: 0x0400078B RID: 1931
		private int idUser5;
	}
}
