using System;
using UnityEngine;

// Token: 0x0200019D RID: 413
public class HouseScr : MyScreen, IChatable
{
	// Token: 0x06000B35 RID: 2869 RVA: 0x00070888 File Offset: 0x0006EC88
	public HouseScr()
	{
		this.cmdBrick = new Command(T.sett, 0);
		this.cmdFinish = new Command(T.finish, 1);
		this.cmdMenu = new Command(T.menu, 2);
		this.imgBuyItem = new FrameImage(Image.createImagePNG(T.getPath() + "/temp/buyItem"), 25 * AvMain.hd, 25 * AvMain.hd);
		this.cmdRotate = new Command(T.rota, 16, this);
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0007097F File Offset: 0x0006ED7F
	public static HouseScr gI()
	{
		if (HouseScr.me == null)
		{
			HouseScr.me = new HouseScr();
		}
		return HouseScr.me;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x0007099A File Offset: 0x0006ED9A
	public void switchToMe(MyScreen sre)
	{
		this.lastScr = sre;
		base.switchToMe();
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x000709A9 File Offset: 0x0006EDA9
	public override void initZoom()
	{
		AvCamera.gI().init(70 + (int)this.typeHome);
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x000709BF File Offset: 0x0006EDBF
	public override void switchToMe()
	{
		base.switchToMe();
		SoundManager.playSoundBG(83);
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x000709D0 File Offset: 0x0006EDD0
	private void addPlayer()
	{
		LoadMap.addPlayer(GameMidlet.avatar);
		GameMidlet.avatar.x = this.posJoin.x;
		GameMidlet.avatar.y = this.posJoin.y;
		GameMidlet.avatar.action = 0;
		AvCamera.gI().setToPos((float)(this.posJoin.x * AvMain.hd), (float)(this.posJoin.y * AvMain.hd));
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00070A4A File Offset: 0x0006EE4A
	public override void doMenu()
	{
		this.doMenuHouse();
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00070A54 File Offset: 0x0006EE54
	protected void doMenuHouse()
	{
		MyVector myVector = new MyVector();
		if (this.idHouse == GameMidlet.avatar.IDDB)
		{
			myVector.addElement(new Command(T.container, 6, this));
			myVector.addElement(new Command(T.homeRepait, 7, this));
			int num = 0;
			for (int i = 0; i < LoadMap.playerLists.size(); i++)
			{
				MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
				if ((int)myObject.catagory == 0)
				{
					num++;
				}
			}
			if (num > 1)
			{
				myVector.addElement(new Command(T.kick, 8, this));
			}
			myVector.addElement(new Command(T.setPass, 9, this));
		}
		myVector.addElement(new Command(T.exit, 13, this));
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00070B28 File Offset: 0x0006EF28
	public override void close()
	{
		MapScr.gI().doExit();
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00070B34 File Offset: 0x0006EF34
	private void doOption()
	{
		this.right = new Command(T.finish, 9);
		this.left = new Command(T.sell, 17, this);
		HouseScr.isChange = true;
		this.x = GameMidlet.avatar.x;
		this.y = GameMidlet.avatar.y;
		LoadMap.removePlayer(GameMidlet.avatar);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00070B98 File Offset: 0x0006EF98
	protected void doBuyItem()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(T.buyItem, 10, this));
		myVector.addElement(new Command(T.latGach, 11, this));
		if (this.listItem.size() > 0)
		{
			myVector.addElement(new Command(T.sellItem, 12, this));
		}
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00070C00 File Offset: 0x0006F000
	private void setStatusBuyItem()
	{
		HomeMsgHandler.onHandler();
		this.x = GameMidlet.avatar.x;
		this.y = GameMidlet.avatar.y;
		LoadMap.removePlayer(GameMidlet.avatar);
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00070C34 File Offset: 0x0006F034
	protected void doDiChuyen()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(T.move, 11));
		myVector.addElement(new Command(T.rota, 12));
		myVector.addElement(new Command(T.sell, 13));
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00070C88 File Offset: 0x0006F088
	protected void doKick()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < LoadMap.playerLists.size(); i++)
		{
			Base @base = (Base)LoadMap.playerLists.elementAt(i);
			if ((int)@base.catagory == 0 && @base.IDDB != GameMidlet.avatar.IDDB)
			{
				myVector.addElement(new Command(@base.name, new HouseScr.IActionKick(@base)));
			}
		}
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00070D0A File Offset: 0x0006F10A
	protected void doCreateMap()
	{
		if (this.listTile == null)
		{
			HomeMsgHandler.onHandler();
			AvatarService.gI().doGetTileInfo();
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x00070D2C File Offset: 0x0006F12C
	private void doSelectMap()
	{
		HouseScr.isBuyTileMap = false;
		HouseScr.me.indexTileMapBuy = -1;
		MyVector myVector = new MyVector();
		for (int i = 0; i < this.listTile.Length; i++)
		{
			int i2 = i;
			if (this.listTile[i].priceXu != -1 || this.listTile[i].priceLuong != -1)
			{
				myVector.addElement(new HouseScr.CommandMap(this.listTile[i].name + "(" + Canvas.getPriceMoney(this.listTile[i].priceXu, this.listTile[i].priceLuong, true) + ")", new HouseScr.IActionMap(this, i2), i2));
			}
		}
		if (myVector.size() > 0)
		{
			Menu.gI().startMenuFarm(myVector, Canvas.hw, Canvas.h - 70 * AvMain.hd - 10, 70 * AvMain.hd, 70 * AvMain.hd);
		}
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00070E20 File Offset: 0x0006F220
	private void reset()
	{
		this.isSelectedItem = -1;
		this.selected = -1;
		HouseScr.isChange = false;
		HouseScr.isSelectObj = false;
		this.left = null;
		this.center = null;
		this.right = null;
		if (LoadMap.getAvatar(GameMidlet.avatar.IDDB) == null)
		{
			this.addPlayer();
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00070E78 File Offset: 0x0006F278
	private void doSelectObject()
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < AvatarData.listMapItemType.size(); i++)
		{
			MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(i);
			if ((int)mapItemType.buy != 0 && (((int)this.typeHome != 4 && ((int)mapItemType.buy == 1 || (int)mapItemType.buy == 2)) || (int)this.typeHome == 4))
			{
				int num = mapItemType.name.IndexOf(":");
				if (num != -1)
				{
					int num2 = 0;
					string text = mapItemType.name.Substring(0, num);
					for (int j = 0; j < myVector.size(); j++)
					{
						if (((Command)myVector.elementAt(j)).caption.Equals(text))
						{
							num2 = 1;
						}
					}
					if (num2 == 0 || myVector.size() == 0)
					{
						myVector.addElement(new Command(text, new HouseScr.IActionObject(HouseScr.me, text)));
					}
				}
			}
		}
		MenuCenter.gI().startAt(myVector);
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00070F94 File Offset: 0x0006F394
	private void doSelectedItem(string name)
	{
		this.reset();
		this.nameSelectedItem = name;
		MyVector myVector = new MyVector();
		int num = 90 * AvMain.hd;
		int num2 = 90 * AvMain.hd;
		for (int i = 0; i < AvatarData.listMapItemType.size(); i++)
		{
			MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(i);
			int num3 = mapItemType.name.IndexOf(name);
			if ((int)mapItemType.buy != 0 && num3 != -1 && (((int)this.typeHome != 4 && ((int)mapItemType.buy == 1 || (int)mapItemType.buy == 2)) || (int)this.typeHome == 4))
			{
				string text = mapItemType.name.Substring(mapItemType.name.IndexOf(":") + 1);
				string text2 = string.Empty;
				if (mapItemType.priceXu > 0)
				{
					text2 = text2 + Canvas.getMoneys(mapItemType.priceXu) + T.xu;
				}
				if (mapItemType.priceLuong > 0)
				{
					if (mapItemType.priceXu > 0)
					{
						text2 += " - ";
					}
					text2 = text2 + Canvas.getMoneys((int)mapItemType.priceLuong) + "l";
				}
				int i2 = i;
				int hh = num2;
				myVector.addElement(new HouseScr.CommandItem(string.Empty, new HouseScr.IActionItem(this, i2, text), mapItemType, text, text2, hh));
			}
		}
		if (myVector.size() > 0)
		{
			Menu.gI().startMenuFarm(myVector, Canvas.hw, Canvas.h - num2 - 10, 120 * AvMain.hd, num2);
			Menu.gI().iNo = new HouseScr.IActionNo(this);
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00071148 File Offset: 0x0006F548
	private bool isDisable(MapItemType map, int x0, int y0)
	{
		x0 += 12;
		y0 += 12;
		if ((int)map.buy != 2 && (int)map.buy != 4)
		{
			if ((int)LoadMap.type[y0 / 24 * (int)LoadMap.wMap + x0 / 24] != 80)
			{
				Canvas.startOKDlg(T.noPlaceItemHere);
				return true;
			}
			for (int i = 0; i < map.listNotTrans.size(); i++)
			{
				AvPosition avPosition = (AvPosition)map.listNotTrans.elementAt(i);
				if ((int)LoadMap.type[((y0 + 12) / 24 + avPosition.y) * (int)LoadMap.wMap + ((x0 + 12) / 24 + avPosition.x)] != 80)
				{
					Canvas.startOKDlg(T.noPlaceItemHere);
					return true;
				}
			}
		}
		else
		{
			string text = string.Empty;
			for (int j = 0; j < this.listItem.size(); j++)
			{
				MapItem mapItem = (MapItem)this.listItem.elementAt(j);
				if (mapItem.typeID == map.idType && j != this.indexChangeItem && x0 / 24 == mapItem.x / 24 && y0 / 24 == mapItem.y / 24)
				{
					text = T.haveItem;
					break;
				}
			}
			if (!text.Equals(string.Empty))
			{
				Canvas.startOKDlg(text);
				return true;
			}
			if (((int)map.buy == 2 || (int)map.buy == 4) && ((int)LoadMap.type[y0 / 24 * (int)LoadMap.wMap + x0 / 24] != 80 || (int)LoadMap.type[(y0 / 24 - 1) * (int)LoadMap.wMap + x0 / 24] != 88))
			{
				Canvas.startOKDlg(T.setTuong);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00071314 File Offset: 0x0006F714
	private void doBuyItemHouse(int ii, string name)
	{
		MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(ii);
		if (this.isDisable(mapItemType, this.xItemTranBuy, this.yItemTranBuy))
		{
			return;
		}
		Canvas.getTypeMoney(mapItemType.priceXu, (int)mapItemType.priceLuong, new HouseScr.IActionBuyItem(this, mapItemType, 1, name), new HouseScr.IActionBuyItem(this, mapItemType, 2, name), new HouseScr.IActionBuyItemClose(this));
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00071374 File Offset: 0x0006F774
	public void onBuyItemHouse(MapItem map3)
	{
		if (this.isSetTuong(map3))
		{
			map3.y++;
		}
		this.listItem.addElement(map3);
		LoadMap.treeLists.addElement(map3);
		this.setType(map3);
		LoadMap.orderVector(LoadMap.treeLists);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x000713C4 File Offset: 0x0006F7C4
	protected void doBrick()
	{
		if (this.selected == -1)
		{
			return;
		}
		int num = (this.y + 12) / 24 * (int)LoadMap.wMap + (this.x + 12) / 24;
		if (this.listTile[(int)LoadMap.map[num]].priceLuong == -1 && this.listTile[(int)LoadMap.map[num]].priceXu == -1)
		{
			Canvas.startOKDlg(T.noPlaceItemHere);
			return;
		}
		if ((this.indexTileMapBuy < (int)HouseScr.numW && LoadMap.map[num] >= HouseScr.numW) || (this.indexTileMapBuy >= (int)HouseScr.numW && LoadMap.map[num] < HouseScr.numW))
		{
			Canvas.startOKDlg(T.noPlaceItemHere);
			return;
		}
		HouseScr.xTemp = this.x;
		HouseScr.yTemp = this.y;
		LoadMap.map[(this.y + 12) / 24 * (int)LoadMap.wMap + (this.x + 12) / 24] = (short)this.indexTileMapBuy;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x000714CC File Offset: 0x0006F8CC
	public override void updateKey()
	{
		base.updateKey();
		if (!HouseScr.isDuyChuyen && !HouseScr.isTranItemBuy && !HouseScr.isBuyTileMap)
		{
			if (!ChatTextField.isShow && PopupShop.gI() != Canvas.currentMyScreen && Input.touchCount <= 1 && !Canvas.isZoom)
			{
				Canvas.loadMap.updatePointer();
			}
			GameMidlet.avatar.updateKey();
		}
		else if (HouseScr.isDuyChuyen)
		{
			this.updateKeyMoveItem();
		}
		else if (HouseScr.isTranItemBuy)
		{
			this.updateKeyBuyItem();
		}
		else if (HouseScr.isBuyTileMap)
		{
			this.updateKeyBuyTile();
		}
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00071580 File Offset: 0x0006F980
	private void updateKeyBuyTile()
	{
		if (Canvas.isPointerClick)
		{
			Canvas.isPointerClick = false;
			this.isTranItem = true;
		}
		if (this.isTranItem)
		{
			if (Canvas.isPointerDown && (CRes.abs(Canvas.dx()) > 10 || CRes.abs(Canvas.dy()) > 10))
			{
				this.isTranItem = false;
				Canvas.isPointerClick = true;
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranItem = false;
				if (CRes.abs(Canvas.dx()) < 10 && CRes.abs(Canvas.dy()) < 10)
				{
					int num = (int)((float)Canvas.px / AvMain.zoom + AvCamera.gI().xCam) / AvMain.hd / 24;
					int num2 = (int)((float)Canvas.py / AvMain.zoom + AvCamera.gI().yCam) / AvMain.hd / 24;
					int num3 = num2 * (int)LoadMap.wMap + num;
					if (((this.indexTileMapBuy < (int)HouseScr.numW && LoadMap.map[num3] < HouseScr.numW) || (this.indexTileMapBuy >= (int)HouseScr.numW && LoadMap.map[num3] >= HouseScr.numW)) && (this.listTile[(int)LoadMap.map[num3]].priceLuong != -1 || this.listTile[(int)LoadMap.map[num3]].priceXu != -1))
					{
						LoadMap.map[num2 * (int)LoadMap.wMap + num] = (short)this.indexTileMapBuy;
					}
				}
			}
		}
		Canvas.loadMap.updateKey();
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00071704 File Offset: 0x0006FB04
	private void updateKeyBuyItem()
	{
		if (Canvas.isPointerClick)
		{
			int num = (int)((float)Canvas.px / AvMain.zoom + AvCamera.gI().xCam) / AvMain.hd;
			int num2 = (int)((float)Canvas.py / AvMain.zoom + AvCamera.gI().yCam) / AvMain.hd;
			MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(this.indexItemTranBuy);
			Image img = AvatarData.getImgIcon(mapItemType.imgID).img;
			if (num > this.xItemTranBuy + (int)mapItemType.dx && num < this.xItemTranBuy + (int)mapItemType.dx + img.w / AvMain.hd && num2 > this.yItemTranBuy + (int)mapItemType.dy && num2 < this.yItemTranBuy + (int)mapItemType.dy + img.h / AvMain.hd)
			{
				this.isTranItem = true;
				this.xTempItem = this.xItemTranBuy;
				this.yTempItem = this.yItemTranBuy;
				Canvas.isPointerClick = false;
			}
			else if (Canvas.isPoint((int)((float)((this.xItemTranBuy + (int)mapItemType.dx) * AvMain.hd - this.imgBuyItem.frameWidth - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam) * AvMain.zoom), (int)((float)((this.yItemTranBuy + (int)mapItemType.dy) * AvMain.hd - this.imgBuyItem.frameHeight - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom)))
			{
				Canvas.isPointerClick = false;
				this.isTranItem = true;
				this.indexFireItem = 1;
			}
			else if (Canvas.isPoint((int)((float)((this.xItemTranBuy + (int)mapItemType.dx) * AvMain.hd + img.w + this.imgBuyItem.frameWidth - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam) * AvMain.zoom), (int)((float)((this.yItemTranBuy + (int)mapItemType.dy) * AvMain.hd - this.imgBuyItem.frameHeight - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom)))
			{
				Canvas.isPointerClick = false;
				this.isTranItem = true;
				this.indexCloseItem = 1;
			}
		}
		if (this.isTranItem)
		{
			if (Canvas.isPointerDown && (CRes.abs(Canvas.dx()) > 10 || CRes.abs(Canvas.dy()) > 10))
			{
				MapItemType mapItemType2 = (MapItemType)AvatarData.listMapItemType.elementAt(this.indexItemTranBuy);
				Image img2 = AvatarData.getImgIcon(mapItemType2.imgID).img;
				if (this.indexFireItem == 1 && !Canvas.isPoint((this.xItemTranBuy + (int)mapItemType2.dx) * AvMain.hd - this.imgBuyItem.frameWidth - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam, (this.yItemTranBuy + (int)mapItemType2.dy) * AvMain.hd - this.imgBuyItem.frameHeight - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam, 32 * AvMain.hd, 32 * AvMain.hd))
				{
					this.indexFireItem = 0;
				}
				else if (this.indexCloseItem == 1 && !Canvas.isPoint((this.xItemTranBuy + (int)mapItemType2.dx) * AvMain.hd + img2.w + this.imgBuyItem.frameWidth - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam, (this.yItemTranBuy + (int)mapItemType2.dy) * AvMain.hd - this.imgBuyItem.frameHeight - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam, 32 * AvMain.hd, 32 * AvMain.hd))
				{
					this.indexCloseItem = 0;
				}
				else
				{
					this.xItemTranBuy = (int)((float)this.xTempItem - (float)Canvas.dx() / ((float)AvMain.hd * AvMain.zoom));
					this.yItemTranBuy = (int)((float)this.yTempItem - (float)Canvas.dy() / ((float)AvMain.hd * AvMain.zoom));
				}
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranItem = false;
				if (this.indexFireItem == 1)
				{
					MapItemType mapItemType3 = (MapItemType)AvatarData.listMapItemType.elementAt(this.indexItemTranBuy);
					string name = mapItemType3.name.Substring(mapItemType3.name.IndexOf(":") + 1);
					this.doBuyItemHouse(this.indexItemTranBuy, name);
				}
				else if (this.indexCloseItem == 1)
				{
					HouseScr.me.reset();
					Canvas.endDlg();
					this.indexItemTranBuy = -1;
					HouseScr.isTranItemBuy = false;
					this.doSelectedItem(this.nameSelectedItem);
				}
				this.indexCloseItem = (this.indexFireItem = 0);
			}
		}
		Canvas.loadMap.updateKey();
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x00071C94 File Offset: 0x00070094
	private void updateKeyMoveItem()
	{
		if (Canvas.isPointerClick)
		{
			int num = (int)((float)Canvas.px / AvMain.zoom + AvCamera.gI().xCam) / AvMain.hd;
			int num2 = (int)((float)Canvas.py / AvMain.zoom + AvCamera.gI().yCam) / AvMain.hd;
			if (!this.isMoveItem)
			{
				for (int i = 0; i < this.listItem.size(); i++)
				{
					MapItem mapItem = (MapItem)this.listItem.elementAt(i);
					MapItemType mapItemTypeByID;
					if (mapItem.isGetImg)
					{
						mapItemTypeByID = LoadMap.getMapItemTypeByID((int)mapItem.typeID);
					}
					else
					{
						mapItemTypeByID = AvatarData.getMapItemTypeByID((int)mapItem.typeID);
					}
					ImageIcon imgIcon = AvatarData.getImgIcon(mapItemTypeByID.imgID);
					if (imgIcon.count != -1 && Canvas.isPoint((int)(((float)((mapItem.x + (int)mapItemTypeByID.dx) * AvMain.hd) - AvCamera.gI().xCam) * AvMain.zoom), (int)(((float)((mapItem.y + (int)mapItemTypeByID.dy) * AvMain.hd) - AvCamera.gI().yCam) * AvMain.zoom), (int)((float)imgIcon.img.w * AvMain.zoom), (int)((float)imgIcon.img.h * AvMain.zoom)))
					{
						Canvas.isPointerClick = false;
						this.isTranItem = true;
						this.indexChangeItem = i;
						this.xItemOld = (this.xTempItem = mapItem.x + 12);
						this.yItemOld = (this.yTempItem = mapItem.y + 12);
						this.removeTrans(mapItem);
						break;
					}
				}
			}
			else
			{
				MapItem mapItem2 = (MapItem)this.listItem.elementAt(this.indexChangeItem);
				MapItemType mapItemTypeByID2;
				if (mapItem2.isGetImg)
				{
					mapItemTypeByID2 = LoadMap.getMapItemTypeByID((int)mapItem2.typeID);
				}
				else
				{
					mapItemTypeByID2 = AvatarData.getMapItemTypeByID((int)mapItem2.typeID);
				}
				ImageIcon imgIcon2 = AvatarData.getImgIcon(mapItemTypeByID2.imgID);
				if (imgIcon2.count == -1)
				{
					return;
				}
				if (Canvas.isPoint((int)(((float)((mapItem2.x + (int)mapItemTypeByID2.dx) * AvMain.hd) - AvCamera.gI().xCam) * AvMain.zoom), (int)(((float)((mapItem2.y + (int)mapItemTypeByID2.dy) * AvMain.hd) - AvCamera.gI().yCam) * AvMain.zoom), (int)((float)imgIcon2.img.w * AvMain.zoom), (int)((float)imgIcon2.img.h * AvMain.zoom)))
				{
					this.xTempItem = mapItem2.x;
					this.yTempItem = mapItem2.y;
					this.isTranItem = true;
					Canvas.isPointerClick = false;
				}
				else if (Canvas.isPoint((int)((float)(mapItem2.X() * AvMain.hd - this.imgBuyItem.frameWidth - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam) * AvMain.zoom), (int)((float)(mapItem2.Y() * AvMain.hd - this.imgBuyItem.frameHeight - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom)))
				{
					this.indexFireItem = 1;
					this.isTranItem = true;
					Canvas.isPointerClick = false;
				}
				else if (Canvas.isPoint((int)((float)(mapItem2.X() * AvMain.hd + (int)mapItem2.w + this.imgBuyItem.frameWidth - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam) * AvMain.zoom), (int)((float)(mapItem2.Y() * AvMain.hd - this.imgBuyItem.frameHeight - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom)))
				{
					this.indexCloseItem = 1;
					this.isTranItem = true;
					Canvas.isPointerClick = false;
				}
				else if (Canvas.isPoint((int)((float)(mapItem2.X() * AvMain.hd + (int)(mapItem2.w / 2) - this.imgBuyItem.frameWidth / 2 - 3 * AvMain.hd - (int)AvCamera.gI().xCam) * AvMain.zoom), (int)((float)(mapItem2.Y() * AvMain.hd - this.imgBuyItem.frameHeight * 2 - this.imgBuyItem.frameHeight / 2 - 3 * AvMain.hd - (int)AvCamera.gI().yCam) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom), (int)((float)(32 * AvMain.hd) * AvMain.zoom)))
				{
					this.indexRotateItem = 1;
					this.isTranItem = true;
					Canvas.isPointerClick = false;
				}
			}
		}
		if (this.isTranItem)
		{
			if (Canvas.isPointerDown && this.indexChangeItem != -1 && this.isMoveItem && this.xTempItem != -1 && (CRes.abs(Canvas.dx()) > 10 || CRes.abs(Canvas.dy()) > 10))
			{
				MapItem mapItem3 = (MapItem)this.listItem.elementAt(this.indexChangeItem);
				mapItem3.x = (mapItem3.xTo = (int)((float)this.xTempItem - (float)Canvas.dx() / ((float)AvMain.hd * AvMain.zoom)));
				mapItem3.y = (mapItem3.yTo = (int)((float)this.yTempItem - (float)Canvas.dy() / ((float)AvMain.hd * AvMain.zoom)));
			}
			if (Canvas.isPointerRelease)
			{
				Canvas.isPointerRelease = false;
				this.isTranItem = false;
				this.xTempItem = (this.yTempItem = -1);
				if (this.indexChangeItem != -1)
				{
					if (this.isMoveItem && CRes.abs(Canvas.dx()) <= 10 && CRes.abs(Canvas.dy()) <= 10)
					{
						this.setItemChange();
					}
					else
					{
						this.isMoveItem = true;
					}
				}
			}
		}
		Canvas.loadMap.updateKey();
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00072304 File Offset: 0x00070704
	private void setItemChange()
	{
		MapItem mapItem = (MapItem)this.listItem.elementAt(this.indexChangeItem);
		this.center = null;
		if (this.indexFireItem == 1)
		{
			this.indexFireItem = 0;
			if (!this.isDisable(AvatarData.getMapItemTypeByID((int)mapItem.typeID), mapItem.x, mapItem.y))
			{
				AvatarService.gI().doSortItem((int)mapItem.typeID, this.xItemOld / 24, this.yItemOld / 24, (mapItem.x + 12) / 24, (mapItem.y + 12) / 24, (int)mapItem.dir);
				this.isSelectedItem = -1;
				this.selected = -1;
				mapItem.x = (mapItem.xTo = (mapItem.x + 12) / 24 * 24);
				mapItem.y = (mapItem.yTo = (mapItem.y + 12) / 24 * 24);
				int num = this.yItemOld / 24 * (int)LoadMap.wMap + this.xItemOld / 24;
				LoadMap.type[num] = 80;
				this.setType(mapItem);
				LoadMap.orderVector(LoadMap.treeLists);
				this.doOption();
				this.isMoveItem = false;
				this.indexChangeItem = -1;
			}
		}
		else if (this.indexCloseItem == 1)
		{
			mapItem.xTo = this.xItemOld / 24 * 24;
			mapItem.yTo = this.yItemOld / 24 * 24;
			this.indexCloseItem = 0;
			this.indexChangeItem = -1;
			this.isMoveItem = false;
			this.setType(mapItem);
		}
		else if (this.indexRotateItem == 1)
		{
			this.indexRotateItem = 0;
			this.cmdRotate.perform();
		}
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x000724AC File Offset: 0x000708AC
	private bool setCollision(int x, int y)
	{
		return (int)LoadMap.map[y * (int)LoadMap.wMap + x] == LoadMap.imgMap.nFrame - 2 || LoadMap.map[y * (int)LoadMap.wMap + x] == -1;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x000724E8 File Offset: 0x000708E8
	public void moveCamera()
	{
		if (Canvas.menuMain != null || Canvas.currentDialog != null)
		{
			return;
		}
		if (AvCamera.gI().vY != 0f)
		{
			if (AvCamera.gI().yCam + AvCamera.gI().vY / 15f < (float)(-(float)(Canvas.hCan / 3 - 100 * AvMain.hd)))
			{
				if (AvCamera.gI().yCam + AvCamera.gI().vY / 15f < (float)(-(float)(Canvas.hCan / 3)))
				{
					AvCamera.gI().yCam = (AvCamera.gI().yTo = (float)(-(float)(Canvas.hCan / 3)));
					AvCamera.gI().vY /= 6f;
					AvCamera.gI().vY *= -1f;
				}
				else
				{
					AvCamera.gI().vY -= AvCamera.gI().vY / 20f;
				}
			}
			if (AvCamera.gI().yCam + AvCamera.gI().vY / 15f > AvCamera.gI().yLimit + (float)(LoadMap.w * AvMain.hd) - (float)(100 * AvMain.hd))
			{
				if (AvCamera.gI().yCam + AvCamera.gI().vY / 15f >= AvCamera.gI().yLimit + (float)(LoadMap.w * AvMain.hd))
				{
					AvCamera.gI().yCam = (AvCamera.gI().yTo = AvCamera.gI().yLimit + (float)(LoadMap.w * AvMain.hd));
					AvCamera.gI().vY /= 6f;
					AvCamera.gI().vY *= -1f;
				}
				else
				{
					AvCamera.gI().vY -= AvCamera.gI().vY / 20f;
				}
			}
			AvCamera.gI().yCam += AvCamera.gI().vY / 15f;
			AvCamera.gI().yTo = AvCamera.gI().yCam;
			AvCamera.gI().vY -= AvCamera.gI().vY / 20f;
		}
		if (AvCamera.gI().vX != 0f)
		{
			if (AvCamera.gI().xCam + AvCamera.gI().vX / 15f < (float)(-(float)LoadMap.w * AvMain.hd + 100 * AvMain.hd))
			{
				if (AvCamera.gI().xCam + AvCamera.gI().vX / 15f < (float)(-(float)LoadMap.w * AvMain.hd))
				{
					AvCamera.gI().xCam = (AvCamera.gI().xTo = (float)(-(float)LoadMap.w * AvMain.hd));
					AvCamera.gI().vX /= 6f;
					AvCamera.gI().vX *= -1f;
				}
				else
				{
					AvCamera.gI().vX -= AvCamera.gI().vX / 20f;
				}
			}
			if (AvCamera.gI().xCam + AvCamera.gI().vX / 15f > AvCamera.gI().xLimit + (float)(LoadMap.w * AvMain.hd) - (float)(100 * AvMain.hd))
			{
				if (AvCamera.gI().xCam + AvCamera.gI().vX / 15f >= AvCamera.gI().xLimit + (float)(LoadMap.w * AvMain.hd))
				{
					AvCamera.gI().xCam = (AvCamera.gI().xTo = AvCamera.gI().xLimit + (float)(LoadMap.w * AvMain.hd));
					AvCamera.gI().vX /= 6f;
					AvCamera.gI().vX *= -1f;
				}
				else
				{
					AvCamera.gI().vX -= AvCamera.gI().vX / 20f;
				}
			}
			AvCamera.gI().xCam += AvCamera.gI().vX / 15f;
			AvCamera.gI().xTo = AvCamera.gI().xCam;
			AvCamera.gI().vX -= AvCamera.gI().vX / 20f;
		}
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00072974 File Offset: 0x00070D74
	public override void update()
	{
		this.moveCamera();
		MapScr.gI().update();
		if (this.isSelectedItem == -1)
		{
			for (int i = 0; i < this.listItem.size(); i++)
			{
				MapItem mapItem = (MapItem)this.listItem.elementAt(i);
				if (CRes.abs(mapItem.x - mapItem.xTo) > 1 || CRes.abs(mapItem.y - mapItem.yTo) > 1)
				{
					mapItem.x += (mapItem.xTo - mapItem.x) / 2;
					mapItem.y += (mapItem.yTo - mapItem.y) / 2;
				}
				else
				{
					mapItem.x = mapItem.xTo;
					mapItem.y = mapItem.yTo;
				}
			}
		}
		if (!HouseScr.isChange && !HouseScr.isSelectObj && this.right == null && MapScr.gI().right != null)
		{
			this.right = LoadMap.cmdNext;
		}
		if (HouseScr.isChange)
		{
			this.x0++;
			if (this.x0 > 5)
			{
				this.x0 = 0;
			}
		}
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00072AB4 File Offset: 0x00070EB4
	public override void paint(MyGraphics g)
	{
		this.paintMain(g);
		base.paint(g);
		Canvas.paintPlus(g);
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00072ACC File Offset: 0x00070ECC
	private void paintIndexAble(MyGraphics g, MapItemType map)
	{
		float num = (AvCamera.gI().xCam + (float)Canvas.w) / (float)LoadMap.w + 1f;
		if (num > (float)LoadMap.wMap)
		{
			num = (float)LoadMap.wMap;
		}
		float num2 = (AvCamera.gI().yCam + (float)Canvas.hCan) / (float)LoadMap.w + 1f;
		if (num2 > (float)LoadMap.Hmap)
		{
			num2 = (float)LoadMap.Hmap;
		}
		int num3 = (int)(AvCamera.gI().xCam / (float)(LoadMap.w * AvMain.hd));
		if (num3 < 0)
		{
			num3 = 0;
		}
		int num4 = (int)(AvCamera.gI().yCam / (float)(LoadMap.w * AvMain.hd));
		if (num4 < 0)
		{
			num4 = 0;
		}
		if ((int)map.buy == 2 || (int)map.buy == 4)
		{
			int num5 = num4;
			while ((float)num5 < num2)
			{
				int num6 = num3;
				while ((float)num6 < num)
				{
					int num7 = (int)LoadMap.map[num5 * (int)LoadMap.wMap + num6];
					if (num7 != -1 && (int)LoadMap.type[num5 * (int)LoadMap.wMap + num6] == 80 && LoadMap.map[num5 * (int)LoadMap.wMap + num6] < HouseScr.numW && LoadMap.map[num5 * (int)LoadMap.wMap + num6 - (int)LoadMap.wMap] >= HouseScr.numW)
					{
						this.paintIndex(g, 2 + num6 * 24, 2 + num5 * 24, 0, 20);
					}
					num6++;
				}
				num5++;
			}
		}
		else
		{
			int num8 = num4;
			while ((float)num8 < num2)
			{
				int num9 = num3;
				while ((float)num9 < num)
				{
					int num7 = (int)LoadMap.map[num8 * (int)LoadMap.wMap + num9];
					if (num7 != -1 && (int)LoadMap.type[num8 * (int)LoadMap.wMap + num9] == 80)
					{
						this.paintIndex(g, 2 + num9 * 24, 2 + num8 * 24, 0, 20);
					}
					num9++;
				}
				num8++;
			}
		}
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00072CD8 File Offset: 0x000710D8
	public override void paintMain(MyGraphics g)
	{
		GUIUtility.ScaleAroundPivot(new Vector2(AvMain.zoom, AvMain.zoom), Vector2.zero);
		Canvas.loadMap.paint(g);
		if (HouseScr.isBuyTileMap && this.indexTileMapBuy != -1)
		{
			float num = (AvCamera.gI().xCam + (float)Canvas.w) / (float)(LoadMap.w * AvMain.hd) + 1f;
			if (num > (float)LoadMap.wMap)
			{
				num = (float)LoadMap.wMap;
			}
			int num2 = (int)(AvCamera.gI().yCam / (float)(LoadMap.w * AvMain.hd));
			float num3 = (AvCamera.gI().yCam + (float)Canvas.hCan) / (float)(LoadMap.w * AvMain.hd) + 1f;
			if (num3 > (float)LoadMap.Hmap)
			{
				num3 = (float)LoadMap.Hmap;
			}
			int num4 = (int)(AvCamera.gI().xCam / (float)(LoadMap.w * AvMain.hd));
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			int num5 = num2;
			while ((float)num5 < num3)
			{
				int num6 = num4;
				while ((float)num6 < num)
				{
					int num7 = (int)LoadMap.map[num5 * (int)LoadMap.wMap + num6];
					if (num7 != -1 && ((this.indexTileMapBuy < (int)HouseScr.numW && LoadMap.map[num5 * (int)LoadMap.wMap + num6] < HouseScr.numW) || (this.indexTileMapBuy >= (int)HouseScr.numW && LoadMap.map[num5 * (int)LoadMap.wMap + num6] >= HouseScr.numW)) && (this.listTile[(int)LoadMap.map[num5 * (int)LoadMap.wMap + num6]].priceLuong != -1 || this.listTile[(int)LoadMap.map[num5 * (int)LoadMap.wMap + num6]].priceXu != -1))
					{
						this.paintIndex(g, 2 + num6 * 24, 2 + num5 * 24, 0, 20);
					}
					num6++;
				}
				num5++;
			}
		}
		if (this.indexChangeItem != -1 && this.isMoveItem)
		{
			MapItem mapItem = (MapItem)this.listItem.elementAt(this.indexChangeItem);
			this.paintIndexAble(g, AvatarData.getMapItemTypeByID((int)mapItem.typeID));
		}
		else if (HouseScr.isTranItemBuy && this.indexItemTranBuy != -1)
		{
			MapItemType map = (MapItemType)AvatarData.listMapItemType.elementAt(this.indexItemTranBuy);
			this.paintIndexAble(g, map);
		}
		Canvas.loadMap.paintObject(g);
		if (this.indexChangeItem != -1 && this.isMoveItem)
		{
			MapItem mapItem2 = (MapItem)this.listItem.elementAt(this.indexChangeItem);
			this.imgBuyItem.drawFrame(0, mapItem2.X() * AvMain.hd - this.imgBuyItem.frameWidth, mapItem2.Y() * AvMain.hd - this.imgBuyItem.frameHeight, 0, 3, g);
			this.imgBuyItem.drawFrame(1, mapItem2.X() * AvMain.hd + (int)mapItem2.w + this.imgBuyItem.frameWidth, mapItem2.Y() * AvMain.hd - this.imgBuyItem.frameHeight, 0, 3, g);
			this.imgBuyItem.drawFrame(2, mapItem2.X() * AvMain.hd + (int)(mapItem2.w / 2), mapItem2.Y() * AvMain.hd - this.imgBuyItem.frameHeight * 2, 0, 3, g);
			if (Canvas.gameTick % 20 >= 10)
			{
				g.setColor(458496);
			}
			else
			{
				g.setColor(2662437);
			}
			g.drawRect((float)((mapItem2.x + 12) * AvMain.hd - 2), (float)((mapItem2.y + 12) * AvMain.hd - 2), 5f, 5f);
			if (Canvas.gameTick % 20 >= 10)
			{
				g.setColor(9239945);
			}
			else
			{
				g.setColor(6804068);
			}
			g.drawRect((float)((mapItem2.x + 12) * AvMain.hd - 2 + 1), (float)((mapItem2.y + 12) * AvMain.hd - 2 + 1), 3f, 3f);
		}
		else if (HouseScr.isTranItemBuy && this.indexItemTranBuy != -1)
		{
			MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(this.indexItemTranBuy);
			Image img = AvatarData.getImgIcon(mapItemType.imgID).img;
			g.drawImage(img, (float)((this.xItemTranBuy + (int)mapItemType.dx) * AvMain.hd), (float)((this.yItemTranBuy + (int)mapItemType.dy) * AvMain.hd), 0);
			this.imgBuyItem.drawFrame(0, (this.xItemTranBuy + (int)mapItemType.dx) * AvMain.hd - this.imgBuyItem.frameWidth, (this.yItemTranBuy + (int)mapItemType.dy) * AvMain.hd - this.imgBuyItem.frameHeight, 0, 3, g);
			this.imgBuyItem.drawFrame(1, (this.xItemTranBuy + (int)mapItemType.dx) * AvMain.hd + img.w + this.imgBuyItem.frameWidth, (this.yItemTranBuy + (int)mapItemType.dy) * AvMain.hd - this.imgBuyItem.frameHeight, 0, 3, g);
			if (Canvas.gameTick % 20 >= 10)
			{
				g.setColor(458496);
			}
			else
			{
				g.setColor(2662437);
			}
			g.drawRect((float)((this.xItemTranBuy + 12) * AvMain.hd - 2), (float)((this.yItemTranBuy + 12) * AvMain.hd - 2), 5f, 5f);
			if (Canvas.gameTick % 20 >= 10)
			{
				g.setColor(9239945);
			}
			else
			{
				g.setColor(6804068);
			}
			g.drawRect((float)((this.xItemTranBuy + 12) * AvMain.hd + 1 - 2), (float)((this.yItemTranBuy + 12) * AvMain.hd + 1 - 2), 3f, 3f);
		}
		GUIUtility.ScaleAroundPivot(new Vector2(1f / AvMain.zoom, 1f / AvMain.zoom), Vector2.zero);
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00073319 File Offset: 0x00071719
	private void paintIndex(MyGraphics g, int x, int y, int i, int w)
	{
		g.setColor(this.color[i]);
		g.drawRect((float)(x * AvMain.hd), (float)(y * AvMain.hd), (float)((w - 1) * AvMain.hd), (float)((w - 1) * AvMain.hd));
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00073358 File Offset: 0x00071758
	public void paintSelected(MyGraphics g)
	{
		if (HouseScr.isSelectObj || this.isSelectedItem != -1)
		{
			if (this.selected != -1)
			{
				MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(this.selected);
				if ((int)mapItemType.buy == 2 || (int)mapItemType.buy == 4)
				{
					for (int i = 0; i < LoadMap.map.Length; i++)
					{
						if (i > 0 && LoadMap.map[i] < HouseScr.numW && LoadMap.map[i - (int)LoadMap.wMap] >= HouseScr.numW)
						{
							this.paintIndex(g, 2 + i % (int)LoadMap.wMap * 24, 2 + i / (int)LoadMap.wMap * 24, 0, 20);
						}
					}
				}
				else
				{
					for (int j = 0; j < LoadMap.type.Length; j++)
					{
						if ((int)LoadMap.type[j] == 80 && (j % (int)LoadMap.wMap != this.x || j / (int)LoadMap.wMap != this.y))
						{
							this.paintIndex(g, 2 + j % (int)LoadMap.wMap * 24, 2 + j / (int)LoadMap.wMap * 24, 0, 20);
						}
					}
				}
			}
		}
		else if (this.selected != -1)
		{
			for (int k = 0; k < LoadMap.type.Length; k++)
			{
				if ((this.indexName == 0 && LoadMap.map[k] >= HouseScr.numW && (int)LoadMap.map[k] < this.listTile.Length && (this.listTile[(int)LoadMap.map[k]].priceLuong != -1 || this.listTile[(int)LoadMap.map[k]].priceXu != -1)) || (this.indexName == 1 && LoadMap.map[k] < HouseScr.numW))
				{
					this.paintIndex(g, 2 + k % (int)LoadMap.wMap * 24, 2 + k / (int)LoadMap.wMap * 24, 0, 20);
				}
			}
			LoadMap.imgMap.drawFrame(this.selected, (this.x - this.xDu) * AvMain.hd, (this.y - this.yDu) * AvMain.hd, 0, 0, g);
		}
		if (this.indexName != -1)
		{
			this.paintIndex(g, this.x - this.xDu, this.y - this.yDu, 1, 24);
		}
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x000735C8 File Offset: 0x000719C8
	public void onJoin(sbyte houseType, int idUser, short[] type, sbyte w, MyVector listObjM, MyVector listPlayer)
	{
		this.typeHome = houseType;
		this.idHouse = idUser;
		this.listItem = listObjM;
		LoadMap.wMap = (short)w;
		LoadMap.Hmap = (short)(type.Length / (int)w);
		LoadMap.map = type;
		if ((int)this.typeHome == 4)
		{
			Canvas.loadMap.load(111, false);
		}
		else
		{
			Canvas.loadMap.load(68 + (int)this.typeHome, false);
		}
		LoadMap.rememMap = -1;
		int num = -1;
		int num2 = 0;
		for (int i = 0; i < (int)w; i++)
		{
			for (int j = 0; j < (int)LoadMap.Hmap; j++)
			{
				if (LoadMap.map[j * (int)w + i] < HouseScr.numW)
				{
					LoadMap.type[j * (int)w + i] = 80;
				}
				else
				{
					LoadMap.type[j * (int)w + i] = 88;
				}
			}
			if ((int)LoadMap.map[(int)(LoadMap.Hmap - 1) * (int)w + i] == this.imgTileMap.img.getHeight() / (24 * AvMain.hd) - 1)
			{
				LoadMap.map[(int)(LoadMap.Hmap - 1) * (int)w + i] = LoadMap.map[(int)(LoadMap.Hmap - 2) * (int)w + i];
				LoadMap.type[(int)(LoadMap.Hmap - 1) * (int)w + i] = 21;
				num2++;
				if (num == -1)
				{
					num = i * 24;
				}
			}
		}
		this.posJoin = new AvPosition(num + num2 * 24 / 2, (int)(LoadMap.Hmap * 24 - 30));
		GameMidlet.avatar.x = this.posJoin.x;
		GameMidlet.avatar.y = this.posJoin.y;
		Pet pet = LoadMap.getPet(GameMidlet.avatar.IDDB);
		if (pet != null)
		{
			pet.setPos(GameMidlet.avatar.x, GameMidlet.avatar.y);
			pet.reset();
		}
		AvCamera.gI().init(70 + (int)this.typeHome);
		LoadMap.imgMap = new FrameImage(this.imgTileMap.img, 24 * AvMain.hd, 24 * AvMain.hd);
		for (int k = 0; k < listPlayer.size(); k++)
		{
			Avatar avatar = (Avatar)listPlayer.elementAt(k);
			avatar.xCur = avatar.x;
			avatar.yCur = avatar.y;
			if (avatar.IDDB != GameMidlet.avatar.IDDB)
			{
				LoadMap.addPlayer(avatar);
			}
		}
		int num3 = 0;
		int num4 = 0;
		for (int l = 0; l < this.listItem.size(); l++)
		{
			MapItem mapItem = (MapItem)this.listItem.elementAt(l);
			if (mapItem.x == 0 && mapItem.y == 0)
			{
				int num5 = 0;
				for (int m = 0; m < LoadMap.map.Length; m++)
				{
					if ((int)LoadMap.type[m] == 80)
					{
						mapItem.x = (mapItem.yTo = m % (int)LoadMap.wMap * 24);
						mapItem.y = (mapItem.yTo = m / (int)LoadMap.wMap * 24);
						num3 = mapItem.x;
						num4 = mapItem.y;
						num5 = 1;
						this.setType(mapItem);
						AvatarService.gI().doSortItem((int)mapItem.typeID, 0, 0, mapItem.x / 24, mapItem.y / 24, (int)mapItem.dir);
						break;
					}
				}
				if (num5 == 0)
				{
					mapItem.x = num3;
					mapItem.y = num4;
					AvatarService.gI().doSortItem((int)mapItem.typeID, 0, 0, mapItem.x / 24, mapItem.y / 24, (int)mapItem.dir);
				}
			}
			if (this.isSetTuong(mapItem))
			{
				mapItem.y++;
			}
		}
		MapScr.gI().move();
		this.addItem(this.listItem);
		this.switchToMe();
		Canvas.endDlg();
		if ((float)Canvas.w >= (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom)
		{
			AvCamera.gI().xTo = (AvCamera.gI().xCam = -((float)Canvas.w - (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom) / 2f);
		}
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00073A3C File Offset: 0x00071E3C
	private bool isSetTuong(MapItem m)
	{
		if ((int)AvatarData.getMapItemTypeByID((int)m.typeID).buy != 2 && (int)AvatarData.getMapItemTypeByID((int)m.typeID).buy != 4)
		{
			int num = (m.y / 24 - 1) * (int)LoadMap.wMap + m.x / 24;
			if (LoadMap.map[num] >= HouseScr.numW && LoadMap.map[m.y / 24 * (int)LoadMap.wMap + m.x / 24] < HouseScr.numW)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00073AD0 File Offset: 0x00071ED0
	private bool getTileMap(short verTile)
	{
		if (this.imgTileMap != null)
		{
			return true;
		}
		this.loadTileMap();
		if (this.imgTileMap == null || verTile != this.imgTileMap.ver)
		{
			if (this.imgTileMap == null)
			{
				this.imgTileMap = new BigImgInfo();
				this.imgTileMap.ver = verTile;
			}
			AvatarService.gI().doGetTileMap();
			return false;
		}
		return true;
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00073B3C File Offset: 0x00071F3C
	private BigImgInfo loadTileMap()
	{
		DataInputStream dataInputStream = AvatarData.initLoad("avatarTileMap");
		if (dataInputStream == null)
		{
			return null;
		}
		this.imgTileMap = new BigImgInfo();
		try
		{
			this.imgTileMap.ver = dataInputStream.readShort();
			HouseScr.numW = dataInputStream.readShort();
			sbyte[] data = new sbyte[dataInputStream.available()];
			dataInputStream.read(ref data);
			this.imgTileMap.img = CRes.createImgByByteArray(ArrayCast.cast(data));
			dataInputStream.close();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		return this.imgTileMap;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00073BDC File Offset: 0x00071FDC
	public void saveTileMap(sbyte[] array, int wNum)
	{
		HouseScr.numW = (short)wNum;
		this.imgTileMap.img = CRes.createImgByByteArray(ArrayCast.cast(array));
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeShort(this.imgTileMap.ver);
			dataOutputStream.writeShort((short)wNum);
			dataOutputStream.write(array);
			RMS.saveRMS("avatarTileMap", dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		if (MapScr.idHouse != -1)
		{
			AvatarService.gI().doJoinHouse(MapScr.idHouse);
			MapScr.idHouse = -1;
		}
		else
		{
			Canvas.endDlg();
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00073C90 File Offset: 0x00072090
	public override void commandTab(int index)
	{
		switch (index)
		{
		case 0:
			this.doBrick();
			break;
		case 1:
		{
			this.indexName = -1;
			int num = 0;
			for (int i = 0; i < this.temp.Length; i++)
			{
				if (this.temp[i] != LoadMap.map[i])
				{
					num = 1;
					break;
				}
			}
			if (num == 1)
			{
				AvatarService.gI().doCreateHome(LoadMap.map, 0);
				Canvas.startWaitDlg();
			}
			this.addPlayer();
			HouseScr.isBuyTileMap = false;
			this.indexTileMapBuy = -1;
			HouseScr.isChange = false;
			this.center = null;
			this.left = null;
			this.right = null;
			break;
		}
		default:
			switch (index)
			{
			case 50:
				AvatarService.gI().doCreateHome(LoadMap.map, 1);
				Canvas.startWaitDlg();
				break;
			case 51:
				LoadMap.map = this.temp;
				this.temp = null;
				ParkMsgHandler.onHandler();
				break;
			default:
				if (index != 100)
				{
					if (index == 101)
					{
						GlobalService.gI().doEnterPass(Canvas.inputDlg.getText(), 0);
					}
				}
				else
				{
					AvatarService.gI().doSetPassMyHouse(Canvas.inputDlg.getText(), 0, 0);
					Canvas.endDlg();
				}
				break;
			case 53:
				GlobalService.gI().doUpdateChest(0);
				Canvas.startWaitDlg();
				break;
			}
			break;
		case 3:
			this.doDiChuyen();
			break;
		case 4:
			this.reset();
			HouseScr.isDuyChuyen = false;
			AvCamera.isFollow = false;
			break;
		case 5:
			this.doSelectMap();
			break;
		case 8:
			InputFace.gI().close();
			break;
		case 9:
			this.reset();
			this.indexCloseItem = 0;
			if (this.indexChangeItem != -1)
			{
				MapItem mapItem = (MapItem)this.listItem.elementAt(this.indexChangeItem);
				mapItem.xTo = this.xItemOld;
				mapItem.yTo = this.yItemOld;
			}
			this.indexChangeItem = -1;
			this.isMoveItem = false;
			HouseScr.isDuyChuyen = false;
			break;
		}
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00073EB4 File Offset: 0x000722B4
	public void onCreateHome(short type1, string str)
	{
		Canvas.endDlg();
		if (type1 == 0)
		{
			Canvas.msgdlg.setInfoLR(str, new Command(T.yes, 50), new Command(T.no, 51));
		}
		else
		{
			Canvas.startOKDlg(str);
			if (type1 == 2)
			{
				LoadMap.map = this.temp;
			}
			this.temp = null;
			ParkMsgHandler.onHandler();
			GameMidlet.avatar.x = this.posJoin.x;
			GameMidlet.avatar.y = this.posJoin.y;
			AvCamera.gI().init(70 + (int)this.typeHome);
		}
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00073F56 File Offset: 0x00072356
	public void onGetTileInfo(Tile[] listTile)
	{
		this.listTile = listTile;
		this.doSelectMap();
		Canvas.endDlg();
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00073F6C File Offset: 0x0007236C
	private void removeTrans(MapItem map)
	{
		int num = 0;
		for (int i = 0; i < this.listItem.size(); i++)
		{
			MapItem mapItem = (MapItem)this.listItem.elementAt(i);
			if (mapItem.x / 24 == map.x / 24 && mapItem.y / 24 == map.y / 24)
			{
				num++;
			}
		}
		if (num == 1)
		{
			MapItemType mapItemTypeByID = AvatarData.getMapItemTypeByID((int)map.typeID);
			for (int j = 0; j < mapItemTypeByID.listNotTrans.size(); j++)
			{
				AvPosition avPosition = (AvPosition)mapItemTypeByID.listNotTrans.elementAt(j);
				LoadMap.type[(map.y / 24 + avPosition.y) * (int)LoadMap.wMap + (map.x / 24 + avPosition.x)] = 80;
			}
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00074054 File Offset: 0x00072454
	private MapItem getMapItem(MapItem p)
	{
		for (int i = 0; i < this.listItem.size(); i++)
		{
			MapItem mapItem = (MapItem)this.listItem.elementAt(i);
			if (mapItem.x / 24 == p.x && mapItem.y / 24 == p.y && mapItem.typeID == p.typeID)
			{
				return mapItem;
			}
		}
		return null;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x000740CC File Offset: 0x000724CC
	public void onRemoveItem(MapItem p)
	{
		MapItem mapItem = this.getMapItem(p);
		LoadMap.treeLists.removeElement(mapItem);
		this.listItem.removeElement(mapItem);
		this.removeTrans(mapItem);
		ParkMsgHandler.onHandler();
		Canvas.endDlg();
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0007410C File Offset: 0x0007250C
	protected void addItem(MyVector listItem2)
	{
		for (int i = 0; i < listItem2.size(); i++)
		{
			MapItem mapItem = (MapItem)listItem2.elementAt(i);
			LoadMap.treeLists.addElement(mapItem);
			this.setType(mapItem);
		}
		LoadMap.orderVector(LoadMap.treeLists);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0007415C File Offset: 0x0007255C
	public void setType(MapItem pos)
	{
		MapItemType mapItemTypeByID = AvatarData.getMapItemTypeByID((int)pos.typeID);
		sbyte b = 88;
		if (mapItemTypeByID.idType == this.IDHo)
		{
			b = 112;
		}
		else if (mapItemTypeByID.idType == this.IDHoa)
		{
			b = 111;
		}
		else if (mapItemTypeByID.iconID == 1)
		{
			b = 79;
		}
		else if (mapItemTypeByID.iconID == 2)
		{
			b = 67;
		}
		for (int i = 0; i < mapItemTypeByID.listNotTrans.size(); i++)
		{
			AvPosition avPosition = (AvPosition)mapItemTypeByID.listNotTrans.elementAt(i);
			LoadMap.type[(pos.yTo / 24 + avPosition.y) * (int)LoadMap.wMap + (pos.xTo / 24 + avPosition.x)] = b;
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0007422C File Offset: 0x0007262C
	public void onGetTypeHouse(int type, int houseType, short verTile, MyVector list)
	{
		if (type == 0)
		{
			GameMidlet.avatar.typeHome = (sbyte)houseType;
			MapScr.gI().switchToMe();
			if (this.getTileMap(verTile))
			{
				if (MapScr.idHouse != -1)
				{
					AvatarService.gI().doJoinHouse(MapScr.idHouse);
					MapScr.idHouse = -1;
				}
				else
				{
					Canvas.load = 1;
					Canvas.endDlg();
				}
			}
			else
			{
				Canvas.load = 1;
			}
		}
		else
		{
			for (int i = 0; i < list.size(); i++)
			{
				Avatar avatar = (Avatar)list.elementAt(i);
				Avatar avatar2 = ListScr.getAvatar(avatar.IDDB);
				if (avatar != null && avatar2 != null)
				{
					avatar2.typeHome = avatar.typeHome;
				}
			}
			Canvas.endDlg();
			this.onRoadFriend();
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000742F6 File Offset: 0x000726F6
	public override void keyPress(int keyCode)
	{
		ChatTextField.gI().startChat(keyCode, this);
		base.keyPress(keyCode);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x0007430B File Offset: 0x0007270B
	public void onChatFromMe(string text)
	{
		MapScr.gI().onChatFromMe(text);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00074318 File Offset: 0x00072718
	public void onRoadFriend()
	{
		if (ListScr.friendL == null)
		{
			Canvas.startWaitDlg();
			CasinoService.gI().requestFriendList();
			ListScr.typeListFriend = 2;
		}
		else
		{
			if (ListScr.isGetTypeHouse)
			{
				ListScr.isGetTypeHouse = false;
				Canvas.startWaitDlg();
				AvatarService.gI().getTypeHouse(1);
				return;
			}
			MyVector myVector = new MyVector();
			for (int i = 0; i < ListScr.friendL.size(); i++)
			{
				Avatar avatar = (Avatar)ListScr.friendL.elementAt(i);
				if ((int)avatar.typeHome == (int)this.typeHome)
				{
					myVector.addElement(avatar);
				}
			}
			if (myVector.size() == 0)
			{
				if (Canvas.currentMyScreen == ListScr.gI())
				{
					ListScr.gI().backMyScreen.switchToMe();
				}
				Canvas.startOKDlg(T.noFriend);
				return;
			}
			ListScr.gI().switchToMe();
			ListScr.gI().isJoinH = true;
			ListScr.tempList = myVector;
			ListScr.gI().init();
			ListScr.gI().cmdSelected = new Command(T.selectt, 2, this);
		}
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00074428 File Offset: 0x00072828
	public void doJoinFriendHome(int houseType)
	{
		this.typeHome = (sbyte)houseType;
		if ((int)GameMidlet.avatar.typeHome == houseType || (int)GameMidlet.avatar.typeHome == -1)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(T.goHome, 0, this));
			myVector.addElement(new Command(T.joinFrHome, 1, this));
			MenuCenter.gI().startAt(myVector);
		}
		else
		{
			this.onRoadFriend();
		}
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x000744A0 File Offset: 0x000728A0
	public override void commandActionPointer(int indexMenu, int subIndex)
	{
		switch (indexMenu)
		{
		case 0:
			AvatarService.gI().doJoinHouse(GameMidlet.avatar.IDDB);
			Canvas.startWaitDlg();
			break;
		case 1:
			this.onRoadFriend();
			break;
		case 2:
		{
			Avatar avatar = (Avatar)ListScr.tempList.elementAt(ListScr.gI().selected);
			AvatarService.gI().doJoinHouse(avatar.IDDB);
			Canvas.startWaitDlg();
			break;
		}
		case 4:
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(T.upgradeChest, 14, this));
			myVector.addElement(new Command(T.setPass, 15, this));
			MenuCenter.gI().startAt(myVector);
			break;
		}
		case 6:
			GlobalService.gI().doRequestChest();
			break;
		case 7:
			this.doBuyItem();
			break;
		case 8:
			this.doKick();
			break;
		case 9:
			ipKeyboard.openKeyBoard(T.pass + ":", ipKeyboard.PASS, string.Empty, new HouseScr.IActionPass(), false);
			break;
		case 10:
			this.doSelectObject();
			break;
		case 11:
			if (this.listTile == null)
			{
				this.doCreateMap();
			}
			else
			{
				this.doSelectMap();
			}
			break;
		case 12:
			HouseScr.isDuyChuyen = true;
			this.doOption();
			break;
		case 13:
			MapScr.gI().doExit();
			break;
		case 14:
			PopupShop.gI().close();
			Canvas.startOKDlg(T.doYouWantUpgradeCoffer, new HouseScr.updateCoffer());
			break;
		case 15:
		{
			TField[] array = new TField[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = new TField(string.Empty, this, new HouseScr.IActionFinish(array));
				array[i].setIputType(ipKeyboard.PASS);
			}
			array[0].setFocus(true);
			Command cmd = new Command(T.finish, new HouseScr.IActionFinish(array));
			PopupShop.gI().close();
			InputFace.gI().setInfo(array, T.changePass, T.nameChangePass, cmd, Canvas.hCan);
			InputFace.gI().show();
			InputFace.gI().left = new Command(T.close, 8);
			break;
		}
		case 16:
			if (this.indexChangeItem != -1)
			{
				MapItem mapItem = (MapItem)this.listItem.elementAt(this.indexChangeItem);
				if ((int)mapItem.dir == 0)
				{
					mapItem.dir = 2;
				}
				else
				{
					mapItem.dir = 0;
				}
				AvatarService.gI().doSortItem((int)mapItem.typeID, this.x / 24, this.y / 24, this.x / 24, this.y / 24, (int)mapItem.dir);
			}
			break;
		case 17:
		{
			MapItem mapItem2 = (MapItem)this.listItem.elementAt(this.indexChangeItem);
			if (mapItem2.typeID != this.IDHoa)
			{
				Canvas.startOKDlg(T.doWantSellItem, new HouseScr.IActionSellItem(mapItem2));
				this.indexCloseItem = 0;
				this.indexChangeItem = -1;
				this.isMoveItem = false;
			}
			break;
		}
		}
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000747D4 File Offset: 0x00072BD4
	public override void commandAction(int indexMenu)
	{
		int num = -1;
		for (int i = 0; i < this.listItem.size(); i++)
		{
			MapItem mapItem = (MapItem)this.listItem.elementAt(i);
			if (mapItem.x / 24 == this.x / 24 && mapItem.y / 24 == this.y / 24)
			{
				num = i;
				break;
			}
		}
		int num2 = num;
		MapItem mapItem2 = null;
		if (num != -1)
		{
			mapItem2 = (MapItem)this.listItem.elementAt(num);
		}
		MapItem mapItem3 = mapItem2;
		switch (indexMenu)
		{
		case 11:
			if (num == -1)
			{
				Canvas.startOKDlg(T.noItem);
				return;
			}
			this.isSelectedItem = num2;
			for (int j = 0; j < AvatarData.listMapItemType.size(); j++)
			{
				MapItemType mapItemType = (MapItemType)AvatarData.listMapItemType.elementAt(j);
				if (mapItemType.idType == mapItem3.typeID)
				{
					this.selected = j;
					break;
				}
			}
			this.left = null;
			this.right = null;
			this.center = null;
			this.removeTrans(mapItem3);
			this.posSort = new AvPosition(this.x / 24, this.y / 24, (int)mapItem3.typeID);
			break;
		case 12:
			if (num == -1)
			{
				Canvas.startOKDlg(T.noItem);
				return;
			}
			if ((int)mapItem3.dir == 0)
			{
				mapItem3.dir = 2;
			}
			else
			{
				mapItem3.dir = 0;
			}
			AvatarService.gI().doSortItem((int)mapItem3.typeID, this.x / 24, this.y / 24, this.x / 24, this.y / 24, (int)mapItem3.dir);
			break;
		case 13:
			if (num == -1)
			{
				Canvas.startOKDlg(T.noItem);
				return;
			}
			Canvas.startOKDlg(T.doWantSellItem, new HouseScr.IActionSellItem(mapItem3));
			break;
		default:
			if (indexMenu == 5)
			{
				MapScr.gI().doExit();
			}
			break;
		case 16:
			break;
		}
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x000749F9 File Offset: 0x00072DF9
	public void doOut()
	{
		this.listP_Chest = null;
		this.listP_Con = null;
		ParkService.gI().doJoinPark(21, 0);
		LoadMap.rememMap = -1;
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00074A1C File Offset: 0x00072E1C
	public void onCustomChest(MyVector listPartCon, MyVector listPartChest, int moneyOnChest, sbyte levelChest)
	{
		this.listP_Con = listPartCon;
		this.listP_Chest = listPartChest;
		this.moneyOnChest = moneyOnChest;
		this.levelChest = levelChest;
		MyVector listCmdDoUsing = MapScr.gI().getListCmdDoUsing(listPartCon, GameMidlet.avatar.IDDB, 3, T.trans, true);
		MyVector listCmdDoUsing2 = MapScr.gI().getListCmdDoUsing(listPartChest, GameMidlet.avatar.IDDB, 2, T.trans, true);
		if (Canvas.currentMyScreen == MainMenu.me)
		{
			return;
		}
		PopupShop.isHorizontal = true;
		PopupShop.gI().addElement(new string[]
		{
			T.basket,
			T.container
		}, new MyVector[]
		{
			listCmdDoUsing,
			listCmdDoUsing2
		}, null, null);
		Command cmd = MapScr.gI().cmdDellPart(listPartCon, 1, 1, false);
		Command cmd2 = new Command(T.menu, 4, this);
		PopupShop.gI().setCmdLeft(cmd, 0);
		PopupShop.gI().setCmdLeft(cmd2, 1);
		if (Canvas.currentMyScreen != PopupShop.gI())
		{
			PopupShop.gI().switchToMe();
			PopupShop.isHorizontal = true;
		}
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00074B19 File Offset: 0x00072F19
	public void onEnterPass()
	{
		Canvas.inputDlg.setInfoIkb(T.pass, new HouseScr.IActionEnterPass(), 0, this);
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00074B34 File Offset: 0x00072F34
	public void onTransChestPart(bool readBoolean, string readUTF)
	{
		if (!readBoolean)
		{
			Canvas.startOKDlg(readUTF);
			return;
		}
		int focusTap = PopupShop.focusTap;
		int focus = PopupShop.focus;
		if (focusTap == 0)
		{
			SeriPart o = (SeriPart)this.listP_Con.elementAt(focus);
			this.listP_Chest.addElement(o);
			this.listP_Con.removeElement(o);
		}
		else
		{
			SeriPart o2 = (SeriPart)this.listP_Chest.elementAt(focus);
			this.listP_Con.addElement(o2);
			this.listP_Chest.removeElement(o2);
		}
		this.restartPopup();
		Canvas.endDlg();
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00074BC4 File Offset: 0x00072FC4
	public void restartPopup()
	{
		int focusTap = PopupShop.focusTap;
		int num = PopupShop.focus;
		PopupShop.gI().close();
		this.onCustomChest(this.listP_Con, this.listP_Chest, this.moneyOnChest, this.levelChest);
		PopupShop.focusTap = focusTap;
		PopupShop.gI().setCmyLim();
		if (num >= PopupShop.gI().listCell[focusTap].size())
		{
			num = 0;
		}
		PopupShop.focus = num;
		PopupShop.gI().setCaption();
		Canvas.cameraList.setSelect(PopupShop.focus);
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00074C50 File Offset: 0x00073050
	public void onOpenShop(sbyte typeShop, string nameShop, string[] nameItem, short[] idPart, short[] idItem, string[] timeLimit, string[] des, int[] price, short[] idPartGirl)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < nameItem.Length; i++)
		{
			myVector.addElement(new HouseScr.CommandShop1(T.selectt, new HouseScr.IActionShop1(typeShop, idItem[i], des[i]), i, nameItem[i], idPart[i], idItem[i], timeLimit[i], (price != null) ? price[i] : -1, des[i], idPartGirl[i]));
		}
		if (myVector.size() > 0)
		{
			PopupShop.gI().switchToMe();
			PopupShop.isHorizontal = true;
			PopupShop.gI().addElement(new string[]
			{
				nameShop
			}, new MyVector[]
			{
				myVector
			}, null, null);
		}
	}

	// Token: 0x04000E7E RID: 3710
	public static HouseScr me;

	// Token: 0x04000E7F RID: 3711
	private int x;

	// Token: 0x04000E80 RID: 3712
	private int y;

	// Token: 0x04000E81 RID: 3713
	private new int selected = -1;

	// Token: 0x04000E82 RID: 3714
	private Command cmdBrick;

	// Token: 0x04000E83 RID: 3715
	private Command cmdFinish;

	// Token: 0x04000E84 RID: 3716
	private Command cmdMenu;

	// Token: 0x04000E85 RID: 3717
	private Command cmdRotate;

	// Token: 0x04000E86 RID: 3718
	private MyScreen lastScr;

	// Token: 0x04000E87 RID: 3719
	private static short numW;

	// Token: 0x04000E88 RID: 3720
	public static bool isSelectObj;

	// Token: 0x04000E89 RID: 3721
	private MyVector listItem;

	// Token: 0x04000E8A RID: 3722
	private sbyte typeHome = -1;

	// Token: 0x04000E8B RID: 3723
	public int indexName = -1;

	// Token: 0x04000E8C RID: 3724
	public int isSelectedItem = -1;

	// Token: 0x04000E8D RID: 3725
	public int idHouse;

	// Token: 0x04000E8E RID: 3726
	public static bool isChange;

	// Token: 0x04000E8F RID: 3727
	public static bool isDuyChuyen;

	// Token: 0x04000E90 RID: 3728
	public static bool isTranItemBuy;

	// Token: 0x04000E91 RID: 3729
	public static bool isBuyTileMap;

	// Token: 0x04000E92 RID: 3730
	private Tile[] listTile;

	// Token: 0x04000E93 RID: 3731
	private AvPosition posSort;

	// Token: 0x04000E94 RID: 3732
	private AvPosition posJoin;

	// Token: 0x04000E95 RID: 3733
	public BigImgInfo imgTileMap;

	// Token: 0x04000E96 RID: 3734
	private FrameImage imgBuyItem;

	// Token: 0x04000E97 RID: 3735
	private static int xTemp = -1;

	// Token: 0x04000E98 RID: 3736
	private static int yTemp = -1;

	// Token: 0x04000E99 RID: 3737
	private new int[] color = new int[]
	{
		1688583,
		14744065
	};

	// Token: 0x04000E9A RID: 3738
	public short IDHoa = 69;

	// Token: 0x04000E9B RID: 3739
	public short IDHo = 68;

	// Token: 0x04000E9C RID: 3740
	private short[] temp;

	// Token: 0x04000E9D RID: 3741
	private int indexTileMapBuy = -1;

	// Token: 0x04000E9E RID: 3742
	private string nameSelectedItem = string.Empty;

	// Token: 0x04000E9F RID: 3743
	private int indexItemTranBuy = -1;

	// Token: 0x04000EA0 RID: 3744
	private int xItemTranBuy;

	// Token: 0x04000EA1 RID: 3745
	private int yItemTranBuy;

	// Token: 0x04000EA2 RID: 3746
	private bool isTranItem;

	// Token: 0x04000EA3 RID: 3747
	private bool isMoveItem;

	// Token: 0x04000EA4 RID: 3748
	private int indexChangeItem = -1;

	// Token: 0x04000EA5 RID: 3749
	private int xItemOld;

	// Token: 0x04000EA6 RID: 3750
	private int yItemOld;

	// Token: 0x04000EA7 RID: 3751
	private int xTempItem = -1;

	// Token: 0x04000EA8 RID: 3752
	private int yTempItem;

	// Token: 0x04000EA9 RID: 3753
	private int indexFireItem;

	// Token: 0x04000EAA RID: 3754
	private int indexCloseItem;

	// Token: 0x04000EAB RID: 3755
	private int indexRotateItem;

	// Token: 0x04000EAC RID: 3756
	private bool isTrans;

	// Token: 0x04000EAD RID: 3757
	private bool isMove;

	// Token: 0x04000EAE RID: 3758
	private int xDu;

	// Token: 0x04000EAF RID: 3759
	private int yDu;

	// Token: 0x04000EB0 RID: 3760
	private int indexChans;

	// Token: 0x04000EB1 RID: 3761
	private int x0;

	// Token: 0x04000EB2 RID: 3762
	public int xHo;

	// Token: 0x04000EB3 RID: 3763
	public int yHo;

	// Token: 0x04000EB4 RID: 3764
	private MyVector listP_Chest;

	// Token: 0x04000EB5 RID: 3765
	private MyVector listP_Con;

	// Token: 0x04000EB6 RID: 3766
	private int moneyOnChest;

	// Token: 0x04000EB7 RID: 3767
	private sbyte levelChest;

	// Token: 0x0200019E RID: 414
	private class IActionKick : IAction
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x00074D08 File Offset: 0x00073108
		public IActionKick(Base b)
		{
			this.ba = b;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00074D17 File Offset: 0x00073117
		public void perform()
		{
			AvatarService.gI().doKickOutHome(this.ba.IDDB);
		}

		// Token: 0x04000EB8 RID: 3768
		private Base ba;
	}

	// Token: 0x0200019F RID: 415
	private class CommandMap : Command
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x00074D2E File Offset: 0x0007312E
		public CommandMap(string name, HouseScr.IActionMap action, int i) : base(name, action)
		{
			this.ii = i;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00074D3F File Offset: 0x0007313F
		public override void paint(MyGraphics g, int x, int y)
		{
			LoadMap.imgMap.drawFrame(this.ii, x + 1, y + 1, 0, 3, g);
		}

		// Token: 0x04000EB9 RID: 3769
		private int ii;
	}

	// Token: 0x020001A0 RID: 416
	private class IActionMap : IAction
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00074D5A File Offset: 0x0007315A
		public IActionMap(HouseScr me, int i)
		{
			this.me = me;
			this.ii = i;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00074D70 File Offset: 0x00073170
		public void perform()
		{
			HouseScr.isBuyTileMap = false;
			this.me.indexTileMapBuy = -1;
			this.me.setStatusBuyItem();
			if (this.me.temp == null)
			{
				this.me.temp = new short[LoadMap.map.Length];
				for (int i = 0; i < LoadMap.map.Length; i++)
				{
					this.me.temp[i] = LoadMap.map[i];
				}
			}
			this.me.right = this.me.cmdFinish;
			this.me.left = new Command(T.selectt, 5);
			this.me.center = null;
			if (this.me.selected < (int)HouseScr.numW)
			{
				this.me.indexName = 1;
			}
			else
			{
				this.me.indexName = 0;
			}
			this.me.indexTileMapBuy = this.ii;
			HouseScr.isBuyTileMap = true;
		}

		// Token: 0x04000EBA RID: 3770
		public readonly int ii;

		// Token: 0x04000EBB RID: 3771
		private HouseScr me;
	}

	// Token: 0x020001A1 RID: 417
	private class IActionObject : IAction
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x00074E6E File Offset: 0x0007326E
		public IActionObject(HouseScr me, string str)
		{
			this.text = str;
			this.me = me;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00074E84 File Offset: 0x00073284
		public void perform()
		{
			this.me.doSelectedItem(this.text);
		}

		// Token: 0x04000EBC RID: 3772
		private readonly string text;

		// Token: 0x04000EBD RID: 3773
		private readonly HouseScr me;
	}

	// Token: 0x020001A2 RID: 418
	private class IActionNo : IAction
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x00074E97 File Offset: 0x00073297
		public IActionNo(HouseScr me)
		{
			this.house = me;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00074EA6 File Offset: 0x000732A6
		public void perform()
		{
			this.house.doSelectObject();
		}

		// Token: 0x04000EBE RID: 3774
		private readonly HouseScr house;
	}

	// Token: 0x020001A3 RID: 419
	private class CommandItem : Command
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00074EB3 File Offset: 0x000732B3
		public CommandItem(string s, HouseScr.IActionItem item, MapItemType map, string money, string na, int hh) : base(s, item)
		{
			this.map = map;
			this.money = money;
			this.na = na;
			this.hh = hh;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00074EDC File Offset: 0x000732DC
		public override void paint(MyGraphics g, int x, int y)
		{
			AvatarData.paintImg(g, (int)this.map.imgID, x, y + this.hh / 2 - (int)AvMain.hBlack - (int)AvMain.hNormal - 5, 33);
			Canvas.blackF.drawString(g, this.money, x, y + this.hh / 2 - (int)AvMain.hBlack, 2);
			Canvas.normalFont.drawString(g, this.na, x, y + this.hh / 2 - (int)AvMain.hBlack - (int)AvMain.hNormal, 2);
		}

		// Token: 0x04000EBF RID: 3775
		public readonly MapItemType map;

		// Token: 0x04000EC0 RID: 3776
		public readonly string na;

		// Token: 0x04000EC1 RID: 3777
		public readonly string money;

		// Token: 0x04000EC2 RID: 3778
		private int hh;
	}

	// Token: 0x020001A4 RID: 420
	private class IActionItem : IAction
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00074F66 File Offset: 0x00073366
		public IActionItem(HouseScr me, int i, string na)
		{
			this.na = na;
			this.me = me;
			this.ii = i;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00074F84 File Offset: 0x00073384
		public void perform()
		{
			this.me.setStatusBuyItem();
			this.me.xItemTranBuy = GameMidlet.avatar.x;
			this.me.yItemTranBuy = GameMidlet.avatar.y;
			HouseScr.isTranItemBuy = true;
			this.me.indexItemTranBuy = this.ii;
		}

		// Token: 0x04000EC3 RID: 3779
		public readonly int ii;

		// Token: 0x04000EC4 RID: 3780
		private string na;

		// Token: 0x04000EC5 RID: 3781
		private readonly HouseScr me;
	}

	// Token: 0x020001A5 RID: 421
	private class IActionCenterSet : IAction
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x00074FDD File Offset: 0x000733DD
		public IActionCenterSet(HouseScr me, int i, string na)
		{
			this.ii = i;
			this.name = na;
			this.me = me;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00074FFA File Offset: 0x000733FA
		public void perform()
		{
			HouseScr.xTemp = this.me.x;
			HouseScr.yTemp = this.me.y;
			this.me.doBuyItemHouse(this.ii, this.name);
		}

		// Token: 0x04000EC6 RID: 3782
		private readonly int ii;

		// Token: 0x04000EC7 RID: 3783
		private readonly string name;

		// Token: 0x04000EC8 RID: 3784
		private readonly HouseScr me;
	}

	// Token: 0x020001A6 RID: 422
	private class IActionBuyItemClose : IAction
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00075033 File Offset: 0x00073433
		public IActionBuyItemClose(HouseScr me)
		{
			this.me = me;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00075042 File Offset: 0x00073442
		public void perform()
		{
			this.me.reset();
			Canvas.endDlg();
			this.me.indexItemTranBuy = -1;
			HouseScr.isTranItemBuy = false;
		}

		// Token: 0x04000EC9 RID: 3785
		private HouseScr me;
	}

	// Token: 0x020001A7 RID: 423
	private class IActionBuyItem : IAction
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00075066 File Offset: 0x00073466
		public IActionBuyItem(HouseScr me, MapItemType map, int type, string na)
		{
			this.name = na;
			this.me = me;
			this.map = map;
			this.type = type;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0007508C File Offset: 0x0007348C
		public void perform()
		{
			MapItem mapItem = new MapItem(this.type, this.me.xItemTranBuy, this.me.yItemTranBuy, 1, this.map.idType);
			AvatarService.gI().doBuyItemHouse(mapItem);
			this.me.reset();
			Canvas.endDlg();
			this.me.indexItemTranBuy = -1;
			HouseScr.isTranItemBuy = false;
			this.me.doSelectedItem(this.name);
		}

		// Token: 0x04000ECA RID: 3786
		private readonly MapItemType map;

		// Token: 0x04000ECB RID: 3787
		private readonly int type;

		// Token: 0x04000ECC RID: 3788
		private HouseScr me;

		// Token: 0x04000ECD RID: 3789
		private string name;
	}

	// Token: 0x020001A8 RID: 424
	private class updateCoffer : IAction
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0007510D File Offset: 0x0007350D
		public void perform()
		{
			GlobalService.gI().doUpdateChest(0);
			Canvas.startWaitDlg();
		}
	}

	// Token: 0x020001A9 RID: 425
	private class IActionPass : IKbAction
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x00075127 File Offset: 0x00073527
		public void perform(string text)
		{
			AvatarService.gI().doSetPassMyHouse(text, 0, 0);
			Canvas.endDlg();
		}
	}

	// Token: 0x020001AA RID: 426
	private class IActionFinish : IAction
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x0007513B File Offset: 0x0007353B
		public IActionFinish(TField[] t)
		{
			this.tf_P = t;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0007514C File Offset: 0x0007354C
		public void perform()
		{
			if (MapScr.setEnterPass(this.tf_P))
			{
				GlobalService.gI().doChangeChestPass(this.tf_P[0].getText(), this.tf_P[1].getText());
				Canvas.startWaitDlg();
				InputFace.gI().close();
			}
		}

		// Token: 0x04000ECE RID: 3790
		private readonly TField[] tf_P;
	}

	// Token: 0x020001AB RID: 427
	private class IActionCenterOk : IAction
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x0007519C File Offset: 0x0007359C
		public IActionCenterOk(HouseScr me, MapItem map)
		{
			this.me = me;
			this.map2 = map;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000751B4 File Offset: 0x000735B4
		public void perform()
		{
			if (this.me.isDisable(AvatarData.getMapItemTypeByID((int)this.map2.typeID), this.me.x, this.me.y))
			{
				return;
			}
			AvatarService.gI().doSortItem(this.me.posSort.anchor, this.me.posSort.x, this.me.posSort.y, this.me.x / 24, this.me.y / 24, (int)this.map2.dir);
			HouseScr.isSelectObj = false;
			this.me.isSelectedItem = -1;
			this.me.selected = -1;
			HouseScr.isChange = false;
			this.me.setType(this.map2);
			if (this.me.isSetTuong(this.map2))
			{
				this.map2.y++;
			}
			LoadMap.orderVector(LoadMap.treeLists);
			this.me.doOption();
		}

		// Token: 0x04000ECF RID: 3791
		private readonly MapItem map2;

		// Token: 0x04000ED0 RID: 3792
		private HouseScr me;
	}

	// Token: 0x020001AC RID: 428
	private class IActionSellItem : IAction
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x000752CE File Offset: 0x000736CE
		public IActionSellItem(MapItem map)
		{
			this.map2 = map;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000752DD File Offset: 0x000736DD
		public void perform()
		{
			HomeMsgHandler.onHandler();
			AvatarService.gI().dodelItem(this.map2);
			Canvas.startWaitDlg();
		}

		// Token: 0x04000ED1 RID: 3793
		private MapItem map2;
	}

	// Token: 0x020001AD RID: 429
	private class IActionEnterPass : IKbAction
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x00075301 File Offset: 0x00073701
		public void perform(string text)
		{
			GlobalService.gI().doEnterPass(text, 0);
			Canvas.endDlg();
		}
	}

	// Token: 0x020001AE RID: 430
	private class CommandShop1 : Command
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x00075314 File Offset: 0x00073714
		public CommandShop1(string caption, IAction action, int i, string nameItem, short idPart, short idItem, string timeLimit, int price, string des, short idPartGirl) : base(caption, action)
		{
			this.i = i;
			this.nameItem = nameItem;
			this.idPart = idPart;
			this.idItem = idItem;
			this.timeLimit = timeLimit;
			this.price = price;
			this.des = des;
			this.idPartGirl = idPartGirl;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00075368 File Offset: 0x00073768
		public override void update()
		{
			if (PopupShop.isTransFocus && this.i == PopupShop.focus)
			{
				PopupShop.resetIsTrans();
				Part part;
				if ((int)GameMidlet.avatar.gender == 1)
				{
					part = AvatarData.getPart(this.idPart);
				}
				else
				{
					part = AvatarData.getPart(this.idPartGirl);
				}
				if (part.IDPart != -1)
				{
					if ((int)GameMidlet.avatar.gender == 1)
					{
						MapScr.setAvatarShop(part);
					}
					else
					{
						MapScr.setAvatarShop(part);
					}
				}
				PopupShop.addStr(this.nameItem);
				if (this.timeLimit != null)
				{
					PopupShop.addStr(this.timeLimit);
				}
				if (this.price >= 0)
				{
					PopupShop.addStr(T.priceStr + Canvas.getMoneys(this.price) + " Tim");
				}
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00075440 File Offset: 0x00073840
		public override void paint(MyGraphics g, int x, int y)
		{
			Part part = AvatarData.getPart(this.idPart);
			part.paintIcon(g, x + PopupShop.wCell / 2, y + PopupShop.wCell / 2, 0, 3);
		}

		// Token: 0x04000ED2 RID: 3794
		private int i;

		// Token: 0x04000ED3 RID: 3795
		private string nameItem;

		// Token: 0x04000ED4 RID: 3796
		private string timeLimit;

		// Token: 0x04000ED5 RID: 3797
		private string des;

		// Token: 0x04000ED6 RID: 3798
		private short idPart;

		// Token: 0x04000ED7 RID: 3799
		private short idItem;

		// Token: 0x04000ED8 RID: 3800
		private short idPartGirl;

		// Token: 0x04000ED9 RID: 3801
		private int price;
	}

	// Token: 0x020001AF RID: 431
	private class IActionShop1 : IAction
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x00075474 File Offset: 0x00073874
		public IActionShop1(sbyte typeShop, short idItem, string des)
		{
			this.typeShop = typeShop;
			this.idItem = idItem;
			this.des = des;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00075491 File Offset: 0x00073891
		public void perform()
		{
			Canvas.startOKDlg(this.des, new HouseScr.IActionDes(this.typeShop, this.idItem));
		}

		// Token: 0x04000EDA RID: 3802
		private short idItem;

		// Token: 0x04000EDB RID: 3803
		private string des;

		// Token: 0x04000EDC RID: 3804
		private sbyte typeShop;
	}

	// Token: 0x020001B0 RID: 432
	private class IActionDes : IAction
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x000754AF File Offset: 0x000738AF
		public IActionDes(sbyte typeShop, short idItem)
		{
			this.typeShop = typeShop;
			this.idItem = idItem;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000754C5 File Offset: 0x000738C5
		public void perform()
		{
			GlobalService.gI().doSendOpenShopHouse(this.typeShop, this.idItem);
		}

		// Token: 0x04000EDD RID: 3805
		private sbyte typeShop;

		// Token: 0x04000EDE RID: 3806
		private short idItem;
	}
}
