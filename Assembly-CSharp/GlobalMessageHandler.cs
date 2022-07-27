using System;
using System.IO;

// Token: 0x020000A1 RID: 161
public class GlobalMessageHandler : IMessageHandler
{
	// Token: 0x060004EC RID: 1260 RVA: 0x0002C457 File Offset: 0x0002A857
	public static GlobalMessageHandler gI()
	{
		if (GlobalMessageHandler.me == null)
		{
			GlobalMessageHandler.me = new GlobalMessageHandler();
		}
		return GlobalMessageHandler.me;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0002C472 File Offset: 0x0002A872
	public void onConnectOK()
	{
		this.globalLogicHandler.onConnectOK();
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0002C47F File Offset: 0x0002A87F
	public void onConnectionFail()
	{
		this.globalLogicHandler.onConnectFail();
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0002C48C File Offset: 0x0002A88C
	public void onDisconnected()
	{
		GlobalLogicHandler.onDisconnect();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0002C494 File Offset: 0x0002A894
	public void onMessage(Message msg)
	{
		try
		{
			sbyte command = msg.command;
			switch (command + 107)
			{
			case 0:
			{
				sbyte b = msg.reader().readByte();
				string nameShop = null;
				string[] array = null;
				string[] array2 = null;
				string[] array3 = null;
				short[] array4 = null;
				short[] array5 = null;
				short[] array6 = null;
				int[] array7 = null;
				if ((int)b == 0)
				{
					nameShop = msg.reader().readUTF();
					short num = msg.reader().readShort();
					array = new string[(int)num];
					array4 = new short[(int)num];
					array2 = new string[(int)num];
					array3 = new string[(int)num];
					array5 = new short[(int)num];
					array6 = new short[(int)num];
					for (int i = 0; i < (int)num; i++)
					{
						array5[i] = msg.reader().readShort();
						array4[i] = msg.reader().readShort();
						array6[i] = msg.reader().readShort();
						array[i] = msg.reader().readUTF();
						array2[i] = msg.reader().readUTF();
						array3[i] = msg.reader().readUTF();
					}
				}
				else if ((int)b == 1)
				{
					nameShop = msg.reader().readUTF();
					short num2 = msg.reader().readShort();
					array5 = new short[(int)num2];
					array = new string[(int)num2];
					array4 = new short[(int)num2];
					array7 = new int[(int)num2];
					array3 = new string[(int)num2];
					array6 = new short[(int)num2];
					array2 = new string[(int)num2];
					for (int j = 0; j < (int)num2; j++)
					{
						array5[j] = msg.reader().readShort();
						array[j] = msg.reader().readUTF();
						array2[j] = msg.reader().readUTF();
						array4[j] = msg.reader().readShort();
						array6[j] = msg.reader().readShort();
						array7[j] = msg.reader().readInt();
						array3[j] = msg.reader().readUTF();
					}
				}
				HouseScr.gI().onOpenShop(b, nameShop, array, array4, array5, array2, array3, array7, array6);
				return;
			}
			case 1:
			{
				sbyte b2 = msg.reader().readByte();
				Out.println("CREATE_CHAR_INFO: " + b2);
				if ((int)b2 == 0)
				{
					Out.println("CREATE_CHAR_INFOaaaaaaaaa: " + msg.reader().available());
					bool isTrue = msg.reader().readBoolean();
					RegisterInfoScr.isTrue = isTrue;
					RegisterInfoScr.isCreate = true;
				}
				else if ((int)b2 == 1)
				{
					RegisterInfoScr.gI().onCreate();
				}
				else if ((int)b2 == 3)
				{
					Canvas.isPaint18 = true;
				}
				break;
			}
			case 2:
			{
				sbyte b3 = msg.reader().readByte();
				MyVector myVector = new MyVector();
				for (int k = 0; k < (int)b3; k++)
				{
					short idImg = msg.reader().readShort();
					string caption = msg.reader().readUTF();
					Command o = new GlobalMessageHandler.CommandFlower(caption, new GlobalMessageHandler.IActionFlower((sbyte)k), idImg);
					myVector.addElement(o);
				}
				Canvas.endDlg();
				FarmScr.gI().startMenuFarm(myVector);
				break;
			}
			default:
				if (command == 34)
				{
					int num3 = msg.reader().readInt();
					if (num3 != -1)
					{
						string text = msg.reader().readUTF();
						int num4 = msg.reader().readInt();
						msg.reader().readShort();
						int exp = msg.reader().readInt();
						int num5 = msg.reader().readInt();
						int num6 = msg.reader().readInt();
						int num7 = msg.reader().readInt();
						int num8 = msg.reader().readInt();
						Avatar avatar = new Avatar();
						avatar.setExp(exp);
						string info = string.Concat(new object[]
						{
							T.nameStr,
							text,
							". ",
							T.moneyStr,
							num4,
							"$. Level: ",
							avatar.lvMain,
							"+",
							avatar.perLvMain,
							"%. ",
							T.win,
							": ",
							num5,
							". ",
							T.lose,
							": ",
							num6,
							". ",
							T.draw,
							": ",
							num7,
							". ",
							T.give,
							": ",
							num8
						});
						Canvas.startOKDlg(info);
					}
					return;
				}
				if (command != 50)
				{
					if (command == 89)
					{
						sbyte b4 = msg.reader().readByte();
						if ((int)b4 == 0)
						{
							sbyte typeDrop = msg.reader().readByte();
							short idDrop = msg.reader().readShort();
							int id = msg.reader().readInt();
							int idPlayer = msg.reader().readInt();
							short xDrop = msg.reader().readShort();
							short yDrop = msg.reader().readShort();
							MapScr.gI().onDropPark(typeDrop, idPlayer, idDrop, id, xDrop, yDrop);
						}
						else
						{
							int id2 = msg.reader().readInt();
							int idUser = msg.reader().readInt();
							MapScr.gI().onGetPart(id2, idUser);
						}
						return;
					}
					if (command == 122)
					{
						Out.println("DICH_CHUYEN");
						sbyte b5 = msg.reader().readByte();
						sbyte roomID = msg.reader().readByte();
						sbyte boardID = msg.reader().readByte();
						int xTe = (int)msg.reader().readShort();
						int yTe = (int)msg.reader().readShort();
						Canvas.loadMap.onDichChuyen(roomID, boardID, xTe, yTe);
						return;
					}
				}
				else if (this.miniGameMessageHandler is FarmMsgHandler || this.miniGameMessageHandler is ParkMsgHandler || this.miniGameMessageHandler is HomeMsgHandler)
				{
					sbyte roomID2 = msg.reader().readByte();
					sbyte b6 = msg.reader().readByte();
					if ((int)b6 == -1)
					{
						Canvas.startOKDlg(msg.reader().readUTF());
						return;
					}
					short x = 0;
					short y = 0;
					MyVector myVector2 = new MyVector();
					if ((int)b6 != -1 && (int)b6 != -2)
					{
						x = msg.reader().readShort();
						y = msg.reader().readShort();
						myVector2 = GlobalMessageHandler.readListPlayer(msg);
					}
					short num9 = msg.reader().readShort();
					MyVector mapItemType = null;
					MyVector mapItem = null;
					if (num9 > 0)
					{
						mapItemType = GlobalMessageHandler.readMapItemType(msg);
						mapItem = GlobalMessageHandler.readMapItem(msg);
					}
					if (GameMidlet.CLIENT_TYPE == 9)
					{
						for (int l = 0; l < myVector2.size(); l++)
						{
							Avatar avatar2 = (Avatar)myVector2.elementAt(l);
							avatar2.idWedding = msg.reader().readShort();
						}
					}
					MapScr.gI().onJoinPark(roomID2, b6, x, y, myVector2, mapItemType, mapItem);
					if (LoadMap.TYPEMAP == 21)
					{
						Canvas.load = 0;
						HomeMsgHandler.onHandler();
						AvatarService.gI().getTypeHouse(0);
						Canvas.startWaitDlg();
					}
				}
				break;
			case 4:
			{
				int id3 = msg.reader().readInt();
				Avatar avatar3 = LoadMap.getAvatar(id3);
				sbyte b7 = msg.reader().readByte();
				if ((int)b7 == 0)
				{
					avatar3.idImg = msg.reader().readShort();
				}
				else
				{
					avatar3.idWedding = msg.reader().readShort();
				}
				break;
			}
			case 5:
			{
				int id4 = msg.reader().readInt();
				int num10 = msg.reader().readInt();
				Avatar avatar4;
				if (onMainMenu.isOngame)
				{
					avatar4 = BoardScr.getAvatarByID(id4);
				}
				else
				{
					avatar4 = LoadMap.getAvatar(id4);
				}
				if (avatar4 != null)
				{
					avatar4.money[3] = num10;
				}
				break;
			}
			case 6:
			{
				sbyte b8 = msg.reader().readByte();
				short num11 = msg.reader().readShort();
				if ((int)b8 == 1)
				{
					StringObj stringObj = new StringObj();
					stringObj.anthor = (int)num11;
					stringObj.str = msg.reader().readUTF();
					stringObj.dis = (int)msg.reader().readShort();
					stringObj.type = (int)msg.reader().readByte();
					MapScr.listCmdRotate.addElement(stringObj);
					if (Canvas.currentMyScreen == PopupShop.gI())
					{
						PopupShop.gI().close();
					}
					if (LoadMap.focusObj != null)
					{
						MainMenu.gI().doExchange();
					}
				}
				else
				{
					for (int m = 0; m < MapScr.listCmdRotate.size(); m++)
					{
						StringObj stringObj2 = (StringObj)MapScr.listCmdRotate.elementAt(m);
						if (stringObj2.anthor == (int)num11)
						{
							MapScr.listCmdRotate.removeElementAt(m);
							break;
						}
					}
				}
				break;
			}
			case 8:
			{
				sbyte idMap = msg.reader().readByte();
				sbyte b9 = msg.reader().readByte();
				MyVector myVector3 = new MyVector();
				for (int n = 0; n < (int)b9; n++)
				{
					Avatar avatar5 = new Avatar();
					avatar5.IDDB = msg.reader().readInt();
					avatar5.setName(msg.reader().readUTF());
					sbyte b10 = msg.reader().readByte();
					for (int num12 = 0; num12 < (int)b10; num12++)
					{
						avatar5.addSeri(new SeriPart(msg.reader().readShort()));
					}
					avatar5.x = (int)msg.reader().readShort();
					avatar5.y = (int)msg.reader().readShort();
					avatar5.blogNews = msg.reader().readByte();
					avatar5.hungerPet = (short)((sbyte)(100 - (int)msg.reader().readByte()));
					avatar5.idImg = msg.reader().readShort();
					sbyte b11 = msg.reader().readByte();
					avatar5.textChat = new string[(int)b11];
					for (int num13 = 0; num13 < (int)b11; num13++)
					{
						avatar5.textChat[num13] = msg.reader().readUTF();
					}
					myVector3.addElement(avatar5);
				}
				short num14 = msg.reader().readShort();
				MyVector mapItemType2 = null;
				MyVector mapItem2 = null;
				if (num14 > 0)
				{
					mapItemType2 = GlobalMessageHandler.readMapItemType(msg);
					mapItem2 = GlobalMessageHandler.readMapItem(msg);
				}
				MapScr.gI().onJoinOfflineMap(idMap, myVector3, mapItemType2, mapItem2);
				break;
			}
			case 9:
			{
				short num15 = msg.reader().readShort();
				short num16 = msg.reader().readShort();
				sbyte[] data = new sbyte[(int)num16];
				msg.reader().read(ref data);
				AvatarData.listImgPart.put(string.Empty + num15, new ImageIcon(CRes.createImgByByteArray(ArrayCast.cast(data))));
				return;
			}
			case 10:
			{
				sbyte[] array8 = new sbyte[msg.reader().available()];
				msg.reader().read(ref array8);
				MyVector myVector4 = AvatarData.readAvatarPart(array8, true);
				Part part = (Part)myVector4.elementAt(0);
				AvatarData.listPartDynamic.put(string.Empty + part.IDPart, part);
				return;
			}
			case 11:
				Canvas.endDlg();
				MapScr.gI().move();
				OnSplashScr.gI().switchToMe();
				OnSplashScr.gI().splashScrStat = 0;
				return;
			case 13:
			{
				sbyte idTileMap = msg.reader().readByte();
				sbyte[] arr = new sbyte[msg.reader().available()];
				msg.reader().read(ref arr);
				Canvas.loadMap.onTileImg(idTileMap, arr);
				return;
			}
			case 14:
			{
				sbyte idMap2 = msg.reader().readByte();
				sbyte idTileImg = msg.reader().readByte();
				short num17 = msg.reader().readShort();
				sbyte wMap = msg.reader().readByte();
				int num18 = (int)msg.reader().readShort();
				sbyte[] map = new sbyte[num18];
				msg.reader().read(ref map);
				short[] array9 = null;
				sbyte b12 = msg.reader().readByte();
				if ((int)b12 > 0)
				{
					array9 = new short[(int)b12];
					for (int num19 = 0; num19 < (int)b12; num19++)
					{
						array9[num19] = msg.reader().readShort();
					}
				}
				short num20 = msg.reader().readShort();
				Image img = null;
				if (num20 > 0)
				{
					sbyte[] data2 = new sbyte[(int)num20];
					msg.reader().read(ref data2);
					img = CRes.createImgByByteArray(ArrayCast.cast(data2));
				}
				short num21 = msg.reader().readShort();
				MyVector myVector5 = null;
				MyVector myVector6 = null;
				if (num21 > 0)
				{
					sbyte b13 = msg.reader().readByte();
					myVector5 = new MyVector();
					for (int num22 = 0; num22 < (int)b13; num22++)
					{
						MapItemType mapItemType3 = new MapItemType();
						mapItemType3.idType = (short)msg.reader().readByte();
						mapItemType3.imgID = msg.reader().readShort();
						mapItemType3.iconID = (short)msg.reader().readByte();
						mapItemType3.dx = msg.reader().readShort();
						mapItemType3.dy = msg.reader().readShort();
						sbyte b14 = msg.reader().readByte();
						mapItemType3.listNotTrans = new MyVector();
						for (int num23 = 0; num23 < (int)b14; num23++)
						{
							AvPosition avPosition = new AvPosition();
							avPosition.x = (int)msg.reader().readByte();
							avPosition.y = (int)msg.reader().readByte();
							mapItemType3.listNotTrans.addElement(avPosition);
						}
						myVector5.addElement(mapItemType3);
					}
					sbyte b15 = msg.reader().readByte();
					myVector6 = new MyVector();
					for (int num24 = 0; num24 < (int)b15; num24++)
					{
						myVector6.addElement(new MapItem
						{
							type = (int)msg.reader().readByte(),
							typeID = (short)msg.reader().readByte(),
							x = (int)msg.reader().readByte(),
							y = (int)msg.reader().readByte(),
							isGetImg = true
						});
					}
				}
				MapScr.gI().onSelectedMiniMap(map, idMap2, idTileImg, wMap, img, array9, myVector5, myVector6);
				return;
			}
			case 15:
			{
				sbyte id5 = msg.reader().readByte();
				int num25 = msg.reader().readInt();
				sbyte[] data3 = new sbyte[num25];
				msg.reader().read(ref data3);
				int num26 = msg.reader().readInt();
				sbyte wMn = msg.reader().readByte();
				sbyte[] array10 = new sbyte[num26];
				for (int num27 = 0; num27 < num26; num27++)
				{
					array10[num27] = msg.reader().readByte();
				}
				sbyte b16 = msg.reader().readByte();
				MyVector myVector7 = new MyVector();
				for (int num28 = 0; num28 < (int)b16; num28++)
				{
					myVector7.addElement(new PositionMap
					{
						id = msg.reader().readByte(),
						idImg = msg.reader().readShort(),
						text = msg.reader().readUTF(),
						x = (int)msg.reader().readByte() * 16 * AvMain.hd,
						y = (int)msg.reader().readByte() * 16 * AvMain.hd
					});
				}
				MiniMap.isCityMap = false;
				MiniMap.isChange = true;
				MiniMap.gI().setInfo(new FrameImage(CRes.createImgByByteArray(ArrayCast.cast(data3)), 16 * AvMain.hd, 16 * AvMain.hd), array10, myVector7, wMn, 16 * AvMain.hd, new Command(T.selectRegion, new GlobalMessageHandler.IActionMiniMap(id5)));
				MiniMap.gI().switchToMe(MapScr.instance);
				LoadMap.TYPEMAP = -1;
				LoadMap.typeAny = -108;
				LoadMap.typeTemp = -1;
				return;
			}
			case 17:
			{
				sbyte index = msg.reader().readByte();
				string str = msg.reader().readUTF();
				this.globalLogicHandler.onUpdateCHest(1, index, str);
				return;
			}
			case 18:
				HouseScr.gI().onTransChestPart(msg.reader().readBoolean(), msg.reader().readUTF());
				return;
			case 19:
				HouseScr.gI().onEnterPass();
				return;
			case 20:
			{
				short num29 = msg.reader().readShort();
				MyVector myVector8 = new MyVector();
				for (int num30 = 0; num30 < (int)num29; num30++)
				{
					myVector8.addElement(new SeriPart
					{
						idPart = msg.reader().readShort(),
						time = msg.reader().readByte(),
						expireString = msg.reader().readUTF()
					});
				}
				int moneyOnChest = msg.reader().readInt();
				sbyte levelChest = msg.reader().readByte();
				short num31 = msg.reader().readShort();
				MyVector myVector9 = new MyVector();
				for (int num32 = 0; num32 < (int)num31; num32++)
				{
					myVector9.addElement(new SeriPart
					{
						idPart = msg.reader().readShort(),
						time = msg.reader().readByte(),
						expireString = msg.reader().readUTF()
					});
				}
				HouseScr.gI().onCustomChest(myVector8, myVector9, moneyOnChest, levelChest);
				return;
			}
			case 22:
			{
				int idUser2 = msg.reader().readInt();
				sbyte b17 = msg.reader().readByte();
				MyVector myVector10 = new MyVector();
				for (int num33 = 0; num33 < (int)b17; num33++)
				{
					myVector10.addElement(new Emotion
					{
						type = msg.reader().readByte(),
						id = msg.reader().readShort(),
						time = msg.reader().readShort()
					});
				}
				MapScr.gI().onEmotionList(idUser2, myVector10);
				return;
			}
			case 23:
			{
				sbyte b18 = msg.reader().readByte();
				short num34 = (short)msg.reader().readByte();
				Out.println("EFFECT_OBJ: " + num34);
				if (num34 == 5 || num34 == 2)
				{
					return;
				}
				if ((int)b18 == 0)
				{
					if (AvatarData.getEffect(num34) == null)
					{
						AvatarService.gI().doRequestEffectData(num34);
					}
					EffectManager effectManager = new EffectManager();
					effectManager.ID = num34;
					effectManager.style = msg.reader().readByte();
					effectManager.loopLimit = (effectManager.count = (short)msg.reader().readByte());
					if ((int)effectManager.style == 4)
					{
						int num35 = (int)msg.reader().readShort();
						sbyte b19 = msg.reader().readByte();
						if (Canvas.currentEffect.size() > 0)
						{
							for (int num36 = 0; num36 < Canvas.currentEffect.size(); num36++)
							{
								Effect effect = (Effect)Canvas.currentEffect.elementAt(num36);
								if (effect.IDAction == num34)
								{
									return;
								}
							}
						}
						new AnimateEffect(2, true, num35)
						{
							timeStop = (int)b19,
							IDAction = num34
						}.show();
					}
					else
					{
						effectManager.loop = msg.reader().readShort();
						effectManager.loopType = msg.reader().readByte();
						if ((int)effectManager.loopType == 1)
						{
							effectManager.radius = msg.reader().readShort();
						}
						else if ((int)effectManager.loopType == 2)
						{
							sbyte b20 = msg.reader().readByte();
							effectManager.xLoop = new short[(int)b20];
							effectManager.yLoop = new short[(int)b20];
							for (int num37 = 0; num37 < (int)b20; num37++)
							{
								effectManager.xLoop[num37] = msg.reader().readShort();
								effectManager.yLoop[num37] = msg.reader().readShort();
							}
						}
						if ((int)effectManager.style == 0)
						{
							effectManager.idPlayer = msg.reader().readInt();
						}
						else
						{
							effectManager.x = msg.reader().readShort();
							effectManager.y = msg.reader().readShort();
						}
						MapScr.gI().onEffect(effectManager);
					}
				}
				else
				{
					EffectData effectData = new EffectData();
					effectData.ID = num34;
					short num38 = msg.reader().readShort();
					sbyte[] data4 = new sbyte[(int)num38];
					msg.reader().read(ref data4);
					effectData.img = CRes.createImgByByteArray(ArrayCast.cast(data4));
					sbyte b21 = msg.reader().readByte();
					effectData.imgImfo = new ImageInfo[(int)b21];
					for (int num39 = 0; num39 < (int)b21; num39++)
					{
						effectData.imgImfo[num39] = new ImageInfo();
						effectData.imgImfo[num39].ID = (short)msg.reader().readByte();
						effectData.imgImfo[num39].x0 = (short)msg.reader().readByte();
						effectData.imgImfo[num39].y0 = (short)msg.reader().readByte();
						effectData.imgImfo[num39].w = (short)msg.reader().readByte();
						effectData.imgImfo[num39].h = (short)msg.reader().readByte();
					}
					sbyte b22 = msg.reader().readByte();
					effectData.frame = new Frame[(int)b22];
					for (int num40 = 0; num40 < (int)b22; num40++)
					{
						effectData.frame[num40] = new Frame();
						sbyte b23 = msg.reader().readByte();
						effectData.frame[num40].dx = new short[(int)b23];
						effectData.frame[num40].dy = new short[(int)b23];
						effectData.frame[num40].idImg = new sbyte[(int)b23];
						for (int num41 = 0; num41 < (int)b23; num41++)
						{
							effectData.frame[num40].dx[num41] = (short)msg.reader().readByte();
							effectData.frame[num40].dy[num41] = (short)msg.reader().readByte();
							effectData.frame[num40].idImg[num41] = msg.reader().readByte();
						}
					}
					sbyte b24 = msg.reader().readByte();
					effectData.arrFrame = new sbyte[(int)b24];
					for (int num42 = 0; num42 < (int)b24; num42++)
					{
						effectData.arrFrame[num42] = msg.reader().readByte();
					}
					AvatarData.effectList.addElement(effectData);
				}
				return;
			}
			case 24:
			{
				sbyte b25 = msg.reader().readByte();
				MyVector myVector11 = new MyVector();
				for (int num43 = 0; num43 < (int)b25; num43++)
				{
					myVector11.addElement(new StringObj
					{
						anthor = (int)msg.reader().readShort(),
						str = msg.reader().readUTF(),
						dis = (int)msg.reader().readShort()
					});
				}
				MapScr.gI().onMenuRotate(myVector11);
				return;
			}
			case 25:
			{
				int idUser3 = msg.reader().readInt();
				short idImg2 = msg.reader().readShort();
				MapScr.gI().onChangeClan(idUser3, idImg2);
				return;
			}
			case 26:
			{
				string text2 = msg.reader().readUTF();
				int num44 = 0;
				for (int num45 = 0; num45 < text2.Length; num45++)
				{
					if (text2[num45] == '-')
					{
						num44++;
					}
				}
				sbyte[] array11 = new sbyte[msg.reader().available()];
				msg.reader().read(ref array11);
				if (num44 == 2 || text2.Equals(ListScr.idFriendList))
				{
					ListScr.hList.put(text2, array11);
					ListScr.gI().setList(text2);
				}
				else
				{
					ListScr.gI().readList(array11, text2);
					Canvas.endDlg();
				}
				return;
			}
			case 27:
			{
				short num46 = msg.reader().readShort();
				short num47 = msg.reader().readShort();
				sbyte[] data5 = new sbyte[(int)num47];
				msg.reader().read(ref data5);
				AvatarData.listImgIcon.put(string.Empty + num46, new ImageIcon(CRes.createImgByByteArray(ArrayCast.cast(data5))));
				return;
			}
			case 29:
			{
				sbyte b26 = msg.reader().readByte();
				int idBoss = msg.reader().readInt();
				short idShop = (short)msg.reader().readByte();
				string nameShop2 = msg.reader().readUTF();
				short num48 = msg.reader().readShort();
				if (num48 > 0)
				{
					short[] array12 = new short[(int)num48];
					string[] array13 = new string[(int)num48];
					string[] array14 = null;
					if ((int)b26 == 1)
					{
						array14 = new string[(int)num48];
					}
					for (int num49 = 0; num49 < (int)num48; num49++)
					{
						array12[num49] = msg.reader().readShort();
						array13[num49] = msg.reader().readUTF();
						if ((int)b26 == 1)
						{
							array14[num49] = msg.reader().readUTF();
						}
					}
					MapScr.gI().onOpenShop(b26, (int)idShop, nameShop2, array12, idBoss, array13, array14);
				}
				return;
			}
			case 30:
			{
				int idBoss2 = msg.reader().readInt();
				sbyte b27 = msg.reader().readByte();
				string text3 = msg.reader().readUTF();
				sbyte b28 = msg.reader().readByte();
				string[] array15 = new string[(int)b28];
				for (int num50 = 0; num50 < (int)b28; num50++)
				{
					array15[num50] = msg.reader().readUTF();
				}
				if (PopupShop.me != Canvas.currentMyScreen)
				{
					MapScr.gI().onCustomPopup(idBoss2, (int)b27, text3, array15);
				}
				return;
			}
			case 33:
			{
				MapItem mapItem3 = new MapItem();
				mapItem3.typeID = msg.reader().readShort();
				mapItem3.x = (mapItem3.xTo = 24 * (int)msg.reader().readByte());
				mapItem3.y = (mapItem3.yTo = 24 * (int)msg.reader().readByte());
				HouseScr.gI().onBuyItemHouse(mapItem3);
				return;
			}
			case 37:
			{
				int idUser4 = msg.reader().readInt();
				sbyte expice = (sbyte)(100 - (int)msg.reader().readByte());
				MapScr.gI().onRequestExpicePet(idUser4, expice);
				return;
			}
			case 43:
			{
				int idUser5 = msg.reader().readInt();
				int degree = (int)msg.reader().readShort();
				sbyte b29 = msg.reader().readByte();
				MyVector myVector12 = new MyVector();
				for (int num51 = 0; num51 < (int)b29; num51++)
				{
					Gift gift = new Gift();
					gift.type = msg.reader().readByte();
					switch (gift.type)
					{
					case 1:
					{
						gift.idPart = msg.reader().readShort();
						sbyte b30 = msg.reader().readByte();
						if ((int)b30 == -1)
						{
							gift.expire = "(" + T.forever + ")";
						}
						else
						{
							gift.expire = string.Concat(new object[]
							{
								"(",
								b30,
								" ",
								T.day,
								")"
							});
						}
						break;
					}
					case 2:
						gift.xu = msg.reader().readInt();
						break;
					case 3:
						gift.xp = msg.reader().readInt();
						break;
					case 4:
						gift.luong = msg.reader().readInt();
						break;
					}
					myVector12.addElement(gift);
				}
				DialLuckyScr.gI().onStart(idUser5, degree, myVector12);
				return;
			}
			case 44:
			{
				sbyte b31 = msg.reader().readByte();
				Out.println("WEATHER: " + b31);
				LoadMap.onWeather(b31);
				return;
			}
			case 45:
				Out.println("CHANGE_PASS");
				LoginScr.gI().tfPass.setText(msg.reader().readUTF());
				LoginScr.gI().saveLogin();
				break;
			case 47:
			{
				int userID = msg.reader().readInt();
				sbyte idMenu = msg.reader().readByte();
				string nameText = msg.reader().readUTF();
				int typeInput = (int)msg.reader().readByte();
				sbyte[] array16 = null;
				if (msg.reader().available() > 0)
				{
					short num52 = msg.reader().readShort();
					array16 = new sbyte[msg.reader().available()];
					msg.reader().read(ref array16);
				}
				this.globalLogicHandler.onTextBox(userID, idMenu, nameText, typeInput);
				if (array16 != null)
				{
					Canvas.inputDlg.setImg(CRes.createImgByByteArray(ArrayCast.cast(array16)));
				}
				return;
			}
			case 48:
			{
				if (Canvas.currentDialog == Canvas.msgdlg)
				{
					Canvas.currentDialog = null;
				}
				if (Canvas.currentDialog != null)
				{
					return;
				}
				int userID2 = msg.reader().readInt();
				sbyte idmenu = msg.reader().readByte();
				int num53 = (int)msg.reader().readByte();
				string[] array17 = new string[num53];
				short[] array18 = new short[num53];
				for (int num54 = 0; num54 < num53; num54++)
				{
					array17[num54] = msg.reader().readUTF();
				}
				if (msg.reader().available() > 0)
				{
					for (int num55 = 0; num55 < num53; num55++)
					{
						array18[num55] = msg.reader().readShort();
					}
				}
				string nameNPC = null;
				string textChat = null;
				bool[] array19 = null;
				if (msg.reader().available() > 0)
				{
					nameNPC = msg.reader().readUTF();
					textChat = msg.reader().readUTF();
					array19 = new bool[num53];
					for (int num56 = 0; num56 < num53; num56++)
					{
						array19[num56] = msg.reader().readBoolean();
					}
				}
				this.globalLogicHandler.onMenuOption(userID2, idmenu, array17, array18, nameNPC, textChat, array19);
				return;
			}
			case 49:
			{
				int num57 = (int)msg.reader().readByte();
				MyHashTable myHashTable = new MyHashTable();
				for (int num58 = 0; num58 < num57; num58++)
				{
					int num59 = (int)msg.reader().readShort();
					int num60 = (int)msg.reader().readShort();
					sbyte[] data6 = new sbyte[num60];
					msg.reader().read(ref data6);
					Image v = CRes.createImgByByteArray(ArrayCast.cast(data6));
					data6 = null;
					myHashTable.put(string.Empty + num59, v);
				}
				string title = msg.reader().readUTF();
				string str2 = msg.reader().readUTF();
				sbyte idAction = -1;
				if (msg.reader().available() > 0)
				{
					idAction = msg.reader().readByte();
				}
				CustomTab.me = null;
				CustomTab.gI().setInfo(myHashTable, title, str2, idAction);
				CustomTab.gI().show();
				return;
			}
			case 53:
			{
				string info2 = msg.reader().readUTF();
				string syst = msg.reader().readUTF();
				string number = msg.reader().readUTF();
				this.globalLogicHandler.onSms(info2, syst, number);
				break;
			}
			case 54:
			{
				sbyte index2 = msg.reader().readByte();
				string str3 = msg.reader().readUTF();
				this.globalLogicHandler.onUpdateContainer(index2, str3);
				return;
			}
			case 55:
			{
				string numSup = msg.reader().readUTF();
				msg.reader().readInt();
				LoginScr.gI().onNumSupport(numSup);
				return;
			}
			case 56:
			{
				Out.println("SOUND_DATA");
				sbyte b32 = msg.reader().readByte();
				sbyte[] array20 = new sbyte[msg.reader().available()];
				msg.reader().read(ref array20);
				byte[] dst = new byte[array20.Length];
				Buffer.BlockCopy(array20, 0, dst, 0, array20.Length);
				return;
			}
			case 57:
			{
				Out.println("REQUEST_SOUND");
				string text4 = msg.reader().readUTF();
				sbyte b33 = msg.reader().readByte();
				return;
			}
			case 58:
			{
				short idShop2 = (short)msg.reader().readByte();
				string nameShop3 = msg.reader().readUTF();
				short[] array21 = null;
				short num61 = msg.reader().readShort();
				if (num61 > 0)
				{
					array21 = new short[(int)num61];
					for (int num62 = 0; num62 < (int)num61; num62++)
					{
						array21[num62] = msg.reader().readShort();
					}
				}
				MapScr.gI().onOpenShop(0, (int)idShop2, nameShop3, array21, -1, null, null);
				return;
			}
			case 59:
			{
				int idUser6 = msg.reader().readInt();
				short idP = msg.reader().readShort();
				MapScr.gI().onUsingPart(idUser6, idP);
				return;
			}
			case 60:
			{
				MyVector myVector13 = new MyVector();
				short num63 = msg.reader().readShort();
				for (int num64 = 0; num64 < (int)num63; num64++)
				{
					myVector13.addElement(new SeriPart
					{
						idPart = msg.reader().readShort(),
						time = msg.reader().readByte(),
						expireString = msg.reader().readUTF()
					});
				}
				MapScr.gI().onContainer(myVector13);
				return;
			}
			case 65:
			{
				MyVector myVector14 = new MyVector();
				int num65 = (int)msg.reader().readByte();
				for (int num66 = 0; num66 < num65; num66++)
				{
					ObjAd objAd = new ObjAd();
					objAd.id = (int)msg.reader().readShort();
					objAd.title = msg.reader().readUTF();
					objAd.text = msg.reader().readUTF();
					objAd.url = msg.reader().readUTF();
					objAd.sms = msg.reader().readUTF();
					objAd.to = msg.reader().readUTF();
					objAd.listPoint = new MyVector();
					int num67 = (int)msg.reader().readByte();
					for (int num68 = 0; num68 < num67; num68++)
					{
						AvPosition avPosition2 = new AvPosition();
						avPosition2.anchor = (int)msg.reader().readByte();
						avPosition2.x = (int)msg.reader().readByte();
						avPosition2.y = (int)msg.reader().readByte();
						objAd.listPoint.addElement(avPosition2);
					}
					myVector14.addElement(objAd);
				}
				for (int num69 = 0; num69 < num65; num69++)
				{
					ObjAd objAd2 = (ObjAd)myVector14.elementAt(num69);
					objAd2.typeShop = (int)msg.reader().readByte();
				}
				AvatarData.onMapAd(myVector14);
				return;
			}
			case 69:
			{
				short num70 = msg.reader().readShort();
				int price = 0;
				if (num70 != -1)
				{
					price = msg.reader().readInt();
				}
				int xu = msg.reader().readInt();
				int luong = msg.reader().readInt();
				int luongK = msg.reader().readInt();
				GameMidlet.avatar.updateMoney(xu, luong, luongK);
				MapScr.gI().onBuyIceDream(num70, price);
				return;
			}
			case 71:
			{
				int idUser7 = msg.reader().readInt();
				int idPart = (int)msg.reader().readShort();
				MapScr.gI().onRemoveItem(idUser7, idPart);
				return;
			}
			case 72:
			{
				bool isCreaCha = msg.reader().readBoolean();
				RegisterScr.gI().onCreaCharacter(isCreaCha);
				return;
			}
			case 74:
			{
				int num71 = msg.reader().readInt();
				int num72 = (int)msg.reader().readByte();
				if (num72 == 1)
				{
				}
				if (num72 == 5)
				{
					GameMidlet.avatar.setMoneyNew(GameMidlet.avatar.money[3] + num71);
					Canvas.addFlyTextSmall(num71 + "xeng", GameMidlet.avatar.x, GameMidlet.avatar.y, -1, 0, -1);
				}
				int xu2 = msg.reader().readInt();
				int luong2 = msg.reader().readInt();
				int luongK2 = msg.reader().readInt();
				GameMidlet.avatar.updateMoney(xu2, luong2, luongK2);
				return;
			}
			case 77:
			{
				string userName = msg.reader().readUTF();
				string pass = msg.reader().readUTF();
				RegisterScr.gI().onRegister(userName, pass);
				return;
			}
			case 82:
			{
				sbyte b34 = msg.reader().readByte();
				string des = null;
				string name = null;
				string pass2 = null;
				if ((int)b34 == 2)
				{
					name = msg.reader().readUTF();
					pass2 = msg.reader().readUTF();
				}
				else
				{
					des = msg.reader().readUTF();
				}
				MiniMap.gI().onRegisterByEmail(b34, des, name, pass2);
				break;
			}
			case 83:
			{
				short num73 = msg.reader().readShort();
				int newMoney = 0;
				sbyte typeBuy = 0;
				if (num73 != -1)
				{
					newMoney = msg.reader().readInt();
					typeBuy = msg.reader().readByte();
				}
				string text5 = msg.reader().readUTF();
				int xu3 = msg.reader().readInt();
				int luong3 = msg.reader().readInt();
				int luongKhoa = msg.reader().readInt();
				MapScr.gI().onBuyItem(num73, newMoney, typeBuy, text5, xu3, luong3, luongKhoa);
				return;
			}
			case 84:
			{
				MyVector myVector15 = new MyVector();
				int num74 = 0;
				while (msg.reader().available() > 0)
				{
					MoneyInfo moneyInfo = new MoneyInfo();
					moneyInfo.info = msg.reader().readUTF();
					moneyInfo.smsContent = msg.reader().readUTF();
					moneyInfo.smsTo = msg.reader().readUTF();
					moneyInfo.strID = msg.reader().readUTF();
					num74++;
					myVector15.addElement(moneyInfo);
				}
				this.globalLogicHandler.onMoneyInfo(myVector15);
				return;
			}
			case 85:
			{
				int id6 = msg.reader().readInt();
				Avatar avatar6 = LoadMap.getAvatar(id6);
				if (avatar6 == null)
				{
					Canvas.endDlg();
					return;
				}
				avatar6.indexP = new sbyte[5];
				avatar6.lvMain = (short)msg.reader().readByte();
				avatar6.perLvMain = msg.reader().readByte();
				avatar6.indexP[(int)Avatar.I_FRIENDLY] = msg.reader().readByte();
				avatar6.indexP[(int)Avatar.I_CRAZY] = msg.reader().readByte();
				avatar6.indexP[(int)Avatar.I_STYLISH] = msg.reader().readByte();
				avatar6.indexP[(int)Avatar.I_HAPPY] = msg.reader().readByte();
				avatar6.indexP[(int)Avatar.I_HUNGER] = msg.reader().readByte();
				Avatar avatar7 = null;
				int num75 = msg.reader().readInt();
				string sologan = string.Empty;
				string tenQuanHe = string.Empty;
				short idImage = 0;
				sbyte lv = 0;
				sbyte perLv = 0;
				short num76 = -1;
				string nameAction = string.Empty;
				if (num75 != -1)
				{
					avatar7 = new Avatar();
					avatar7.IDDB = num75;
					avatar7.setName(msg.reader().readUTF());
					sbyte b35 = msg.reader().readByte();
					for (int num77 = 0; num77 < (int)b35; num77++)
					{
						avatar7.addSeri(new SeriPart(msg.reader().readShort()));
					}
					sologan = msg.reader().readUTF();
					idImage = msg.reader().readShort();
					lv = msg.reader().readByte();
					perLv = msg.reader().readByte();
					tenQuanHe = msg.reader().readUTF();
					num76 = msg.reader().readShort();
					if (num76 != -1)
					{
						nameAction = msg.reader().readUTF();
					}
				}
				if (msg.reader().available() > 0)
				{
					GameMidlet.avatar.lvMain = msg.reader().readShort();
				}
				if (MapScr.isOpenInfo)
				{
					MapScr.isOpenInfo = false;
					MapScr.gI().onInfoPlayer(avatar6, avatar7, sologan, idImage, lv, perLv, tenQuanHe, num76, nameAction);
				}
				return;
			}
			case 86:
			{
				Avatar avatar8 = new Avatar();
				avatar8.IDDB = msg.reader().readInt();
				avatar8.name = msg.reader().readUTF();
				string text6 = msg.reader().readUTF();
				MapScr.gI().onRequestAddFriend(avatar8, text6);
				return;
			}
			case 88:
			{
				Avatar avatar9 = new Avatar();
				avatar9.IDDB = msg.reader().readInt();
				avatar9.name = msg.reader().readUTF();
				bool tr = msg.reader().readBoolean();
				string text7 = msg.reader().readUTF();
				MapScr.gI().onAddFriend(avatar9, tr, text7);
				return;
			}
			case 95:
			{
				Out.println("LOGIN_NEW_GAME");
				string nameNewGame = msg.reader().readUTF();
				string passNewGame = msg.reader().readUTF();
				LoginScr.gI().onLoginNewGame(nameNewGame, passNewGame);
				break;
			}
			case 97:
			{
				string error = msg.reader().readUTF();
				bool boo = false;
				if (msg.reader().available() > 0)
				{
					boo = msg.reader().readBoolean();
				}
				this.globalLogicHandler.onSetMoneyError(error, boo);
				return;
			}
			case 98:
				this.globalLogicHandler.onServerMessage(msg.reader().readUTF());
				return;
			case 99:
				this.globalLogicHandler.onServerInfo(msg.reader().readUTF());
				return;
			case 100:
				this.globalLogicHandler.onVersion(msg.reader().readUTF(), msg.reader().readUTF());
				return;
			case 101:
			{
				int id7 = msg.reader().readInt();
				string name2 = msg.reader().readUTF();
				string info3 = msg.reader().readUTF();
				this.globalLogicHandler.onChatFrom(id7, name2, info3);
				return;
			}
			case 106:
				this.globalLogicHandler.doGetHandler(msg.reader().readByte());
				return;
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		if (this.miniGameMessageHandler != null)
		{
			this.miniGameMessageHandler.onMessage(msg);
			return;
		}
		try
		{
			sbyte command2 = msg.command;
			if (command2 != -5)
			{
				if (command2 == -4)
				{
					LoginScr.gI().saveLogin();
					if (!RegisterScr.isCreateChar || Canvas.currentMyScreen != RegisterScr.instance)
					{
						GameMidlet.avatar = new Avatar();
					}
					GameMidlet.avatar.IDDB = msg.reader().readInt();
					GameMidlet.avatar.setName(LoginScr.gI().tfUser.getText().ToLower());
					sbyte b36 = msg.reader().readByte();
					MyVector myVector16 = new MyVector();
					for (int num78 = 0; num78 < (int)b36; num78++)
					{
						myVector16.addElement(new SeriPart
						{
							idPart = msg.reader().readShort()
						});
					}
					if (!RegisterScr.isCreateChar || Canvas.currentMyScreen != RegisterScr.instance)
					{
						GameMidlet.avatar.seriPart = myVector16;
					}
					sbyte gender = msg.reader().readByte();
					if (!RegisterScr.isCreateChar || Canvas.currentMyScreen != RegisterScr.instance)
					{
						GameMidlet.avatar.gender = gender;
					}
					GameMidlet.avatar.lvMain = (short)msg.reader().readByte();
					GameMidlet.avatar.perLvMain = msg.reader().readByte();
					GameMidlet.avatar.setMoney(msg.reader().readInt());
					GameMidlet.avatar.indexP = new sbyte[5];
					GameMidlet.avatar.indexP[(int)Avatar.I_FRIENDLY] = msg.reader().readByte();
					GameMidlet.avatar.indexP[(int)Avatar.I_CRAZY] = msg.reader().readByte();
					GameMidlet.avatar.indexP[(int)Avatar.I_STYLISH] = msg.reader().readByte();
					GameMidlet.avatar.indexP[(int)Avatar.I_HAPPY] = msg.reader().readByte();
					GameMidlet.avatar.indexP[(int)Avatar.I_HUNGER] = msg.reader().readByte();
					GameMidlet.avatar.money[2] = msg.reader().readInt();
					GameMidlet.avatar.blogNews = msg.reader().readByte();
					for (int num79 = 0; num79 < GameMidlet.avatar.seriPart.size(); num79++)
					{
						SeriPart seriPart = (SeriPart)GameMidlet.avatar.seriPart.elementAt(num79);
						seriPart.time = msg.reader().readByte();
						seriPart.expireString = msg.reader().readUTF();
					}
					GameMidlet.avatar.idImg = msg.reader().readShort();
					MapScr.listCmd = new MyVector();
					sbyte b37 = msg.reader().readByte();
					for (int num80 = 0; num80 < (int)b37; num80++)
					{
						StringObj stringObj3 = new StringObj();
						stringObj3.str = msg.reader().readUTF();
						stringObj3.dis = (int)msg.reader().readShort();
						MapScr.listCmd.addElement(stringObj3);
					}
					MapScr.listCmdRotate = new MyVector();
					sbyte b38 = msg.reader().readByte();
					for (int num81 = 0; num81 < (int)b38; num81++)
					{
						StringObj stringObj4 = new StringObj();
						stringObj4.anthor = (int)msg.reader().readShort();
						stringObj4.str = msg.reader().readUTF();
						stringObj4.dis = (int)msg.reader().readShort();
						MapScr.listCmdRotate.addElement(stringObj4);
					}
					MapScr.gI().isTour = msg.reader().readBool();
					if (msg.reader().available() > 0)
					{
						for (int num82 = 0; num82 < (int)b38; num82++)
						{
							StringObj stringObj5 = (StringObj)MapScr.listCmdRotate.elementAt(num82);
							stringObj5.type = (int)msg.reader().readByte();
						}
					}
					if (msg.reader().available() > 0)
					{
						Canvas.iOpenOngame = (int)msg.reader().readByte();
					}
					GameMidlet.avatar.lvMain = (GameMidlet.myIndexP.level = msg.reader().readShort());
					if (Canvas.iOpenOngame == 1 || Canvas.iOpenOngame == 2)
					{
						T.nameCasino = T.nameCasino1;
					}
					GameMidlet.avatar.idWedding = msg.reader().readShort();
					if (msg.reader().available() > 0)
					{
						MapScr.isNewVersion = msg.reader().readBoolean();
					}
					if (MapScr.isNewVersion)
					{
						GameMidlet.avatar.money[3] = msg.reader().readInt();
					}
					sbyte b39 = msg.reader().readByte();
					MapScr.listItemEffect = new MyVector();
					for (int num83 = 0; num83 < (int)b39; num83++)
					{
						ItemEffectInfo itemEffectInfo = new ItemEffectInfo();
						itemEffectInfo.IDAction = msg.reader().readShort();
						itemEffectInfo.name = msg.reader().readUTF();
						itemEffectInfo.IDIcon = msg.reader().readShort();
						itemEffectInfo.money = msg.reader().readInt();
						itemEffectInfo.typeMoney = msg.reader().readByte();
						MapScr.listItemEffect.addElement(itemEffectInfo);
					}
					GameMidlet.avatar.setGold(msg.reader().readInt());
					GameMidlet.avatar.luongKhoa = msg.reader().readInt();
					sbyte b40 = msg.reader().readByte();
					string name3 = msg.reader().readUTF();
					GameMidlet.avatar.setName(name3);
					Out.println(string.Concat(new object[]
					{
						"Money: ",
						GameMidlet.avatar.money[2],
						"    ",
						GameMidlet.avatar.luongKhoa
					}));
					this.globalLogicHandler.onLoginSuccess();
				}
			}
			else
			{
				this.globalLogicHandler.onLoginFail(msg.reader().readUTF());
			}
		}
		catch (Exception e2)
		{
			Out.logError(e2);
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0002F928 File Offset: 0x0002DD28
	public static MyVector readListPlayer(Message msg)
	{
		MyVector myVector = new MyVector();
		try
		{
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < (int)b; i++)
			{
				Avatar avatar = new Avatar();
				avatar.IDDB = msg.reader().readInt();
				avatar.setName(msg.reader().readUTF());
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < (int)b2; j++)
				{
					short idP = msg.reader().readShort();
					avatar.addSeri(new SeriPart(idP));
				}
				avatar.x = (int)msg.reader().readShort();
				avatar.y = (int)msg.reader().readShort();
				avatar.blogNews = msg.reader().readByte();
				myVector.addElement(avatar);
			}
			for (int k = 0; k < (int)b; k++)
			{
				Avatar avatar2 = (Avatar)myVector.elementAt(k);
				avatar2.direct = msg.reader().readByte();
			}
			for (int l = 0; l < (int)b; l++)
			{
				Avatar avatar3 = (Avatar)myVector.elementAt(l);
				avatar3.hungerPet = (short)((sbyte)(100 - (int)msg.reader().readByte()));
			}
			for (int m = 0; m < (int)b; m++)
			{
				Avatar avatar4 = (Avatar)myVector.elementAt(m);
				avatar4.idImg = msg.reader().readShort();
			}
			sbyte b3 = msg.reader().readByte();
			for (int n = 0; n < (int)b3; n++)
			{
				myVector.addElement(new Drop_Part
				{
					type = msg.reader().readByte(),
					idDrop = msg.reader().readShort(),
					ID = msg.reader().readInt(),
					x = (int)msg.reader().readShort(),
					y = (int)msg.reader().readShort()
				});
			}
			LoadMap.listImgAD = null;
			sbyte b4 = 0;
			if (msg.reader().available() > 0)
			{
				b4 = msg.reader().readByte();
			}
			if ((int)b4 > 0)
			{
				LoadMap.listImgAD = new MyVector();
				for (int num = 0; num < (int)b4; num++)
				{
					AvPosition avPosition = new AvPosition();
					avPosition.anchor = (int)msg.reader().readShort();
					avPosition.x = (int)msg.reader().readShort();
					avPosition.y = (int)msg.reader().readShort();
					avPosition.depth = msg.reader().readByte();
					LoadMap.listImgAD.addElement(avPosition);
				}
			}
		}
		catch (Exception ex)
		{
			return new MyVector();
		}
		return myVector;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0002FC18 File Offset: 0x0002E018
	public static MyVector readMapItem(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			MyVector myVector = new MyVector();
			for (int i = 0; i < (int)b; i++)
			{
				myVector.addElement(new MapItem
				{
					type = (int)msg.reader().readByte(),
					typeID = (short)msg.reader().readByte(),
					x = (int)msg.reader().readByte(),
					y = (int)msg.reader().readByte(),
					isGetImg = true
				});
			}
			return myVector;
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		return null;
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0002FCD0 File Offset: 0x0002E0D0
	public static MyVector readMapItemType(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			MyVector myVector = new MyVector();
			for (int i = 0; i < (int)b; i++)
			{
				MapItemType mapItemType = new MapItemType();
				mapItemType.idType = (short)msg.reader().readByte();
				mapItemType.imgID = msg.reader().readShort();
				mapItemType.iconID = (short)msg.reader().readByte();
				mapItemType.dx = msg.reader().readShort();
				mapItemType.dy = msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				mapItemType.listNotTrans = new MyVector();
				for (int j = 0; j < (int)b2; j++)
				{
					AvPosition avPosition = new AvPosition();
					avPosition.x = (int)msg.reader().readByte();
					avPosition.y = (int)msg.reader().readByte();
					mapItemType.listNotTrans.addElement(avPosition);
				}
				myVector.addElement(mapItemType);
			}
			return myVector;
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		return null;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0002FDFC File Offset: 0x0002E1FC
	public static void readMove(Message msg)
	{
		int id = msg.reader().readInt();
		int xM = (int)msg.reader().readShort();
		int yM = (int)msg.reader().readShort();
		int direct = (int)msg.reader().readByte();
		MapScr.gI().onMovePark(id, xM, yM, direct);
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0002FE48 File Offset: 0x0002E248
	public static void readChat(Message msg)
	{
		int num = msg.reader().readInt();
		string text = msg.reader().readUTF();
		if (num != GameMidlet.avatar.IDDB)
		{
			MapScr.gI().onChatFrom(num, text);
		}
	}

	// Token: 0x04000783 RID: 1923
	public GlobalLogicHandler globalLogicHandler = new GlobalLogicHandler();

	// Token: 0x04000784 RID: 1924
	public static GlobalMessageHandler me;

	// Token: 0x04000785 RID: 1925
	public IMiniGameMsgHandler miniGameMessageHandler;

	// Token: 0x020000A2 RID: 162
	private class CommandFlower : Command
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x0002FE89 File Offset: 0x0002E289
		public CommandFlower(string caption, IAction ac, short idImg) : base(caption, ac)
		{
			this.idImg = idImg;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002FE9A File Offset: 0x0002E29A
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.paintImg(g, (int)this.idImg, x, y, 3);
		}

		// Token: 0x04000786 RID: 1926
		private short idImg;
	}

	// Token: 0x020000A3 RID: 163
	private class IActionFlower : IAction
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0002FEAB File Offset: 0x0002E2AB
		public IActionFlower(sbyte i)
		{
			this.ii = i;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002FEBA File Offset: 0x0002E2BA
		public void perform()
		{
			GlobalService.gI().doFlowerLoveSelected((int)this.ii);
		}

		// Token: 0x04000787 RID: 1927
		private sbyte ii;
	}

	// Token: 0x020000A4 RID: 164
	private class IActionMiniMap : IAction
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0002FECD File Offset: 0x0002E2CD
		public IActionMiniMap(sbyte id)
		{
			this.idCityMap = id;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002FEDC File Offset: 0x0002E2DC
		public void perform()
		{
			PositionMap positionMap = (PositionMap)MiniMap.gI().listPos.elementAt(MiniMap.gI().selected);
			Canvas.startWaitDlg();
			MapScr.idSelectedMini = positionMap.id;
			MapScr.idCityMap = this.idCityMap;
			GlobalService.gI().doSelectedMiniMap(this.idCityMap, positionMap.id);
		}

		// Token: 0x04000788 RID: 1928
		private sbyte idCityMap;
	}
}
