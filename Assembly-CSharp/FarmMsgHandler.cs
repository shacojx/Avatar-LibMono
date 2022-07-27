using System;

// Token: 0x0200008B RID: 139
public class FarmMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x06000483 RID: 1155 RVA: 0x000298CA File Offset: 0x00027CCA
	public static void onHandler()
	{
		if (FarmMsgHandler.instance == null)
		{
			FarmMsgHandler.instance = new FarmMsgHandler();
		}
		GlobalMessageHandler.gI().miniGameMessageHandler = FarmMsgHandler.instance;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x000298F0 File Offset: 0x00027CF0
	public void onMessage(Message msg)
	{
		try
		{
			switch (msg.command)
			{
			case 51:
			{
				sbyte b = msg.reader().readByte();
				short[] array = new short[(int)b];
				short[] array2 = new short[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					array[i] = msg.reader().readShort();
					array2[i] = msg.reader().readShort();
				}
				int verBigImg = msg.reader().readInt();
				int verPart = msg.reader().readInt();
				FarmData.checkDataFarm(b, array, array2, verBigImg, verPart);
				break;
			}
			case 54:
			{
				short index = msg.reader().readShort();
				short version = msg.reader().readShort();
				int num = (int)msg.reader().readUnsignedShort();
				sbyte[] array3 = new sbyte[num];
				for (int j = 0; j < num; j++)
				{
					array3[j] = msg.reader().readByte();
				}
				FarmData.addDataBig(index, version, array3);
				break;
			}
			case 55:
			{
				sbyte[] arr = new sbyte[msg.reader().available()];
				msg.reader().read(ref arr);
				FarmData.saveImageData(arr);
				break;
			}
			case 56:
			{
				sbyte[] arr2 = new sbyte[msg.reader().available()];
				msg.reader().read(ref arr2);
				FarmData.saveTreeInfo(arr2);
				break;
			}
			case 60:
			{
				sbyte b2 = msg.reader().readByte();
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				for (int k = 0; k < (int)b2; k++)
				{
					Item item = new Item();
					item.ID = (short)msg.reader().readByte();
					item.number = (int)msg.reader().readShort();
					if (item.ID > 100)
					{
						myVector2.addElement(item);
					}
					else
					{
						myVector.addElement(item);
					}
				}
				sbyte b3 = msg.reader().readByte();
				MyVector myVector3 = new MyVector();
				for (int l = 0; l < (int)b3; l++)
				{
					myVector3.addElement(new Item
					{
						ID = (short)msg.reader().readByte(),
						number = (int)msg.reader().readShort()
					});
				}
				GameMidlet.avatar.money[1] = msg.reader().readInt();
				GameMidlet.avatar.lvFarm = (short)msg.reader().readByte();
				GameMidlet.avatar.perLvFarm = msg.reader().readByte();
				sbyte b4 = msg.reader().readByte();
				MyVector myVector4 = new MyVector();
				for (int m = 0; m < (int)b4; m++)
				{
					myVector4.addElement(new Item
					{
						ID = msg.reader().readShort(),
						number = (int)msg.reader().readShort()
					});
				}
				sbyte b5 = msg.reader().readByte();
				MyVector myVector5 = new MyVector();
				for (int n = 0; n < (int)b5; n++)
				{
					myVector5.addElement(new Item
					{
						ID = msg.reader().readShort(),
						number = (int)msg.reader().readShort()
					});
				}
				sbyte levelStore = msg.reader().readByte();
				int capacity = msg.reader().readInt();
				bool isNew = msg.reader().readBoolean();
				GameMidlet.avatar.lvFarm = msg.reader().readShort();
				GameMidlet.avatar.perLvFarm = msg.reader().readByte();
				sbyte b6 = msg.reader().readByte();
				myVector3.removeAllElements();
				for (int num2 = 0; num2 < (int)b6; num2++)
				{
					myVector3.addElement(new Item
					{
						ID = msg.reader().readShort(),
						number = msg.reader().readInt()
					});
				}
				myVector5.removeAllElements();
				b5 = msg.reader().readByte();
				for (int num3 = 0; num3 < (int)b5; num3++)
				{
					myVector5.addElement(new Item
					{
						ID = msg.reader().readShort(),
						number = msg.reader().readInt()
					});
				}
				FarmScr.gI().onInventory(myVector, myVector3, myVector2, myVector4, myVector5, levelStore, capacity, isNew);
				if (FarmData.playing == 0 && LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53 && LoadMap.TYPEMAP != 25)
				{
					FarmData.saveVersion();
					ParkService.gI().doJoinPark(25, 0);
					FarmScr.init();
					FarmScr.gI().doJoinFarm(GameMidlet.avatar.IDDB, false);
				}
				break;
			}
			case 61:
				FarmMsgHandler.readFarmData(msg);
				break;
			case 62:
			{
				Item item2 = new Item();
				item2.ID = msg.reader().readShort();
				item2.number = (int)msg.reader().readByte();
				int newMoney = msg.reader().readInt();
				sbyte typeBuy = msg.reader().readByte();
				int xu = msg.reader().readInt();
				int luong = msg.reader().readInt();
				int luongK = msg.reader().readInt();
				FarmScr.gI().onBuyItem(item2, newMoney, typeBuy, xu, luong, luongK);
				break;
			}
			case 63:
			{
				int sellMoney = msg.reader().readInt();
				int curMoney = msg.reader().readInt();
				short idItem = msg.reader().readShort();
				FarmScr.gI().onSell(sellMoney, curMoney, idItem);
				break;
			}
			case 64:
			{
				int idUser = msg.reader().readInt();
				int indexCell = (int)msg.reader().readByte();
				int idSeed = (int)msg.reader().readByte();
				FarmScr.gI().onPlantSeed(idUser, indexCell, idSeed);
				break;
			}
			case 65:
			{
				msg.reader().readByte();
				int id = (int)msg.reader().readShort();
				FarmItem farmItem = FarmScr.getFarmItem(id);
				if (farmItem != null)
				{
					Item itemByList = Item.getItemByList(FarmScr.listItemFarm, id);
					if (itemByList != null)
					{
						itemByList.number--;
						if (itemByList.number <= 0)
						{
							FarmScr.listItemFarm.removeElement(itemByList);
						}
					}
				}
				break;
			}
			case 66:
			{
				int indexCell2 = (int)msg.reader().readByte();
				int number = (int)msg.reader().readShort();
				FarmScr.gI().onHarvestTree(indexCell2, number);
				break;
			}
			case 67:
				FarmScr.gI().onKick(msg.reader().readInt());
				break;
			case 69:
				FarmScr.gI().onPricePlant(msg.reader().readUTF());
				break;
			case 70:
			{
				int idfarm = msg.reader().readInt();
				int curMoney2 = msg.reader().readInt();
				sbyte typeBuy2 = msg.reader().readByte();
				string text = msg.reader().readUTF();
				int xu2 = msg.reader().readInt();
				int luong2 = msg.reader().readInt();
				int luongKhoa = msg.reader().readInt();
				FarmScr.gI().onOpenLand(idfarm, curMoney2, typeBuy2, text, xu2, luong2, luongKhoa);
				break;
			}
			case 71:
			{
				Item item3 = new Item();
				item3.ID = (short)msg.reader().readByte();
				int newMoney2 = msg.reader().readInt();
				sbyte typeBuy3 = msg.reader().readByte();
				int xu3 = msg.reader().readInt();
				int luong3 = msg.reader().readInt();
				int luongK2 = msg.reader().readInt();
				FarmScr.gI().onBuyItem(item3, newMoney2, typeBuy3, xu3, luong3, luongK2);
				break;
			}
			case 72:
			{
				sbyte index2 = msg.reader().readByte();
				string str = msg.reader().readUTF();
				FarmScr.gI().onPriceAnimal(index2, str);
				break;
			}
			case 73:
			{
				int idFarm = msg.reader().readInt();
				int index3 = (int)msg.reader().readByte();
				int curMoney3 = msg.reader().readInt();
				FarmScr.gI().onSellAnimal(idFarm, index3, curMoney3);
				break;
			}
			case 74:
			{
				int indexCell3 = (int)msg.reader().readByte();
				int number2 = (int)msg.reader().readShort();
				FarmScr.gI().onHarvestAnimal(indexCell3, number2);
				break;
			}
			case 75:
			{
				int money = msg.reader().readInt();
				int num4 = msg.reader().readInt();
				GameMidlet.avatar.setMoney(money);
				GameMidlet.avatar.money[1] = num4;
				string info = msg.reader().readUTF();
				Canvas.startOKDlg(info);
				break;
			}
			case 76:
				GlobalMessageHandler.readMove(msg);
				break;
			case 77:
				GlobalMessageHandler.readChat(msg);
				break;
			case 78:
				if (LoadMap.TYPEMAP == 24 || LoadMap.TYPEMAP == 53)
				{
					sbyte b7 = msg.reader().readByte();
					CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt((int)b7);
					cellFarm.idTree = (int)msg.reader().readByte();
					FarmMsgHandler.readInfoCell(cellFarm, msg);
					FarmScr.gI().setInfoCell((int)b7);
				}
				break;
			case 79:
				if (LoadMap.TYPEMAP == 24 || LoadMap.TYPEMAP == 53)
				{
					sbyte b8 = msg.reader().readByte();
					sbyte b9 = msg.reader().readByte();
					if ((int)b9 != -1)
					{
						Animal animalByIndex = FarmScr.getAnimalByIndex((int)b8);
						animalByIndex.species = b9;
						FarmMsgHandler.readInfoAnimal(animalByIndex, msg);
						FarmScr.gI().setAnimal();
					}
				}
				break;
			case 80:
			{
				sbyte b10 = msg.reader().readByte();
				if ((int)b10 == 0)
				{
					string info2 = msg.reader().readUTF();
					Canvas.msgdlg.setInfoLCR(info2, new Command(T.xu, new FarmMsgHandler.IActionCattle(0)), new Command(T.gold, new FarmMsgHandler.IActionCattle(1)), Canvas.cmdEndDlg);
				}
				else
				{
					sbyte b11 = msg.reader().readByte();
					int num5 = msg.reader().readInt();
					Canvas.load = 1;
					FarmScr.gI().doJoinFarm(GameMidlet.avatar.IDDB, true);
					FarmScr.isReSize = true;
				}
				int xu4 = msg.reader().readInt();
				int luong4 = msg.reader().readInt();
				int luongK3 = msg.reader().readInt();
				GameMidlet.avatar.updateMoney(xu4, luong4, luongK3);
				break;
			}
			case 81:
			{
				sbyte b12 = msg.reader().readByte();
				if ((int)b12 == 0)
				{
					string info3 = msg.reader().readUTF();
					Canvas.msgdlg.setInfoLCR(info3, new Command(T.xu, new FarmMsgHandler.IActionFish(0)), new Command(T.gold, new FarmMsgHandler.IActionFish(1)), Canvas.cmdEndDlg);
				}
				else
				{
					sbyte b13 = msg.reader().readByte();
					int num6 = msg.reader().readInt();
					Canvas.load = 1;
					FarmScr.gI().doJoinFarm(GameMidlet.avatar.IDDB, true);
					FarmScr.isReSize = true;
				}
				int xu5 = msg.reader().readInt();
				int luong5 = msg.reader().readInt();
				int luongK4 = msg.reader().readInt();
				GameMidlet.avatar.updateMoney(xu5, luong5, luongK4);
				break;
			}
			case 82:
			{
				short num7 = msg.reader().readShort();
				short num8 = msg.reader().readShort();
				sbyte[] data = new sbyte[(int)num8];
				msg.reader().read(ref data);
				FarmData.listImgIcon.put(string.Empty + num7, new ImageIcon(CRes.createImgByByteArray(ArrayCast.cast(data))));
				break;
			}
			case 83:
			{
				bool flag = msg.reader().readBoolean();
				if (flag)
				{
					StarFruitObj.imgID = (int)msg.reader().readShort();
					StarFruitObj starFruil = FarmScr.starFruil;
					starFruil.lv += 1;
				}
				break;
			}
			case 84:
			{
				sbyte b14 = msg.reader().readByte();
				if ((int)b14 == 0)
				{
					string info4 = msg.reader().readUTF();
					Canvas.startOKDlg(info4, 7, FarmScr.instance);
				}
				else
				{
					int num9 = msg.reader().readInt();
					short num10 = msg.reader().readShort();
					GameMidlet.avatar.money[1] -= num9;
					FarmScr.starFruil.timeFinish = (int)(num10 * 60);
					FarmScr.starFruil.time = Canvas.getTick();
					Canvas.addFlyText(-num9, GameMidlet.avatar.x, GameMidlet.avatar.y, -1, -1);
				}
				break;
			}
			case 85:
			{
				short productIDH = msg.reader().readShort();
				short numberPro = msg.reader().readShort();
				FarmScr.gI().onHarvestStarFruit(productIDH, numberPro);
				break;
			}
			case 86:
			{
				sbyte b15 = msg.reader().readByte();
				if ((int)b15 == 0)
				{
					string info5 = msg.reader().readUTF();
					Canvas.startOKDlg(info5, 8, FarmScr.instance);
				}
				else
				{
					int num11 = msg.reader().readInt();
					StarFruitObj.imgID = (int)msg.reader().readShort();
					FarmScr.starFruil.timeFinish = 0;
					StarFruitObj starFruil2 = FarmScr.starFruil;
					starFruil2.lv += 1;
					int luong6 = msg.reader().readInt();
					int luongK5 = msg.reader().readInt();
					GameMidlet.avatar.updateMoney(GameMidlet.avatar.money[0], luong6, luongK5);
				}
				break;
			}
			case 90:
			{
				sbyte b16 = msg.reader().readByte();
				if ((int)b16 == 0)
				{
					string info6 = msg.reader().readUTF();
					Canvas.msgdlg.setInfoLCR(info6, new Command(T.xu, 9, FarmScr.instance), new Command(T.gold, 10, FarmScr.instance), Canvas.cmdEndDlg);
				}
				else
				{
					sbyte b17 = msg.reader().readByte();
					int num12 = msg.reader().readInt();
					sbyte b18 = msg.reader().readByte();
					Canvas.startOKDlg(msg.reader().readUTF());
					CellFarm cellFarm2 = (CellFarm)FarmScr.cell.elementAt((int)b18);
					CellFarm cellFarm3 = cellFarm2;
					cellFarm3.level = (sbyte)((int)cellFarm3.level + 1);
					int luong7 = msg.reader().readInt();
					int luongK6 = msg.reader().readInt();
					GameMidlet.avatar.updateMoney(GameMidlet.avatar.money[0], luong7, luongK6);
					FarmScr.gI().onJoin(FarmScr.idFarm, FarmScr.cell, FarmScr.animalLists, FarmScr.numBarn, FarmScr.numPond, FarmScr.foodID, FarmScr.remainTime);
				}
				break;
			}
			case 91:
			{
				short num13 = msg.reader().readShort();
				if (num13 == -1)
				{
					FarmScr.foodID = 0;
				}
				else
				{
					short num14 = msg.reader().readShort();
					FarmScr.foodID = num13;
					FarmScr.remainTime = (int)(num14 * 60);
					FarmScr.curTimeCooking = (int)(Canvas.getTick() / 1000L);
				}
				Canvas.endDlg();
				break;
			}
			case 92:
			{
				Food foodByID = FarmData.getFoodByID(FarmScr.foodID);
				FarmItem farmItem2 = FarmScr.getFarmItem((int)foodByID.productID);
				Item item4 = FarmScr.getItemProductByID((int)foodByID.productID);
				if (item4 != null)
				{
					item4.number++;
				}
				else
				{
					item4 = new Item();
					item4.ID = foodByID.productID;
					item4.number = 1;
					FarmScr.listFarmProduct.addElement(item4);
				}
				Canvas.addFlyText(0, FarmScr.xPosCook, FarmScr.yPosCook, -1, 0, (int)farmItem2.IDImg, -1);
				FarmScr.foodID = 0;
				break;
			}
			case 93:
			{
				sbyte b19 = msg.reader().readByte();
				if ((int)b19 == 0)
				{
					string info7 = msg.reader().readUTF();
					Canvas.startOKDlg(info7, 11, FarmScr.instance);
				}
				else
				{
					int num15 = msg.reader().readInt();
					FarmScr.remainTime = 0;
					int luong8 = msg.reader().readInt();
					int luongK7 = msg.reader().readInt();
					GameMidlet.avatar.updateMoney(GameMidlet.avatar.money[0], luong8, luongK7);
				}
				break;
			}
			case 94:
				if (this.aa != 1)
				{
					sbyte b20 = msg.reader().readByte();
					if ((int)b20 == 0)
					{
						string info8 = msg.reader().readUTF();
						Canvas.msgdlg.setInfoLCR(info8, new Command(T.xu, 13, FarmScr.instance), new Command(T.gold, 14, FarmScr.instance), Canvas.cmdEndDlg);
					}
					else
					{
						sbyte b21 = msg.reader().readByte();
						int num16 = msg.reader().readInt();
						if ((int)b21 == 1)
						{
							GameMidlet.avatar.money[1] -= num16;
						}
						else
						{
							GameMidlet.avatar.money[2] -= num16;
						}
						FarmScr.capacityStore = msg.reader().readInt();
						Canvas.startOKDlg(msg.reader().readUTF());
						FarmScr.gI().onJoin(FarmScr.idFarm, FarmScr.cell, FarmScr.animalLists, FarmScr.numBarn, FarmScr.numPond, FarmScr.foodID, FarmScr.remainTime);
					}
					this.aa = 1;
				}
				break;
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0002AA90 File Offset: 0x00028E90
	public static void readInfoCell(CellFarm c, Message msg)
	{
		short num = msg.reader().readShort();
		FarmScr.startTextSmall((int)c.time, (int)num, c, null);
		c.time = num;
		c.tempTime = (long)(c.time * 60);
		sbyte b = msg.reader().readByte();
		FarmScr.startTextSmall((int)c.vitalityPer, (int)b, c, null);
		c.vitalityPer = b;
		c.hervestPer = msg.reader().readByte();
		c.isArid = msg.reader().readBoolean();
		c.isWorm = msg.reader().readBoolean();
		c.isGrass = msg.reader().readBoolean();
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0002AB38 File Offset: 0x00028F38
	public static void readInfoAnimal(Animal ani, Message msg)
	{
		ani.bornTime = msg.reader().readInt();
		sbyte b = msg.reader().readByte();
		FarmScr.startTextSmall(ani.health, (int)b, null, ani);
		ani.health = (int)b;
		ani.harvestPer = msg.reader().readByte();
		ani.numEggOne = (int)msg.reader().readByte();
		ani.hunger = msg.reader().readBoolean();
		ani.disease[0] = msg.reader().readBoolean();
		ani.disease[1] = msg.reader().readBoolean();
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0002ABD4 File Offset: 0x00028FD4
	public static void readFarmData(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			MyVector myVector = new MyVector();
			MyVector myVector2 = new MyVector();
			short num2 = 0;
			if (num != -1)
			{
				num2 = (short)msg.reader().readByte();
				for (int i = 0; i < (int)num2; i++)
				{
					CellFarm cellFarm = new CellFarm();
					cellFarm.idTree = (int)msg.reader().readByte();
					if (cellFarm.idTree == -1)
					{
						myVector.addElement(cellFarm);
					}
					else
					{
						FarmMsgHandler.readInfoCell(cellFarm, msg);
						myVector.addElement(cellFarm);
					}
				}
				short num3 = (short)msg.reader().readByte();
				if (LoadMap.TYPEMAP != 24 || GameMidlet.avatar.IDDB != num)
				{
					Cattle.numPig = 0;
					Dog.numBer = 0;
					Chicken.numChicken = 0;
					FarmScr.animalLists.removeAllElements();
				}
				for (int j = 0; j < (int)num3; j++)
				{
					Animal animal = null;
					sbyte b = msg.reader().readByte();
					int num4 = FarmScr.animalLists.size();
					if (LoadMap.TYPEMAP != 24 || num4 == 0 || num4 != (int)num3)
					{
						AnimalInfo animalByID = FarmData.getAnimalByID((int)b);
						if ((int)b != -1)
						{
							switch (animalByID.area)
							{
							case 1:
								animal = new Chicken(j, b, 0);
								break;
							case 2:
								animal = new Cattle(j, b);
								break;
							case 3:
								animal = new Dog(j, b);
								break;
							case 4:
								animal = new FishFarm(j, b, 0);
								break;
							}
						}
					}
					else
					{
						animal = FarmScr.getAnimalByIndex(j);
						animal = (Animal)FarmScr.animalLists.elementAt(j);
					}
					if ((int)b != -1 && animal != null)
					{
						animal.species = b;
						FarmMsgHandler.readInfoAnimal(animal, msg);
						myVector2.addElement(animal);
					}
				}
			}
			sbyte numBarn = msg.reader().readByte();
			sbyte numPond = msg.reader().readByte();
			FarmScr.starFruil = new StarFruitObj();
			FarmScr.starFruil.lv = msg.reader().readShort();
			StarFruitObj.imgID = (int)msg.reader().readShort();
			FarmScr.starFruil.fruitID = msg.reader().readShort();
			FarmScr.starFruil.numberFruit = msg.reader().readShort();
			msg.reader().readShort();
			FarmScr.starFruil.anTrom = msg.reader().readShort();
			FarmScr.starFruil.timeFinish = (int)(msg.reader().readShort() * 60);
			FarmScr.starFruil.time = Canvas.getTick();
			for (int k = 0; k < (int)num2; k++)
			{
				CellFarm cellFarm2 = (CellFarm)myVector.elementAt(k);
				cellFarm2.level = msg.reader().readByte();
			}
			short foodID = 0;
			int remainTime = 0;
			if (msg.reader().available() > 0)
			{
				foodID = msg.reader().readShort();
				remainTime = (int)(msg.reader().readShort() * 60);
			}
			FarmScr.gI().onJoin(num, myVector, myVector2, numBarn, numPond, foodID, remainTime);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0400076E RID: 1902
	private static FarmMsgHandler instance;

	// Token: 0x0400076F RID: 1903
	private int aa;

	// Token: 0x0200008C RID: 140
	private class IActionSteal : IAction
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0002AF38 File Offset: 0x00029338
		public void perform()
		{
			FarmService.gI().doSteal(1);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x0200008D RID: 141
	private class IActionCattle : IAction
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0002AF4A File Offset: 0x0002934A
		public IActionCattle(int type)
		{
			this.typeMoney = type;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0002AF59 File Offset: 0x00029359
		public void perform()
		{
			FarmService.gI().doUpdateFarm(1, this.typeMoney);
		}

		// Token: 0x04000770 RID: 1904
		private int typeMoney;
	}

	// Token: 0x0200008E RID: 142
	private class IActionFish : IAction
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0002AF6C File Offset: 0x0002936C
		public IActionFish(int type)
		{
			this.typeMoney = type;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0002AF7B File Offset: 0x0002937B
		public void perform()
		{
			FarmService.gI().doUpdateFish(1, this.typeMoney);
		}

		// Token: 0x04000771 RID: 1905
		private int typeMoney;
	}
}
