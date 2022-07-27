using System;
using System.IO;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class FarmScr : MyScreen
{
	// Token: 0x06000634 RID: 1588 RVA: 0x0003A5FC File Offset: 0x000389FC
	static FarmScr()
	{
		FarmScr.FRAME[0] = new sbyte[]
		{
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1
		};
		FarmScr.FRAME[1] = new sbyte[]
		{
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3
		};
		FarmScr.FRAME[2] = new sbyte[]
		{
			4,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			5
		};
		FarmScr.FRAME[3] = new sbyte[]
		{
			6,
			6,
			6,
			6,
			6,
			7,
			7,
			7,
			7,
			7
		};
		FarmScr.FRAME[4] = new sbyte[]
		{
			8,
			8,
			8,
			8,
			8,
			9,
			9,
			9,
			9,
			9
		};
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0003A744 File Offset: 0x00038B44
	public FarmScr()
	{
		FarmScr.listFood[0] = new MyVector();
		FarmScr.listFood[1] = new MyVector();
		FarmScr.cmdMenu = new Command(T.selectt, 0);
		FarmScr.cmdLeftMenu = new Command(T.menu, 1);
		FarmScr.cmdFocusBet = new Command(T.selectt, 2);
		FarmScr.cmdFeeding = new Command(T.selectt, 3);
		this.doLeftMenu();
		FarmScr.cmdFinishAuto = new Command(T.finish, 29, this);
		FarmScr.cmdNextAuto = new Command(T.next, 28, this);
		this.cmdNextSteal = new Command(T.next, 21, this);
		this.cmdCloseSteal = new Command(T.close, 18, this);
		this.cmdStreal = new Command("Trộm", 20, this);
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0003A87B File Offset: 0x00038C7B
	public static FarmScr gI()
	{
		if (FarmScr.instance == null)
		{
			FarmScr.instance = new FarmScr();
		}
		return FarmScr.instance;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0003A896 File Offset: 0x00038C96
	public override void switchToMe()
	{
		base.switchToMe();
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0003A89E File Offset: 0x00038C9E
	public static void init()
	{
		FarmScr.isSteal = (FarmScr.isAbleSteal = false);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0003A8AC File Offset: 0x00038CAC
	public override void initZoom()
	{
		AvCamera.gI().init(25);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0003A8BA File Offset: 0x00038CBA
	public static void resetImg()
	{
		FarmScr.img = null;
		FarmScr.imgBuyLant = null;
		FarmScr.imgWorm_G = null;
		FarmScr.imgWormAndGrass = null;
		FarmScr.imgFocusCel = null;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0003A8DC File Offset: 0x00038CDC
	public static void initImg()
	{
		FarmScr.imgBuyLant = Image.createImagePNG(T.getPath() + "/farm/buyLand");
		FarmScr.img = new FrameImage(Image.createImagePNG(T.getPath() + "/farm/cut"), 24 * AvMain.hd, 24 * AvMain.hd);
		FarmScr.imgWorm_G = new Image[2];
		FarmScr.imgWorm_G[0] = Image.createImagePNG(T.getPath() + "/farm/w");
		FarmScr.imgWorm_G[1] = Image.createImagePNG(T.getPath() + "/farm/g");
		FarmScr.imgWormAndGrass = new FrameImage(Image.createImagePNG(T.getPath() + "/farm/wg"), 13 * AvMain.hd, 9 * AvMain.hd);
		FarmScr.imgTrough = new FrameImage(Image.createImagePNG(T.getPath() + "/farm/m"), 27 * AvMain.hd, 17 * AvMain.hd);
		FarmScr.imgDogTr = new FrameImage(Image.createImagePNG(T.getPath() + "/farm/tc"), 13 * AvMain.hd, 13 * AvMain.hd);
		FarmScr.imgFocusCel = Image.createImagePNG(T.getPath() + "/temp/focusCell");
		FarmScr.imgBenh = new FrameImage(Image.createImagePNG(T.getPath() + "/farm/iB"), 9 * AvMain.hd, 13 * AvMain.hd);
		FarmScr.imgSell = Image.createImagePNG(T.getPath() + "/farm/coin");
		FarmScr.imgCell = new Image[8];
		for (int i = 0; i < 8; i++)
		{
			FarmScr.imgCell[i] = Image.createImagePNG(T.getPath() + "/farm/cell" + i);
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0003AA99 File Offset: 0x00038E99
	public override void close()
	{
		Canvas.startWaitDlg();
		GlobalService.gI().getHandler(8);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0003AAAC File Offset: 0x00038EAC
	protected void doFeeding()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
		{
			Item item = (Item)FarmScr.listItemFarm.elementAt(i);
			FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
			if ((int)farmItem.action == 5 && ((int)farmItem.type == 4 || (int)farmItem.type == 101))
			{
				myVector.addElement(new FarmScr.FeedingCommand(farmItem.des, new FarmScr.IActionFeeding1(this, item), farmItem));
			}
		}
		int num = MyScreen.hText + 10;
		Menu.gI().startMenuFarm(myVector, Canvas.hw, Canvas.h - num - 10, num, num);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0003AB64 File Offset: 0x00038F64
	public void doLeftMenu()
	{
		this.listLeftMenu.addElement(new Command("Chăm sóc vật nuôi", 27, this));
		this.listLeftMenu.addElement(new Command("Gieo hạt", 25, this));
		this.listLeftMenu.addElement(new Command("Chăm sóc cây trồng", 26, this));
		this.listLeftMenu.addElement(MapScr.gI().cmdEvent);
		Command o = new Command(T.exit, 0, this);
		this.listLeftMenu.addElement(o);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0003ABE7 File Offset: 0x00038FE7
	protected void doExit()
	{
		Canvas.startWaitDlg();
		GlobalService.gI().getHandler(8);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0003ABFC File Offset: 0x00038FFC
	protected void doSellect()
	{
		if (FarmScr.isSteal && !FarmScr.isAbleSteal)
		{
			return;
		}
		int posTreeByFocus = this.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
		if (posTreeByFocus - FarmScr.cell.size() == 0)
		{
			this.doRequestPricePlant();
			return;
		}
		if (posTreeByFocus >= 0 && posTreeByFocus < FarmScr.cell.size())
		{
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus);
			if (cellFarm.idTree == -1 && (((int)cellFarm.level == 1 && (int)cellFarm.status != (int)this.typeCell[1]) || ((int)cellFarm.level == 2 && (int)cellFarm.status != (int)this.typeCell1[1])))
			{
				this.doVatPham(cellFarm);
				return;
			}
			if (cellFarm.idTree == -1 && (((int)cellFarm.level == 1 && (int)cellFarm.status == (int)this.typeCell[1]) || ((int)cellFarm.level == 2 && (int)cellFarm.status == (int)this.typeCell1[1])))
			{
				this.doKhoGiong();
				return;
			}
		}
		this.doMenuCenter();
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0003AD2C File Offset: 0x0003912C
	public override void doMenu()
	{
		this.commandTab(1);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0003AD38 File Offset: 0x00039138
	private void doMenuCenter()
	{
		if (LoadMap.TYPEMAP == 25)
		{
			return;
		}
		int posTreeByFocus = this.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
		int num = FarmScr.cell.size();
		if (posTreeByFocus - num == 0)
		{
			this.doRequestPricePlant();
		}
		if (posTreeByFocus >= 0 && posTreeByFocus < num)
		{
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus);
			if (cellFarm.idTree == -1)
			{
				this.doKhoGiong();
			}
			else if (cellFarm.statusTree == 5)
			{
				this.doHarvest();
			}
			else
			{
				this.doVatPham(cellFarm);
			}
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0003ADDA File Offset: 0x000391DA
	public void setStatusCell(CellFarm c, int index)
	{
		if ((int)c.level == 2)
		{
			c.status = (sbyte)this.typeCell1[index];
		}
		else
		{
			c.status = this.typeCell[index];
		}
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0003AE0C File Offset: 0x0003920C
	protected void doKhoGiong()
	{
		if (FarmScr.itemSeed.size() == 0)
		{
			Canvas.startOKDlg(T.StoreEmtpy);
			return;
		}
		if ((int)FarmScr.action == -1)
		{
			MyVector myVector = new MyVector();
			int posTreeByFocus = this.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus);
			CellFarm cellFarm2 = null;
			if (posTreeByFocus > 0)
			{
				cellFarm2 = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus - 1);
			}
			try
			{
				for (int i = 0; i < FarmScr.itemSeed.size(); i++)
				{
					Item item = (Item)FarmScr.itemSeed.elementAt(i);
					if (FarmData.getTreeByID((int)item.ID) != null)
					{
						myVector.addElement(new FarmScr.CommandKhoGiong2(string.Concat(new object[]
						{
							item.name,
							"(",
							item.number,
							")"
						}), new FarmScr.IActionSeed(i, posTreeByFocus), item));
					}
				}
				if (((int)cellFarm.level == 1 && posTreeByFocus == 0) || (posTreeByFocus > 0 && (int)cellFarm.level < (int)cellFarm2.level))
				{
					myVector.addElement(new FarmScr.CommandKhoGiong(T.update, 11));
				}
			}
			catch (Exception e)
			{
				Out.logError(e);
			}
			this.startMenuFarm(myVector);
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0003AF84 File Offset: 0x00039384
	protected void doRequestPricePlant()
	{
		Canvas.startOKDlg(T.gettingPrice);
		FarmService.gI().doRequestPricePlant(FarmScr.idFarm);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0003AF9F File Offset: 0x0003939F
	public void onPricePlant(string str)
	{
		Canvas.msgdlg.setInfoLCR(str, new Command(T.xu, 1, this), new Command(T.gold, 2, this), Canvas.cmdEndDlg);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0003AFCC File Offset: 0x000393CC
	public void doVatPham(CellFarm cell)
	{
		int posTreeByFocus = this.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
		CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus);
		CellFarm cellFarm2 = null;
		if (posTreeByFocus > 0)
		{
			cellFarm2 = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus - 1);
		}
		Command command = null;
		if (((int)cellFarm.level == 1 && posTreeByFocus == 0) || (posTreeByFocus > 0 && (int)cellFarm.level < (int)cellFarm2.level))
		{
			command = new FarmScr.CommandVatPham1(T.update, 11);
		}
		if (cell.idTree != -1 && cell.statusTree < 6 && cell.isArid)
		{
			this.setAction(new FarmScr.IActionVatPham1());
		}
		if (cell.idTree == -1 || cell.statusTree >= 6)
		{
			IAction ac = new FarmScr.IActionVatPham2(cell);
			if (command != null)
			{
				MyVector myVector = new MyVector();
				myVector.addElement(new FarmScr.CommandVatPham2(T.land, ac, 0));
				myVector.addElement(command);
				this.startMenuFarm(myVector);
				return;
			}
			this.setAction(ac);
		}
		if (cell.idTree != -1 && cell.statusTree < 6 && posTreeByFocus < FarmScr.cell.size() && FarmScr.listItemFarm.size() > 0)
		{
			if (cell.isWorm)
			{
				this.setBonPhan(cell, posTreeByFocus, 7);
			}
			if (cell.isGrass)
			{
				this.setBonPhan(cell, posTreeByFocus, 3);
			}
		}
		if ((int)FarmScr.action == -1)
		{
			MyVector myVector2 = new MyVector();
			Command o = new FarmScr.CommandVatPham2(T.watering, 1, 2);
			myVector2.addElement(o);
			if (FarmScr.idFarm == GameMidlet.avatar.IDDB)
			{
				myVector2.addElement(new FarmScr.CommandVatPham2(T.land, new FarmScr.IActionVatPham2(cell), 1));
			}
			if (command != null)
			{
				myVector2.addElement(command);
			}
			for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
			{
				Item item = (Item)FarmScr.listItemFarm.elementAt(i);
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				if ((int)farmItem.type == 0 && (((int)farmItem.action == 3 && cell.isGrass) || ((int)farmItem.action == 7 && cell.isWorm) || ((int)farmItem.action != 3 && (int)farmItem.action != 7)))
				{
					string caption = string.Concat(new object[]
					{
						farmItem.des,
						"(",
						item.number,
						")"
					});
					myVector2.addElement(new FarmScr.iCommandItemFarm(caption, 6, i, farmItem, this));
				}
			}
			this.startMenuFarm(myVector2);
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0003B298 File Offset: 0x00039698
	private void doVatPhamAnimal()
	{
		Animal animalByIndex = FarmScr.getAnimalByIndex(((Base)LoadMap.focusObj).IDDB);
		AnimalInfo animalByID = FarmData.getAnimalByID((int)animalByIndex.species);
		if (!false && (int)FarmScr.action == -1)
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
			{
				Item item = (Item)FarmScr.listItemFarm.elementAt(i);
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				if ((int)farmItem.action != 5 && (int)farmItem.type != 0 && ((int)farmItem.type == (int)animalByID.area || (int)farmItem.type == 101 || ((int)farmItem.type == 100 && (int)animalByID.area != 4)))
				{
					myVector.addElement(new FarmScr.CommandThuoc(string.Concat(new object[]
					{
						farmItem.des,
						"(",
						item.number,
						")"
					}), new FarmScr.IActionThuoc(farmItem, item), farmItem));
				}
			}
			for (int j = 0; j < FarmScr.listItemFarm.size(); j++)
			{
				Item item2 = (Item)FarmScr.listItemFarm.elementAt(j);
				FarmItem farmItem2 = FarmScr.getFarmItem((int)item2.ID);
				if ((int)farmItem2.type == (int)animalByID.area && (int)farmItem2.action == 5 && ((int)animalByID.area == 4 || (int)animalByID.area == 1))
				{
					int num = item2.number;
					if ((int)animalByID.area == 4)
					{
						num -= FarmScr.listFood[1].size();
					}
					else if ((int)animalByID.area == 1)
					{
						num -= FarmScr.listFood[0].size();
					}
					if ((int)farmItem2.action != 4 || animalByIndex.disease[0] || animalByIndex.disease[1])
					{
						myVector.addElement(new FarmScr.CommandItem5(string.Concat(new object[]
						{
							farmItem2.des,
							"(",
							num,
							")"
						}), new FarmScr.IActionItem5(farmItem2, animalByID, item2), farmItem2));
					}
				}
			}
			if (FarmScr.idFarm == GameMidlet.avatar.IDDB)
			{
				myVector.addElement(new FarmScr.CommandSellAnimal(T.sell, 3, this));
			}
			int num2 = MyScreen.hText + 10;
			Menu.gI().startMenuFarm(myVector, Canvas.hw, Canvas.h - num2 - 10, num2, num2);
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0003B550 File Offset: 0x00039950
	public override void commandActionPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			this.doExit();
			break;
		case 1:
			FarmService.gI().doOpenLand(FarmScr.idFarm, 1);
			this.curTime = (long)Environment.TickCount;
			this.doJoinFarm(FarmScr.idFarm, true);
			break;
		case 2:
			FarmService.gI().doOpenLand(FarmScr.idFarm, 2);
			this.curTime = (long)Environment.TickCount;
			this.doJoinFarm(FarmScr.idFarm, true);
			break;
		case 3:
			if (LoadMap.focusObj != null)
			{
				Canvas.endDlg();
				FarmService.gI().doRequestPriceAnimal(FarmScr.idFarm, ((Base)LoadMap.focusObj).IDDB);
			}
			break;
		case 5:
			if (LoadMap.focusObj != null)
			{
				Animal animalByIndex = FarmScr.getAnimalByIndex(((Base)LoadMap.focusObj).IDDB);
				AnimalInfo animalByID = FarmData.getAnimalByID((int)animalByIndex.species);
				for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
				{
					if (subIndex == i)
					{
						Item item = (Item)FarmScr.listItemFarm.elementAt(i);
						this.doUsingVatPhamAnimal(item, ((int)animalByID.area != 1) ? 1 : 0);
					}
				}
			}
			break;
		case 6:
			for (int j = 0; j < FarmScr.listItemFarm.size(); j++)
			{
				if (j == subIndex)
				{
					Item item2 = (Item)FarmScr.listItemFarm.elementAt(j);
					if (item2.number > 0)
					{
						this.doUsingVatPham((int)item2.ID, item2);
					}
					else
					{
						Canvas.startOKDlg(T.empty + item2.name);
					}
				}
			}
			break;
		case 7:
			FarmService.gI().doUpdateStarFruil(1);
			break;
		case 8:
			FarmService.gI().doUpdateStarFruitByMoney(1);
			break;
		case 9:
			FarmService.gI().doUpdateLand(1, 1);
			break;
		case 10:
			FarmService.gI().doUpdateLand(1, 2);
			break;
		case 11:
			FarmService.gI().nauNhanh(1);
			break;
		case 12:
			Canvas.msgdlg.setInfoLR("Bạn có muốn nâng cấp kho hàng không ?", new Command(T.yes, 15, this), Canvas.cmdEndDlg);
			break;
		case 13:
			FarmService.gI().doUpdateStore(1, 1);
			break;
		case 14:
			FarmService.gI().doUpdateStore(1, 2);
			break;
		case 15:
			FarmService.gI().doUpdateStore(0, -1);
			break;
		case 16:
			ListScr.gI().setFriendList(true);
			break;
		case 17:
			FarmService.gI().doStealInfo();
			break;
		case 18:
			FarmScr.gI().doGoFarmWay();
			break;
		case 19:
			FarmService.gI().doLichSuAnTrom();
			break;
		case 20:
			FarmScr.isAbleSteal = true;
			this.left = null;
			this.center = null;
			break;
		case 21:
			FarmService.gI().doSteal(0);
			CustomTab.gI().close();
			Canvas.startWaitDlg();
			break;
		case 22:
			PopupShop.gI().close();
			if (FarmScr.remainTime == 0)
			{
				FarmService.gI().doHarvestCook();
			}
			else
			{
				FarmService.gI().nauNhanh(0);
			}
			break;
		case 23:
			Canvas.startOKDlg(T.doUWantCancel, 24, this);
			break;
		case 24:
			FarmService.gI().doCooking(-1);
			PopupShop.gI().close();
			break;
		case 25:
			this.commandTab(5);
			this.doGieoHat();
			break;
		case 26:
			this.isChamSoc = true;
			this.setAuto(0);
			break;
		case 27:
			FarmScr.isAutoVatNuoi = true;
			for (int k = this.indexAuto; k < FarmScr.animalLists.size(); k++)
			{
				Animal pet = (Animal)FarmScr.animalLists.elementAt(k);
				if (this.doAutoVatNuoi(pet))
				{
					return;
				}
				this.indexAuto++;
				if (this.indexAuto == FarmScr.animalLists.size())
				{
					this.indexAuto = 0;
				}
			}
			this.commandActionPointer(29, -1);
			Canvas.startOKDlg("Không có vật nuôi nào bị bệnh");
			break;
		case 28:
			this.indexAuto++;
			if (this.indexAuto == FarmScr.animalLists.size())
			{
				this.indexAuto = 0;
			}
			this.commandActionPointer(27, -1);
			break;
		case 29:
			FarmScr.isAutoVatNuoi = false;
			this.right = null;
			this.center = null;
			this.left = null;
			this.indexAuto = 0;
			AvCamera.isFollow = false;
			break;
		}
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0003BA02 File Offset: 0x00039E02
	private void setActionAnimal(FarmItem fItem, short ID, Animal ani)
	{
		this.setAction(new FarmScr.IActionSetAnimal(fItem, ID, ani));
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0003BA14 File Offset: 0x00039E14
	protected void doUsingVatPhamAnimal(Item item, int typeItem)
	{
		int num = ((int)GameMidlet.avatar.direct != (int)Base.RIGHT) ? -1 : 1;
		int num2 = FarmScr.listFood[typeItem].size();
		if (item.number - num2 <= 0)
		{
			Canvas.startOKDlg(T.foodForEmpty);
			return;
		}
		int num3 = 0;
		while (num3 < 3 && num3 < item.number - num2)
		{
			Point point = new Point(GameMidlet.avatar.x, GameMidlet.avatar.y - 40);
			FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
			point.itemID = item.ID;
			point.w = (point.h = 2);
			point.g = -(4 + CRes.rnd(3));
			point.v = num * (2 + CRes.rnd(3));
			point.limitY = GameMidlet.avatar.y - 20 + CRes.rnd(4) * 5;
			if ((int)farmItem.type == 4)
			{
				int num4 = LoadMap.getposMap(GameMidlet.avatar.x, GameMidlet.avatar.y + 23);
				if (LoadMap.map[num4] == 14)
				{
					point.limitY = 50 + CRes.rnd(50);
					point.v = num * CRes.rnd(3);
				}
			}
			point.layer = new FarmScr.PoLayer(point);
			FarmScr.listFood[typeItem].addElement(point);
			LoadMap.dynamicLists.addElement(point);
			num3++;
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0003BB88 File Offset: 0x00039F88
	protected void doUsingVatPham(int index, Item it)
	{
		int posTreeByFocus = this.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
		if (posTreeByFocus >= FarmScr.cell.size() || FarmScr.listItemFarm.size() == 0)
		{
			return;
		}
		FarmItem farmItem = FarmScr.getFarmItem((int)it.ID);
		sbyte b = farmItem.action;
		if ((int)b == 7)
		{
			b = 3;
		}
		this.setAction(b, (int)farmItem.ID);
		FarmService.gI().doUsingItem(FarmScr.idFarm, posTreeByFocus, (int)farmItem.ID);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0003BC10 File Offset: 0x0003A010
	public static void startTextSmall(int fir, int cur, CellFarm c, Animal ani)
	{
		if (LoadMap.TYPEMAP != 25 && fir != cur)
		{
			string text = string.Empty;
			if (cur - fir > 0)
			{
				text += "+";
			}
			int x;
			int y;
			if (c != null)
			{
				x = c.xCell * LoadMap.w + LoadMap.w / 2;
				y = c.yCell * LoadMap.w - LoadMap.w / 2;
			}
			else
			{
				x = ani.x;
				y = ani.y - 30;
			}
			Canvas.addFlyTextSmall(text + (cur - fir), x, y, -1, 0, -1);
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0003BCAC File Offset: 0x0003A0AC
	protected void doHarvest()
	{
		if (!FarmScr.isSteal && GameMidlet.avatar.IDDB != FarmScr.idFarm)
		{
			return;
		}
		int posTreeByFocus = this.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
		CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus);
		SoundManager.playSound(6);
		FarmService.gI().doHervest(FarmScr.idFarm, posTreeByFocus);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0003BD1C File Offset: 0x0003A11C
	private void doPlantSeed(int index, int pos)
	{
		if (Canvas.isInitChar)
		{
			Welcome.goFarm();
		}
		Item item = (Item)FarmScr.itemSeed.elementAt(index);
		FarmService.gI().doPlantSeed(FarmScr.idFarm, pos, (int)item.ID);
		if (item.number <= 1)
		{
			Canvas.startOKDlg("Bạn đã hết hạt giống, xin vào cửa hàng đễ mua.");
			this.commandTab(5);
		}
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0003BD7C File Offset: 0x0003A17C
	public int getPosTreeByFocus(int x, int y)
	{
		for (int i = 0; i < this.posTree.Length; i++)
		{
			for (int j = 0; j < FarmScr.numO; j++)
			{
				int num = this.posTree[i].x + j / FarmScr.numH;
				int num2 = this.posTree[i].y + j % FarmScr.numH;
				if (x == num && y == num2)
				{
					return i * FarmScr.numO + j;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0003BE00 File Offset: 0x0003A200
	private void setAction(sbyte ac, int idItemUsing)
	{
		FarmScr.idItemUsing = idItemUsing;
		FarmScr.action = ac;
		GameMidlet.avatar.task = -1;
		GameMidlet.avatar.idFrom = -1;
		GameMidlet.avatar.idTo = -1;
		if ((int)FarmScr.action == 4)
		{
			this.posDoing = new AvPosition(LoadMap.focusObj.x / LoadMap.w, LoadMap.focusObj.y / LoadMap.w);
		}
		else
		{
			this.posDoing = new AvPosition(FarmScr.focusCell.x, FarmScr.focusCell.y);
		}
		GameMidlet.avatar.yCur = this.posDoing.y * LoadMap.w + LoadMap.w / 2;
		GameMidlet.avatar.xCur = this.posDoing.x * LoadMap.w;
		if ((int)GameMidlet.avatar.direct == (int)Base.LEFT)
		{
			GameMidlet.avatar.xCur += LoadMap.w;
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0003BF00 File Offset: 0x0003A300
	private void doLamDat(CellFarm c)
	{
		IAction action = new FarmScr.IActionCuocDat(this);
		if (c.idTree == -1 || c.statusTree >= 6)
		{
			action.perform();
			SoundManager.playSound(4);
			return;
		}
		Canvas.startOKDlg(T.youWantBreakTree, action);
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0003BF44 File Offset: 0x0003A344
	private void updateDoing()
	{
		if ((int)FarmScr.action != -1 && this.timeDoing == -1L && (int)GameMidlet.avatar.action == 0)
		{
			this.timeDoing = (long)(Environment.TickCount / 100);
			int num = -1;
			if (this.posDoing != null)
			{
				num = this.getPosTreeByFocus(this.posDoing.x, this.posDoing.y);
			}
			if ((int)FarmScr.action == 4)
			{
				num = 0;
			}
			if (this.posDoing.x * LoadMap.w < GameMidlet.avatar.x)
			{
				GameMidlet.avatar.direct = Base.LEFT;
			}
			else
			{
				GameMidlet.avatar.direct = Base.RIGHT;
			}
			GameMidlet.avatar.dirFirst = GameMidlet.avatar.direct;
			if (this.aniDoing != null)
			{
				this.aniDoing.isStand = false;
				this.aniDoing = null;
			}
			if (num == -1)
			{
				this.resetAction();
				return;
			}
			SubObject subObject = new SubObject(-2, GameMidlet.avatar.x, GameMidlet.avatar.y - 5, FarmScr.img.frameWidth, FarmScr.img.frameHeight);
			LoadMap.treeLists.addElement(subObject);
			int num2 = 0;
			if ((int)FarmScr.action == 0)
			{
				num2 = 5;
				subObject.y = GameMidlet.avatar.y - 8;
			}
			if ((int)GameMidlet.avatar.direct == (int)Base.RIGHT)
			{
				subObject.x = GameMidlet.avatar.x + 10 + num2;
			}
			else
			{
				subObject.x = GameMidlet.avatar.x - 10 - num2;
			}
		}
		if (this.timeDoing != -1L && ((int)FarmScr.action == 1 || (int)FarmScr.action == 0 || (int)FarmScr.action == 2) && (long)(Environment.TickCount / 100) - this.timeDoing > 2L)
		{
			this.timeDoing = (long)(Environment.TickCount / 100);
			if ((int)GameMidlet.avatar.action == 6)
			{
				GameMidlet.avatar.setAction(0);
			}
			else
			{
				GameMidlet.avatar.setAction(6);
			}
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0003C169 File Offset: 0x0003A569
	public void reset()
	{
		FarmScr.focusCell = new AvPosition();
		FarmScr.action = -1;
		this.timeLimit = 0;
		Cattle.itemID = -1;
		Dog.itemID = -1;
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0003C190 File Offset: 0x0003A590
	public void setCellAll()
	{
		for (int i = 0; i < this.posTree.Length; i++)
		{
			for (int j = 0; j < FarmScr.numO; j++)
			{
				int num = this.posTree[i].x + j / FarmScr.numH;
				int num2 = this.posTree[i].y + j % FarmScr.numH;
				if (i * FarmScr.numO + j < FarmScr.cell.size())
				{
					LoadMap.setType(num, num2, 51);
					CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(i * FarmScr.numO + j);
					cellFarm.xCell = num;
					cellFarm.yCell = num2;
					cellFarm.x = num * LoadMap.w + LoadMap.w / 2;
					cellFarm.y = num2 * LoadMap.w + 18;
					this.setInfoCell(i * FarmScr.numO + j);
					LoadMap.treeLists.addElement(cellFarm);
				}
				else
				{
					if (i * FarmScr.numO + j == FarmScr.cell.size())
					{
						LoadMap.treeLists.addElement(new SubObject(-3, num * LoadMap.w + 20, num2 * LoadMap.w + 20, FarmScr.imgBuyLant.getWidth()));
						LoadMap.setType(num, num2, 51);
						LoadMap.orderVector(LoadMap.treeLists);
					}
					if (LoadMap.map[num2 * (int)LoadMap.wMap + num] == (short)((byte)this.typeCell[0]))
					{
						LoadMap.orderVector(LoadMap.treeLists);
						return;
					}
					if (num == this.posTree[i].x && num2 == this.posTree[i].y)
					{
						LoadMap.map[num2 * (int)LoadMap.wMap + num] = 4;
					}
				}
			}
		}
		LoadMap.orderVector(LoadMap.treeLists);
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0003C350 File Offset: 0x0003A750
	public int setCell(int x, int y)
	{
		int num = FarmScr.cell.size();
		for (int i = 0; i < num; i++)
		{
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(i);
			if (cellFarm.xCell == x && cellFarm.yCell == y)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0003C3A8 File Offset: 0x0003A7A8
	public override void update()
	{
		Canvas.paint.setSoundAnimalFarm();
		this.t++;
		if (this.t >= 10)
		{
			this.t = 0;
		}
		if (this.n >= 8)
		{
			this.n = 0;
		}
		this.n++;
		if ((int)FarmScr.action != -1)
		{
			FarmScr.frame = FarmScr.FRAME[(int)FarmScr.action][this.t];
			this.timeLimit++;
			if (this.timeLimit > 20)
			{
				this.timeLimit = 0;
				this.resetAction();
			}
		}
		this.updateTime();
		Canvas.loadMap.update();
		if (!FarmScr.isAutoVatNuoi && !FarmScr.isSelected)
		{
			this.setFocus();
		}
		this.updateDoing();
		if ((LoadMap.TYPEMAP == 24 || LoadMap.TYPEMAP == 53) && FarmScr.animalLists.size() > 0)
		{
			this.updateStatusAnimal();
		}
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0003C4AC File Offset: 0x0003A8AC
	public void updateStatusAnimal()
	{
		if (OptionScr.instance.volume > 0 && FarmScr.animalLists.size() > 0)
		{
			this.tempTime++;
			if (this.tempTime >= this.repeatTime)
			{
				this.tempTime = 0;
				int i = CRes.rnd(FarmScr.animalLists.size());
				Animal animal = (Animal)FarmScr.animalLists.elementAt(i);
				if (FarmScr.animalLists.size() < 4)
				{
					this.repeatTime = 500;
				}
				else
				{
					this.repeatTime = 350;
				}
				switch (animal.species)
				{
				case 51:
					SoundManager.playSound(70);
					break;
				case 52:
					SoundManager.playSound(72);
					break;
				case 53:
					SoundManager.playSound(71);
					break;
				default:
					SoundManager.playSound(73);
					break;
				}
			}
		}
		FarmScr.numStatusAnimal++;
		if (FarmScr.numStatusAnimal > 250)
		{
			FarmScr.numStatusAnimal = 0;
			int i2 = CRes.rnd(FarmScr.animalLists.size());
			Animal animal2 = (Animal)FarmScr.animalLists.elementAt(i2);
			string text = string.Empty;
			if (animal2.disease[0])
			{
				text += T.diarrhea;
			}
			if (animal2.disease[1])
			{
				if (!text.Equals(string.Empty))
				{
					text += ", ";
				}
				text += T.flu;
			}
			if (animal2.hunger)
			{
				if (!text.Equals(string.Empty))
				{
					text += ", ";
				}
				text += T.hunger;
			}
			if (animal2.health < 20)
			{
				if (!text.Equals(string.Empty))
				{
					text += ", ";
				}
				text += T.tire;
			}
			if (!text.Equals(string.Empty))
			{
				animal2.chat = new ChatPopup(25, text, 0);
				animal2.chat.setPos(animal2.x, animal2.y - 45);
			}
		}
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0003C6F0 File Offset: 0x0003AAF0
	public void resetAction()
	{
		for (int i = 0; i < LoadMap.treeLists.size(); i++)
		{
			SubObject subObject = (SubObject)LoadMap.treeLists.elementAt(i);
			if (subObject.type == -2)
			{
				LoadMap.treeLists.removeElementAt(i);
				if (i > 0)
				{
					i--;
				}
			}
		}
		this.timeDoing = -1L;
		int num = -1;
		if (this.posDoing != null)
		{
			num = this.setCell(this.posDoing.x, this.posDoing.y);
		}
		if (num == -1)
		{
			FarmScr.action = -1;
			GameMidlet.avatar.action = 0;
			GameMidlet.avatar.task = 0;
			this.doAction();
			return;
		}
		if (FarmScr.idItemUsing == -1)
		{
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(num);
			sbyte b = FarmScr.action;
			if (b != 0)
			{
				if (b != 1)
				{
					if (b != 2)
					{
					}
				}
				else
				{
					this.setStatusCell(cellFarm, 4);
					cellFarm.isArid = false;
					LoadMap.map[cellFarm.yCell * (int)LoadMap.wMap + cellFarm.xCell] = (short)cellFarm.status;
					FarmService.gI().doUsingItem(FarmScr.idFarm, num, 100);
				}
			}
			else
			{
				this.setStatusCell(cellFarm, 1);
				cellFarm.statusTree = 0;
				LoadMap.map[cellFarm.yCell * (int)LoadMap.wMap + cellFarm.xCell] = (short)cellFarm.status;
				if (cellFarm.idTree != -1)
				{
					FarmService.gI().doPlantSeed(FarmScr.idFarm, num, -1);
				}
				cellFarm.idTree = -1;
				if (Canvas.isInitChar)
				{
					Canvas.welcome = new Welcome();
					Canvas.welcome.initFarm();
				}
			}
		}
		FarmScr.idItemUsing = -1;
		this.posDoing = null;
		FarmScr.action = -1;
		GameMidlet.avatar.task = 0;
		GameMidlet.avatar.action = 0;
		this.doAction();
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0003C8D8 File Offset: 0x0003ACD8
	private void doAction()
	{
		if (FarmScr.isAutoVatNuoi)
		{
			this.commandAction(10);
			return;
		}
		if (this.listAction.size() > 0)
		{
			IAction action = (IAction)this.listAction.elementAt(0);
			action.perform();
			this.listAction.removeElement(action);
		}
		else if (this.isChamSoc)
		{
			this.setGieoHat();
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0003C944 File Offset: 0x0003AD44
	private void setFocus()
	{
		if (LoadMap.TYPEMAP == 25)
		{
			return;
		}
		int num;
		if ((int)GameMidlet.avatar.direct == (int)Base.LEFT)
		{
			num = GameMidlet.avatar.x - 23;
		}
		else
		{
			num = GameMidlet.avatar.x + 23;
		}
		num /= LoadMap.w;
		int num2 = GameMidlet.avatar.y / LoadMap.w;
		int num3 = (int)LoadMap.type[num2 * (int)LoadMap.wMap + num];
		int posTreeByFocus = this.getPosTreeByFocus(num, num2);
		if (num3 == 51 && posTreeByFocus <= FarmScr.cell.size())
		{
			FarmScr.focusCell.x = num;
			FarmScr.focusCell.y = num2;
			if ((int)FarmScr.action != 0 && (int)FarmScr.action != 1)
			{
				this.cmdSelected = FarmScr.cmdMenu;
			}
			else
			{
				this.cmdSelected = null;
			}
		}
		else
		{
			if (this.cmdSelected == FarmScr.cmdMenu || this.cmdSelected == FarmScr.cmdFeeding)
			{
				this.cmdSelected = null;
			}
			FarmScr.focusCell.x = -1;
			FarmScr.focusCell.y = -1;
			if (LoadMap.focusObj != null || !this.setFeeding())
			{
				if (LoadMap.focusObj != null && this.cmdSelected == null)
				{
					this.cmdSelected = FarmScr.cmdFocusBet;
				}
				if (LoadMap.focusObj == null && this.cmdSelected == FarmScr.cmdFocusBet)
				{
					this.cmdSelected = null;
					this.right = null;
				}
			}
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0003CAC4 File Offset: 0x0003AEC4
	private bool setFeeding()
	{
		int num = LoadMap.getposMap(GameMidlet.avatar.x + 12, GameMidlet.avatar.y);
		int num2 = LoadMap.getposMap(GameMidlet.avatar.x, GameMidlet.avatar.y + 12);
		if ((LoadMap.map[num] == 100 && (int)GameMidlet.avatar.direct == (int)Base.RIGHT) || LoadMap.map[num2] == 14)
		{
			this.cmdSelected = FarmScr.cmdFeeding;
			return true;
		}
		this.cmdSelected = null;
		return false;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0003CB52 File Offset: 0x0003AF52
	public override void updateKey()
	{
		this.updatePoint();
		base.updateKey();
		Canvas.loadMap.updateKey();
		if ((int)FarmScr.action == -1)
		{
			GameMidlet.avatar.updateKey();
		}
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0003CB80 File Offset: 0x0003AF80
	private void updatePoint()
	{
		if (this.isTrans && (int)GameMidlet.avatar.action == 0 && GameMidlet.avatar.task == 0 && GameMidlet.avatar.x == GameMidlet.avatar.xCur && GameMidlet.avatar.y == GameMidlet.avatar.yCur)
		{
			this.isTrans = false;
			GameMidlet.avatar.direct = Base.RIGHT;
			this.setFocus();
			if ((int)FarmScr.action == -1)
			{
				if (FarmScr.indexItem != -1)
				{
					this.setDoing();
				}
				else
				{
					FarmScr.indexItem = -1;
					this.doSellect();
				}
			}
		}
		if (Canvas.isPointerClick)
		{
			int num = (int)((float)Canvas.px / AvMain.zoom + AvCamera.gI().xCam);
			int num2 = (int)((float)Canvas.py / AvMain.zoom + AvCamera.gI().yCam);
			int num3 = LoadMap.w * AvMain.hd;
			if (num2 / num3 * (int)LoadMap.wMap + num / num3 >= 0 && num2 / num3 * (int)LoadMap.wMap + num / num3 <= LoadMap.type.Length)
			{
				int num4 = (int)LoadMap.type[num2 / num3 * (int)LoadMap.wMap + num / num3];
				if (num4 == 51)
				{
					int posTreeByFocus = this.getPosTreeByFocus(num / num3, num2 / num3);
					if (posTreeByFocus == FarmScr.cell.size())
					{
						this.doRequestPricePlant();
						return;
					}
					this.isTran = true;
					FarmScr.isSelected = true;
					if (posTreeByFocus >= 0 && posTreeByFocus < FarmScr.cell.size())
					{
						CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus);
						FarmScr.focusCell.x = cellFarm.x / LoadMap.w;
						FarmScr.focusCell.y = cellFarm.y / LoadMap.w;
					}
				}
			}
		}
		if (this.isTran && Canvas.isPointerRelease)
		{
			this.isTran = false;
			FarmScr.isSelected = false;
			int num5 = (int)((float)Canvas.px / AvMain.zoom + AvCamera.gI().xCam);
			int num6 = (int)((float)Canvas.py / AvMain.zoom + AvCamera.gI().yCam);
			int num7 = LoadMap.w * AvMain.hd;
			if (FarmScr.isAbleSteal && this.center != null && FarmScr.focusCell != null && num5 / num7 == FarmScr.focusCell.x && num6 / num7 == FarmScr.focusCell.y)
			{
				this.center.perform();
			}
			else if (num6 / num7 * (int)LoadMap.wMap + num5 / num7 >= 0 && num6 / num7 * (int)LoadMap.wMap + num5 / num7 <= LoadMap.type.Length)
			{
				int num8 = (int)LoadMap.type[num6 / num7 * (int)LoadMap.wMap + num5 / num7];
				if (num8 == 51)
				{
					int posTreeByFocus2 = this.getPosTreeByFocus(num5 / num7, num6 / num7);
					if (posTreeByFocus2 >= 0 && posTreeByFocus2 < FarmScr.cell.size())
					{
						CellFarm cellFarm2 = (CellFarm)FarmScr.cell.elementAt(posTreeByFocus2);
						FarmScr.focusCell.x = cellFarm2.x / LoadMap.w;
						FarmScr.focusCell.y = cellFarm2.y / LoadMap.w;
						if (this.isSelectedCell && posTreeByFocus2 >= 0 && posTreeByFocus2 < FarmScr.cell.size())
						{
							FarmScr.idSelected = posTreeByFocus2;
							if (cellFarm2.idTree == -1 || cellFarm2.statusTree == 5 || cellFarm2.statusTree >= 6)
							{
								Canvas.isPointerRelease = false;
								if (this.isChamSoc && cellFarm2.statusTree != 5)
								{
									Canvas.startOKDlg("ô này không có cây.");
								}
								else
								{
									if (!cellFarm2.isSelected)
									{
										this.listSelectedCell.addElement(new AvPosition(num5 / LoadMap.w, num6 / LoadMap.w, posTreeByFocus2));
									}
									cellFarm2.isSelected = true;
									this.setGieoHat();
								}
							}
							else
							{
								Canvas.isPointerRelease = false;
								if (this.isChamSoc)
								{
									if (!cellFarm2.isSelected)
									{
										this.listSelectedCell.addElement(new AvPosition(num5 / LoadMap.w, num6 / LoadMap.w, posTreeByFocus2));
									}
									cellFarm2.isSelected = true;
									this.setGieoHat();
								}
								else if (cellFarm2.statusTree != 5)
								{
									Canvas.startOKDlg("Hãy chọn ô không có cây hoặc cây đã chết.");
								}
							}
						}
						else
						{
							Canvas.px -= (int)((float)(LoadMap.w * AvMain.hd) * AvMain.zoom);
							Canvas.pxLast = Canvas.px;
							this.isTrans = true;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0003D030 File Offset: 0x0003B430
	private void setGieoHat()
	{
		if (this.listSelectedCell.size() > 0 && FarmScr.indexItem != -1)
		{
			this.isTrans = true;
			AvPosition avPosition = (AvPosition)this.listSelectedCell.elementAt(0);
			LoadMap.posFocus.x = avPosition.x * 24 - 24;
			LoadMap.posFocus.y = avPosition.y * 24 + 12;
			GameMidlet.avatar.task = -5;
			GameMidlet.avatar.isJumps = -1;
			GameMidlet.avatar.xCur = GameMidlet.avatar.x;
			GameMidlet.avatar.yCur = GameMidlet.avatar.y;
			Canvas.loadMap.change();
		}
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0003D0E8 File Offset: 0x0003B4E8
	private void setDoing()
	{
		if (this.listSelectedCell.size() > 0 && FarmScr.indexItem != -1)
		{
			AvPosition avPosition = (AvPosition)this.listSelectedCell.elementAt(0);
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(avPosition.anchor);
			cellFarm.isSelected = false;
			FarmScr.focusCell.x = cellFarm.x / LoadMap.w;
			FarmScr.focusCell.y = cellFarm.y / LoadMap.w;
			if (this.isChamSoc)
			{
				if (cellFarm.statusTree == 5)
				{
					this.doHarvest();
					this.setGieoHat();
				}
				else
				{
					bool flag = false;
					if (cellFarm.idTree != -1 && cellFarm.statusTree < 6 && (int)cellFarm.status == 36)
					{
						this.setAction(new FarmScr.IActionSet1(cellFarm));
						flag = true;
					}
					if (cellFarm.idTree != -1 && cellFarm.statusTree < 6)
					{
						if (avPosition.anchor >= FarmScr.cell.size())
						{
							return;
						}
						this.setAction(new FarmScr.IActionVatPham1());
						if (cellFarm.isWorm && this.setBonPhan(cellFarm, avPosition.anchor, 7))
						{
							flag = true;
						}
						if (cellFarm.isGrass && this.setBonPhan(cellFarm, avPosition.anchor, 3))
						{
							flag = true;
						}
						if ((int)cellFarm.vitalityPer < 50)
						{
							bool flag2 = false;
							for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
							{
								Item item = (Item)FarmScr.listItemFarm.elementAt(i);
								FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
								if ((int)farmItem.action == 2)
								{
									FarmService.gI().doUsingItem(FarmScr.idFarm, avPosition.anchor, (int)farmItem.ID);
									break;
								}
								flag2 = true;
							}
							if (!flag2)
							{
								Canvas.startOKDlg("Kho của bạn đã hết phân bón, xin vào cửa hàng đễ mua.");
							}
						}
					}
					if (!flag)
					{
						this.setGieoHat();
					}
				}
			}
			else if (cellFarm.statusTree == 5)
			{
				this.doHarvest();
				this.setGieoHat();
			}
			else
			{
				this.setAction(new FarmScr.IActionSet2(cellFarm));
				this.setAction(new FarmScr.IActionSet3(avPosition));
			}
			this.listSelectedCell.removeElement(avPosition);
		}
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0003D326 File Offset: 0x0003B726
	private void setAction(IAction ac)
	{
		if ((int)FarmScr.action != -1)
		{
			this.listAction.addElement(ac);
		}
		else
		{
			ac.perform();
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0003D34C File Offset: 0x0003B74C
	public bool setBonPhan(CellFarm c, int pos, int action)
	{
		bool flag = false;
		for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
		{
			Item item = (Item)FarmScr.listItemFarm.elementAt(i);
			FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
			if ((int)farmItem.type == 0 && (int)farmItem.action == action)
			{
				this.setAction(new FarmScr.IActionBonPhan(pos, farmItem));
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Canvas.startOKDlg("Kho của bạn đã hết thuốc diệt cỏ, xin vào cửa hàng đễ mua.");
		}
		return flag;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0003D3D1 File Offset: 0x0003B7D1
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		if (Canvas.welcome == null || !Welcome.isPaintArrow)
		{
			base.paint(g);
		}
		Canvas.paintPlus(g);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0003D3FC File Offset: 0x0003B7FC
	public override void paintMain(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(Vector2.one * AvMain.zoom, Vector2.zero);
		Canvas.resetTrans(g);
		Canvas.paint.paintBGCMD(g, 0, Canvas.h, Canvas.w, Canvas.hTab);
		Canvas.resetTrans(g);
		g.translate(-AvCamera.gI().xCam, -AvCamera.gI().yCam);
		Canvas.loadMap.paintBackGround(g);
		if (LoadMap.imgCreateMap != null)
		{
			int num = 0;
			int num2 = (int)FarmScr.numBarn;
			int num3 = (int)FarmScr.numPond;
			for (int i = 0; i < LoadMap.imgCreateMap.Length; i++)
			{
				if (AvCamera.gI().xCam + (float)Canvas.w > (float)num && AvCamera.gI().xCam < (float)(num + LoadMap.imgCreateMap[i].w))
				{
					g.drawImage(LoadMap.imgCreateMap[i], (float)(num - 1), (float)((int)LoadMap.Hmap * LoadMap.w * AvMain.hd - LoadMap.imgCreateMap[i].h), 0);
				}
				num += LoadMap.imgCreateMap[i].w - 2;
				if (i == 1 && num2 > 0)
				{
					i--;
					num2--;
				}
				else if (i == 4 && num3 > 0)
				{
					i--;
					num3--;
				}
			}
		}
		for (int j = 0; j < FarmScr.cell.size(); j++)
		{
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(j);
			g.drawImage(FarmScr.imgCell[(int)cellFarm.status], (float)(cellFarm.x * AvMain.hd - 14 * AvMain.hd), (float)(cellFarm.y * AvMain.hd - 20 * AvMain.hd), 0);
		}
		Canvas.loadMap.paintTouchMap(g);
		Canvas.loadMap.paintObject(g);
		this.paintFocus(g);
		this.paintName(g);
		Canvas.resetTrans(g);
		GUIUtility.ScaleAroundPivot(Vector2.one / AvMain.zoom, Vector2.zero);
		Canvas.loadMap.paintEffectCamera(g);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0003D610 File Offset: 0x0003BA10
	private void paintName(MyGraphics g)
	{
		if (LoadMap.TYPEMAP == 25)
		{
			return;
		}
		Canvas.smallFontYellow.drawString(g, this.nameFarm, (FarmScr.posName.x + 27) * AvMain.hd, (FarmScr.posName.y - 14) * AvMain.hd + (AvMain.hd - 1) * 7 - 2, 2);
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0003D670 File Offset: 0x0003BA70
	public void paintFocus(MyGraphics g)
	{
		if (FarmScr.idSelected == -1)
		{
			if (FarmScr.focusCell == null || FarmScr.focusCell.x == -1 || LoadMap.TYPEMAP != 25)
			{
			}
		}
		else if (FarmScr.focusCell != null && FarmScr.focusCell.x != -1 && LoadMap.TYPEMAP != 25)
		{
			g.drawImage(MapScr.imgFocusP, (float)((FarmScr.focusCell.x * LoadMap.w + LoadMap.w / 2) * AvMain.hd), (float)((FarmScr.focusCell.y * LoadMap.w - 4 + this.n / 2) * AvMain.hd), 3);
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0003D728 File Offset: 0x0003BB28
	public void onInventory(MyVector itemMua, MyVector itemProduct1, MyVector itemVP, MyVector itemFarm, MyVector listFarmProdut, sbyte levelStore, int capacity, bool isNew)
	{
		FarmScr.itemSeed = itemMua;
		FarmScr.isNew = isNew;
		FarmScr.levelStore = levelStore;
		FarmScr.capacityStore = capacity;
		int num = FarmScr.itemSeed.size();
		for (int i = 0; i < num; i++)
		{
			Item item = (Item)FarmScr.itemSeed.elementAt(i);
			TreeInfo treeByID = FarmData.getTreeByID((int)item.ID);
			if (treeByID != null)
			{
				item.name = treeByID.name;
			}
		}
		FarmScr.itemProduct = itemProduct1;
		int num2 = FarmScr.itemProduct.size();
		for (int j = 0; j < num2; j++)
		{
			Item nameItem = (Item)FarmScr.itemProduct.elementAt(j);
			FarmScr.setNameItem(nameItem);
		}
		FarmScr.listItemFarm = itemFarm;
		FarmScr.listFarmProduct = listFarmProdut;
		FarmScr.initImg();
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0003D7F4 File Offset: 0x0003BBF4
	private static void setNameItem(Item item)
	{
		if (item.ID < 50)
		{
			item.price[0] = (int)FarmData.getTreeByID((int)item.ID).priceProduct;
			item.name = FarmData.getTreeByID((int)item.ID).name;
		}
		else if (item.ID < 100)
		{
			item.price[0] = (int)FarmData.getAnimalByID((int)item.ID).priceProduct;
			if ((int)FarmData.getAnimalByID((int)item.ID).area == 1)
			{
				item.name = T.egg + " " + FarmData.getAnimalByID((int)item.ID).name;
			}
			else if ((int)FarmData.getAnimalByID((int)item.ID).area == 2)
			{
				if (item.ID == 55)
				{
					item.name = "Lông " + FarmData.getAnimalByID((int)item.ID).name;
				}
				else
				{
					item.name = T.milk + " " + FarmData.getAnimalByID((int)item.ID).name;
				}
			}
		}
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0003D918 File Offset: 0x0003BD18
	public static FarmItem getFarmItem(int id)
	{
		for (int i = 0; i < FarmData.listItemFarm.size(); i++)
		{
			FarmItem farmItem = (FarmItem)FarmData.listItemFarm.elementAt(i);
			if ((int)farmItem.ID == id)
			{
				return farmItem;
			}
		}
		return null;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0003D960 File Offset: 0x0003BD60
	public void onBuyItem(Item item, int newMoney, sbyte typeBuy, int xu, int luong, int luongK)
	{
		GameMidlet.avatar.updateMoney(xu, luong, luongK);
		PopupShop.isTransFocus = true;
		SoundManager.playSound(2);
		if (item.number <= 0)
		{
			return;
		}
		if (item.ID >= 111)
		{
			Item itemByList = Item.getItemByList(FarmScr.listItemFarm, (int)item.ID);
			if (itemByList != null)
			{
				itemByList.number += item.number;
			}
			else
			{
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				item.name = farmItem.des;
				FarmScr.listItemFarm.addElement(item);
			}
		}
		else if (item.ID <= 100)
		{
			if (item.ID < 50)
			{
				Item itemByList2 = Item.getItemByList(FarmScr.itemSeed, (int)item.ID);
				if (itemByList2 != null)
				{
					itemByList2.number += item.number;
				}
				else
				{
					FarmScr.itemSeed.addElement(item);
					item.name = FarmData.getTreeByID((int)item.ID).name;
				}
				if (FarmScr.itemSeed.size() == 0)
				{
					FarmScr.itemSeed.addElement(item);
				}
			}
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0003DA88 File Offset: 0x0003BE88
	private void updateTime()
	{
		if (FarmScr.remainTime > 0 && Canvas.getTick() / 1000L - (long)FarmScr.curTimeCooking >= 1L)
		{
			FarmScr.remainTime--;
			FarmScr.curTimeCooking = (int)(Canvas.getTick() / 1000L);
		}
		if (LoadMap.TYPEMAP != 24 || LoadMap.TYPEMAP != 53)
		{
			return;
		}
		if (((long)Environment.TickCount - this.curTime) / 1000L > 300L)
		{
			this.curTime = (long)Environment.TickCount;
			this.doJoinFarm(FarmScr.idFarm, true);
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0003DB28 File Offset: 0x0003BF28
	public void onJoin(int idFarm1, MyVector cell1, MyVector listAni, sbyte numBarn, sbyte numPond, short foodID, int remainTime)
	{
		FarmScr.foodID = foodID;
		FarmScr.remainTime = remainTime;
		FarmScr.numBarn = numBarn;
		FarmScr.numPond = numPond;
		FarmScr.idFarm = idFarm1;
		Dog.isCan = false;
		if (idFarm1 != GameMidlet.avatar.IDDB)
		{
			if (!FarmScr.isSteal)
			{
				Avatar avatar = ListScr.getAvatar(idFarm1);
				if (avatar == null)
				{
					Canvas.startOKDlg(T.notOnFarm);
					return;
				}
				if (avatar.showName == null)
				{
					avatar.setName(avatar.name);
				}
				this.nameFarm = avatar.showName;
			}
			else
			{
				LoadMap.TYPEMAP = 25;
				this.nameFarm = FarmScr.nameTemp;
			}
			FarmScr.listFood[0].removeAllElements();
			FarmScr.listFood[1].removeAllElements();
		}
		else
		{
			this.nameFarm = GameMidlet.avatar.showName;
		}
		FarmScr.cell = cell1;
		if (LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53 && FarmScr.animalLists.size() == 0)
		{
			FarmScr.animalLists = listAni;
		}
		this.setAnimal();
		if (this.isJoin)
		{
			if (FarmScr.isSteal || FarmScr.isReSize || (LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53))
			{
				FarmScr.isReSize = false;
				this.reset();
				this.posTree = new AvPosition[4];
				Canvas.loadMap.load(25, false);
				Canvas.load = 0;
				this.resizeMap(numBarn, numPond);
				FarmScr.listNest = new MyVector();
				FarmScr.listBucket = new MyVector();
				this.setChickNest(1, Chicken.posNest, 87, -8, FarmScr.listNest);
				this.setChickNest(2, Cattle.posBucket, 86, -7, FarmScr.listBucket);
				int num = FarmScr.animalLists.size();
				for (int i = 0; i < num; i++)
				{
					Animal animal = (Animal)FarmScr.animalLists.elementAt(i);
					animal.setInit();
					LoadMap.playerLists.addElement(animal);
				}
				Canvas.load = 1;
				Canvas.endDlg();
			}
			for (int j = 0; j < LoadMap.treeLists.size(); j++)
			{
				SubObject subObject = (SubObject)LoadMap.treeLists.elementAt(j);
				if ((subObject.type < 800 && subObject.type >= 100) || subObject.type == -3 || subObject is CellFarm)
				{
					LoadMap.treeLists.removeElement(subObject);
					j--;
				}
			}
			GameMidlet.avatar.xCur = GameMidlet.avatar.x;
			GameMidlet.avatar.yCur = GameMidlet.avatar.y;
			this.setCellAll();
			this.curTime = (long)Environment.TickCount;
			if (Canvas.currentMyScreen != this)
			{
				this.switchToMe();
			}
			if (Canvas.isInitChar)
			{
				Canvas.welcome = new Welcome();
				Canvas.welcome.initFarm();
			}
		}
		this.isJoin = true;
		if (FarmScr.xRemember != -1)
		{
			GameMidlet.avatar.x = (GameMidlet.avatar.xCur = FarmScr.xRemember);
			GameMidlet.avatar.y = (GameMidlet.avatar.yCur = FarmScr.yRemember);
			FarmScr.xRemember = -1;
			FarmScr.yRemember = -1;
		}
		if (FarmScr.isSteal)
		{
			this.left = this.cmdNextSteal;
			this.right = this.cmdCloseSteal;
			this.center = this.cmdStreal;
			if (FarmScr.priceSteal != 0)
			{
				GameMidlet.avatar.money[1] -= FarmScr.priceSteal;
				Canvas.addFlyText(-FarmScr.priceSteal, GameMidlet.avatar.x, GameMidlet.avatar.y, -1, -1);
				FarmScr.priceSteal = 0;
			}
		}
		else
		{
			this.left = null;
			this.right = null;
			this.center = null;
		}
		this.commandTab(5);
		AvCamera.gI().setPosFollowPlayer();
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0003DF08 File Offset: 0x0003C308
	private void setChickNest(int type, AvPosition pos, sbyte typeMap, int typeObj, MyVector listPos)
	{
		int num = 0;
		for (int i = 0; i < FarmScr.animalLists.size(); i++)
		{
			Animal animal = (Animal)FarmScr.animalLists.elementAt(i);
			AnimalInfo animalByID = FarmData.getAnimalByID((int)animal.species);
			if ((int)animalByID.area == type && animalByID.iconProduct != -1)
			{
				bool flag = false;
				for (int j = 0; j < listPos.size(); j++)
				{
					AvPosition avPosition = (AvPosition)listPos.elementAt(j);
					if (avPosition.anchor == (int)animal.species)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					listPos.addElement(new AvPosition(pos.x + num * 24, pos.y, (int)animal.species));
					int num2 = LoadMap.getposMap(pos.x + num * 24, pos.y);
					LoadMap.type[num2] = typeMap;
					LoadMap.addObjTree(typeObj, pos.x + num * 24, pos.y, -1);
					if (GameMidlet.avatar.IDDB != FarmScr.idFarm && FarmScr.isSteal && animal.numEggOne > 0)
					{
						LoadMap.addPopup("trom", pos.x + num * 24, pos.y + 12, ((int)typeMap != 87) ? -51 : -50);
					}
					num++;
				}
			}
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0003E07C File Offset: 0x0003C47C
	public void resizeMap(sbyte numBarn, sbyte numPond)
	{
		try
		{
			FarmScr.numTilePond = FishFarm.WTile + (int)numPond;
			FarmScr.numTileBarn = (int)Cattle.numTileW + (int)numBarn;
			int num = FarmScr.posPond.x / 24;
			int num2 = FarmScr.posBarn.x / 24 + 2;
			DataInputStream dataInputStream = LoadMap.loadDataMap(25);
			LoadMap.map = new short[dataInputStream.available()];
			for (int i = 0; i < LoadMap.map.Length; i++)
			{
				LoadMap.map[i] = (short)((byte)dataInputStream.readByte());
			}
			short[] array = new short[LoadMap.map.Length + (int)LoadMap.Hmap * ((int)numPond + (int)numBarn)];
			int num3 = 0;
			for (int j = 0; j < LoadMap.map.Length; j++)
			{
				array[num3] = LoadMap.map[j];
				num3++;
				if (j % (int)LoadMap.wMap == num)
				{
					for (int k = 0; k < (int)numPond; k++)
					{
						array[num3] = LoadMap.map[j];
						num3++;
					}
				}
				if (j % (int)LoadMap.wMap == num2)
				{
					for (int l = 0; l < (int)numBarn; l++)
					{
						array[num3] = LoadMap.map[j];
						num3++;
					}
				}
			}
			LoadMap.wMap += (short)((int)numPond + (int)numBarn);
			LoadMap.map = array;
			LoadMap.treeLists.removeAllElements();
			Canvas.loadMap.setMap(null, LoadMap.TYPEMAP + 1, true);
			GameMidlet.avatar.x += (int)numBarn * 24;
			LoadMap.addObjTree(849, FarmScr.posPond.x + 12 + CRes.rnd(FarmScr.numTilePond - 2) * 24, FarmScr.posPond.y + 12 + CRes.rnd(3) * 24, -1);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0003E278 File Offset: 0x0003C678
	public void setAnimal()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmScr.animalLists.size(); i++)
		{
			Animal animal = (Animal)FarmScr.animalLists.elementAt(i);
			AnimalInfo animalByID = FarmData.getAnimalByID((int)animal.species);
			if (animal is AnimalDan)
			{
				bool flag = false;
				for (int j = 0; j < myVector.size(); j++)
				{
					AvPosition avPosition = (AvPosition)myVector.elementAt(j);
					if (avPosition.anchor == (int)animal.species)
					{
						((AnimalDan)animal).captainID = avPosition.x;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					((AnimalDan)animal).captainID = animal.IDDB;
					myVector.addElement(new AvPosition(animal.IDDB, 0, (int)animal.species));
				}
			}
			int num = animalByID.harvestTime * 60 / 3;
			if (num > 0)
			{
				animal.period = animal.bornTime / num;
			}
			if (animal.period > 2)
			{
				animal.period = 2;
			}
			if (animal.bornTime == -1 || (int)animalByID.area == 3)
			{
				animal.period = 0;
			}
			Canvas.paint.setAnimalSound(FarmScr.animalLists);
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0003E3C0 File Offset: 0x0003C7C0
	public void onPlantSeed(int idUser, int indexCell, int idSeed)
	{
		if (LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53)
		{
			return;
		}
		Item itemByList = Item.getItemByList(FarmScr.itemSeed, idSeed);
		if (itemByList != null)
		{
			CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(indexCell);
			cellFarm.idTree = idSeed;
			this.setStatusCell(cellFarm, 4);
			LoadMap.map[cellFarm.yCell * (int)LoadMap.wMap + cellFarm.xCell] = (short)cellFarm.status;
			cellFarm.statusTree = 0;
			cellFarm.isGrass = false;
			cellFarm.isWorm = false;
			cellFarm.time = 0;
			cellFarm.tempTime = 0L;
			cellFarm.vitalityPer = 100;
			cellFarm.hervestPer = 0;
			itemByList.number--;
			if (itemByList.number <= 0)
			{
				FarmScr.itemSeed.removeElement(itemByList);
			}
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0003E490 File Offset: 0x0003C890
	public void setInfoCell(int index)
	{
		CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(index);
		if (cellFarm.idTree == -1)
		{
			this.setStatusCell(cellFarm, 2);
		}
		else
		{
			TreeInfo treeInfoByID = FarmData.getTreeInfoByID(cellFarm.idTree);
			int num = (int)(treeInfoByID.harvestTime * 60 / 5);
			cellFarm.statusTree = (int)cellFarm.time / num;
			if (cellFarm.statusTree >= 5)
			{
				cellFarm.statusTree = 5;
			}
			if (cellFarm.time < 0 || (treeInfoByID.dieTime != -1 && cellFarm.time - treeInfoByID.harvestTime * 60 > treeInfoByID.dieTime * 60) || (int)cellFarm.hervestPer == 100 || cellFarm.statusTree < 0)
			{
				cellFarm.statusTree = 6;
			}
			if (cellFarm.isArid)
			{
				this.setStatusCell(cellFarm, 3);
			}
			else
			{
				this.setStatusCell(cellFarm, 4);
			}
		}
		LoadMap.map[cellFarm.yCell * (int)LoadMap.wMap + cellFarm.xCell] = (short)cellFarm.status;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0003E598 File Offset: 0x0003C998
	public void onHarvestTree(int indexCell2, int number)
	{
		CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(indexCell2);
		if (FarmScr.idFarm == GameMidlet.avatar.IDDB)
		{
			cellFarm.statusTree = 6;
			cellFarm.hervestPer = 100;
			cellFarm.isGrass = false;
			cellFarm.isWorm = false;
		}
		if (number > 0)
		{
			TreeInfo treeByID = FarmData.getTreeByID(cellFarm.idTree);
			if (treeByID.isDynamic)
			{
				Canvas.addFlyText(number, cellFarm.xCell * LoadMap.w + 11, cellFarm.yCell * LoadMap.w, -1, 0, (int)treeByID.idImg[cellFarm.statusTree], -1);
			}
			else
			{
				ImageInfo imageInfo = FarmData.listImgInfo[(int)treeByID.idImg[5]];
				Canvas.addFlyText(number, cellFarm.xCell * LoadMap.w + 11, cellFarm.yCell * LoadMap.w, -1, CRes.createImgByImg((int)imageInfo.x0 * AvMain.hd, (int)imageInfo.y0 * AvMain.hd, (int)imageInfo.w * AvMain.hd, (int)imageInfo.h * AvMain.hd, FarmData.imgBig[(int)imageInfo.bigID]), -1);
			}
		}
		TreeInfo treeByID2 = FarmData.getTreeByID(cellFarm.idTree);
		FarmScr.addProductTree(treeByID2, number);
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0003E6C4 File Offset: 0x0003CAC4
	public static void addProductTree(TreeInfo tree, int numSt)
	{
		if (tree.isDynamic)
		{
			Item item = FarmScr.getItemProductByID((int)tree.productID);
			if (item != null)
			{
				item.number += numSt;
			}
			else
			{
				item = new Item();
				item.ID = tree.productID;
				item.number = numSt;
				item.price[0] = (int)tree.priceProduct;
				item.name = tree.name;
				FarmScr.listFarmProduct.addElement(item);
			}
		}
		else
		{
			Item item2 = Item.getItemByList(FarmScr.itemProduct, (int)tree.ID);
			if (item2 != null)
			{
				item2.number += numSt;
			}
			else
			{
				item2 = new Item();
				item2.ID = tree.ID;
				item2.number = numSt;
				item2.price[0] = (int)FarmData.getTreeByID((int)tree.ID).priceProduct;
				item2.name = FarmData.getTreeByID((int)tree.ID).name;
				FarmScr.itemProduct.addElement(item2);
			}
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0003E7C0 File Offset: 0x0003CBC0
	public static void addProductAnimal(AnimalInfo ani, int numSt)
	{
		Item itemByList = Item.getItemByList(FarmScr.itemProduct, (int)ani.species);
		if (itemByList != null)
		{
			itemByList.number += numSt;
		}
		else
		{
			Item item = new Item();
			item.ID = (short)ani.species;
			item.number = numSt;
			item.name = ani.name;
			item.price[0] = (int)ani.priceProduct;
			FarmScr.setNameItem(item);
			FarmScr.itemProduct.addElement(item);
		}
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0003E840 File Offset: 0x0003CC40
	public void onHarvestAnimal(int indexCell3, int number4)
	{
		Animal animalByIndex = FarmScr.getAnimalByIndex(indexCell3);
		if (number4 <= 0 || animalByIndex == null)
		{
			return;
		}
		AnimalInfo animalByID = FarmData.getAnimalByID((int)animalByIndex.species);
		FarmScr.addProductAnimal(animalByID, number4);
		if (AvatarData.getImgIcon(animalByID.iconProduct) != null)
		{
			AvPosition avPosition = null;
			if ((int)animalByID.area == 1)
			{
				avPosition = FarmScr.getPosO(FarmScr.listNest, (int)animalByIndex.species);
			}
			else if ((int)animalByID.area == 2)
			{
				avPosition = FarmScr.getPosO(FarmScr.listBucket, (int)animalByIndex.species);
			}
			if (avPosition != null)
			{
				Canvas.addFlyText(number4, avPosition.x, avPosition.y - 25, -1, AvatarData.getImgIcon(animalByID.iconProduct).img, -1);
			}
		}
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0003E8F8 File Offset: 0x0003CCF8
	public static AvPosition getPosO(MyVector list, int type)
	{
		for (int i = 0; i < list.size(); i++)
		{
			AvPosition avPosition = (AvPosition)list.elementAt(i);
			if (avPosition.anchor == type)
			{
				return avPosition;
			}
		}
		return null;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0003E938 File Offset: 0x0003CD38
	public void onOpenLand(int idfarm, int curMoney, sbyte typeBuy, string text, int xu1, int luong1, int luongKhoa1)
	{
		if (idfarm != FarmScr.idFarm)
		{
			return;
		}
		GameMidlet.avatar.updateMoney(xu1, luong1, luongKhoa1);
		Canvas.startOKDlg(text);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0003E95D File Offset: 0x0003CD5D
	public void doJoinFarm(int idFarm, bool join)
	{
		this.isJoin = join;
		FarmService.gI().doJoinFarm(idFarm);
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0003E971 File Offset: 0x0003CD71
	private void doSellProduct(short idItem)
	{
		Canvas.startOKDlg(T.youWantBuyPro, new FarmScr.IActionSellProduct(idItem));
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0003E983 File Offset: 0x0003CD83
	protected void doBuyAnimal(AnimalInfo animal)
	{
		Canvas.getTypeMoney(animal.price[0], animal.price[1], new FarmScr.IActionBuyAnimalXu(animal), new FarmScr.IActionBuyAnimalLuong(animal), null);
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0003E9A8 File Offset: 0x0003CDA8
	public void doOpenCuaHang()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmData.treeInfo.Length; i++)
		{
			int num = i;
			Command o = new FarmScr.CommandBuyItemCuaHang(T.buy, new FarmScr.IactionBuyTreeInput((int)FarmData.treeInfo[num].ID), num);
			myVector.addElement(o);
		}
		int num2 = FarmData.listAnimalInfo.size();
		for (int j = 0; j < num2; j++)
		{
			AnimalInfo ani = (AnimalInfo)FarmData.listAnimalInfo.elementAt(j);
			int num3 = j;
			Command o2 = new FarmScr.CommandBuyAnimalCuaHang(T.buy, new FarmScr.IActionBuyAnimalCuaHang(this, ani, num3), ani, num3);
			myVector.addElement(o2);
		}
		PopupShop.gI().switchToMe();
		PopupShop.isHorizontal = true;
		PopupShop.gI().addElement(new string[]
		{
			T.seed,
			T.item,
			T.storePro
		}, new MyVector[]
		{
			myVector,
			this.goVatPham(),
			this.goKhoHang()
		}, null, null);
		if (Canvas.isInitChar && !Welcome.isOut)
		{
			Canvas.welcome = new Welcome();
			Canvas.welcome.initFarmPath(PopupShop.me);
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0003EAD8 File Offset: 0x0003CED8
	public MyVector goVatPham()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmData.listItemFarm.size(); i++)
		{
			FarmItem farmItem = (FarmItem)FarmData.listItemFarm.elementAt(i);
			if (farmItem.isItem && (farmItem.priceLuong > 0 || farmItem.priceXu > 0))
			{
				int num = i;
				myVector.addElement(new FarmScr.CommandGoVatPham(T.selectt, new FarmScr.IActionGoVatPham(this, farmItem), farmItem, num));
			}
		}
		return myVector;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0003EB58 File Offset: 0x0003CF58
	public MyVector goKhoHang()
	{
		MyVector myVector = new MyVector();
		int num = FarmScr.itemProduct.size();
		for (int i = 0; i < num; i++)
		{
			int i2 = i;
			Item item = (Item)FarmScr.itemProduct.elementAt(i2);
			Command o = new FarmScr.CommandGoKhoHang1(T.sell, new FarmScr.IActionGoKhoHang1(this, item), item, i2);
			myVector.addElement(o);
		}
		for (int j = 0; j < FarmScr.listFarmProduct.size(); j++)
		{
			int num2 = j;
			Item item2 = (Item)FarmScr.listFarmProduct.elementAt(j);
			FarmItem farmItem = FarmScr.getFarmItem((int)item2.ID);
			myVector.addElement(new FarmScr.CommandGoKhoHang2(T.sell, new FarmScr.IActionGoKhoHang2(this, farmItem), farmItem, num2, item2));
		}
		return myVector;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0003EC20 File Offset: 0x0003D020
	public void doOpenKhoHang()
	{
		if (FarmScr.isSteal)
		{
			if (!FarmScr.isAbleSteal)
			{
				return;
			}
			FarmService.gI().doStealStore();
			Canvas.startWaitDlg();
			return;
		}
		else
		{
			if (GameMidlet.avatar.IDDB != FarmScr.idFarm)
			{
				Canvas.startOKDlg(T.notOnFarmOther);
				return;
			}
			MyVector myVector = new MyVector();
			for (int i = 0; i < FarmScr.itemSeed.size(); i++)
			{
				int num = i;
				Command o = new FarmScr.CommandOpenKhoHang1(string.Empty, new FarmScr.IActionEmpty(), num);
				myVector.addElement(o);
			}
			for (int j = 0; j < FarmScr.listItemFarm.size(); j++)
			{
				int num2 = j;
				Command o2 = new FarmScr.CommandOpenKhoHang2(string.Empty, new FarmScr.IActionEmpty(), num2);
				myVector.addElement(o2);
			}
			PopupShop.gI().switchToMe();
			int num3 = 0;
			for (int k = 0; k < FarmScr.itemProduct.size(); k++)
			{
				Item item = (Item)FarmScr.itemProduct.elementAt(k);
				num3 += item.number;
			}
			for (int l = 0; l < FarmScr.listFarmProduct.size(); l++)
			{
				Item item2 = (Item)FarmScr.listFarmProduct.elementAt(l);
				num3 += item2.number;
			}
			PopupShop.gI().addElement(new string[]
			{
				T.storePro,
				T.StoreSeed
			}, new MyVector[]
			{
				this.goKhoHang(),
				myVector
			}, null, null);
			PopupShop.isHorizontal = true;
			PopupShop.gI().setCmyLim();
			return;
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0003EDB8 File Offset: 0x0003D1B8
	public override void commandAction(int index)
	{
		switch (index)
		{
		case 10:
			FarmScr.isAutoVatNuoi = true;
			for (int i = this.indexAuto; i < FarmScr.animalLists.size(); i++)
			{
				Animal pet = (Animal)FarmScr.animalLists.elementAt(i);
				if (this.doAutoVatNuoi(pet))
				{
					return;
				}
				this.indexAuto++;
			}
			this.commandTab(8);
			Canvas.startOKDlg("Không có vật nuôi nào bị bệnh");
			break;
		case 11:
			FarmService.gI().doUpdateLand(0, 0);
			break;
		case 12:
			FarmService.gI().doHarvestStarFruit();
			break;
		case 13:
			if (FarmScr.starFruil.timeFinish > 0)
			{
				FarmService.gI().doUpdateStarFruitByMoney(0);
			}
			else
			{
				FarmService.gI().doUpdateStarFruil(0);
			}
			break;
		case 14:
			FarmService.gI().getInfoStarFruit();
			break;
		default:
			if (index != 1)
			{
				if (index != 5)
				{
				}
			}
			else
			{
				this.setAction(1, FarmScr.idItemUsing);
			}
			break;
		}
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0003EED2 File Offset: 0x0003D2D2
	private void setAuto(int subIndex)
	{
		FarmScr.idSelected = 0;
		this.left = new Command(T.finish, 5);
		this.right = null;
		AvCamera.isFollow = true;
		this.center = null;
		this.isSelectedCell = true;
		FarmScr.indexItem = subIndex;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0003EF0C File Offset: 0x0003D30C
	private void doGieoHat()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmScr.itemSeed.size(); i++)
		{
			int i2 = i;
			Item item = (Item)FarmScr.itemSeed.elementAt(i2);
			myVector.addElement(new FarmScr.CommandGieoHat1(string.Concat(new object[]
			{
				item.name,
				"(",
				item.number,
				")"
			}), new FarmScr.IActionHieoHat1(i), item));
		}
		int num = MyScreen.hText + 10;
		Menu.gI().startMenuFarm(myVector, Canvas.hw, Canvas.h - num - 10, num, num);
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0003EFBC File Offset: 0x0003D3BC
	public override void commandTab(int index)
	{
		switch (index)
		{
		case 0:
			this.doSellect();
			break;
		case 1:
			if ((!Dog.isHound || (LoadMap.TYPEMAP != 24 && LoadMap.TYPEMAP != 53)) && (Canvas.welcome == null || Welcome.isPaintArrow))
			{
				MenuCenter.gI().startAt(this.listLeftMenu);
			}
			break;
		case 2:
			this.doVatPhamAnimal();
			break;
		case 3:
			this.doFeeding();
			break;
		case 4:
			break;
		case 5:
			this.left = null;
			this.isSelectedCell = false;
			AvCamera.isFollow = false;
			this.isChamSoc = false;
			this.listSelectedCell.removeAllElements();
			for (int i = 0; i < FarmScr.cell.size(); i++)
			{
				CellFarm cellFarm = (CellFarm)FarmScr.cell.elementAt(i);
				cellFarm.isSelected = false;
			}
			FarmScr.idSelected = -1;
			FarmScr.indexItem = -1;
			FarmScr.isSelected = false;
			break;
		default:
			switch (index)
			{
			case 51:
				FarmService.gI().doOpenLand(FarmScr.idFarm, 1);
				this.doJoinFarm(FarmScr.idFarm, true);
				break;
			case 52:
				FarmService.gI().doOpenLand(FarmScr.idFarm, 2);
				this.doJoinFarm(FarmScr.idFarm, true);
				break;
			case 53:
				this.setAction(0, -1);
				Canvas.endDlg();
				break;
			case 54:
				this.doGoFarmWay();
				break;
			}
			break;
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0003F148 File Offset: 0x0003D548
	private bool doAutoVatNuoi(Animal pet)
	{
		if (pet.disease[1])
		{
			LoadMap.focusObj = pet;
			AvCamera.gI().setToPos((float)(pet.x * Main.hdtype), (float)(pet.y * Main.hdtype));
			AvCamera.isFollow = true;
			this.center = new Command("Trị bệnh 1", new FarmScr.IActionTriBenh1(pet));
			this.left = FarmScr.cmdFinishAuto;
			this.right = FarmScr.cmdNextAuto;
			return true;
		}
		if (pet.disease[0])
		{
			LoadMap.focusObj = pet;
			AvCamera.gI().setToPos((float)(pet.x * Main.hdtype), (float)(pet.y * Main.hdtype));
			AvCamera.isFollow = true;
			this.center = new Command("Trị bệnh 2", new FarmScr.IActionTriBenh2(pet));
			this.left = FarmScr.cmdFinishAuto;
			this.right = FarmScr.cmdNextAuto;
			return true;
		}
		if (pet.hunger)
		{
			LoadMap.focusObj = pet;
			AvCamera.gI().setToPos((float)(pet.x * AvMain.hd), (float)(pet.y * AvMain.hd));
			AvCamera.isFollow = true;
			this.center = new Command("Cho ăn", new FarmScr.IActionEat(pet));
			this.left = FarmScr.cmdFinishAuto;
			this.right = FarmScr.cmdNextAuto;
			return true;
		}
		if (pet.health < 80)
		{
			LoadMap.focusObj = pet;
			AvCamera.gI().setToPos((float)(pet.x * Main.hdtype), (float)(pet.y * Main.hdtype));
			AvCamera.isFollow = true;
			this.center = new Command("Thuốc bổ", new FarmScr.IActionTriBenh3(pet));
			this.left = FarmScr.cmdFinishAuto;
			this.right = FarmScr.cmdNextAuto;
			return true;
		}
		return false;
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0003F300 File Offset: 0x0003D700
	public void onKick(int idFarm1)
	{
		if (LoadMap.TYPEMAP == 24 || LoadMap.TYPEMAP == 53)
		{
			Canvas.menuMain = null;
			Canvas.msgdlg.setInfoC(T.youAreBittenByDogByHound, new Command(T.OK, 8, this));
		}
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0003F33C File Offset: 0x0003D73C
	public bool doEat(short itemID, int index)
	{
		if (Item.getItemByList(FarmScr.listItemFarm, (int)itemID) == null)
		{
			return false;
		}
		FarmService.gI().doUsingItem(FarmScr.idFarm, index, (int)itemID);
		return false;
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0003F370 File Offset: 0x0003D770
	public void doCattleFeeding(sbyte type, sbyte action)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
		{
			Item item = (Item)FarmScr.listItemFarm.elementAt(i);
			FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
			if (((int)farmItem.type == (int)type || (int)farmItem.type == 101) && (int)farmItem.action == (int)action)
			{
				myVector.addElement(new FarmScr.CommandCattleFeeding(string.Concat(new object[]
				{
					farmItem.des,
					"(",
					item.number,
					")"
				}), new FarmScr.IActionCattleFeeding(type, item), farmItem));
			}
		}
		int num = MyScreen.hText + 10;
		Menu.gI().startMenuFarm(myVector, Canvas.hw, Canvas.h - LoadMap.w * AvMain.hd - 10, num, num);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0003F45C File Offset: 0x0003D85C
	private void sendHarvestAnimal(Animal pet, AvPosition pos)
	{
		if (Dog.isHound && FarmScr.idFarm != GameMidlet.avatar.IDDB)
		{
			Canvas.addFlyTextSmall(T.theft, pos.x, pos.y - 25, -1, 1, -1);
			if ((int)Dog.numBer > 0)
			{
				this.listHound.addElement(new FarmScr.IActionSendHarvestAnimal(pet));
			}
			else
			{
				FarmService.gI().doHarvestAnimal(FarmScr.idFarm, pet.IDDB);
			}
			return;
		}
		FarmService.gI().doHarvestAnimal(FarmScr.idFarm, pet.IDDB);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0003F4F0 File Offset: 0x0003D8F0
	public void doHarvestAnimal(int type, int index, MyVector list)
	{
		if (FarmScr.isSteal && !FarmScr.isAbleSteal)
		{
			return;
		}
		if (!FarmScr.isSteal && GameMidlet.avatar.IDDB != FarmScr.idFarm)
		{
			return;
		}
		if (index < 0 || index >= list.size())
		{
			return;
		}
		if (GameMidlet.avatar.IDDB != FarmScr.idFarm)
		{
			Dog.isHound = true;
		}
		if (Dog.isHound && this.listHound == null)
		{
			this.listHound = new MyVector();
		}
		AvPosition avPosition = (AvPosition)list.elementAt(index);
		for (int i = 0; i < FarmScr.animalLists.size(); i++)
		{
			Animal animal = (Animal)FarmScr.animalLists.elementAt(i);
			AnimalInfo animalByID = FarmData.getAnimalByID((int)animal.species);
			if (animal.numEggOne > 0 && avPosition.anchor == (int)animal.species)
			{
				animal.numEggOne = 0;
				if (type == 1 && (int)animalByID.area == type)
				{
					this.sendHarvestAnimal(animal, Chicken.posNest);
					FarmScr.removePopup(-50);
				}
				if (type == 2 && (int)animalByID.area == type)
				{
					this.sendHarvestAnimal(animal, Cattle.posBucket);
					FarmScr.removePopup(-51);
				}
			}
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0003F638 File Offset: 0x0003DA38
	public void onSell(int sellMoney, int curMoney, short idItem)
	{
		GameMidlet.avatar.money[0] = curMoney;
		PopupShop.isTransFocus = true;
		Canvas.startOKDlg(T.moneySellPro + sellMoney + T.xu);
		Item itemByList = Item.getItemByList(FarmScr.itemProduct, (int)idItem);
		if (itemByList == null)
		{
			itemByList = Item.getItemByList(FarmScr.listFarmProduct, (int)idItem);
			FarmScr.listFarmProduct.removeElement(itemByList);
		}
		else
		{
			FarmScr.itemProduct.removeElement(itemByList);
		}
		if (Canvas.currentMyScreen == PopupShop.gI())
		{
			PopupShop.gI().closeTabAll();
			if (LoadMap.TYPEMAP == 25)
			{
				this.doOpenCuaHang();
				PopupShop.gI().setTap(2);
			}
			else
			{
				this.doOpenKhoHang();
			}
		}
		Canvas.endDlg();
		SoundManager.playSound(3);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0003F6F8 File Offset: 0x0003DAF8
	public void onSellAnimal(int idFarm, int index, int curMoney1)
	{
		Animal animalByIndex = FarmScr.getAnimalByIndex(index);
		if (animalByIndex != null)
		{
			int text = curMoney1 - GameMidlet.avatar.money[1];
			Canvas.addFlyText(text, animalByIndex.x, animalByIndex.y - 30, -1, -1);
			LoadMap.focusObj = null;
			FarmScr.animalLists.removeElement(animalByIndex);
			LoadMap.playerLists.removeElement(animalByIndex);
			SoundManager.playSound(3);
		}
		GameMidlet.avatar.money[1] = curMoney1;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0003F767 File Offset: 0x0003DB67
	public void onPriceAnimal(sbyte index, string str1)
	{
		Canvas.startOKDlg(str1, new FarmScr.IActionPriceAnimal(index));
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0003F778 File Offset: 0x0003DB78
	public void doGoFarmWay()
	{
		FarmScr.isSteal = false;
		FarmScr.isAbleSteal = false;
		if (this.listHound != null)
		{
			for (int i = 0; i < this.listHound.size(); i++)
			{
				((IAction)this.listHound.elementAt(i)).perform();
			}
		}
		Cattle.itemID = -1;
		Dog.itemID = -1;
		this.listHound = null;
		this.right = null;
		ParkService.gI().doJoinPark(25, 0);
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0003F7F5 File Offset: 0x0003DBF5
	public void doExitBus()
	{
		FarmScr.resetImg();
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0003F7FC File Offset: 0x0003DBFC
	public void onBittenByDog()
	{
		this.listHound = null;
		this.doGoFarmWay();
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0003F80C File Offset: 0x0003DC0C
	public static Animal getAnimalByIndex(int index)
	{
		for (int i = 0; i < FarmScr.animalLists.size(); i++)
		{
			Animal animal = (Animal)FarmScr.animalLists.elementAt(i);
			if (animal.IDDB == index)
			{
				return animal;
			}
		}
		return null;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0003F854 File Offset: 0x0003DC54
	public void doMenuStarFruit()
	{
		if (FarmScr.isSteal)
		{
			if (!FarmScr.isAbleSteal)
			{
				return;
			}
			if (FarmScr.starFruil.numberFruit > 0)
			{
				FarmService.gI().doHarvestStarFruit();
				Canvas.startWaitDlg();
				FarmScr.removePopup(-2);
			}
			return;
		}
		else
		{
			if (GameMidlet.avatar.IDDB != FarmScr.idFarm)
			{
				return;
			}
			MyVector myVector = new MyVector();
			if (FarmScr.starFruil.numberFruit > 0)
			{
				myVector.addElement(new FarmScr.CommandMenuStarFruit1(string.Concat(new object[]
				{
					T.harvest,
					"(",
					FarmScr.starFruil.numberFruit,
					")"
				}), 12, 62));
			}
			myVector.addElement(new FarmScr.CommandMenuStarFruit1((FarmScr.starFruil.timeFinish <= 0) ? T.update : T.QuickUpgrade, 13, (FarmScr.starFruil.timeFinish <= 0) ? 63 : 64));
			myVector.addElement(new FarmScr.CommandMenuStarFruit1(T.info, 14, 61));
			this.startMenuFarm(myVector);
			return;
		}
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0003F96C File Offset: 0x0003DD6C
	public void startMenuFarm(MyVector menu)
	{
		int num = MyScreen.hText + 10;
		Menu.gI().startMenuFarm(menu, Canvas.hw, Canvas.h - num - 10, num, num);
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0003F9A0 File Offset: 0x0003DDA0
	public void doOpenCooking()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < FarmData.listFood.size(); i++)
		{
			Food food = (Food)FarmData.listFood.elementAt(i);
			int num = i;
			myVector.addElement(new FarmScr.CommandCooking1(T.cook, new FarmScr.IActionCooking1(food), num, food));
		}
		MyVector myVector2 = new MyVector();
		if (FarmScr.foodID > 0)
		{
			myVector2.addElement(null);
			Command o = new FarmScr.CommandCooking((FarmScr.remainTime != 0) ? T.QuickCooking : T.done, 22, this);
			myVector2.addElement(o);
		}
		PopupShop.gI().switchToMe();
		PopupShop.isHorizontal = true;
		if (FarmScr.foodID > 0)
		{
			PopupShop popupShop = PopupShop.gI();
			string[] name = new string[]
			{
				T.cook,
				T.cooking
			};
			MyVector[] array = new MyVector[2];
			array[0] = myVector;
			popupShop.addElement(name, array, myVector2, null);
			PopupShop.gI().setCmdLeft(new Command(T.cancel, 23, this), 1);
			PopupShop.focusTap = 1;
			PopupShop.gI().setTap(0);
			PopupShop.gI().setCmyLim();
			PopupShop.gI().setCaption();
		}
		else
		{
			PopupShop.gI().addElement(new string[]
			{
				T.cook
			}, new MyVector[]
			{
				myVector
			}, null, null);
		}
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0003FAF0 File Offset: 0x0003DEF0
	public void onHarvestStarFruit(short productIDH, short numberPro)
	{
		for (int i = 0; i < FarmScr.starFruil.xFruit.Length; i++)
		{
			Canvas.addFlyText(0, FarmScr.starFruil.x + (int)FarmScr.starFruil.xFruit[i], FarmScr.starFruil.y - 45 + (int)FarmScr.starFruil.yFruit[i], -1, 0, (int)FarmScr.starFruil.fruitID, -1);
		}
		Canvas.addFlyText((int)numberPro, GameMidlet.avatar.x, GameMidlet.avatar.y - (int)GameMidlet.avatar.height, -1, 10);
		FarmScr.starFruil.numberFruit = 0;
		Item item = FarmScr.getItemProductByID((int)productIDH);
		if (item != null)
		{
			item.number += (int)numberPro;
		}
		else
		{
			item = new Item();
			item.ID = productIDH;
			item.number = (int)numberPro;
			FarmScr.listFarmProduct.addElement(item);
		}
		Canvas.endDlg();
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0003FBD8 File Offset: 0x0003DFD8
	public static Item getItemProductByID(int id)
	{
		for (int i = 0; i < FarmScr.listFarmProduct.size(); i++)
		{
			Item item = (Item)FarmScr.listFarmProduct.elementAt(i);
			if ((int)item.ID == id)
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0003FC20 File Offset: 0x0003E020
	public static Item getProductByID(int id)
	{
		for (int i = 0; i < FarmScr.itemProduct.size(); i++)
		{
			Item item = (Item)FarmScr.itemProduct.elementAt(i);
			if ((int)item.ID == id)
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0003FC68 File Offset: 0x0003E068
	public void doMenuFarmFriend()
	{
		ListScr.gI().setFriendList(true);
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0003FC78 File Offset: 0x0003E078
	public static void removePopup(int type)
	{
		for (int i = 0; i < LoadMap.treeLists.size(); i++)
		{
			SubObject subObject = (SubObject)LoadMap.treeLists.elementAt(i);
			if ((int)subObject.catagory == 9 && subObject.type == type)
			{
				LoadMap.treeLists.removeElement(subObject);
				return;
			}
		}
	}

	// Token: 0x0400085D RID: 2141
	public static FarmScr instance;

	// Token: 0x0400085E RID: 2142
	public static int idFarm;

	// Token: 0x0400085F RID: 2143
	private string nameFarm;

	// Token: 0x04000860 RID: 2144
	public static MyVector cell;

	// Token: 0x04000861 RID: 2145
	private static MyVector itemSeed = new MyVector();

	// Token: 0x04000862 RID: 2146
	public static MyVector listItemFarm = new MyVector();

	// Token: 0x04000863 RID: 2147
	public static MyVector listFarmProduct = new MyVector();

	// Token: 0x04000864 RID: 2148
	public static MyVector itemProduct;

	// Token: 0x04000865 RID: 2149
	public static MyVector listNest;

	// Token: 0x04000866 RID: 2150
	public static MyVector listBucket;

	// Token: 0x04000867 RID: 2151
	public static MyVector animalLists = new MyVector();

	// Token: 0x04000868 RID: 2152
	public static MyVector[] listFood = new MyVector[2];

	// Token: 0x04000869 RID: 2153
	public static Image[] imgWorm_G;

	// Token: 0x0400086A RID: 2154
	public static Image imgBuyLant;

	// Token: 0x0400086B RID: 2155
	public static Image imgFocusCel;

	// Token: 0x0400086C RID: 2156
	public static Image imgSell;

	// Token: 0x0400086D RID: 2157
	public static FrameImage imgWormAndGrass;

	// Token: 0x0400086E RID: 2158
	public static FrameImage imgTrough;

	// Token: 0x0400086F RID: 2159
	public static FrameImage imgDogTr;

	// Token: 0x04000870 RID: 2160
	public static FrameImage img;

	// Token: 0x04000871 RID: 2161
	public static FrameImage imgBenh;

	// Token: 0x04000872 RID: 2162
	public AvPosition[] posTree;

	// Token: 0x04000873 RID: 2163
	private MyVector listHound;

	// Token: 0x04000874 RID: 2164
	public static int numTileBarn;

	// Token: 0x04000875 RID: 2165
	public static int numTilePond;

	// Token: 0x04000876 RID: 2166
	public sbyte[] typeCell = new sbyte[]
	{
		1,
		0,
		1,
		2,
		3
	};

	// Token: 0x04000877 RID: 2167
	public byte[] typeCell1 = new byte[]
	{
		5,
		4,
		5,
		6,
		7
	};

	// Token: 0x04000878 RID: 2168
	public static AvPosition focusCell;

	// Token: 0x04000879 RID: 2169
	public static AvPosition posName;

	// Token: 0x0400087A RID: 2170
	public static AvPosition posBarn;

	// Token: 0x0400087B RID: 2171
	public static AvPosition posPond;

	// Token: 0x0400087C RID: 2172
	public new static sbyte action = -1;

	// Token: 0x0400087D RID: 2173
	public static sbyte frame;

	// Token: 0x0400087E RID: 2174
	public AvPosition posDoing;

	// Token: 0x0400087F RID: 2175
	public const sbyte CUT_DAT = 0;

	// Token: 0x04000880 RID: 2176
	public const sbyte TUOI_NUOC = 1;

	// Token: 0x04000881 RID: 2177
	public const sbyte BON_PHAN = 2;

	// Token: 0x04000882 RID: 2178
	public const sbyte DIET_CO = 3;

	// Token: 0x04000883 RID: 2179
	public const sbyte CHICH_THUOC = 4;

	// Token: 0x04000884 RID: 2180
	private int t;

	// Token: 0x04000885 RID: 2181
	public static int numO = 12;

	// Token: 0x04000886 RID: 2182
	public static int numW = 3;

	// Token: 0x04000887 RID: 2183
	public static int numH = 4;

	// Token: 0x04000888 RID: 2184
	public static int idItemUsing = -1;

	// Token: 0x04000889 RID: 2185
	public int timeLimit;

	// Token: 0x0400088A RID: 2186
	public long curTime;

	// Token: 0x0400088B RID: 2187
	public static int money = 0;

	// Token: 0x0400088C RID: 2188
	public static int numStatusAnimal = 0;

	// Token: 0x0400088D RID: 2189
	private static sbyte[][] FRAME = new sbyte[5][];

	// Token: 0x0400088E RID: 2190
	public static bool isAutoVatNuoi = false;

	// Token: 0x0400088F RID: 2191
	public MyVector listAction = new MyVector();

	// Token: 0x04000890 RID: 2192
	private Command cmdSelected;

	// Token: 0x04000891 RID: 2193
	public static StarFruitObj starFruil;

	// Token: 0x04000892 RID: 2194
	public static int priceSteal = -1;

	// Token: 0x04000893 RID: 2195
	public static string nameTemp = string.Empty;

	// Token: 0x04000894 RID: 2196
	public static bool isSteal = false;

	// Token: 0x04000895 RID: 2197
	public static bool isAbleSteal = false;

	// Token: 0x04000896 RID: 2198
	public static bool isNew = false;

	// Token: 0x04000897 RID: 2199
	public Command cmdNextSteal;

	// Token: 0x04000898 RID: 2200
	public Command cmdCloseSteal;

	// Token: 0x04000899 RID: 2201
	public Command cmdStreal;

	// Token: 0x0400089A RID: 2202
	public static Image[] imgCell;

	// Token: 0x0400089B RID: 2203
	public static Command cmdMenu;

	// Token: 0x0400089C RID: 2204
	public static Command cmdLeftMenu;

	// Token: 0x0400089D RID: 2205
	public static Command cmdFocusBet;

	// Token: 0x0400089E RID: 2206
	public static Command cmdFeeding;

	// Token: 0x0400089F RID: 2207
	public static Command cmdFinishAuto;

	// Token: 0x040008A0 RID: 2208
	public static Command cmdNextAuto;

	// Token: 0x040008A1 RID: 2209
	public MyVector listLeftMenu = new MyVector();

	// Token: 0x040008A2 RID: 2210
	private Animal aniDoing;

	// Token: 0x040008A3 RID: 2211
	public long timeDoing = -1L;

	// Token: 0x040008A4 RID: 2212
	private int tempTime;

	// Token: 0x040008A5 RID: 2213
	private int repeatTime = 350;

	// Token: 0x040008A6 RID: 2214
	public static bool isSelected = false;

	// Token: 0x040008A7 RID: 2215
	private bool isSelectedCell;

	// Token: 0x040008A8 RID: 2216
	private bool isChamSoc;

	// Token: 0x040008A9 RID: 2217
	public static int indexItem = -1;

	// Token: 0x040008AA RID: 2218
	public static int idSelected = -1;

	// Token: 0x040008AB RID: 2219
	private bool isTrans;

	// Token: 0x040008AC RID: 2220
	private MyVector listSelectedCell = new MyVector();

	// Token: 0x040008AD RID: 2221
	private new bool isTran;

	// Token: 0x040008AE RID: 2222
	private int n;

	// Token: 0x040008AF RID: 2223
	public static sbyte levelStore;

	// Token: 0x040008B0 RID: 2224
	public static int capacityStore;

	// Token: 0x040008B1 RID: 2225
	public static bool isReSize = false;

	// Token: 0x040008B2 RID: 2226
	public static sbyte numBarn;

	// Token: 0x040008B3 RID: 2227
	public static sbyte numPond;

	// Token: 0x040008B4 RID: 2228
	public static int xRemember = -1;

	// Token: 0x040008B5 RID: 2229
	public static int yRemember = -1;

	// Token: 0x040008B6 RID: 2230
	public static int remainTime;

	// Token: 0x040008B7 RID: 2231
	public static int curTimeCooking;

	// Token: 0x040008B8 RID: 2232
	public static short foodID = 0;

	// Token: 0x040008B9 RID: 2233
	private bool isJoin = true;

	// Token: 0x040008BA RID: 2234
	private int ii;

	// Token: 0x040008BB RID: 2235
	private int indexAuto;

	// Token: 0x040008BC RID: 2236
	public static int xPosCook;

	// Token: 0x040008BD RID: 2237
	public static int yPosCook;

	// Token: 0x020000C1 RID: 193
	private class IActionFeeding1 : IAction
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0003FCD7 File Offset: 0x0003E0D7
		public IActionFeeding1(FarmScr p, Item item)
		{
			this.p = p;
			this.item = item;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0003FCED File Offset: 0x0003E0ED
		public void perform()
		{
			this.p.doUsingVatPhamAnimal(this.item, 1);
		}

		// Token: 0x040008BE RID: 2238
		private readonly FarmScr p;

		// Token: 0x040008BF RID: 2239
		private readonly Item item;
	}

	// Token: 0x020000C2 RID: 194
	private class FeedingCommand : Command
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0003FD01 File Offset: 0x0003E101
		public FeedingCommand(string des, FarmScr.IActionFeeding1 feeding1, FarmItem fItem) : base(des, feeding1)
		{
			this.fItem = fItem;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0003FD12 File Offset: 0x0003E112
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008C0 RID: 2240
		private readonly FarmItem fItem;
	}

	// Token: 0x020000C3 RID: 195
	private class CommandKhoGiong : Command
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0003FD24 File Offset: 0x0003E124
		public CommandKhoGiong(string caption, int type) : base(caption, type)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0003FD2E File Offset: 0x0003E12E
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.paintImg(g, 65, x, y, 3);
		}
	}

	// Token: 0x020000C4 RID: 196
	private class IActionSeed : IAction
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0003FD3B File Offset: 0x0003E13B
		public IActionSeed(int i, int pos)
		{
			this.ii = i;
			this.pos = pos;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003FD51 File Offset: 0x0003E151
		public void perform()
		{
			FarmScr.gI().doPlantSeed(this.ii, this.pos);
		}

		// Token: 0x040008C1 RID: 2241
		private int ii;

		// Token: 0x040008C2 RID: 2242
		private int pos;
	}

	// Token: 0x020000C5 RID: 197
	private class CommandKhoGiong2 : Command
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x0003FD69 File Offset: 0x0003E169
		public CommandKhoGiong2(string caption, IAction ac, Item it) : base(caption, ac)
		{
			this.item = it;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003FD7A File Offset: 0x0003E17A
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.getTreeByID((int)this.item.ID).paint(g, 7, x, y, 3);
		}

		// Token: 0x040008C3 RID: 2243
		private Item item;
	}

	// Token: 0x020000C6 RID: 198
	private class IActionPlantSeed : IAction
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0003FD96 File Offset: 0x0003E196
		public IActionPlantSeed(int ii, FarmScr p)
		{
			this.ii = ii;
			this.p = p;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003FDAC File Offset: 0x0003E1AC
		public void perform()
		{
			int posTreeByFocus = FarmScr.instance.getPosTreeByFocus(FarmScr.focusCell.x, FarmScr.focusCell.y);
			if (posTreeByFocus >= FarmScr.cell.size())
			{
				return;
			}
			this.p.doPlantSeed(this.ii, posTreeByFocus);
		}

		// Token: 0x040008C4 RID: 2244
		private readonly int ii;

		// Token: 0x040008C5 RID: 2245
		private readonly FarmScr p;
	}

	// Token: 0x020000C7 RID: 199
	private class CommandPlantSeed : Command
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0003FDFB File Offset: 0x0003E1FB
		public CommandPlantSeed(string s, FarmScr.IActionPlantSeed seed, Item item) : base(s, seed)
		{
			this.item = item;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003FE0C File Offset: 0x0003E20C
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.getTreeByID((int)this.item.ID).paint(g, 7, x, y - (int)AvMain.hSmall / 2, 3);
			Canvas.smallFontYellow.drawString(g, "(" + this.item.number + ")", x, y + Menu.gI().menuH / 2 - AvMain.hDuBox - (int)AvMain.hSmall * 2 + (AvMain.hd - 1) * 10, 2);
			Canvas.smallFontYellow.drawString(g, this.item.name, x, y + Menu.gI().menuH / 2 - AvMain.hDuBox - (int)AvMain.hSmall + (AvMain.hd - 1) * 10, 2);
		}

		// Token: 0x040008C6 RID: 2246
		private readonly Item item;
	}

	// Token: 0x020000C8 RID: 200
	private class CommandVatPham1 : Command
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0003FECE File Offset: 0x0003E2CE
		public CommandVatPham1(string caption, int type) : base(caption, type)
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0003FED8 File Offset: 0x0003E2D8
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.paintImg(g, 65, x, y, 3);
		}
	}

	// Token: 0x020000C9 RID: 201
	private class IActionVatPham1 : IAction
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x0003FEED File Offset: 0x0003E2ED
		public void perform()
		{
			FarmScr.gI().setAction(1, FarmScr.idItemUsing);
		}
	}

	// Token: 0x020000CA RID: 202
	private class IActionVatPham2 : IAction
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x0003FEFF File Offset: 0x0003E2FF
		public IActionVatPham2(CellFarm c)
		{
			this.cell = c;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0003FF0E File Offset: 0x0003E30E
		public void perform()
		{
			FarmScr.gI().doLamDat(this.cell);
		}

		// Token: 0x040008C7 RID: 2247
		private CellFarm cell;
	}

	// Token: 0x020000CB RID: 203
	private class CommandVatPham2 : Command
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0003FF20 File Offset: 0x0003E320
		public CommandVatPham2(string caption, IAction ac, int ind) : base(caption, ac)
		{
			this.index = ind;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0003FF31 File Offset: 0x0003E331
		public CommandVatPham2(string caption, int type, int ind) : base(caption, type)
		{
			this.index = ind;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0003FF42 File Offset: 0x0003E342
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmScr.img.drawFrame(this.index, x, y, 0, 3, g);
		}

		// Token: 0x040008C8 RID: 2248
		private int index;
	}

	// Token: 0x020000CC RID: 204
	private class ActionVatPham3 : IAction
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x0003FF61 File Offset: 0x0003E361
		public void perform()
		{
		}
	}

	// Token: 0x020000CD RID: 205
	private class iCommandItemFarm : Command
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0003FF63 File Offset: 0x0003E363
		public iCommandItemFarm(string caption, IAction action, FarmItem f) : base(caption, action)
		{
			this.fItem = f;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0003FF74 File Offset: 0x0003E374
		public iCommandItemFarm(string caption, int index, int subIndex, FarmItem f, AvMain pointer) : base(caption, index, subIndex, pointer)
		{
			this.fItem = f;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0003FF89 File Offset: 0x0003E389
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008C9 RID: 2249
		private FarmItem fItem;
	}

	// Token: 0x020000CE RID: 206
	private class IActionLamDat : IAction
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x0003FF9B File Offset: 0x0003E39B
		public IActionLamDat(FarmScr p, CellFarm cell)
		{
			this.p = p;
			this.cell = cell;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0003FFB1 File Offset: 0x0003E3B1
		public void perform()
		{
			this.p.doLamDat(this.cell);
		}

		// Token: 0x040008CA RID: 2250
		private readonly FarmScr p;

		// Token: 0x040008CB RID: 2251
		private readonly CellFarm cell;
	}

	// Token: 0x020000CF RID: 207
	private class CommandLamDat : Command
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x0003FFC4 File Offset: 0x0003E3C4
		public CommandLamDat(string land, FarmScr.IActionLamDat dat) : base(land, dat)
		{
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0003FFCE File Offset: 0x0003E3CE
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmScr.img.drawFrame(1, x, y, 0, 3, g);
		}
	}

	// Token: 0x020000D0 RID: 208
	private class IActionVatPham : IAction
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0003FFE0 File Offset: 0x0003E3E0
		public IActionVatPham(FarmScr p, Item item)
		{
			this.p = p;
			this.item = item;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0003FFF8 File Offset: 0x0003E3F8
		public void perform()
		{
			if (this.item.number > 0)
			{
				this.p.doUsingVatPham((int)this.item.ID, this.item);
			}
			else
			{
				Canvas.startOKDlg(T.empty + this.item.name);
			}
		}

		// Token: 0x040008CC RID: 2252
		private readonly FarmScr p;

		// Token: 0x040008CD RID: 2253
		private readonly Item item;
	}

	// Token: 0x020000D1 RID: 209
	private class CommandVatPham : Command
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x00040051 File Offset: 0x0003E451
		public CommandVatPham(string na, FarmScr.IActionVatPham pham, FarmItem fr) : base(na, pham)
		{
			this.fr = fr;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00040062 File Offset: 0x0003E462
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fr.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008CE RID: 2254
		private readonly FarmItem fr;
	}

	// Token: 0x020000D2 RID: 210
	private class IActionItem5 : IAction
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x00040074 File Offset: 0x0003E474
		public IActionItem5(FarmItem fItem, AnimalInfo aInfo, Item item)
		{
			this.fItem = fItem;
			this.aInfo = aInfo;
			this.item = item;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00040091 File Offset: 0x0003E491
		public void perform()
		{
			if (LoadMap.focusObj != null)
			{
				FarmScr.gI().doUsingVatPhamAnimal(this.item, ((int)this.aInfo.area != 1) ? 1 : 0);
			}
		}

		// Token: 0x040008CF RID: 2255
		private FarmItem fItem;

		// Token: 0x040008D0 RID: 2256
		private AnimalInfo aInfo;

		// Token: 0x040008D1 RID: 2257
		private Item item;
	}

	// Token: 0x020000D3 RID: 211
	private class CommandItem5 : Command
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x000400C6 File Offset: 0x0003E4C6
		public CommandItem5(string name, IAction ac, FarmItem fItem) : base(name, ac)
		{
			this.fItem = fItem;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000400D7 File Offset: 0x0003E4D7
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008D2 RID: 2258
		private FarmItem fItem;
	}

	// Token: 0x020000D4 RID: 212
	private class IActionThuoc : IAction
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x000400E9 File Offset: 0x0003E4E9
		public IActionThuoc(FarmItem fItem, Item item)
		{
			this.fItem = fItem;
			this.item = item;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00040100 File Offset: 0x0003E500
		public void perform()
		{
			if (LoadMap.focusObj != null)
			{
				if ((int)this.fItem.action == 4)
				{
					FarmScr.gI().setAction(4, (int)this.item.ID);
					FarmScr.gI().aniDoing = (Animal)LoadMap.focusObj;
					FarmScr.gI().aniDoing.isStand = true;
					FarmScr.gI().aniDoing.timeStand = (int)Canvas.getTick() / 1000;
				}
				FarmService.gI().doUsingItem(FarmScr.idFarm, ((Base)LoadMap.focusObj).IDDB, (int)this.item.ID);
			}
		}

		// Token: 0x040008D3 RID: 2259
		private FarmItem fItem;

		// Token: 0x040008D4 RID: 2260
		private Item item;
	}

	// Token: 0x020000D5 RID: 213
	private class CommandThuoc : Command
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x000401A7 File Offset: 0x0003E5A7
		public CommandThuoc(string name, FarmScr.IActionThuoc ac, FarmItem fItem) : base(name, ac)
		{
			this.fItem = fItem;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000401B8 File Offset: 0x0003E5B8
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008D5 RID: 2261
		private FarmItem fItem;
	}

	// Token: 0x020000D6 RID: 214
	private class IActionSetAnimal : IAction
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x000401CA File Offset: 0x0003E5CA
		public IActionSetAnimal(FarmItem f, short ID, Animal ani)
		{
			this.fItem = f;
			this.ID = ID;
			this.ani = ani;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000401E8 File Offset: 0x0003E5E8
		public void perform()
		{
			if ((int)this.fItem.action == 4)
			{
				FarmScr.instance.setAction(4, (int)this.ID);
				FarmScr.instance.aniDoing = (Animal)LoadMap.focusObj;
				FarmScr.instance.aniDoing.isStand = true;
				FarmScr.instance.aniDoing.timeStand = Canvas.getSecond();
			}
			FarmService.gI().doUsingItem(FarmScr.idFarm, this.ani.IDDB, (int)this.ID);
		}

		// Token: 0x040008D6 RID: 2262
		private FarmItem fItem;

		// Token: 0x040008D7 RID: 2263
		private Animal ani;

		// Token: 0x040008D8 RID: 2264
		private short ID;
	}

	// Token: 0x020000D7 RID: 215
	private class CommandSellAnimal : Command
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00040270 File Offset: 0x0003E670
		public CommandSellAnimal(string sell, int index, AvMain pointer) : base(sell, index, pointer)
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0004027B File Offset: 0x0003E67B
		public override void paint(MyGraphics g, int x, int y)
		{
			g.drawImage(FarmScr.imgSell, (float)x, (float)y, 3);
		}
	}

	// Token: 0x020000D8 RID: 216
	private class IActionVatPhamAnimal : IAction
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x0004028D File Offset: 0x0003E68D
		public IActionVatPhamAnimal(FarmScr p, AnimalInfo aInfo, Item item)
		{
			this.p = p;
			this.aInfo = aInfo;
			this.item = item;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000402AA File Offset: 0x0003E6AA
		public void perform()
		{
			if (LoadMap.focusObj != null)
			{
				this.p.doUsingVatPhamAnimal(this.item, ((int)this.aInfo.area != 1) ? 1 : 0);
			}
		}

		// Token: 0x040008D9 RID: 2265
		private readonly FarmScr p;

		// Token: 0x040008DA RID: 2266
		private readonly AnimalInfo aInfo;

		// Token: 0x040008DB RID: 2267
		private readonly Item item;
	}

	// Token: 0x020000D9 RID: 217
	private class CommandVatPhamAnimal : Command
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x000402E0 File Offset: 0x0003E6E0
		public CommandVatPhamAnimal(string s, FarmScr.IActionVatPhamAnimal animal, FarmItem fItem) : base(s, animal)
		{
			this.fItem = fItem;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000402F1 File Offset: 0x0003E6F1
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008DC RID: 2268
		private readonly FarmItem fItem;
	}

	// Token: 0x020000DA RID: 218
	private class IActionChichThuocAnimal : IAction
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00040303 File Offset: 0x0003E703
		public IActionChichThuocAnimal(FarmScr p, Item item, FarmItem fItem)
		{
			this.p = p;
			this.item = item;
			this.fItem = fItem;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00040320 File Offset: 0x0003E720
		public void perform()
		{
			if (LoadMap.focusObj != null)
			{
				if ((int)this.fItem.action == 4)
				{
					this.p.setAction(4, (int)this.item.ID);
					this.p.aniDoing = (Animal)LoadMap.focusObj;
					this.p.aniDoing.isStand = true;
					this.p.aniDoing.timeStand = Environment.TickCount / 1000;
				}
				FarmService.gI().doUsingItem(FarmScr.idFarm, ((Base)LoadMap.focusObj).IDDB, (int)this.item.ID);
			}
		}

		// Token: 0x040008DD RID: 2269
		private readonly FarmScr p;

		// Token: 0x040008DE RID: 2270
		private readonly Item item;

		// Token: 0x040008DF RID: 2271
		private readonly FarmItem fItem;
	}

	// Token: 0x020000DB RID: 219
	private class CommandChichThuocAnimal : Command
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x000403CA File Offset: 0x0003E7CA
		public CommandChichThuocAnimal(string s, FarmScr.IActionChichThuocAnimal animal, FarmItem fItem) : base(s, animal)
		{
			this.fItem = fItem;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000403DB File Offset: 0x0003E7DB
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x040008E0 RID: 2272
		private readonly FarmItem fItem;
	}

	// Token: 0x020000DC RID: 220
	private class PoLayer : Layer
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x000403ED File Offset: 0x0003E7ED
		public PoLayer(Point po)
		{
			this.po = po;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000403FC File Offset: 0x0003E7FC
		public override void paint(MyGraphics g, int x, int y)
		{
			PaintPopup.fill(this.po.x * AvMain.hd, this.po.y * AvMain.hd, this.po.w * AvMain.hd, this.po.h * AvMain.hd, 5921542, g);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00040458 File Offset: 0x0003E858
		public override void update()
		{
			if (this.po.y < this.po.limitY)
			{
				this.po.x += this.po.v;
				this.po.y += this.po.g;
				this.po.g++;
			}
			else
			{
				this.po.v = 0;
				this.po.g = 0;
			}
		}

		// Token: 0x040008E1 RID: 2273
		private readonly Point po;
	}

	// Token: 0x020000DD RID: 221
	private class IActionHound : IAction
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x000404EA File Offset: 0x0003E8EA
		public IActionHound(int pos)
		{
			this.pos = pos;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000404F9 File Offset: 0x0003E8F9
		public void perform()
		{
			FarmService.gI().doHervest(FarmScr.idFarm, this.pos);
		}

		// Token: 0x040008E2 RID: 2274
		private readonly int pos;
	}

	// Token: 0x020000DE RID: 222
	private class IActionCuocDat : IAction
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x00040510 File Offset: 0x0003E910
		public IActionCuocDat(FarmScr p)
		{
			this.p = p;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0004051F File Offset: 0x0003E91F
		public void perform()
		{
			this.p.setAction(0, -1);
			Canvas.endDlg();
		}

		// Token: 0x040008E3 RID: 2275
		private readonly FarmScr p;
	}

	// Token: 0x020000DF RID: 223
	private class IActionBonPhan : IAction
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x00040533 File Offset: 0x0003E933
		public IActionBonPhan(int pos, FarmItem fr)
		{
			this.pos = pos;
			this.fr = fr;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00040549 File Offset: 0x0003E949
		public void perform()
		{
			FarmScr.instance.setAction(3, (int)this.fr.ID);
			FarmService.gI().doUsingItem(FarmScr.idFarm, this.pos, (int)this.fr.ID);
		}

		// Token: 0x040008E4 RID: 2276
		private int pos;

		// Token: 0x040008E5 RID: 2277
		private FarmItem fr;
	}

	// Token: 0x020000E0 RID: 224
	private class IActionSet3 : IAction
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x00040581 File Offset: 0x0003E981
		public IActionSet3(AvPosition pos)
		{
			this.pos = pos;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00040590 File Offset: 0x0003E990
		public void perform()
		{
			FarmScr.instance.doPlantSeed(FarmScr.indexItem, this.pos.anchor);
			FarmScr.instance.setGieoHat();
		}

		// Token: 0x040008E6 RID: 2278
		private AvPosition pos;
	}

	// Token: 0x020000E1 RID: 225
	private class IActionSet2 : IAction
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x000405B6 File Offset: 0x0003E9B6
		public IActionSet2(CellFarm c)
		{
			this.c = c;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000405C8 File Offset: 0x0003E9C8
		public void perform()
		{
			GameMidlet.avatar.action = 0;
			FarmScr.focusCell.x = this.c.x / LoadMap.w;
			FarmScr.focusCell.y = this.c.y / LoadMap.w;
			FarmScr.instance.doLamDat(this.c);
		}

		// Token: 0x040008E7 RID: 2279
		private CellFarm c;
	}

	// Token: 0x020000E2 RID: 226
	private class IActionSet1 : IAction
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x00040626 File Offset: 0x0003EA26
		public IActionSet1(CellFarm c)
		{
			this.c = c;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00040638 File Offset: 0x0003EA38
		public void perform()
		{
			FarmScr.focusCell.x = this.c.x / LoadMap.w;
			FarmScr.focusCell.y = this.c.y / LoadMap.w;
			FarmScr.instance.setAction(1, FarmScr.idItemUsing);
		}

		// Token: 0x040008E8 RID: 2280
		private CellFarm c;
	}

	// Token: 0x020000E3 RID: 227
	private class IActionSellProduct : IAction
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0004068B File Offset: 0x0003EA8B
		public IActionSellProduct(short idItem)
		{
			this.idItem = idItem;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0004069A File Offset: 0x0003EA9A
		public void perform()
		{
			FarmService.gI().doSellItem(this.idItem);
			Canvas.startWaitDlg();
		}

		// Token: 0x040008E9 RID: 2281
		private readonly short idItem;
	}

	// Token: 0x020000E4 RID: 228
	private class IActionBuyAnimalXu : IAction
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x000406B1 File Offset: 0x0003EAB1
		public IActionBuyAnimalXu(AnimalInfo animal)
		{
			this.animal = animal;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000406C0 File Offset: 0x0003EAC0
		public void perform()
		{
			FarmService.gI().doBuyAnimal(this.animal, 1);
		}

		// Token: 0x040008EA RID: 2282
		private readonly AnimalInfo animal;
	}

	// Token: 0x020000E5 RID: 229
	private class IActionBuyAnimalLuong : IAction
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x000406D3 File Offset: 0x0003EAD3
		public IActionBuyAnimalLuong(AnimalInfo animal)
		{
			this.animal = animal;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000406E2 File Offset: 0x0003EAE2
		public void perform()
		{
			FarmService.gI().doBuyAnimal(this.animal, 2);
		}

		// Token: 0x040008EB RID: 2283
		private readonly AnimalInfo animal;
	}

	// Token: 0x020000E6 RID: 230
	private class IactionBuyTreeInput : IAction
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x000406F5 File Offset: 0x0003EAF5
		public IactionBuyTreeInput(int id)
		{
			this.idTree = id;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00040704 File Offset: 0x0003EB04
		public void perform()
		{
			ipKeyboard.openKeyBoard(T.number, ipKeyboard.NUMBERIC, string.Empty, new FarmScr.IactionBuyTree(this.idTree), false);
		}

		// Token: 0x040008EC RID: 2284
		private int idTree;
	}

	// Token: 0x020000E7 RID: 231
	private class IactionBuyTree : IKbAction
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00040726 File Offset: 0x0003EB26
		public IactionBuyTree(int id)
		{
			this.idTree = id;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00040738 File Offset: 0x0003EB38
		public void perform(string text)
		{
			int num = int.Parse(text);
			TreeInfo treeInfoByID = FarmData.getTreeInfoByID(this.idTree);
			int num2 = (int)treeInfoByID.priceSeed[0];
			int num3 = (int)treeInfoByID.priceSeed[1];
			int pri = num2;
			Canvas.getTypeMoney(num2 * num, num3 * num, new FarmScr.IActionXuTree(this.idTree, num, pri, 1), new FarmScr.IActionXuTree(this.idTree, num, pri, 2), new FarmScr.IActionEnd());
		}

		// Token: 0x040008ED RID: 2285
		private int idTree;
	}

	// Token: 0x020000E8 RID: 232
	private class IActionEnd : IAction
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x000407AA File Offset: 0x0003EBAA
		public void perform()
		{
		}
	}

	// Token: 0x020000E9 RID: 233
	private class IActionXuTree : IAction
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x000407AC File Offset: 0x0003EBAC
		public IActionXuTree(int id, int num, int pri, int type)
		{
			this.idTree = id;
			this.num = num;
			this.price = pri;
			this.type = type;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000407D1 File Offset: 0x0003EBD1
		public void perform()
		{
			FarmService.gI().doBuyItem((short)this.idTree, (sbyte)this.num, this.type, this.price * this.num);
		}

		// Token: 0x040008EE RID: 2286
		private int idTree;

		// Token: 0x040008EF RID: 2287
		private int num;

		// Token: 0x040008F0 RID: 2288
		private int price;

		// Token: 0x040008F1 RID: 2289
		private int type;
	}

	// Token: 0x020000EA RID: 234
	private class IActionBuyItemCuaHang : IAction
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x000407FE File Offset: 0x0003EBFE
		public IActionBuyItemCuaHang(FarmScr p, int ii)
		{
			this.p = p;
			this.ii = ii;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00040814 File Offset: 0x0003EC14
		public void perform()
		{
			ipKeyboard.openKeyBoard(T.number, ipKeyboard.NUMBERIC, string.Empty, new FarmScr.IActionBuyItem(this.ii, 0), false);
		}

		// Token: 0x040008F2 RID: 2290
		private readonly FarmScr p;

		// Token: 0x040008F3 RID: 2291
		private readonly int ii;
	}

	// Token: 0x020000EB RID: 235
	private class IActionBuyItem : IKbAction
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00040837 File Offset: 0x0003EC37
		public IActionBuyItem(int index, int type)
		{
			this.type = type;
			this.index = index;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00040850 File Offset: 0x0003EC50
		public void perform(string text)
		{
			int num = int.Parse(text);
			int num2 = 0;
			int num3 = 0;
			if (this.type == 0)
			{
				TreeInfo treeInfoByID = FarmData.getTreeInfoByID(this.index);
				num2 = (int)treeInfoByID.priceSeed[0];
				num3 = (int)treeInfoByID.priceSeed[1];
			}
			else if (this.type == 2)
			{
				num2 = FarmData.getVPbyID(this.index).price[0];
				num3 = FarmData.getVPbyID(this.index).price[1];
			}
			else if (this.type == 4)
			{
				FarmItem farmItem = FarmScr.getFarmItem(this.index);
				if (farmItem != null)
				{
					num2 = farmItem.priceXu;
					num3 = farmItem.priceLuong;
				}
			}
			int a = num2;
			int b = num3;
			Canvas.getTypeMoney(num2 * num, num3 * num, new FarmScr.IActionDoBuyItem1(this.index, a, num), new FarmScr.IActionDoBuyItem2(this.index, b, num), null);
		}

		// Token: 0x040008F4 RID: 2292
		private int type;

		// Token: 0x040008F5 RID: 2293
		private int index;
	}

	// Token: 0x020000EC RID: 236
	private class CommandBuyItemCuaHang : Command
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x0004092B File Offset: 0x0003ED2B
		public CommandBuyItemCuaHang(string select, IAction hang, int ii) : base(select, hang)
		{
			this.ii = ii;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0004093C File Offset: 0x0003ED3C
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.treeInfo[this.ii].paint(g, 7, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00040964 File Offset: 0x0003ED64
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(string.Concat(new object[]
				{
					FarmData.treeInfo[this.ii].name1,
					"(",
					FarmData.treeInfo[this.ii].harvestTime,
					T.h,
					")"
				}));
				PopupShop.addStr(T.priceStr + Canvas.getPriceMoney((int)FarmData.treeInfo[this.ii].priceSeed[0], (int)FarmData.treeInfo[this.ii].priceSeed[1], false));
				PopupShop.addStr(T.level[2] + ": " + FarmData.treeInfo[this.ii].lv);
				if (FarmData.treeInfo[this.ii].isDynamic)
				{
					FarmItem farmItem = FarmScr.getFarmItem((int)FarmData.treeInfo[this.ii].productID);
					PopupShop.addStr("Sản lượng: " + Canvas.getMoneys((int)FarmData.treeInfo[this.ii].numProduct) + " " + farmItem.des);
				}
				else
				{
					PopupShop.addStr("Sản lượng: " + Canvas.getMoneys((int)FarmData.treeInfo[this.ii].numProduct));
				}
			}
		}

		// Token: 0x040008F6 RID: 2294
		private readonly int ii;
	}

	// Token: 0x020000ED RID: 237
	private class IActionBuyAnimalCuaHang : IAction
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00040AD3 File Offset: 0x0003EED3
		public IActionBuyAnimalCuaHang(FarmScr p, AnimalInfo ani, int ii)
		{
			this.p = p;
			this.ani = ani;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00040AE9 File Offset: 0x0003EEE9
		public void perform()
		{
			this.p.doBuyAnimal(this.ani);
		}

		// Token: 0x040008F7 RID: 2295
		private readonly FarmScr p;

		// Token: 0x040008F8 RID: 2296
		private readonly AnimalInfo ani;
	}

	// Token: 0x020000EE RID: 238
	private class CommandBuyAnimalCuaHang : Command
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x00040AFC File Offset: 0x0003EEFC
		public CommandBuyAnimalCuaHang(string select, FarmScr.IActionBuyAnimalCuaHang hang, AnimalInfo ani, int ii) : base(select, hang)
		{
			this.ani = ani;
			this.ii = ii;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00040B15 File Offset: 0x0003EF15
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.paintImg(g, (int)this.ani.iconID, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00040B3C File Offset: 0x0003EF3C
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus - FarmData.treeInfo.Length)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(string.Concat(new object[]
				{
					this.ani.name,
					"(",
					this.ani.harvestTime,
					T.h,
					")"
				}));
				PopupShop.addStr(T.priceStr + Canvas.getPriceMoney(this.ani.price[0], this.ani.price[1], true));
				PopupShop.addStr(this.ani.des);
			}
		}

		// Token: 0x040008F9 RID: 2297
		private readonly AnimalInfo ani;

		// Token: 0x040008FA RID: 2298
		private readonly int ii;
	}

	// Token: 0x020000EF RID: 239
	private class IActionGoVatPham : IAction
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00040BF8 File Offset: 0x0003EFF8
		public IActionGoVatPham(FarmScr p, FarmItem item)
		{
			this.p = p;
			this.item = item;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00040C0E File Offset: 0x0003F00E
		public void perform()
		{
			ipKeyboard.openKeyBoard(T.number, ipKeyboard.NUMBERIC, string.Empty, new FarmScr.IActionBuyItem((int)this.item.ID, 4), false);
		}

		// Token: 0x040008FB RID: 2299
		private readonly FarmScr p;

		// Token: 0x040008FC RID: 2300
		private readonly FarmItem item;
	}

	// Token: 0x020000F0 RID: 240
	private class CommandGoVatPham : Command
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00040C36 File Offset: 0x0003F036
		public CommandGoVatPham(string s, FarmScr.IActionGoVatPham pham, FarmItem item, int ii) : base(s, pham)
		{
			this.item = item;
			this.ii = ii;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00040C4F File Offset: 0x0003F04F
		public override void paint(MyGraphics g, int x, int y)
		{
			this.item.paint(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 0, 3);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00040C74 File Offset: 0x0003F074
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(this.item.des);
				PopupShop.addStr(T.priceStr + Canvas.getPriceMoney(this.item.priceXu, this.item.priceLuong, false));
			}
		}

		// Token: 0x040008FD RID: 2301
		private readonly FarmItem item;

		// Token: 0x040008FE RID: 2302
		private readonly int ii;
	}

	// Token: 0x020000F1 RID: 241
	private class IActionGoKhoHang1 : IAction
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x00040CDB File Offset: 0x0003F0DB
		public IActionGoKhoHang1(FarmScr p, Item item)
		{
			this.p = p;
			this.item = item;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00040CF1 File Offset: 0x0003F0F1
		public void perform()
		{
			this.p.doSellProduct(this.item.ID);
		}

		// Token: 0x040008FF RID: 2303
		private readonly FarmScr p;

		// Token: 0x04000900 RID: 2304
		private readonly Item item;
	}

	// Token: 0x020000F2 RID: 242
	private class CommandGoKhoHang1 : Command
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00040D09 File Offset: 0x0003F109
		public CommandGoKhoHang1(string sell, FarmScr.IActionGoKhoHang1 hang1, Item item, int ii) : base(sell, hang1)
		{
			this.item = item;
			this.ii = ii;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00040D24 File Offset: 0x0003F124
		public override void paint(MyGraphics g, int x, int y)
		{
			if (this.item.ID < 50)
			{
				FarmData.getTreeByID((int)this.item.ID).paint(g, 7, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
			}
			else
			{
				AnimalInfo animalByID = FarmData.getAnimalByID((int)this.item.ID);
				AvatarData.paintImg(g, (int)animalByID.iconProduct, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00040DA4 File Offset: 0x0003F1A4
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				if (this.item.ID < 50)
				{
					PopupShop.addStr(this.item.name);
				}
				else
				{
					AnimalInfo animalByID = FarmData.getAnimalByID((int)this.item.ID);
					PopupShop.addStr(animalByID.name);
				}
				PopupShop.addStr(T.number + this.item.number);
				PopupShop.addStr(T.inCome + Canvas.getMoneys(this.item.price[0] * this.item.number) + T.xu);
				PopupShop.addStr(MapScr.strTkFarm());
			}
		}

		// Token: 0x04000901 RID: 2305
		private readonly Item item;

		// Token: 0x04000902 RID: 2306
		private readonly int ii;
	}

	// Token: 0x020000F3 RID: 243
	private class IActionGoKhoHang2 : IAction
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x00040E6E File Offset: 0x0003F26E
		public IActionGoKhoHang2(FarmScr p, FarmItem fItem)
		{
			this.p = p;
			this.fItem = fItem;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00040E84 File Offset: 0x0003F284
		public void perform()
		{
			this.p.doSellProduct(this.fItem.ID);
		}

		// Token: 0x04000903 RID: 2307
		private readonly FarmScr p;

		// Token: 0x04000904 RID: 2308
		private readonly FarmItem fItem;
	}

	// Token: 0x020000F4 RID: 244
	private class CommandGoKhoHang2 : Command
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00040E9C File Offset: 0x0003F29C
		public CommandGoKhoHang2(string s, FarmScr.IActionGoKhoHang2 hang2, FarmItem fItem, int ii, Item item) : base(s, hang2)
		{
			this.fItem = fItem;
			this.ii = ii;
			this.item = item;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00040EBD File Offset: 0x0003F2BD
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 0, 3);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00040EE0 File Offset: 0x0003F2E0
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus - FarmScr.itemProduct.size())
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(this.fItem.des);
				if (this.fItem.priceLuong > 0)
				{
					PopupShop.addStr(T.inCome + Canvas.getMoneys(this.item.number * this.fItem.priceLuong) + T.dola + "(Tài khoản chính)");
				}
				else if (this.fItem.priceXu > 0)
				{
					PopupShop.addStr(T.inCome + Canvas.getMoneys(this.item.number * this.fItem.priceXu) + T.dola);
				}
				PopupShop.addStr(T.number + this.item.number);
				PopupShop.addStr(MapScr.strTkFarm());
			}
		}

		// Token: 0x04000905 RID: 2309
		private readonly FarmItem fItem;

		// Token: 0x04000906 RID: 2310
		private readonly int ii;

		// Token: 0x04000907 RID: 2311
		private readonly Item item;
	}

	// Token: 0x020000F5 RID: 245
	private class IActionEmpty : IAction
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x00040FE4 File Offset: 0x0003F3E4
		public void perform()
		{
		}
	}

	// Token: 0x020000F6 RID: 246
	private class CommandOpenKhoHang1 : Command
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00040FE6 File Offset: 0x0003F3E6
		public CommandOpenKhoHang1(string s, FarmScr.IActionEmpty empty, int ii) : base(s, empty)
		{
			this.ii = ii;
			this.item = (Item)FarmScr.itemSeed.elementAt(ii);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0004100D File Offset: 0x0003F40D
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.getTreeByID((int)this.item.ID).paint(g, 7, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0004103C File Offset: 0x0003F43C
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(this.item.name);
				PopupShop.addStr(T.number + this.item.number);
			}
		}

		// Token: 0x04000908 RID: 2312
		private readonly int ii;

		// Token: 0x04000909 RID: 2313
		private Item item;
	}

	// Token: 0x020000F7 RID: 247
	private class CommandOpenKhoHang2 : Command
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x00041097 File Offset: 0x0003F497
		public CommandOpenKhoHang2(string s, FarmScr.IActionEmpty empty, int ii) : base(s, empty)
		{
			this.ii = ii;
			this.item = (Item)FarmScr.listItemFarm.elementAt(ii);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000410C0 File Offset: 0x0003F4C0
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmItem farmItem = FarmScr.getFarmItem((int)this.item.ID);
			farmItem.paint(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 0, 3);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000410FC File Offset: 0x0003F4FC
		public override void update()
		{
			if (PopupShop.isTransFocus && this.ii == PopupShop.focus - FarmScr.itemSeed.size())
			{
				PopupShop.resetIsTrans();
				FarmItem farmItem = FarmScr.getFarmItem((int)this.item.ID);
				PopupShop.addStr(farmItem.des);
				FarmItem farmItem2 = FarmScr.getFarmItem((int)this.item.ID);
				int num = this.item.number;
				if ((int)farmItem2.type == 4)
				{
					num -= FarmScr.listFood[1].size();
				}
				else if ((int)farmItem2.type == 1)
				{
					num -= FarmScr.listFood[0].size();
				}
				PopupShop.addStr(T.number + num);
			}
		}

		// Token: 0x0400090A RID: 2314
		private readonly int ii;

		// Token: 0x0400090B RID: 2315
		private Item item;
	}

	// Token: 0x020000F8 RID: 248
	private class IActionDoBuyItem1 : IAction
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x000411BE File Offset: 0x0003F5BE
		public IActionDoBuyItem1(int index, int a, int n)
		{
			this.index = index;
			this.a = a;
			this.n = n;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000411DB File Offset: 0x0003F5DB
		public void perform()
		{
			FarmService.gI().doBuyItem((short)this.index, (sbyte)this.n, 1, this.a * this.n);
		}

		// Token: 0x0400090C RID: 2316
		private readonly int index;

		// Token: 0x0400090D RID: 2317
		private readonly int a;

		// Token: 0x0400090E RID: 2318
		private readonly int n;
	}

	// Token: 0x020000F9 RID: 249
	private class IActionDoBuyItem2 : IAction
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x00041203 File Offset: 0x0003F603
		public IActionDoBuyItem2(int index, int b, int n)
		{
			this.index = index;
			this.b = b;
			this.n = n;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00041220 File Offset: 0x0003F620
		public void perform()
		{
			FarmService.gI().doBuyItem((short)this.index, (sbyte)this.n, 2, this.b * this.n);
		}

		// Token: 0x0400090F RID: 2319
		private readonly int index;

		// Token: 0x04000910 RID: 2320
		private readonly int b;

		// Token: 0x04000911 RID: 2321
		private readonly int n;
	}

	// Token: 0x020000FA RID: 250
	private class CommandGieoHat1 : Command
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x00041248 File Offset: 0x0003F648
		public CommandGieoHat1(string name, FarmScr.IActionHieoHat1 action, Item item) : base(name, action)
		{
			this.item = item;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00041259 File Offset: 0x0003F659
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.getTreeByID((int)this.item.ID).paint(g, 7, x, y, 3);
		}

		// Token: 0x04000912 RID: 2322
		private Item item;
	}

	// Token: 0x020000FB RID: 251
	private class IActionHieoHat1 : IAction
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x00041275 File Offset: 0x0003F675
		public IActionHieoHat1(int index)
		{
			this.index = index;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00041284 File Offset: 0x0003F684
		public void perform()
		{
			FarmScr.instance.setAuto(this.index);
		}

		// Token: 0x04000913 RID: 2323
		private int index;
	}

	// Token: 0x020000FC RID: 252
	private class IActionEat : IAction
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x00041296 File Offset: 0x0003F696
		public IActionEat(Animal pet)
		{
			this.pet = pet;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000412A8 File Offset: 0x0003F6A8
		public void perform()
		{
			bool flag = false;
			AnimalInfo animalByID = FarmData.getAnimalByID((int)this.pet.species);
			for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
			{
				Item item = (Item)FarmScr.listItemFarm.elementAt(i);
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				if ((int)farmItem.type == (int)animalByID.area && (int)farmItem.action == 5 && ((int)animalByID.area == 4 || (int)animalByID.area == 1))
				{
					int number = item.number;
					if (number > 0)
					{
						flag = true;
						this.pet.hunger = false;
						FarmScr.gI().doEat(farmItem.ID, this.pet.IDDB);
						FarmScr.gI().commandAction(10);
					}
				}
			}
			if (!flag)
			{
				Canvas.startOKDlg("Kho của bạn đã hết thức ăn, xin vào cửa hàng để mua.");
				FarmScr.gI().commandTab(8);
			}
		}

		// Token: 0x04000914 RID: 2324
		private Animal pet;
	}

	// Token: 0x020000FD RID: 253
	private class IActionTriBenh3 : IAction
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x000413A0 File Offset: 0x0003F7A0
		public IActionTriBenh3(Animal pet)
		{
			this.pet = pet;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000413B0 File Offset: 0x0003F7B0
		public void perform()
		{
			bool flag = false;
			for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
			{
				Item item = (Item)FarmScr.listItemFarm.elementAt(i);
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				if ((int)farmItem.action == 6)
				{
					FarmService.gI().doUsingItem(FarmScr.idFarm, this.pet.IDDB, (int)item.ID);
					flag = true;
					FarmScr.gI().commandAction(10);
					break;
				}
			}
			if (!flag)
			{
				FarmScr.gI().commandTab(8);
				Canvas.startOKDlg("Kho của bạn đã hết thuốc bổ, xin vào cửa hàng để mua.");
			}
		}

		// Token: 0x04000915 RID: 2325
		private Animal pet;
	}

	// Token: 0x020000FE RID: 254
	private class IActionTriBenh2 : IAction
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00041452 File Offset: 0x0003F852
		public IActionTriBenh2(Animal pet)
		{
			this.pet = pet;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00041464 File Offset: 0x0003F864
		public void perform()
		{
			bool flag = false;
			for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
			{
				Item item = (Item)FarmScr.listItemFarm.elementAt(i);
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				if (farmItem.ID == 121)
				{
					FarmScr.gI().setActionAnimal(farmItem, item.ID, this.pet);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Canvas.startOKDlg("Kho của bạn đã hết thuốc cúm, xin vào cửa hàng để mua.");
				FarmScr.gI().commandTab(8);
			}
		}

		// Token: 0x04000916 RID: 2326
		private Animal pet;
	}

	// Token: 0x020000FF RID: 255
	private class IActionTriBenh1 : IAction
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x000414F1 File Offset: 0x0003F8F1
		public IActionTriBenh1(Animal pet)
		{
			this.pet = pet;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00041500 File Offset: 0x0003F900
		public void perform()
		{
			bool flag = false;
			for (int i = 0; i < FarmScr.listItemFarm.size(); i++)
			{
				Item item = (Item)FarmScr.listItemFarm.elementAt(i);
				FarmItem farmItem = FarmScr.getFarmItem((int)item.ID);
				if (farmItem.ID == 120)
				{
					FarmScr.gI().setActionAnimal(farmItem, item.ID, this.pet);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Canvas.startOKDlg("Kho của bạn đã hết thuốc tiêu chảy, xin vào cửa hàng để mua.");
				FarmScr.gI().commandTab(8);
			}
		}

		// Token: 0x04000917 RID: 2327
		private Animal pet;
	}

	// Token: 0x02000100 RID: 256
	private class IActionCattleFeeding : IAction
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0004158D File Offset: 0x0003F98D
		public IActionCattleFeeding(sbyte type, Item item)
		{
			this.type = type;
			this.item = item;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000415A3 File Offset: 0x0003F9A3
		public void perform()
		{
			if ((int)this.type == 2)
			{
				Cattle.itemID = this.item.ID;
			}
			else
			{
				Dog.itemID = this.item.ID;
			}
		}

		// Token: 0x04000918 RID: 2328
		private readonly sbyte type;

		// Token: 0x04000919 RID: 2329
		private readonly Item item;
	}

	// Token: 0x02000101 RID: 257
	private class CommandCattleFeeding : Command
	{
		// Token: 0x06000722 RID: 1826 RVA: 0x000415D7 File Offset: 0x0003F9D7
		public CommandCattleFeeding(string name, FarmScr.IActionCattleFeeding feeding, FarmItem fItem) : base(name, feeding)
		{
			this.fItem = fItem;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000415E8 File Offset: 0x0003F9E8
		public override void paint(MyGraphics g, int x, int y)
		{
			this.fItem.paint(g, x, y, 0, 3);
		}

		// Token: 0x0400091A RID: 2330
		private readonly FarmItem fItem;
	}

	// Token: 0x02000102 RID: 258
	private class IActionSendHarvestAnimal : IAction
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x000415FA File Offset: 0x0003F9FA
		public IActionSendHarvestAnimal(Animal pet)
		{
			this.pet = pet;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00041609 File Offset: 0x0003FA09
		public void perform()
		{
			FarmService.gI().doHarvestAnimal(FarmScr.idFarm, this.pet.IDDB);
		}

		// Token: 0x0400091B RID: 2331
		private readonly Animal pet;
	}

	// Token: 0x02000103 RID: 259
	private class IActionPriceAnimal : IAction
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00041625 File Offset: 0x0003FA25
		public IActionPriceAnimal(sbyte index)
		{
			this.index = index;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00041634 File Offset: 0x0003FA34
		public void perform()
		{
			FarmService.gI().doSellAnimal(FarmScr.idFarm, this.index);
		}

		// Token: 0x0400091C RID: 2332
		private readonly sbyte index;
	}

	// Token: 0x02000104 RID: 260
	private class CommandMenuStarFruit1 : Command
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x0004164B File Offset: 0x0003FA4B
		public CommandMenuStarFruit1(string name, int type, int index) : base(name, type)
		{
			this.index = index;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0004165C File Offset: 0x0003FA5C
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmData.paintImg(g, this.index, x, y, 3);
		}

		// Token: 0x0400091D RID: 2333
		private int index;
	}

	// Token: 0x02000105 RID: 261
	private class CommandCooking1 : Command
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x0004166D File Offset: 0x0003FA6D
		public CommandCooking1(string caption, FarmScr.IActionCooking1 ac, int ii, Food food) : base(caption, ac)
		{
			this.food = food;
			this.ii = ii;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00041688 File Offset: 0x0003FA88
		public override void paint(MyGraphics g, int x, int y)
		{
			FarmItem farmItem = FarmScr.getFarmItem((int)this.food.productID);
			FarmData.paintImg(g, (int)farmItem.IDImg, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 3);
			g.translate(CameraList.cmx, CameraList.cmy);
			g.setClip(0f, 0f, (float)(6 * PopupShop.wCell), (float)PopupShop.h);
			if (this.ii == PopupShop.focus && !PopupShop.gI().isHide)
			{
				for (int i = 0; i < this.food.material.Length; i++)
				{
					Item item;
					if (this.food.material[i] < 50)
					{
						item = FarmScr.getProductByID((int)this.food.material[i]);
						FarmData.getTreeByID((int)this.food.material[i]).paint(g, 7, PopupShop.w / 2 - this.food.material.Length * 50 * AvMain.hd / 2 + 50 * i * AvMain.hd + 25 * (AvMain.hd - 1) + ((AvMain.hd != 1) ? 0 : 15), PopupShop.wCell * 2 + 10 * AvMain.hd + (int)AvMain.hBlack * 4 + ((AvMain.hd != 1) ? 0 : 15), 3);
					}
					else if (this.food.material[i] < 100)
					{
						item = FarmScr.getProductByID((int)this.food.material[i]);
						AnimalInfo animalByID = FarmData.getAnimalByID((int)this.food.material[i]);
						AvatarData.paintImg(g, (int)animalByID.iconProduct, PopupShop.w / 2 - this.food.material.Length * 50 * AvMain.hd / 2 + 50 * i * AvMain.hd + 25 * (AvMain.hd - 1) + ((AvMain.hd != 1) ? 0 : 15), PopupShop.wCell * 2 + 10 * AvMain.hd + (int)AvMain.hBlack * 4 + ((AvMain.hd != 1) ? 0 : 15), 3);
					}
					else
					{
						item = FarmScr.getItemProductByID((int)this.food.material[i]);
						FarmItem farmItem2 = FarmScr.getFarmItem((int)this.food.material[i]);
						FarmData.paintImg(g, (int)farmItem2.IDImg, PopupShop.w / 2 - this.food.material.Length * 50 * AvMain.hd / 2 + 50 * i * AvMain.hd + 25 * (AvMain.hd - 1) + ((AvMain.hd != 1) ? 0 : 15), PopupShop.wCell * 2 + 10 * AvMain.hd + (int)AvMain.hBlack * 4 + ((AvMain.hd != 1) ? 0 : 15), 3);
					}
					FontX fontX = Canvas.blackF;
					if (item == null || item.number < (int)this.food.numberMaterial[i])
					{
						fontX = Canvas.arialFont;
					}
					fontX.drawString(g, this.food.numberMaterial[i] + string.Empty, PopupShop.w / 2 - this.food.material.Length * 50 * AvMain.hd / 2 + 50 * i * AvMain.hd - 1 + 25 * (AvMain.hd - 1) + ((AvMain.hd != 1) ? 0 : 15), PopupShop.wCell * 2 + 10 * AvMain.hd + (int)AvMain.hBlack * 4 + 8 * AvMain.hd + ((AvMain.hd != 1) ? 0 : 15), 2);
					if (i != this.food.material.Length - 1)
					{
						Canvas.blackF.drawString(g, "+", PopupShop.w / 2 - this.food.material.Length * 50 * AvMain.hd / 2 + 50 * i * AvMain.hd + 25 * AvMain.hd + 25 * (AvMain.hd - 1) + ((AvMain.hd != 1) ? 0 : 15), PopupShop.wCell * 2 + 10 * AvMain.hd + (int)AvMain.hBlack * 4 + ((AvMain.hd != 1) ? 0 : 15), 2);
					}
				}
			}
			g.setClip(0f, 0f, (float)(6 * PopupShop.wCell), (float)(PopupShop.numH * PopupShop.wCell - PopupShop.duCam));
			g.translate(-CameraList.cmx, -CameraList.cmy);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00041B14 File Offset: 0x0003FF14
		public override void update()
		{
			if (this.ii == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				PopupShop.addStr(this.food.text);
				PopupShop.addStr(T.time + this.food.cookTime + "phut");
				FarmItem farmItem = FarmScr.getFarmItem((int)this.food.productID);
				if (farmItem.priceXu > 0)
				{
					PopupShop.addStr(T.salePrice + Canvas.getMoneys(farmItem.priceXu) + T.xu);
				}
				else if (farmItem.priceLuong > 0)
				{
					PopupShop.addStr(T.salePrice + Canvas.getMoneys(farmItem.priceLuong) + T.xu);
				}
				PopupShop.addStr(T.material);
			}
		}

		// Token: 0x0400091E RID: 2334
		private int ii;

		// Token: 0x0400091F RID: 2335
		private Food food;
	}

	// Token: 0x02000106 RID: 262
	private class IActionCooking1 : IAction
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x00041BE0 File Offset: 0x0003FFE0
		public IActionCooking1(Food f)
		{
			this.food = f;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00041BF0 File Offset: 0x0003FFF0
		public void perform()
		{
			for (int i = 0; i < this.food.material.Length; i++)
			{
				string str = string.Empty;
				Item item;
				if (this.food.material[i] < 100)
				{
					item = FarmScr.getProductByID((int)this.food.material[i]);
					if (this.food.material[i] < 50)
					{
						str = FarmData.getTreeByID((int)this.food.material[i]).name;
					}
					else if ((int)FarmData.getAnimalByID((int)this.food.material[i]).area == 1)
					{
						str = T.egg + " " + FarmData.getAnimalByID((int)this.food.material[i]).name;
					}
					else if ((int)FarmData.getAnimalByID((int)this.food.material[i]).area == 2)
					{
						str = T.milk + " " + FarmData.getAnimalByID((int)this.food.material[i]).name;
					}
				}
				else
				{
					item = FarmScr.getItemProductByID((int)this.food.material[i]);
					str = FarmScr.getFarmItem((int)this.food.material[i]).des;
				}
				if (item == null || item.number < (int)this.food.numberMaterial[i])
				{
					Canvas.startOKDlg(T.notEnough + str);
					return;
				}
			}
			FarmService.gI().doCooking(this.food.ID);
			PopupShop.gI().close();
		}

		// Token: 0x04000920 RID: 2336
		private Food food;
	}

	// Token: 0x02000107 RID: 263
	private class CommandCooking2 : Command
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x00041D90 File Offset: 0x00040190
		public override void paint(MyGraphics g, int x, int y)
		{
			Food foodByID = FarmData.getFoodByID(FarmScr.foodID);
			FarmItem farmItem = FarmScr.getFarmItem((int)foodByID.productID);
			FarmData.paintImg(g, (int)farmItem.IDImg, Canvas.cameraList.disX / 2, PopupShop.h / 2 - 30, 3);
			Canvas.blackF.drawString(g, foodByID.text, Canvas.cameraList.disX / 2, PopupShop.h / 2 - 30 + 5 + (int)(FarmData.getImgIcon(farmItem.IDImg).h / 2) + (int)AvMain.hSmall + 2, 2);
			string text = string.Empty;
			int num = FarmScr.remainTime / 3600;
			FontX fontX = Canvas.smallFontYellow;
			if (num > 0)
			{
				text = num + ":";
			}
			int num2 = (FarmScr.remainTime - num * 3600) / 60;
			if (num2 > 0 || num > 0)
			{
				text = text + num2 + ":";
			}
			int num3 = FarmScr.remainTime - num * 3600 - num2 * 60;
			text = text + num3 + string.Empty;
			if (FarmScr.remainTime == 0)
			{
				text = T.done;
				fontX = Canvas.blackF;
			}
			fontX.drawString(g, text, Canvas.cameraList.disX / 2, PopupShop.h / 2 - 30 + 5 + (int)(FarmData.getImgIcon(farmItem.IDImg).h / 2), 2);
		}
	}

	// Token: 0x02000108 RID: 264
	private class CommandCooking : Command
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00041EF6 File Offset: 0x000402F6
		public CommandCooking(string caption, int index, AvMain pointer) : base(caption, index, pointer)
		{
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00041F04 File Offset: 0x00040304
		public override void update()
		{
			if (PopupShop.gI().center != null)
			{
				PopupShop.gI().center.x = Canvas.w / 2 + PaintPopup.wButtonSmall;
				PopupShop.gI().center.y = PopupShop.y + PopupShop.h - PaintPopup.hButtonSmall;
			}
			if (PopupShop.gI().left != null)
			{
				PopupShop.gI().left.x = Canvas.w / 2 - PaintPopup.wButtonSmall;
				PopupShop.gI().left.y = PopupShop.y + PopupShop.h - PaintPopup.hButtonSmall;
			}
			else if (PopupShop.gI().center != null)
			{
				PopupShop.gI().center.x = Canvas.w / 2;
				PopupShop.gI().center.y = PopupShop.y + PopupShop.h - PaintPopup.hButtonSmall;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00041FF4 File Offset: 0x000403F4
		public override void paint(MyGraphics g, int x, int y)
		{
			Food foodByID = FarmData.getFoodByID(FarmScr.foodID);
			FarmItem farmItem = FarmScr.getFarmItem((int)foodByID.productID);
			FarmData.paintImg(g, (int)farmItem.IDImg, Canvas.cameraList.disX / 2, PopupShop.h / 3 - 25 * AvMain.hd, 3);
			Canvas.blackF.drawString(g, foodByID.text, Canvas.cameraList.disX / 2, PopupShop.h / 3 - 15 * AvMain.hd + 5 + (int)(FarmData.getImgIcon(farmItem.IDImg).h / 2) + (int)AvMain.hSmall + 2, 2);
			string text = string.Empty;
			int num = FarmScr.remainTime / 3600;
			FontX fontX = Canvas.smallFontYellow;
			if (num > 0)
			{
				text = num + ":";
			}
			int num2 = (FarmScr.remainTime - num * 3600) / 60;
			if (num2 > 0 || num > 0)
			{
				text = text + num2 + ":";
			}
			int num3 = FarmScr.remainTime - num * 3600 - num2 * 60;
			text = text + num3 + string.Empty;
			if (FarmScr.remainTime == 0)
			{
				text = T.done;
				fontX = Canvas.blackF;
			}
			fontX.drawString(g, text, Canvas.cameraList.disX / 2, PopupShop.h / 3 - 20 * AvMain.hd + 5 + (int)(FarmData.getImgIcon(farmItem.IDImg).h / 2), 2);
		}
	}
}
