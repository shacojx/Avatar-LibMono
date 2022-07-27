using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class LoadMap
{
	// Token: 0x060007B3 RID: 1971 RVA: 0x00046B6C File Offset: 0x00044F6C
	public LoadMap()
	{
		LoadMap.cmdNext = new Command(T.next, new LoadMap.IActionNextFocus());
		LoadMap.star = CRes.rnd(3);
		LoadMap.w = 24;
		LoadMap.imgDen = Image.createImagePNG(T.getPath() + "/effect/den");
		LoadMap.imgShadow = Image.createImage(T.getPath() + "/effect/s0");
		LoadMap.imgFocus = new FrameImage(Image.createImagePNG(T.getPath() + "/effect/focus"), 32 * AvMain.hd, 11 * AvMain.hd);
		LoadMap.posFocus = new AvPosition();
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00046C3C File Offset: 0x0004503C
	public void loadBG(int i)
	{
		if (i != 107 && (i < 0 || i >= LoadMap.bg.Length || (int)LoadMap.bg[i] == -1))
		{
			LoadMap.rememBg = -1;
			LoadMap.imgBG = null;
			this.imgTreeBg = null;
			this.imgClound = null;
			return;
		}
		if (LoadMap.imgBG == null)
		{
			LoadMap.rememBg = -1;
		}
		if (i != 107 && LoadMap.rememBg == (int)LoadMap.bg[i] && LoadMap.rememMap == (int)LoadMap.status)
		{
			return;
		}
		int num;
		if (i == 107)
		{
			num = 0;
		}
		else
		{
			num = (int)LoadMap.bg[i];
		}
		LoadMap.rememBg = num;
		this.imgTreeBg = Image.createImagePNG(string.Concat(new object[]
		{
			T.getPath(),
			"/bgHD/may",
			num,
			string.Empty,
			LoadMap.status
		}));
		LoadMap.imgBG = Image.createImagePNG(string.Concat(new object[]
		{
			T.getPath(),
			"/bgHD/",
			num,
			string.Empty,
			LoadMap.status
		}));
		this.imgClound = new Image[2];
		for (int j = 0; j < 2; j++)
		{
			this.imgClound[j] = Image.createImagePNG(string.Concat(new object[]
			{
				T.getPath(),
				"/effect/cl",
				j,
				LoadMap.status
			}));
		}
		LoadMap.x0_imgBG = (LoadMap.x0_imgTreeBg = 0);
		if (LoadMap.rememBg == 1)
		{
			LoadMap.x0_imgTreeBg = 25 * AvMain.hd;
			LoadMap.x0_imgBG += 20 * AvMain.hd;
		}
		else if (LoadMap.rememBg == 0)
		{
			LoadMap.x0_imgTreeBg = 20 * AvMain.hd;
			LoadMap.x0_imgBG = 2 * AvMain.hd;
		}
		else if (LoadMap.rememBg == 2)
		{
			LoadMap.x0_imgTreeBg = 10 * AvMain.hd;
			LoadMap.x0_imgBG = 15 * AvMain.hd;
		}
		else if (LoadMap.rememBg == 3)
		{
			LoadMap.x0_imgTreeBg = 33 * AvMain.hd;
			if ((int)LoadMap.status == 1)
			{
				LoadMap.x0_imgBG = 12 * AvMain.hd;
				LoadMap.x0_imgTreeBg -= 12 * AvMain.hd;
			}
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00046EA0 File Offset: 0x000452A0
	public void resetImg()
	{
		this.imgTreeBg = null;
		LoadMap.rememBg = -1;
		LoadMap.rememMap = -1;
		if (LoadMap.idTileImg == -1)
		{
			LoadMap.imgMap = null;
		}
		LoadMap.imgCreateMap = null;
		AvatarData.listImgIcon.clear();
		AvatarData.listImgPart.clear();
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00046EF1 File Offset: 0x000452F1
	public void updateKey()
	{
		if (PopupShop.gI() != Canvas.currentMyScreen && Input.touchCount <= 1 && !Canvas.isZoom)
		{
			this.updatePointer();
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00046F20 File Offset: 0x00045320
	private void updateSound()
	{
		if (LoadMap.imgBG != null)
		{
			if (this.countRndSound == 0)
			{
				this.countRndSound = 5 + CRes.rnd(5);
				this.timeCurSound = Canvas.getTick();
				int num = CRes.rnd(2);
				if (num != 0)
				{
					if (num != 1)
					{
					}
				}
			}
			else if (Canvas.getTick() - this.timeCurSound >= 1000L)
			{
				this.timeCurSound = Canvas.getTick();
				this.countRndSound--;
			}
		}
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00046FB4 File Offset: 0x000453B4
	public void update()
	{
		if (!Canvas.isZoom && Canvas.currentMyScreen != HouseScr.me)
		{
			AvCamera.gI().update();
		}
		this.updateSound();
		if (Canvas.menuMain == null && GameMidlet.avatar.task == -5 && Input.touchCount <= 1 && !Canvas.isZoom)
		{
			this.updatePathAvatar();
		}
		if ((Canvas.stypeInt == 0 || Canvas.currentMyScreen != MainMenu.gI()) && LoadMap.playerLists.size() > 0)
		{
			for (int i = 0; i < LoadMap.playerLists.size(); i++)
			{
				MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
				myObject.update();
			}
			LoadMap.orderVector(LoadMap.playerLists);
		}
		if (LoadMap.dynamicLists.size() > 0)
		{
			LoadMap.orderVector(LoadMap.dynamicLists);
			for (int j = 0; j < LoadMap.dynamicLists.size(); j++)
			{
				((MyObject)LoadMap.dynamicLists.elementAt(j)).update();
			}
		}
		if (LoadMap.treeLists.size() > 0)
		{
			for (int k = 0; k < LoadMap.treeLists.size(); k++)
			{
				MyObject myObject2 = (MyObject)LoadMap.treeLists.elementAt(k);
				myObject2.update();
			}
		}
		this.updateClound();
		if (Canvas.gameTick % 4 == 2 && !FarmScr.isSelected && !FarmScr.isAutoVatNuoi && Canvas.currentMyScreen != RaceScr.me)
		{
			LoadMap.setFocus();
		}
		if (Bus.isRun)
		{
			LoadMap.bus.update();
		}
		if (LoadMap.effManager != null)
		{
			for (int l = 0; l < LoadMap.effManager.size(); l++)
			{
				EffectManager effectManager = (EffectManager)LoadMap.effManager.elementAt(l);
				effectManager.update();
			}
		}
		if (LoadMap.effBgList != null)
		{
			for (int m = 0; m < LoadMap.effBgList.size(); m++)
			{
				EffectObj effectObj = (EffectObj)LoadMap.effBgList.elementAt(m);
				effectObj.update();
			}
		}
		if (LoadMap.effCameraList != null)
		{
			for (int n = 0; n < LoadMap.effCameraList.size(); n++)
			{
				EffectObj effectObj2 = (EffectObj)LoadMap.effCameraList.elementAt(n);
				effectObj2.update();
			}
		}
		if (LoadMap.imgFocus != null && LoadMap.dirFocus != -1 && LoadMap.nPath > 0)
		{
			LoadMap.posFocus.anchor++;
			if (LoadMap.posFocus.anchor >= 10)
			{
				LoadMap.posFocus.anchor = 0;
			}
		}
		LoadMap.numF++;
		if (LoadMap.numF >= 6)
		{
			LoadMap.numF = 0;
		}
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00047298 File Offset: 0x00045698
	public void updatePointer()
	{
		if (GameMidlet.avatar.ableShow || Canvas.isZoom || Canvas.currentDialog != null || Canvas.currentFace != null || Canvas.menuMain != null)
		{
			return;
		}
		if (Canvas.welcome != null & Welcome.isPaintArrow)
		{
			return;
		}
		float num = AvMain.zoom;
		this.count += 1L;
		if (Canvas.isPointerClick && Canvas.isPointer(0, 0, Canvas.w, Canvas.hCan))
		{
			this.pyLast = Canvas.pyLast;
			this.pxLast = Canvas.pxLast;
			Canvas.isPointerClick = false;
			this.timeDelay = this.count;
			this.pa = AvCamera.gI().yCam;
			this.pb = AvCamera.gI().xCam;
			this.transY = true;
			AvCamera.gI().vY = 0f;
			AvCamera.gI().vX = 0f;
		}
		if (this.transY)
		{
			long num2 = this.count - this.timeDelay;
			int num3 = (int)((float)(this.pyLast - Canvas.py) / AvMain.zoom);
			this.pyLast = Canvas.py;
			int num4 = (int)((float)(this.pxLast - Canvas.px) / AvMain.zoom);
			this.pxLast = Canvas.px;
			if (Canvas.isPointerDown)
			{
				if (this.count % 2L == 0L)
				{
					this.dyTran = (float)Canvas.py;
					this.dxTran = (float)Canvas.px;
					this.timePointY = this.count;
					this.timePointX = this.count;
				}
				AvCamera.gI().vY = 0f;
				AvCamera.gI().vX = 0f;
				if (Canvas.currentMyScreen != HouseScr.me && (Canvas.w < (int)LoadMap.wMap * LoadMap.w * AvMain.hd + LoadMap.w * 2 * AvMain.hd || Canvas.hCan < (int)LoadMap.Hmap * LoadMap.w * 2 * AvMain.hd + LoadMap.w * 2 * AvMain.hd))
				{
					if ((float)Canvas.w < (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom)
					{
						AvCamera.gI().xTo = (float)((int)(this.pb + (float)num4));
						if (AvCamera.gI().xTo <= 0f)
						{
							AvCamera.gI().xTo = 0f;
						}
						else if (AvCamera.gI().xTo > AvCamera.gI().xLimit)
						{
							AvCamera.gI().xTo = AvCamera.gI().xLimit;
						}
					}
					else
					{
						AvCamera.gI().xTo = -((float)Canvas.w - (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom) / 2f;
					}
				}
				if (LoadMap.imgBG != null || LoadMap.TYPEMAP == 68 || LoadMap.TYPEMAP == 69 || LoadMap.TYPEMAP == 70 || LoadMap.TYPEMAP == 110 || LoadMap.idTileImg != -1 || Canvas.w < (int)LoadMap.wMap * LoadMap.w * AvMain.hd + LoadMap.w * 2 * AvMain.hd || Canvas.hCan < (int)LoadMap.Hmap * LoadMap.w * 2 * AvMain.hd + LoadMap.w * 2 * AvMain.hd)
				{
					AvCamera.gI().yTo = (float)((int)(this.pa + (float)num3));
					if (Canvas.currentMyScreen == HouseScr.me)
					{
						if (AvCamera.gI().yTo < (float)(-(float)(Canvas.hCan / 3)))
						{
							AvCamera.gI().yTo = (float)(-(float)(Canvas.hCan / 3));
						}
						if (AvCamera.gI().yTo > AvCamera.gI().yLimit + (float)(LoadMap.w * AvMain.hd))
						{
							AvCamera.gI().yTo = AvCamera.gI().yLimit + (float)(LoadMap.w * AvMain.hd);
						}
						if ((float)Canvas.w < (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom)
						{
							AvCamera.gI().xTo = (float)((int)(this.pb + (float)num4));
							if (AvCamera.gI().xTo <= (float)(-(float)LoadMap.w * AvMain.hd))
							{
								AvCamera.gI().xTo = (float)(-(float)LoadMap.w * AvMain.hd);
							}
							else if (AvCamera.gI().xTo > AvCamera.gI().xLimit + (float)(LoadMap.w * AvMain.hd))
							{
								AvCamera.gI().xTo = AvCamera.gI().xLimit + (float)(LoadMap.w * AvMain.hd);
							}
						}
						else
						{
							AvCamera.gI().xTo = -((float)Canvas.w - (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom) / 2f;
						}
					}
					else if (AvCamera.gI().yTo < (float)(-(float)Canvas.hCan))
					{
						AvCamera.gI().yTo = (float)(-(float)Canvas.hCan);
					}
					else if (LoadMap.imgBG != null && AvCamera.gI().yTo > AvCamera.gI().yLimit)
					{
						AvCamera.gI().yTo = AvCamera.gI().yLimit;
					}
					this.pa = AvCamera.gI().yTo;
					AvCamera.gI().yCam = AvCamera.gI().yTo;
				}
				this.pb = AvCamera.gI().xTo;
				AvCamera.gI().xCam = AvCamera.gI().xTo;
				if (CRes.abs(Canvas.dx()) > 20 * AvMain.hd || CRes.abs(Canvas.dy()) > 20 * AvMain.hd)
				{
					AvCamera.gI().timeDelay = (long)(Environment.TickCount / 100);
				}
			}
			else
			{
				this.transY = false;
			}
			if (Canvas.isPointerRelease)
			{
				this.transY = false;
				int num5 = (int)(this.count - this.timePointY);
				float num6 = this.dyTran - (float)Canvas.py;
				float num7 = this.dxTran - (float)Canvas.px;
				if (!MapScr.isWedding)
				{
					if (CRes.abs(Canvas.dx()) > 20 * AvMain.hd || CRes.abs(Canvas.dy()) > 20 * AvMain.hd)
					{
						AvCamera.gI().timeDelay = (long)(Environment.TickCount / 100);
						if (CRes.abs((int)num6) > 20 * AvMain.hd && num5 < 20)
						{
							AvCamera.gI().vY = num6 / (float)num5 * 5f;
							AvCamera.isMove = true;
						}
						if ((float)((int)LoadMap.Hmap * LoadMap.w * AvMain.hd) <= (float)(Canvas.hCan / 3 * 2) / AvMain.zoom)
						{
							AvCamera.gI().vY = 0f;
						}
						int num8 = (int)(this.count - this.timePointY);
						if (CRes.abs((int)num7) > 20 * AvMain.hd && num8 < 20 && AvCamera.gI().xTo > 0f && AvCamera.gI().xTo < AvCamera.gI().xLimit)
						{
							AvCamera.gI().vX = num7 / (float)num8 * 5f;
							AvCamera.isMove = true;
						}
					}
					else
					{
						int num9 = (int)((float)Canvas.px + AvCamera.gI().xCam);
						int num10 = (int)((float)Canvas.py + AvCamera.gI().yCam);
						int num11 = (int)((float)Canvas.px / num + AvCamera.gI().xCam);
						int num12 = (int)((float)Canvas.py / num + AvCamera.gI().yCam - (float)Canvas.transTab);
						bool flag = false;
						for (int i = 0; i < LoadMap.treeLists.size(); i++)
						{
							MyObject myObject = (MyObject)LoadMap.treeLists.elementAt(i);
							if (myObject.index != -1 && num9 > myObject.x * AvMain.hd - (int)(myObject.w / 2) && num9 < myObject.x * AvMain.hd - (int)(myObject.w / 2) + (int)myObject.w && num10 > myObject.y * AvMain.hd - (int)myObject.h && num10 < myObject.y * AvMain.hd && this.setJoin((int)myObject.index))
							{
								Canvas.isPointerRelease = false;
								LoadMap.posFocus.x = myObject.x * AvMain.hd;
								LoadMap.posFocus.y = (myObject.y - 4) * AvMain.hd;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							LoadMap.posFocus.x = num11;
							LoadMap.posFocus.y = num12;
							SoundManager.playSound(7);
						}
						int num13 = 88;
						int num14 = LoadMap.w * AvMain.hd;
						if (LoadMap.posFocus.y < 0)
						{
							LoadMap.posFocus.y = num14 + num14 / 2;
						}
						if (LoadMap.posFocus.y / num14 * (int)LoadMap.wMap + LoadMap.posFocus.x / num14 > 0 && LoadMap.posFocus.y / num14 * (int)LoadMap.wMap + LoadMap.posFocus.x / num14 < LoadMap.type.Length)
						{
							num13 = (int)LoadMap.type[LoadMap.posFocus.y / num14 * (int)LoadMap.wMap + LoadMap.posFocus.x / num14];
						}
						LoadMap.posFocus.anchor = 0;
						if (GameMidlet.avatar.task == 0 || GameMidlet.avatar.task == -5)
						{
							GameMidlet.avatar.task = -5;
							GameMidlet.avatar.isJumps = -1;
							GameMidlet.avatar.xCur = GameMidlet.avatar.x;
							GameMidlet.avatar.yCur = GameMidlet.avatar.y;
							this.xfirDu = GameMidlet.avatar.x % LoadMap.w;
							this.yfirDu = GameMidlet.avatar.y % LoadMap.w;
							this.xLastDu = LoadMap.posFocus.x % num14 / 2;
							this.yLastDu = LoadMap.posFocus.y % num14 / 2 + 3;
							this.xFirFocus = GameMidlet.avatar.x;
							this.yFirFocus = GameMidlet.avatar.y;
							if (GameMidlet.avatar.y > LoadMap.posFocus.y / AvMain.hd)
							{
								this.iTop = -1;
							}
							else
							{
								this.iTop = 1;
							}
							if (GameMidlet.avatar.x > LoadMap.posFocus.x / AvMain.hd)
							{
								this.iLeft = 1;
							}
							else
							{
								this.iLeft = -1;
							}
							if (GameMidlet.avatar.x > LoadMap.posFocus.x / AvMain.hd)
							{
								LoadMap.dirFocus = (int)Base.LEFT;
							}
							else
							{
								LoadMap.dirFocus = (int)Base.RIGHT;
							}
							if (!Canvas.paint.selectedPointer(num11, num12))
							{
								if (num13 == 80 || LoadMap.TYPEMAP == 24 || this.setTypeJoint(num13) || this.setTypeFind(num13))
								{
									this.change();
								}
							}
						}
					}
				}
				this.timePointY = -1L;
				this.timePointX = -1L;
				this.transX = false;
			}
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00047E2C File Offset: 0x0004622C
	public bool setJoin(int type)
	{
		switch (type)
		{
		case 93:
		case 94:
		case 97:
		case 98:
		case 100:
		case 103:
		case 104:
		case 107:
		case 108:
		case 109:
		case 110:
		case 111:
		case 112:
			break;
		default:
			switch (type)
			{
			case 58:
			case 59:
			case 63:
			case 64:
				break;
			default:
				switch (type)
				{
				case 24:
				case 25:
				case 29:
					break;
				default:
					switch (type)
					{
					case 68:
					case 69:
					case 70:
						break;
					default:
						switch (type)
						{
						case 52:
						case 55:
							break;
						default:
							if (type != 18 && type != 89)
							{
								return false;
							}
							break;
						}
						break;
					}
					break;
				}
				break;
			}
			break;
		}
		return true;
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00047F20 File Offset: 0x00046320
	public void change()
	{
		if ((int)GameMidlet.avatar.action == 14)
		{
			Out.println(string.Concat(new object[]
			{
				"pos: ",
				LoadMap.posFocus.x / AvMain.hd,
				"    ",
				GameMidlet.avatar.x,
				"    ",
				GameMidlet.avatar.direct
			}));
			if (LoadMap.posFocus.x / AvMain.hd < GameMidlet.avatar.x && (int)GameMidlet.avatar.direct == (int)Base.RIGHT)
			{
				GameMidlet.avatar.direct = Base.LEFT;
				return;
			}
			if (LoadMap.posFocus.x / AvMain.hd > GameMidlet.avatar.x && (int)GameMidlet.avatar.direct == (int)Base.LEFT)
			{
				GameMidlet.avatar.direct = Base.RIGHT;
				return;
			}
			GameMidlet.avatar.action = 0;
			GameMidlet.avatar.setPos(HouseScr.gI().xHo, HouseScr.gI().yHo);
			AvatarService.gI().doFeel(0);
			MapScr.gI().doMove(GameMidlet.avatar.x, GameMidlet.avatar.y, (int)GameMidlet.avatar.direct);
			return;
		}
		else
		{
			int num = LoadMap.w * AvMain.hd;
			if (GameMidlet.avatar.x / LoadMap.w == LoadMap.posFocus.x / num && GameMidlet.avatar.y / LoadMap.w == LoadMap.posFocus.y / num)
			{
				GameMidlet.avatar.setPosTo(LoadMap.posFocus.x / num * LoadMap.w + LoadMap.posFocus.x % num / 2, LoadMap.posFocus.y / num * LoadMap.w + LoadMap.posFocus.y % num / 2);
				return;
			}
			if (!this.findPath(GameMidlet.avatar.x / LoadMap.w, GameMidlet.avatar.y / LoadMap.w, LoadMap.posFocus.x / num, LoadMap.posFocus.y / num))
			{
				LoadMap.nPath = 0;
				LoadMap.dirFocus = -1;
				return;
			}
			return;
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00048178 File Offset: 0x00046578
	public void updatePathAvatar()
	{
		if (GameMidlet.avatar.xCur == GameMidlet.avatar.x && GameMidlet.avatar.yCur == GameMidlet.avatar.y)
		{
			LoadMap.nPath--;
			if (LoadMap.nPath < 0)
			{
				GameMidlet.avatar.task = 0;
				LoadMap.dirFocus = -1;
				GameMidlet.avatar.isSetAction = false;
				return;
			}
			int num = (int)(this.mPath[LoadMap.nPath] & 255);
			int num2 = this.mPath[LoadMap.nPath] >> 8;
			int num3 = LoadMap.w / 2;
			int num4 = LoadMap.w / 2;
			if ((int)this.iTop == -1)
			{
				if (num2 == this.yFirFocus / LoadMap.w && num3 > this.yfirDu)
				{
					num3 = this.yfirDu;
				}
				else if (num2 == LoadMap.posFocus.y / AvMain.hd / LoadMap.w && num3 < this.yLastDu)
				{
					num3 = this.yLastDu;
				}
			}
			else if (num3 > this.yLastDu && num2 == LoadMap.posFocus.y / AvMain.hd / LoadMap.w)
			{
				num3 = this.yLastDu;
			}
			else if (num2 == GameMidlet.avatar.y / LoadMap.w && num3 < this.yfirDu)
			{
				num3 = this.yfirDu;
			}
			if ((int)this.iLeft == 1)
			{
				if (num == this.xFirFocus / LoadMap.w && num4 > this.xfirDu)
				{
					num4 = this.xfirDu;
				}
				else if (num == LoadMap.posFocus.x / AvMain.hd / LoadMap.w && num4 < this.xLastDu)
				{
					num4 = this.xLastDu;
				}
			}
			else if (num == this.xFirFocus / LoadMap.w && num4 < this.xfirDu)
			{
				num4 = this.xfirDu;
			}
			else if (num == LoadMap.posFocus.x / AvMain.hd / LoadMap.w && num4 > this.xLastDu)
			{
				num4 = this.xLastDu;
			}
			num = num * LoadMap.w + num4;
			num2 = num2 * LoadMap.w + num3;
			if (LoadMap.nPath == 0)
			{
				GameMidlet.avatar.isSetAction = true;
				num = LoadMap.posFocus.x / AvMain.hd;
				num2 = LoadMap.posFocus.y / AvMain.hd;
				LoadMap.nPath = 0;
			}
			if (!GameMidlet.avatar.doJoin(num - GameMidlet.avatar.x, num2 - GameMidlet.avatar.y) && !GameMidlet.avatar.detectCollisionMap(num - GameMidlet.avatar.x, num2 - GameMidlet.avatar.y))
			{
				if (!this.setTypeFindEnd(LoadMap.getTypeMap(num, num2)))
				{
					GameMidlet.avatar.setPosTo(num, num2);
					GameMidlet.avatar.action = 1;
				}
			}
			else
			{
				LoadMap.nPath = 0;
				GameMidlet.avatar.isSetAction = false;
			}
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00048488 File Offset: 0x00046888
	public void initFindPath()
	{
		int num = (int)LoadMap.wMap;
		int hmap = (int)LoadMap.Hmap;
		this.used = new bool[num][];
		for (int i = 0; i < num; i++)
		{
			this.used[i] = new bool[hmap];
		}
		this.to = new short[num * hmap];
		this.from = new short[num * hmap];
		this.mPath = new short[num * hmap];
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x000484F7 File Offset: 0x000468F7
	public static void resetPath()
	{
		LoadMap.nPath = 0;
		GameMidlet.avatar.isSetAction = false;
		GameMidlet.avatar.task = 0;
		Out.println("resetPath");
		LoadMap.dirFocus = -1;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00048528 File Offset: 0x00046928
	public bool findPath(int from_x, int from_y, int to_x, int to_y)
	{
		int num = 1;
		int num2 = 0;
		int[] array = new int[4];
		array[1] = -1;
		array[2] = 1;
		int[] array2 = array;
		int[] array3 = new int[]
		{
			-1,
			0,
			0,
			1
		};
		bool flag = false;
		for (int i = 0; i < this.used.Length * this.used[0].Length; i++)
		{
			this.to[i] = 0;
			this.from[i] = 0;
			this.mPath[i] = 0;
			if ((int)LoadMap.type[i] != 80 && (int)LoadMap.type[i] != 51 && !this.setTypeJoint((int)LoadMap.type[i]))
			{
				this.used[i % (int)LoadMap.wMap][i / (int)LoadMap.wMap] = true;
			}
			else
			{
				this.used[i % (int)LoadMap.wMap][i / (int)LoadMap.wMap] = false;
			}
		}
		int typeMap = LoadMap.getTypeMap(to_x * LoadMap.w, to_y * LoadMap.w);
		if (this.setTypeFind(typeMap))
		{
			this.used[to_x][to_y] = false;
		}
		this.to[num2] = (short)((from_y << 8) + from_x);
		while (!flag && num2 < num)
		{
			int num3 = (int)(this.to[num2] & 255);
			int num4 = this.to[num2] >> 8;
			int j = 0;
			while (j < 4 && !flag)
			{
				int num5 = num3 + array2[j];
				int num6 = num4 + array3[j];
				if (num5 >= 0 && num5 < this.used.Length && num6 >= 0 && num6 < this.used[0].Length && !this.used[num5][num6])
				{
					this.from[num] = this.to[num2];
					this.to[num++] = (short)((num6 << 8) + num5);
					this.used[num5][num6] = true;
					if (to_x == num5 && to_y == num6)
					{
						flag = true;
					}
				}
				if (num >= this.used.Length * this.used[0].Length)
				{
					flag = true;
					break;
				}
				j++;
			}
			num2++;
		}
		LoadMap.nPath = 0;
		if (flag)
		{
			GameMidlet.avatar.resetAction();
			int j = num - 1;
			this.mPath[LoadMap.nPath++] = this.to[j];
			while (j > 0)
			{
				for (int k = 0; k < num; k++)
				{
					if (this.to[k] == this.from[j])
					{
						j = k;
						this.mPath[LoadMap.nPath++] = this.to[j];
						break;
					}
				}
			}
		}
		LoadMap.nPath--;
		return flag;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x000487F8 File Offset: 0x00046BF8
	private void updateClound()
	{
		if (this.clound == null || LoadMap.imgBG == null || this.imgClound == null)
		{
			return;
		}
		for (int i = 0; i < this.clound.Length; i++)
		{
			this.clound[i].x -= (int)this.clound[i].index;
			if (this.clound[i].x / 100 < -this.imgClound[this.clound[i].anchor].w)
			{
				int num = 0;
				if (LoadMap.rememBg == 0)
				{
					num = -10 * AvMain.hd;
				}
				else if (LoadMap.rememBg == 2)
				{
					num = 5 * AvMain.hd;
				}
				else if (LoadMap.rememBg == 3)
				{
					num = 25 * AvMain.hd;
				}
				this.clound[i].anchor = CRes.rnd(2);
				this.clound[i].y = -(LoadMap.imgBG.h / 2 + this.imgClound[this.clound[i].anchor].h + num + CRes.rnd(LoadMap.imgBG.h / 2));
				if (this.clound[i].anchor == 1)
				{
					this.clound[i].index = (short)(10 + CRes.rnd(30));
				}
				else
				{
					this.clound[i].index = (short)(30 + CRes.rnd(30));
				}
				this.clound[i].x = ((int)LoadMap.wMap * LoadMap.w + this.imgClound[this.clound[i].anchor].w + CRes.rnd(LoadMap.imgBG.w)) * 100;
			}
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000489C0 File Offset: 0x00046DC0
	public static void setFocus()
	{
		if (LoadMap.TYPEMAP == -1 || LoadMap.isGo || Canvas.currentMyScreen == MainMenu.me || Canvas.menuMain != null)
		{
			return;
		}
		if (LoadMap.focusObj == null)
		{
			for (int i = 0; i < LoadMap.playerLists.size(); i++)
			{
				if (LoadMap.setFocus(i))
				{
					break;
				}
			}
		}
		else if (CRes.abs(LoadMap.focusObj.x - GameMidlet.avatar.x) / LoadMap.w >= LoadMap.wFocus || CRes.abs(LoadMap.focusObj.y - GameMidlet.avatar.y) / LoadMap.w >= LoadMap.wFocus)
		{
			LoadMap.focusObj = null;
			MapScr.focusP = null;
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00048A98 File Offset: 0x00046E98
	public static void NextFocus()
	{
		if (LoadMap.focusObj == null)
		{
			return;
		}
		LoadMap.isGo = false;
		int num = 0;
		int num2 = LoadMap.playerLists.size();
		for (int i = 0; i < num2; i++)
		{
			MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
			if ((int)myObject.catagory != 4 && myObject == LoadMap.focusObj)
			{
				num = i;
				break;
			}
		}
		LoadMap.focusObj = null;
		for (int j = num + 1; j < num2; j++)
		{
			if (LoadMap.setFocus(j))
			{
				break;
			}
		}
		if (LoadMap.focusObj == null)
		{
			for (int k = 0; k <= num; k++)
			{
				if (LoadMap.setFocus(k))
				{
					break;
				}
			}
		}
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00048B68 File Offset: 0x00046F68
	private static bool setFocus(int i)
	{
		MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
		if ((int)myObject.catagory != 4 && myObject != GameMidlet.avatar && (int)myObject.catagory != 6 && global::Math.abs(myObject.x - GameMidlet.avatar.x) / LoadMap.w < LoadMap.wFocus && global::Math.abs(myObject.y - GameMidlet.avatar.y) / LoadMap.w < LoadMap.wFocus)
		{
			if ((int)myObject.catagory != 0 || !((Avatar)myObject).ableShow)
			{
				LoadMap.focusObj = myObject;
			}
			if ((int)myObject.catagory == 0 && !((Avatar)myObject).ableShow)
			{
				MapScr.focusP = (Avatar)LoadMap.playerLists.elementAt(i);
			}
			return true;
		}
		return false;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00048C50 File Offset: 0x00047050
	private bool setTypeJoint(int type)
	{
		return (type >= -125 && type < 0) || (type == -1 || type == 0 || type == 1 || type == 2 || type == 3 || type == 4 || type == 5 || type == 6 || type == 7 || type == 8 || type == 12 || type == 11 || type == 14 || type == 15 || type == 16 || type == 13 || type == 25 || type == 24 || type == 52 || type == 53 || type == 9 || type == 56 || type == 72 || type == 73 || type == 75 || type == 74 || type == 76 || type == 77 || type == 21 || type == 68 || type == 110 || type == 69 || type == 70 || type == 17 || type == 18 || type == 51 || type == 71 || type == 95 || type == 96 || type == 111 || type == 112);
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00048DB0 File Offset: 0x000471B0
	public bool setTypeFind(int type)
	{
		return (type >= -125 && type < 0) || (type == 57 || type == 62 || type == 58 || type == 63 || type == 59 || type == 64 || type == 99 || type == 106 || type == 108 || type == 109 || type == 55 || type == 93 || type == 78 || type == 89 || type == 27 || type == 28 || type == 29 || type == 84 || type == 85 || type == 86 || type == 83 || type == 87 || type == 54 || type == 67 || type == 81 || type == 71 || type == 79 || type == 92 || type == 52 || type == 94 || type == 95 || type == 96 || type == 97 || type == 98 || type == 100 || type == 103 || type == 101 || type == 104 || type == 23 || type == 107 || type == 19 || type == 10);
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00048F24 File Offset: 0x00047324
	public bool setTypeFindEnd(int type)
	{
		return (type >= -125 && type < 0) || (type == 57 || type == 62 || type == 58 || type == 63 || type == 59 || type == 64 || type == 99 || type == 106 || type == 108 || type == 109 || type == 55 || type == 93 || type == 78 || type == 89 || type == 27 || type == 28 || type == 29 || type == 84 || type == 85 || type == 86 || type == 83 || type == 87 || type == 54 || type == 71 || type == 52 || type == 94 || type == 95 || type == 96 || type == 97 || type == 98 || type == 100 || type == 103 || type == 101 || type == 104 || type == 23 || type == 107 || type == 19 || type == 10);
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00049078 File Offset: 0x00047478
	public bool doJoin(int x, int y)
	{
		LoadMap.isGo = false;
		int typeMap = LoadMap.getTypeMap(x, y);
		if (typeMap == -2)
		{
			return false;
		}
		switch (typeMap + 1)
		{
		case 0:
			MapScr.gI().move();
			if (LoadMap.TYPEMAP == 25)
			{
				FarmScr.gI().doExitBus();
			}
			if (LoadMap.imgBG != null)
			{
				LoadMap.bus.setBus(-1);
			}
			else
			{
				MapScr.gI().doExit();
			}
			return true;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 12:
		case 14:
		case 15:
		case 16:
		case 17:
		case 19:
			MapScr.gI().move();
			ParkService.gI().doJoinPark(typeMap, -1);
			return true;
		case 10:
			Canvas.startOKDlg(T.doYouWantExit2, new LoadMap.IActionExitToCity());
			return true;
		case 11:
			Canvas.startWaitDlg();
			MapScr.gI().move();
			LoadMap.rememMap = -1;
			ParkService.gI().doJoinPark(10, -1);
			return true;
		case 13:
			Canvas.startOKDlg(T.doYouWantExit2, new LoadMap.IActionExitToCity());
			return true;
		case 18:
			Canvas.startOKDlg(T.doYouWantExit2, new LoadMap.IActionExitToCity());
			return true;
		case 20:
			Canvas.startWaitDlg();
			MapScr.gI().move();
			LoadMap.rememMap = -1;
			ParkService.gI().doJoinPark(19, -1);
			return true;
		case 21:
			GlobalService.gI().requestJoinAny(0);
			Canvas.startWaitDlg();
			return true;
		case 22:
			HouseScr.gI().doOut();
			return true;
		case 24:
			GlobalService.gI().getHandler(9);
			Canvas.startWaitDlg();
			return true;
		case 25:
			if (FarmScr.cell == null || FarmScr.idFarm != GameMidlet.avatar.IDDB)
			{
				Canvas.startWaitDlg();
				FarmScr.gI().doJoinFarm(GameMidlet.avatar.IDDB, true);
			}
			else
			{
				FarmScr.gI().onJoin(FarmScr.idFarm, FarmScr.cell, FarmScr.animalLists, FarmScr.numBarn, FarmScr.numPond, FarmScr.foodID, FarmScr.remainTime);
			}
			return true;
		case 26:
			FarmScr.gI().doGoFarmWay();
			return true;
		case 28:
		case 57:
			if (LoadMap.TYPEMAP != 18 && LoadMap.TYPEMAP != 109 && LoadMap.TYPEMAP != 108 && GameMidlet.CLIENT_TYPE == 8)
			{
				MapScr.gI().doOpenShopOffline(GameMidlet.avatar, 0);
			}
			return true;
		case 29:
			FarmScr.gI().doOpenKhoHang();
			return true;
		case 30:
			Canvas.startWaitDlg();
			ParkService.gI().doRequestBoardList(MapScr.roomID);
			return true;
		case 53:
			FarmScr.gI().doOpenCuaHang();
			return true;
		case 54:
			FarmScr.gI().doMenuFarmFriend();
			return true;
		case 55:
			return FishingScr.gI().doSat(x, y);
		case 56:
			Canvas.startWaitDlg();
			GlobalService.gI().requestChargeMoneyInfo();
			return true;
		case 58:
			MapScr.gI().move();
			MapScr.gI().doJoinShop(1);
			return true;
		case 59:
			MapScr.gI().doJoinShop(2);
			return true;
		case 60:
			MapScr.gI().doJoinShop(3);
			return true;
		case 63:
			MapScr.gI().move();
			MapScr.gI().doJoinShop(6);
			return true;
		case 64:
			MapScr.gI().doJoinShop(7);
			return true;
		case 65:
			MapScr.gI().doJoinShop(8);
			return true;
		case 69:
		case 70:
		case 71:
			HouseScr.gI().doJoinFriendHome(typeMap - 67);
			return true;
		case 72:
			Canvas.startWaitDlg();
			GlobalService.gI().requestCityMap(-1);
			return true;
		case 73:
		case 74:
		case 75:
		case 76:
		case 77:
		case 78:
			MapScr.indexMap = LoadMap.TYPEMAP;
			LoadMap.xJoinCasino = GameMidlet.avatar.x;
			LoadMap.yJoinCasino = GameMidlet.avatar.y;
			if (typeMap - 72 == 2 && Canvas.iOpenOngame == 1)
			{
				Canvas.startWaitDlg();
				MapScr.idCityMap = 1;
				MapScr.idSelectedMini = 0;
				GlobalService.gI().requestJoinAny(4);
			}
			else
			{
				MapScr.gI().doGetHandlerCasino(typeMap - 72);
			}
			return true;
		case 79:
			MapScr.gI().doOpenIceDream(T.food, 5);
			return true;
		case 84:
			Canvas.loadMap.getAd(x / LoadMap.w, y / LoadMap.w);
			return true;
		case 85:
			FarmScr.gI().doCattleFeeding(2, 5);
			return true;
		case 86:
			FarmScr.gI().doCattleFeeding(3, 5);
			return true;
		case 87:
		{
			int num = LoadMap.getposMap(x, y);
			int num2 = LoadMap.getposMap(Cattle.posBucket.x, Cattle.posBucket.y);
			FarmScr.gI().doHarvestAnimal(2, num - num2, FarmScr.listBucket);
			return true;
		}
		case 88:
		{
			int num3 = LoadMap.getposMap(x, y);
			int num4 = LoadMap.getposMap(Chicken.posNest.x, Chicken.posNest.y);
			FarmScr.gI().doHarvestAnimal(1, num3 - num4, FarmScr.listNest);
			return true;
		}
		case 90:
		{
			int idUser;
			if (LoadMap.TYPEMAP == 108 || LoadMap.TYPEMAP == 109)
			{
				idUser = 1;
			}
			else if (LoadMap.TYPEMAP == 13)
			{
				idUser = 2;
			}
			else
			{
				idUser = 3;
			}
			GlobalService.gI().doCommunicate(idUser);
			Canvas.startWaitDlg();
			return true;
		}
		case 94:
			MapScr.gI().doOpenIceDream(T.food, 4);
			return true;
		case 95:
			GlobalService.gI().doCommunicate(4);
			Canvas.startWaitDlg();
			return true;
		case 96:
			Canvas.startWaitDlg();
			FarmScr.xRemember = GameMidlet.avatar.x;
			FarmScr.yRemember = GameMidlet.avatar.y;
			FarmService.gI().doUpdateFarm(0, 0);
			return true;
		case 97:
			Canvas.startWaitDlg();
			FarmScr.xRemember = GameMidlet.avatar.x;
			FarmScr.yRemember = GameMidlet.avatar.y;
			FarmService.gI().doUpdateFish(0, 0);
			return true;
		case 98:
			FarmScr.gI().doMenuStarFruit();
			return true;
		case 99:
			FarmScr.gI().doOpenCooking();
			return true;
		case 101:
			MapScr.gI().doJoinMapOffline(5);
			return true;
		case 102:
			MapScr.gI().doJoinMapOffline(6);
			return true;
		case 104:
			MapScr.gI().doJoinMapOffline(3);
			return true;
		case 105:
			MapScr.gI().doJoinMapOffline(4);
			return true;
		case 108:
			Canvas.startWaitDlg();
			MapScr.indexMap = LoadMap.TYPEMAP;
			LoadMap.xJoinCasino = GameMidlet.avatar.x;
			LoadMap.yJoinCasino = GameMidlet.avatar.y;
			GlobalService.gI().getHandler(12);
			return true;
		case 109:
		case 110:
			Canvas.startWaitDlg();
			MapScr.idCityMap = 1;
			MapScr.idSelectedMini = 0;
			LoadMap.xJoinCasino = GameMidlet.avatar.x;
			LoadMap.yJoinCasino = GameMidlet.avatar.y;
			GlobalService.gI().requestJoinAny(4);
			return true;
		case 111:
			Canvas.startWaitDlg();
			AvatarService.gI().doJoinHouse4(GameMidlet.avatar.IDDB);
			return true;
		case 112:
			Canvas.startWaitDlg();
			GlobalService.gI().doFlowerLove();
			return true;
		}
		if (typeMap == 112)
		{
			if ((int)GameMidlet.avatar.action != 14)
			{
				HouseScr.gI().xHo = GameMidlet.avatar.x;
				HouseScr.gI().yHo = GameMidlet.avatar.y;
				GameMidlet.avatar.setPos(x / LoadMap.w * LoadMap.w + LoadMap.w / 2 + 2, y / LoadMap.w * LoadMap.w + 5);
				MapScr.gI().doMove(GameMidlet.avatar.x, GameMidlet.avatar.y, (int)GameMidlet.avatar.direct);
				GameMidlet.avatar.doAction(14);
				AvatarService.gI().doFeel(14);
			}
		}
		else
		{
			if (typeMap < -125 || typeMap >= 0)
			{
				return false;
			}
			Canvas.startWaitDlg();
			LoadMap.typeTemp = typeMap;
			GlobalService.gI().requestJoinAny((short)(typeMap - -125));
		}
		return true;
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00049970 File Offset: 0x00047D70
	public void paintEffectCamera(MyGraphics g)
	{
		if (LoadMap.effCameraList != null)
		{
			for (int i = 0; i < LoadMap.effCameraList.size(); i++)
			{
				EffectObj effectObj = (EffectObj)LoadMap.effCameraList.elementAt(i);
				effectObj.paint(g);
			}
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x000499BC File Offset: 0x00047DBC
	public void paint(MyGraphics g)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintBGCMD(g, 0, Canvas.h, Canvas.w, Canvas.hTab);
		Canvas.resetTrans(g);
		g.translate(-AvCamera.gI().xCam, -AvCamera.gI().yCam);
		this.paintM(g);
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00049A14 File Offset: 0x00047E14
	public void paintCreateMap(MyGraphics g, int x)
	{
		if (LoadMap.imgCreateMap != null)
		{
			int num = 0;
			int num2 = 1;
			if (LoadMap.imgCreateMap.Length == 1)
			{
				num2 = 0;
			}
			for (int i = 0; i < LoadMap.imgCreateMap.Length; i++)
			{
				if (AvCamera.gI().xCam + (float)Canvas.w > (float)(x - num2 + num) && AvCamera.gI().xCam < (float)(x - num2 + num + (LoadMap.imgCreateMap[i].w - num2 * 2)))
				{
					g.drawImage(LoadMap.imgCreateMap[i], (float)(x - num2 + num), (float)((int)LoadMap.Hmap * LoadMap.w * AvMain.hd - LoadMap.imgCreateMap[i].h), 0);
				}
				num += LoadMap.imgCreateMap[i].w - num2 * 2;
			}
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00049AE0 File Offset: 0x00047EE0
	public void paintM(MyGraphics g)
	{
		this.paintBackGround(g);
		if (LoadMap.imgCreateMap != null)
		{
			if ((float)Canvas.w < (float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom)
			{
				g.setColor(0);
			}
			this.paintCreateMap(g, 0);
			if (this.imgDayDien0 != null)
			{
				g.drawImage(this.imgDayDien0, 0f, (float)(LoadMap.w + ((AvMain.hd != 2) ? 0 : LoadMap.w) + LoadMap.w / 2 - this.imgDayDien0.getHeight()), 0);
			}
			if (this.imgDayDien1 != null)
			{
				g.drawImage(this.imgDayDien1, (float)this.imgDayDien0.getWidth(), (float)(LoadMap.w + ((AvMain.hd != 2) ? 0 : LoadMap.w) + LoadMap.w / 2 - this.imgDayDien0.getHeight()), 0);
			}
			if (this.imgDayDien2 != null)
			{
				g.drawImage(this.imgDayDien2, (float)(this.imgDayDien0.getWidth() + this.imgDayDien1.getWidth()), (float)(LoadMap.w + ((AvMain.hd != 2) ? 0 : LoadMap.w) + LoadMap.w / 2 - this.imgDayDien0.getHeight()), 0);
			}
		}
		else
		{
			this.paintMap(g);
		}
		this.paintTouchMap(g);
		if ((float)Canvas.w > (float)((int)LoadMap.wMap * LoadMap.w) * LoadMap.zoom)
		{
			g.setColor(0);
			g.fillRect((float)((int)AvCamera.gI().xCam), (float)((int)AvCamera.gI().yCam), (float)(-(float)((int)AvCamera.gI().xCam)), (float)Canvas.hCan);
			g.fillRect((float)((int)LoadMap.wMap * LoadMap.w * AvMain.hd) * AvMain.zoom, (float)((int)AvCamera.gI().yCam), (float)(-(float)((int)AvCamera.gI().xCam)), (float)Canvas.hCan);
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00049CDC File Offset: 0x000480DC
	public void paintTouchMap(MyGraphics g)
	{
		if (LoadMap.imgFocus != null && LoadMap.dirFocus != -1 && LoadMap.nPath > 0)
		{
			LoadMap.imgFocus.drawFrame(LoadMap.posFocus.anchor / 2, LoadMap.posFocus.x, LoadMap.posFocus.y, LoadMap.dirFocus, 3, g);
		}
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00049D3C File Offset: 0x0004813C
	public void paintMap(MyGraphics g)
	{
		float num = (AvCamera.gI().xCam + (float)Canvas.w) / (float)LoadMap.w + 1f;
		if (num > (float)LoadMap.wMap)
		{
			num = (float)LoadMap.wMap;
		}
		float num2 = (AvCamera.gI().yCam + (float)Canvas.h) / (float)LoadMap.w + 1f;
		if (num2 > (float)LoadMap.Hmap)
		{
			num2 = (float)LoadMap.Hmap;
		}
		int num3 = (int)(AvCamera.gI().xCam / (float)(LoadMap.w * AvMain.hd));
		if (num3 < 0)
		{
			num3 = 0;
		}
		int num4 = 0;
		while ((float)num4 < num2)
		{
			int num5 = num3;
			while ((float)num5 < num)
			{
				int num6 = (int)LoadMap.map[num4 * (int)LoadMap.wMap + num5];
				if (num6 != -1)
				{
					int idx = num6 / LoadMap.imgMap.nFrame;
					LoadMap.imgMap.drawFrameXY(idx, num6 % LoadMap.imgMap.nFrame, num5 * (LoadMap.w * AvMain.hd), num4 * LoadMap.w * AvMain.hd, 0, g);
				}
				num5++;
			}
			num4++;
		}
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00049E5C File Offset: 0x0004825C
	public void paintObject(MyGraphics g)
	{
		try
		{
			this.p = 0;
			this.o = 0;
			this.d = 0;
			this.temp1.x = 10000;
			this.temp1.y = 10000;
			while (this.p < LoadMap.playerLists.size() || this.o < LoadMap.treeLists.size() || this.d < LoadMap.dynamicLists.size())
			{
				this.player = this.temp1;
				this.obj = this.temp2;
				this.dynamic = this.temp1;
				if (this.p < LoadMap.playerLists.size())
				{
					this.player = (MyObject)LoadMap.playerLists.elementAt(this.p);
				}
				if (this.o < LoadMap.treeLists.size())
				{
					this.obj = (MyObject)LoadMap.treeLists.elementAt(this.o);
				}
				if (this.d < LoadMap.dynamicLists.size())
				{
					this.dynamic = (Point)LoadMap.dynamicLists.elementAt(this.d);
				}
				if (this.player.y < this.obj.y && this.player.y < this.dynamic.y)
				{
					this.player.paint(g);
					this.p++;
				}
				else if (this.obj.y < this.dynamic.y)
				{
					this.obj.paint(g);
					this.o++;
				}
				else
				{
					this.dynamic.paint(g);
					this.d++;
				}
			}
			this.paintFocusPlayer(g);
			if (Bus.isRun)
			{
				LoadMap.bus.paint(g);
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0004A090 File Offset: 0x00048490
	private void paintFocusPlayer(MyGraphics g)
	{
		if (Canvas.stypeInt != 0 || LoadMap.focusObj == null)
		{
			return;
		}
		int num = (int)(((int)LoadMap.focusObj.catagory != 7) ? LoadMap.focusObj.height : 10);
		g.drawImage(MapScr.imgFocusP, (float)(LoadMap.focusObj.x * AvMain.hd), (float)((LoadMap.focusObj.y - num) * AvMain.hd - LoadMap.numF / 2), 3);
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0004A110 File Offset: 0x00048510
	public void paintBackGround(MyGraphics g)
	{
		if (LoadMap.imgBG == null)
		{
			g.setColor(1);
			g.fillRect((float)((int)AvCamera.gI().xCam), (float)((int)AvCamera.gI().yCam), (float)Canvas.w, (float)Canvas.hCan);
			return;
		}
		if (LoadMap.idTileImg != -1)
		{
			g.setColor(LoadMap.colorBackGr);
		}
		else
		{
			g.setColor(LoadMap.colorBg[(int)LoadMap.status]);
		}
		g.fillRect((float)((int)AvCamera.gI().xCam), (float)((int)AvCamera.gI().yCam), (float)Canvas.w, (float)Canvas.h);
		int num = (int)(AvCamera.gI().xCam * 30f / 30f);
		int num2 = (int)((AvCamera.gI().xCam - (float)num) / (float)LoadMap.imgBG.w);
		int num3 = -LoadMap.imgBG.h * AvMain.hd;
		if (Canvas.currentMyScreen == RaceScr.me)
		{
			num3 += 2 * LoadMap.w * AvMain.hd;
		}
		if (LoadMap.idTileImg != -1)
		{
			num3 += AvMain.hd;
		}
		if (this.imgTreeBg != null)
		{
			for (int i = num2; i <= num2 + Canvas.w / this.imgTreeBg.w + 1; i++)
			{
				g.drawImage(this.imgTreeBg, (float)(num + i * (this.imgTreeBg.w - 2) - 1), (float)(-(float)this.imgTreeBg.h - LoadMap.x0_imgTreeBg), 0);
			}
		}
		int num4 = LoadMap.listStar.size();
		if (num4 > 0)
		{
			for (int j = 0; j < num4; j++)
			{
				AvPosition avPosition = (AvPosition)LoadMap.listStar.elementAt(j);
				if ((float)(avPosition.x + num) > AvCamera.gI().xCam && (float)(avPosition.x + num) < AvCamera.gI().yCam + (float)Canvas.w)
				{
					g.setColor(LoadMap.colorStar[avPosition.anchor]);
					g.fillRect((float)(avPosition.x + num), (float)avPosition.y, 1f, 1f);
				}
			}
		}
		if (this.clound != null && LoadMap.imgBG != null)
		{
			int num5 = (int)(AvCamera.gI().xCam * 30f / 35f);
			for (int k = 0; k < this.clound.Length; k++)
			{
				if (this.clound[k].anchor == 1 && (float)(num5 + this.clound[k].x / 100 + this.imgClound[this.clound[k].anchor].w) > AvCamera.gI().xCam && (float)(num5 + this.clound[k].x / 100) < AvCamera.gI().xCam + (float)Canvas.w)
				{
					g.drawImage(this.imgClound[this.clound[k].anchor], (float)(num5 + this.clound[k].x / 100), (float)this.clound[k].y, 0);
				}
			}
			int num6 = (int)(AvCamera.gI().xCam * 30f / 40f);
			for (int l = 0; l < this.clound.Length; l++)
			{
				if (this.clound[l].anchor == 0 && (float)(num6 + this.clound[l].x / 100 + this.imgClound[this.clound[l].anchor].w) > AvCamera.gI().xCam && (float)(num6 + this.clound[l].x / 100) < AvCamera.gI().xCam + (float)Canvas.w)
				{
					g.drawImage(this.imgClound[this.clound[l].anchor], (float)(num6 + this.clound[l].x / 100), (float)this.clound[l].y, 0);
				}
			}
		}
		int num7 = (int)(AvCamera.gI().xCam * 30f / 50f);
		int num8 = (int)((AvCamera.gI().xCam - (float)num7) / (float)LoadMap.imgBG.w);
		if (LoadMap.imgBG != null)
		{
			for (int m = num8; m <= num8 + Canvas.w / LoadMap.imgBG.w + 1; m++)
			{
				g.drawImage(LoadMap.imgBG, (float)(num7 + m * (LoadMap.imgBG.w - 2) - 1), (float)(-(float)LoadMap.imgBG.h + LoadMap.x0_imgBG), 0);
			}
		}
		if (Canvas.currentEffect.size() > 0)
		{
			for (int n = 0; n < Canvas.currentEffect.size(); n++)
			{
				Effect effect = (Effect)Canvas.currentEffect.elementAt(n);
				effect.paintBack(g);
			}
		}
		if (LoadMap.effBgList != null)
		{
			for (int num9 = 0; num9 < LoadMap.effBgList.size(); num9++)
			{
				EffectObj effectObj = (EffectObj)LoadMap.effBgList.elementAt(num9);
				effectObj.paint(g);
			}
		}
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0004A660 File Offset: 0x00048A60
	public static void loadMapImage(int index)
	{
		Out.println("loadMapImage: " + index);
		if (LoadMap.rememMap == (int)LoadMap.status && LoadMap.imgMap != null)
		{
			return;
		}
		LoadMap.rememMap = (int)LoadMap.status;
		if (index - 1 == 19)
		{
			LoadMap.rememMap = -1;
			LoadMap.imgMap = new FrameImage(Image.createImage(T.getPath() + "/wedding"), LoadMap.w * AvMain.hd, LoadMap.w * AvMain.hd);
		}
		else if (index - 1 != 107)
		{
			DataInputStream resourceAsStream = DataInputStream.getResourceAsStream(T.getPath() + "/data/h" + LoadMap.status);
			DataInputStream resourceAsStream2 = DataInputStream.getResourceAsStream(T.getPath() + "/data/data");
			try
			{
				sbyte[] header = new sbyte[resourceAsStream.available()];
				resourceAsStream.read(ref header);
				sbyte[] data = new sbyte[resourceAsStream2.available()];
				resourceAsStream2.read(ref data);
				LoadMap.imgMap = new FrameImage(CRes.createImgByHeader(header, data), LoadMap.w * AvMain.hd, LoadMap.w * AvMain.hd);
			}
			catch (Exception e)
			{
				Out.logError(e);
			}
		}
		else
		{
			try
			{
				LoadMap.w = 12;
				LoadMap.rememMap = -1;
				if (index - 1 == 107)
				{
					LoadMap.x0_imgBG = 30 * AvMain.hd;
					LoadMap.x0_imgTreeBg = -30 * AvMain.hd;
				}
			}
			catch (Exception e2)
			{
				Out.logError(e2);
			}
		}
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0004A7F8 File Offset: 0x00048BF8
	public static void setStar()
	{
		LoadMap.listStar.removeAllElements();
		if ((int)LoadMap.status == 0 || LoadMap.star == 0 || (int)LoadMap.weather != -1)
		{
			return;
		}
		if (LoadMap.TYPEMAP == 9 || LoadMap.TYPEMAP == 12)
		{
			int num = CRes.rnd(Canvas.w / 10);
			for (int i = 0; i < num; i++)
			{
				LoadMap.listStar.addElement(new AvPosition(CRes.rnd((int)LoadMap.wMap * LoadMap.w), -(98 + CRes.rnd(Canvas.hh)), CRes.rnd(4)));
			}
		}
		else
		{
			int num2 = CRes.rnd(Canvas.w / 10);
			for (int j = 0; j < num2; j++)
			{
				LoadMap.listStar.addElement(new AvPosition(CRes.rnd((int)LoadMap.wMap * LoadMap.w), -(38 + CRes.rnd(Canvas.hh)), CRes.rnd(4)));
			}
		}
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0004A8F4 File Offset: 0x00048CF4
	public static DataInputStream loadDataMap(int index)
	{
		try
		{
			return DataInputStream.getResourceAsStream("map/" + index);
		}
		catch (Exception ex)
		{
			Out.println("ERROR LOAD DATA MAP");
		}
		return null;
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0004A940 File Offset: 0x00048D40
	private void loadImageMapFull(int index)
	{
		LoadMap.imgCreateMap = null;
		Image[] array = new Image[10];
		int num = 0;
		for (int i = 0; i < 10; i++)
		{
			array[i] = Image.createImagePNG(string.Concat(new object[]
			{
				T.getPath(),
				"/imageMap/",
				index,
				"/",
				i
			}));
			if (array[i] == null)
			{
				num = i;
				break;
			}
		}
		if (num > 0)
		{
			LoadMap.imgCreateMap = new Image[num];
			for (int j = 0; j < num; j++)
			{
				LoadMap.imgCreateMap[j] = array[j];
			}
		}
		this.imgDayDien0 = Image.createImagePNG(string.Concat(new object[]
		{
			T.getPath(),
			"/imageMap/",
			index,
			"/daydien0"
		}));
		this.imgDayDien1 = Image.createImagePNG(string.Concat(new object[]
		{
			T.getPath(),
			"/imageMap/",
			index,
			"/daydien1"
		}));
		this.imgDayDien2 = Image.createImagePNG(string.Concat(new object[]
		{
			T.getPath(),
			"/imageMap/",
			index,
			"/daydien2"
		}));
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0004AA94 File Offset: 0x00048E94
	public void load(int index, bool isCreate)
	{
		if (index - 1 == 14 || index - 1 == 15 || index - 1 == 16)
		{
			AvMain.zoom = 1f;
		}
		if (Session_ME.gI().isConnected())
		{
			Canvas.load = 0;
			Canvas.endDlg();
		}
		ipKeyboard.isReset = true;
		Canvas.rh = 0;
		this.hBG = 214 * AvMain.hd;
		LoadMap.nPath = 0;
		LoadMap.idTileImg = -1;
		LoadMap.isCasino = false;
		LoadMap.cmdNext.caption = T.next;
		Canvas.currentEffect.removeAllElements();
		GameMidlet.avatar.ableShow = false;
		Bus.isRun = false;
		AvCamera.disable = false;
		GameMidlet.avatar.setAction(0);
		LoadMap.resetObject();
		MapScr.listFish.removeAllElements();
		LoadMap.focusObj = null;
		MapScr.focusP = null;
		int hour = DateTime.Now.Hour;
		if (hour >= 18 || hour < 6)
		{
			LoadMap.status = 1;
		}
		else
		{
			LoadMap.status = 0;
		}
		this.loadBG(index - 1);
		this.loadImageMapFull(index);
		if (LoadMap.imgCreateMap == null || index - 1 == 107)
		{
			LoadMap.loadMapImage(index);
		}
		else
		{
			LoadMap.imgMap = null;
		}
		DataInputStream dataInputStream = LoadMap.loadDataMap(index);
		MyScreen.colorBar = MyScreen.colorMiniMap;
		if (dataInputStream != null)
		{
			LoadMap.Hmap = 8;
			int num = index - 1;
			switch (num)
			{
			case 9:
				MyScreen.colorBar = MyScreen.colorCity[(int)LoadMap.status];
				LoadMap.Hmap = 8;
				goto IL_2D0;
			case 10:
				LoadMap.Hmap = 9;
				goto IL_2D0;
			case 11:
			case 13:
				MyScreen.colorBar = MyScreen.colorCity[(int)LoadMap.status];
				goto IL_2D0;
			default:
				switch (num)
				{
				case 57:
				case 58:
				case 59:
				case 62:
				case 63:
				case 64:
					break;
				case 60:
				case 61:
				case 65:
					LoadMap.Hmap = 5;
					goto IL_2D0;
				default:
					switch (num)
					{
					case 100:
					case 101:
					case 103:
					case 104:
					case 108:
					case 109:
						break;
					case 102:
					case 105:
					case 106:
						goto IL_2D0;
					case 107:
						LoadMap.Hmap = 16;
						goto IL_2D0;
					default:
						goto IL_2D0;
					}
					break;
				}
				break;
			case 17:
				MyScreen.colorBar = MyScreen.colorFarmPath[(int)LoadMap.status];
				LoadMap.Hmap = 6;
				goto IL_2D0;
			case 18:
				LoadMap.Hmap = 10;
				goto IL_2D0;
			case 19:
				LoadMap.Hmap = 13;
				goto IL_2D0;
			case 20:
				break;
			case 21:
				MyScreen.colorBar = MyScreen.colorCity[(int)LoadMap.status];
				LoadMap.Hmap = 7;
				goto IL_2D0;
			case 24:
				goto IL_2D0;
			case 25:
				MyScreen.colorBar = MyScreen.colorFarmPath[(int)LoadMap.status];
				LoadMap.Hmap = 7;
				goto IL_2D0;
			}
			LoadMap.Hmap = 11;
		}
		IL_2D0:
		this.setMap(dataInputStream, index, true);
		LoadMap.TYPEMAP = index - 1;
		if ((int)LoadMap.weather != -1 && LoadMap.TYPEMAP < LoadMap.bg.Length && (int)LoadMap.bg[LoadMap.TYPEMAP] != -1)
		{
			AnimateEffect animateEffect = new AnimateEffect(LoadMap.weather, false, 0);
			animateEffect.show();
		}
		this.setClound();
		if (Session_ME.gI().isConnected() && GameMidlet.avatar.seriPart != null)
		{
			LoadMap.addPlayer(GameMidlet.avatar);
		}
		if (Canvas.load == 0)
		{
			Canvas.load = 1;
		}
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0004AE04 File Offset: 0x00049204
	public void setMap(DataInputStream ip, int index, bool newType)
	{
		sbyte b = 0;
		sbyte b2 = 0;
		sbyte b3 = 0;
		sbyte b4 = 0;
		sbyte b5 = 0;
		sbyte b6 = 0;
		sbyte b7 = 0;
		sbyte b8 = 0;
		sbyte b9 = 0;
		sbyte b10 = 0;
		sbyte b11 = 0;
		sbyte b12 = 0;
		sbyte b13 = 0;
		sbyte b14 = 0;
		sbyte b15 = 0;
		sbyte b16 = 0;
		sbyte b17 = 0;
		sbyte b18 = 0;
		sbyte b19 = 0;
		sbyte b20 = 0;
		sbyte b21 = 0;
		sbyte b22 = 0;
		sbyte b23 = 0;
		sbyte b24 = 0;
		sbyte b25 = 0;
		sbyte b26 = 0;
		sbyte b27 = 0;
		sbyte b28 = 0;
		sbyte b29 = 0;
		sbyte b30 = 0;
		sbyte b31 = 0;
		sbyte b32 = 0;
		sbyte b33 = 0;
		sbyte b34 = 0;
		sbyte b35 = 0;
		sbyte b36 = 0;
		sbyte b37 = 0;
		sbyte b38 = 0;
		sbyte b39 = 0;
		sbyte b40 = 0;
		int num = 0;
		sbyte[] array = new sbyte[13];
		try
		{
			if (ip != null)
			{
				LoadMap.wMap = (short)(ip.available() / (int)LoadMap.Hmap);
				LoadMap.map = new short[(int)(LoadMap.Hmap * LoadMap.wMap)];
			}
			if (newType)
			{
				LoadMap.type = new sbyte[(int)(LoadMap.Hmap * LoadMap.wMap)];
			}
			for (int i = 0; i < (int)(LoadMap.Hmap * LoadMap.wMap); i++)
			{
				if (ip != null)
				{
					LoadMap.map[i] = (short)((byte)ip.readByte());
					if (LoadMap.map[i] == 255)
					{
						LoadMap.map[i] = -1;
					}
				}
			}
			if (index - 1 == 19)
			{
				MapScr.listChair = new MyVector();
				for (int j = 0; j < LoadMap.map.Length; j++)
				{
					if (LoadMap.map[j] < 32)
					{
						LoadMap.type[j] = 80;
					}
					else
					{
						LoadMap.type[j] = 88;
					}
					if (LoadMap.map[j] == 65)
					{
						LoadMap.type[j] = 10;
						LoadMap.map[j] = 1;
						if ((int)b40 == 1)
						{
							LoadMap.map[j] = 16;
							GameMidlet.avatar.x = (GameMidlet.avatar.xCur = LoadMap.x(j) + LoadMap.w);
							GameMidlet.avatar.y = (GameMidlet.avatar.yCur = LoadMap.y(j) + 12);
							LoadMap.addPopup(T.joinA, LoadMap.x(j) + LoadMap.w / 2, LoadMap.y(j) + 12);
						}
						b40 = (sbyte)((int)b40 + 1);
					}
					else if (LoadMap.map[j] == 27)
					{
						AvPosition avPosition = new AvPosition();
						avPosition.x = LoadMap.x(j);
						avPosition.y = LoadMap.y(j);
						avPosition.index = (short)((5 - MapScr.listChair.size() % 6) * 2 + MapScr.listChair.size() / 6);
						MapScr.listChair.addElement(avPosition);
					}
				}
				Avatar avatar = new Avatar();
				avatar.x = (avatar.xCur = 26 * LoadMap.w + LoadMap.w / 2);
				avatar.y = (avatar.yCur = 8 * LoadMap.w + LoadMap.w / 2);
				avatar.name = "chu hon";
				avatar.IDDB = -100;
				avatar.addSeri(new SeriPart(2480));
				LoadMap.playerLists.addElement(avatar);
			}
			else if (index - 1 == 107)
			{
				for (int k = 0; k < (int)(LoadMap.Hmap * LoadMap.wMap); k++)
				{
					if (LoadMap.map[k] == 61 && CRes.rnd(2) == 1)
					{
						Avatar avatar2 = new Avatar();
						Avatar avatar3 = (Avatar)RaceScr.gI().listPlayer.elementAt(CRes.rnd(RaceScr.gI().listPlayer.size()));
						avatar2.seriPart = avatar3.seriPart;
						avatar2.x = (avatar2.xCur = LoadMap.x(k) + 12);
						avatar2.y = (avatar2.yCur = LoadMap.y(k) + 12);
						avatar2.action = 2;
						avatar2.catagory = 11;
						LoadMap.playerLists.addElement(avatar2);
					}
					if (LoadMap.map[k] != 59)
					{
						if (LoadMap.map[k] == 60)
						{
						}
					}
				}
			}
			else
			{
				for (int l = 0; l < (int)(LoadMap.Hmap * LoadMap.wMap); l++)
				{
					if (LoadMap.map[l] == -4)
					{
						LoadMap.type[l] = 80;
					}
					else if (LoadMap.map[l] == -5)
					{
						LoadMap.type[l] = 88;
					}
					else if (LoadMap.map[l] != -3 && LoadMap.map[l] != -6)
					{
						if (LoadMap.map[l] >= 120 && LoadMap.map[l] <= 123)
						{
							LoadMap.type[l] = 80;
						}
						else if (LoadMap.map[l] >= 114 && LoadMap.map[l] <= 119)
						{
							LoadMap.type[l] = 80;
						}
						else if (LoadMap.map[l] == 67 || LoadMap.map[l] == 85)
						{
							LoadMap.type[l] = 92;
						}
						else if (LoadMap.map[l] >= 20 && LoadMap.map[l] <= 23)
						{
							LoadMap.type[l] = 79;
						}
						else if (LoadMap.map[l] < 7)
						{
							LoadMap.type[l] = 80;
						}
						else
						{
							LoadMap.type[l] = 88;
						}
						if (LoadMap.map[l] >= 44 && LoadMap.map[l] <= 55)
						{
							LoadMap.type[l] = 80;
						}
						if (index - 1 != 103 && index - 1 != 100 && index - 1 != 101 && index - 1 != 104 && LoadMap.map[l] == 62 && index - 1 != 62)
						{
							LoadMap.type[l] = 56;
						}
						if (LoadMap.map[l] == 111 || LoadMap.map[l] == 112)
						{
							LoadMap.type[l] = 80;
						}
					}
					if (ip != null || GameMidlet.CLIENT_TYPE != 11)
					{
						short num2 = LoadMap.map[l];
						switch (num2)
						{
						case 127:
							if ((int)b32 == 0)
							{
								if (index - 1 != 9)
								{
									LoadMap.addObjTree(830, LoadMap.x(l) + 36, LoadMap.y(l) + LoadMap.w - 2, 108);
								}
								int i2 = l;
								sbyte b41 = b32;
								b32 = (sbyte)((int)b41 + 1);
								this.setPopup(i2, b41, 2);
							}
							this.setTypeMap(l, 108, 96);
							break;
						case 128:
							if (index - 1 != 25)
							{
								int i3 = l;
								sbyte b42 = b;
								b = (sbyte)((int)b42 + 1);
								this.setPopup(i3, b42, 2);
								this.setTypeMap(l, 55, 20);
								LoadMap.map[l] = LoadMap.map[l + (int)LoadMap.wMap];
							}
							break;
						case 129:
						case 160:
							if ((int)b2 == 0)
							{
								if (index - 1 == 17)
								{
									LoadMap.addObjTree(836, l, (LoadMap.map[l] != 129) ? 62 : 57);
								}
								else if (index - 1 != 23)
								{
									LoadMap.addObjTree(829, l, (LoadMap.map[l] != 129) ? 62 : 57);
								}
								int i4 = l;
								sbyte b43 = b2;
								b2 = (sbyte)((int)b43 + 1);
								this.setPopup(i4, b43, 2);
							}
							this.setTypeMap(l, (LoadMap.map[l] != 129) ? 62 : 57, 96);
							break;
						case 130:
						case 131:
						case 132:
						case 133:
						case 134:
						case 135:
						case 136:
						case 137:
						case 138:
						{
							int num3 = (int)(LoadMap.map[l] - 130);
							int i5 = l;
							sbyte[] array2 = array;
							int num4 = num3;
							sbyte b44;
							array2[num4] = (sbyte)((int)(b44 = array2[num4]) + 1);
							this.setPopup(i5, b44, 0);
							this.setTypePark(l, (sbyte)num3);
							break;
						}
						case 139:
						{
							LoadMap.type[l] = -1;
							int i6 = l;
							sbyte b45 = b27;
							b27 = (sbyte)((int)b45 + 1);
							this.setPopup(i6, b45, 0);
							if (LoadMap.TYPEMAP == -1 && index != 21 && LoadMap.imgBG != null)
							{
								Bus.posBusStop = new AvPosition(LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) - LoadMap.w / 2);
								LoadMap.bus.setBus(1);
							}
							this.setMapPaint(l, LoadMap.map);
							break;
						}
						case 140:
						{
							int i7 = l;
							sbyte b46 = b25;
							b25 = (sbyte)((int)b46 + 1);
							this.setPopup(i7, b46, 0);
							this.setTypeMap(l, 25, 55);
							b25 = (sbyte)((int)b25 + 1);
							break;
						}
						case 141:
							if ((int)b4 == 0)
							{
								int i8 = l;
								sbyte b47 = b4;
								b4 = (sbyte)((int)b47 + 1);
								this.setPopup(i8, b47, 0);
							}
							this.setTypeMap(l, 24, 5);
							LoadMap.map[l] = LoadMap.map[l + (int)LoadMap.wMap];
							break;
						case 142:
							this.setTypeMap(l, 80, 4);
							FarmScr.gI().posTree[(int)b14] = new AvPosition(LoadMap.x(l) / LoadMap.w, LoadMap.y(l) / LoadMap.w, 0);
							b14 = (sbyte)((int)b14 + 1);
							break;
						case 143:
							if ((int)b3 == 0)
							{
								int i9 = l;
								sbyte b48 = b3;
								b3 = (sbyte)((int)b48 + 1);
								this.setPopup(i9, b48, 2);
							}
							this.setTypeMap(l, 52, 51);
							LoadMap.map[l] = LoadMap.map[l + (int)LoadMap.wMap];
							break;
						case 144:
							if ((int)b5 == 0)
							{
								int i10 = l;
								sbyte b49 = b5;
								b5 = (sbyte)((int)b49 + 1);
								this.setPopup(i10, b49, 2);
							}
							this.setTypeMap(l, 53, 5);
							break;
						case 145:
						{
							int i11 = l;
							sbyte b50 = b26;
							b26 = (sbyte)((int)b50 + 1);
							this.setPopup(i11, b50, 0);
							if (index - 1 == 109 || (index - 1 == 57 && LoadMap.TYPEMAP == 17))
							{
								this.setTypeMap(l, 17, -1);
							}
							else if (LoadMap.TYPEMAP == 23)
							{
								this.setTypeMap(l, 23, -1);
							}
							else
							{
								this.setTypeMap(l, 9, -1);
								if (index - 1 == 100)
								{
									LoadMap.map[l] = 47;
								}
							}
							if (index - 1 == 100)
							{
								LoadMap.map[l] = 47;
							}
							break;
						}
						default:
							switch (num2)
							{
							case 24:
							case 25:
							case 26:
								if (newType && (index - 1 != 9 || (index - 1 == 9 && l / (int)LoadMap.wMap > (int)(LoadMap.wMap / 2))))
								{
									LoadMap.addObjTree(845, LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w, -1);
								}
								break;
							case 27:
								if (newType)
								{
									LoadMap.addObjTree(844, LoadMap.x(l) + 11, LoadMap.y(l) + 1, -1);
								}
								break;
							case 28:
								if (newType && !Session_ME.gI().isConnected())
								{
									LoadMap.map[l] = 4;
								}
								break;
							default:
								switch (num2)
								{
								case 63:
								case 65:
									if (index - 1 != 103 && index - 1 != 100 && index - 1 != 101 && index - 1 != 104)
									{
										LoadMap.type[l] = 27;
										int i12 = l;
										sbyte b51 = b24;
										b24 = (sbyte)((int)b51 + 1);
										this.setPopup(i12, b51, 0);
										if (index - 1 == 57 || index - 1 == 62)
										{
											LoadMap.addPopup(T.joinA, LoadMap.x(l) - 12, LoadMap.y(l) + 12);
										}
										else if (index - 1 == 58 || index - 1 == 63)
										{
											LoadMap.addPopup(T.joinA, LoadMap.x(l) + 12, LoadMap.y(l) + 36);
										}
										else
										{
											LoadMap.addPopup(T.joinA, LoadMap.x(l) - 12, LoadMap.y(l) + 12);
										}
									}
									break;
								default:
									switch (num2)
									{
									case 97:
										LoadMap.type[l] = 54;
										break;
									case 98:
										LoadMap.type[l] = 29;
										LoadMap.addObjTree(846, l, 29);
										if (index - 1 == 108 || index - 1 == 109)
										{
											LoadMap.map[l] = 56;
										}
										break;
									default:
										if (num2 != -1)
										{
											if (num2 != 110)
											{
												b35 = 0;
												b27 = 0;
											}
											else
											{
												FarmScr.posName = new AvPosition(LoadMap.x(l) - LoadMap.w + 8, LoadMap.y(l) - 2);
												LoadMap.addObjTree(847, LoadMap.x(l) + 11, LoadMap.y(l), -1);
											}
										}
										else
										{
											LoadMap.type[l] = 88;
										}
										break;
									case 102:
									{
										LoadMap.type[l] = 92;
										BoardScr.listPosAvatar.addElement(new AvPosition(LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w));
										int num5 = 0;
										int x = (int)LoadMap.wMap * LoadMap.w;
										if ((int)LoadMap.wMap * LoadMap.w < Canvas.w)
										{
											num5 = -(Canvas.w - (int)LoadMap.wMap * LoadMap.w) / 2;
											x = (int)LoadMap.wMap * LoadMap.w - num5;
										}
										AvPosition avPosition2 = new AvPosition(num5, LoadMap.y(l) + LoadMap.w);
										if (index == 66)
										{
											if ((int)b29 == 2 || (int)b29 == 4)
											{
												avPosition2.x = x;
											}
										}
										else if (index == 62)
										{
											if ((int)b29 == 1 || (int)b29 == 3)
											{
												avPosition2.x = x;
											}
										}
										else if ((int)b29 == 1)
										{
											avPosition2.x = x;
										}
										BoardScr.listPosCasino.addElement(avPosition2);
										b29 = (sbyte)((int)b29 + 1);
										break;
									}
									}
									break;
								case 68:
									if (index - 1 == 108 || index - 1 == 109 || LoadMap.isCasino)
									{
										LoadMap.type[l] = (sbyte)(72 + (int)b36);
										b36 = (sbyte)((int)b36 + 1);
									}
									break;
								case 69:
									if (index - 1 == 108 || index - 1 == 109 || LoadMap.isCasino)
									{
										LoadMap.type[l] = (sbyte)(72 + (int)b28);
										b28 = (sbyte)((int)b28 + 1);
									}
									break;
								}
								break;
							}
							break;
						case 147:
						case 161:
							if ((int)b6 == 0)
							{
								if (index - 1 != 23)
								{
									LoadMap.addObjTree(832, l, (LoadMap.map[l] != 147) ? 63 : 58);
								}
								int i13 = l;
								sbyte b52 = b6;
								b6 = (sbyte)((int)b52 + 1);
								this.setPopup(i13, b52, 2);
							}
							this.setTypeMap(l, (LoadMap.map[l] != 147) ? 63 : 58, 96);
							break;
						case 148:
						case 162:
							if ((int)b7 == 0)
							{
								if (index - 1 != 23)
								{
									LoadMap.addObjTree(833, LoadMap.x(l) + 48, LoadMap.y(l) + LoadMap.w - 2, (LoadMap.map[l] != 148) ? 64 : 59);
								}
								int i14 = l;
								sbyte b53 = b7;
								b7 = (sbyte)((int)b53 + 1);
								this.setPopup(i14, b53, 2);
							}
							LoadMap.map[l] = 0;
							this.setTypeMap(l, (LoadMap.map[l] != 148) ? 64 : 59, 96);
							break;
						case 149:
							if ((int)b8 == 0)
							{
								if (GameMidlet.avatar.IDDB == FarmScr.idFarm)
								{
									this.setPopup(l, b8, 2);
								}
								b8 = (sbyte)((int)b8 + 1);
							}
							this.setTypeMap(l, 28, 4);
							break;
						case 150:
							if ((int)b31 == 0)
							{
								LoadMap.addObjTree(842, l, 93);
							}
							if (index == 26)
							{
								this.setTypeMap(l, 93, 4);
							}
							else
							{
								this.setTypeMap(l, 93, 0);
							}
							b31 = (sbyte)((int)b31 + 1);
							break;
						case 151:
							if ((int)b33 == 0)
							{
								LoadMap.addObjTree(843, l, 78);
							}
							this.setTypeMap(l, 78, 0);
							b33 = (sbyte)((int)b33 + 1);
							break;
						case 152:
							if ((int)b9 == 0)
							{
								LoadMap.addObjTree(835, l, 81);
							}
							this.setTypeMap(l, 81, (index - 1 != 25) ? 0 : 55);
							b9 = (sbyte)((int)b9 + 1);
							break;
						case 153:
							if ((int)b30 == 0)
							{
								int i15 = l;
								sbyte b54 = b30;
								b30 = (sbyte)((int)b54 + 1);
								this.setPopup(i15, b54, 0);
							}
							this.setTypePark(l, 11);
							break;
						case 155:
							this.setTypeMap(l, 80, 55);
							if ((int)Cattle.numPig > 0)
							{
								this.setTypeMap(l, 84, 112);
								LoadMap.addObjTree(-5, LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2, 84);
								Cattle.posPigTr = new AvPosition(LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2);
							}
							break;
						case 156:
							this.setTypeMap(l, 80, 5);
							if ((int)Dog.numBer > 0)
							{
								this.setTypeMap(l, 85, 5);
								LoadMap.addObjTree(-6, LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2, 85);
								Dog.posDosTr = new AvPosition(LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2);
							}
							break;
						case 157:
							this.setTypeMap(l, 80, 111);
							Cattle.posBucket = new AvPosition(LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2);
							break;
						case 158:
							this.setTypeMap(l, 80, 5);
							if (Chicken.numChicken > 0)
							{
								Chicken.posNest = new AvPosition(LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2);
							}
							break;
						case 159:
						{
							int m = 4;
							if (index - 1 == 25)
							{
								m = 4;
							}
							else if (index - 1 == 108 || index - 1 == 109)
							{
								m = 47;
							}
							else if (index - 1 == 13)
							{
								m = 0;
							}
							this.setTypeMap(l, 89, m);
							if ((int)b11 == 0)
							{
								LoadMap.addObjTree(848, LoadMap.x(l) + 12, LoadMap.y(l) + 20, 89);
							}
							b11 = (sbyte)((int)b11 + 1);
							break;
						}
						case 163:
						{
							int i16 = l;
							sbyte b55 = b26;
							b26 = (sbyte)((int)b55 + 1);
							this.setPopup(i16, b55, 0);
							this.setTypeMap(l, 12, -1);
							break;
						}
						case 164:
						{
							this.setPopup(l, array[9], 0);
							sbyte[] array3 = array;
							int num6 = 9;
							array3[num6] = (sbyte)((int)array3[num6] + 1);
							this.setTypeMap(l, 13, 6);
							break;
						}
						case 165:
						{
							this.setPopup(l, array[10], 0);
							this.setTypeMap(l, 14, 0);
							sbyte[] array4 = array;
							int num7 = 10;
							array4[num7] = (sbyte)((int)array4[num7] + 1);
							break;
						}
						case 166:
						{
							this.setPopup(l, array[11], 0);
							this.setTypeMap(l, 15, 0);
							sbyte[] array5 = array;
							int num8 = 11;
							array5[num8] = (sbyte)((int)array5[num8] + 1);
							break;
						}
						case 167:
						{
							this.setPopup(l, array[12], 0);
							sbyte[] array6 = array;
							int num9 = 12;
							array6[num9] = (sbyte)((int)array6[num9] + 1);
							this.setTypeMap(l, 16, 43);
							break;
						}
						case 172:
							this.setTypeMap(l, 88, 96);
							break;
						case 173:
							this.setTypeMap(l, 88, 96);
							break;
						case 174:
							this.setTypeMap(l, 88, 96);
							break;
						case 175:
						{
							int i17 = l;
							sbyte b56 = b15;
							b15 = (sbyte)((int)b56 + 1);
							this.setPopup(i17, b56, 0);
							this.setTypeMap(l, 68, 96);
							break;
						}
						case 176:
						{
							int i18 = l;
							sbyte b57 = b16;
							b16 = (sbyte)((int)b57 + 1);
							this.setPopup(i18, b57, 0);
							this.setTypeMap(l, 69, 96);
							break;
						}
						case 177:
						{
							int i19 = l;
							sbyte b58 = b17;
							b17 = (sbyte)((int)b58 + 1);
							this.setPopup(i19, b58, 0);
							this.setTypeMap(l, 70, 96);
							break;
						}
						case 178:
							if ((int)b32 == 0)
							{
								LoadMap.addObjTree(830, LoadMap.x(l) + LoadMap.w, LoadMap.y(l) + LoadMap.w - 2, 109);
								int i20 = l;
								sbyte b59 = b32;
								b32 = (sbyte)((int)b59 + 1);
								this.setPopup(i20, b59, 2);
							}
							this.setTypeMap(l, 109, 96);
							break;
						case 179:
							if ((int)b17 == 0)
							{
								int i21 = l;
								sbyte b60 = b17;
								b17 = (sbyte)((int)b60 + 1);
								this.setPopup(i21, b60, 2);
								LoadMap.addObjTree(837, l, 18);
							}
							this.setTypeMap(l, 18, 96);
							break;
						case 180:
						{
							int i22 = l;
							sbyte b61 = b17;
							b17 = (sbyte)((int)b61 + 1);
							this.setPopup(i22, b61, 0);
							this.setTypeMap(l, 17, 77);
							if (index - 1 == 101)
							{
								LoadMap.map[l] = 0;
							}
							break;
						}
						case 181:
							if (index - 1 != 103 && index - 1 != 100 && index - 1 != 101 && index - 1 != 104)
							{
								if ((int)b34 == 0)
								{
									LoadMap.addPopup(T.joinA, LoadMap.x(l) + LoadMap.w / 2, LoadMap.y(l) + LoadMap.w / 2);
								}
								b34 = (sbyte)((int)b34 + 1);
								this.setTypeMap(l, 56, 46);
							}
							break;
						case 182:
							FarmScr.posBarn = new AvPosition(LoadMap.x(l), LoadMap.y(l));
							this.setTypeMap(l, 80, 39);
							break;
						case 183:
							FarmScr.posPond = new AvPosition(LoadMap.x(l) + 24, LoadMap.y(l) + 24);
							this.setTypeMap(l, 88, 13);
							break;
						case 184:
							LoadMap.type[l] = (sbyte)(72 + (int)b28);
							break;
						case 185:
							if ((int)b35 == 1 && index == 18)
							{
								LoadMap.addObjTree(975, LoadMap.x(l) + 24, LoadMap.y(l) + 24, 71);
							}
							if (index == 18)
							{
								this.setTypeMap(l, 71, 43);
								if ((int)b35 == 2)
								{
									LoadMap.addPopup(T.joinA, LoadMap.x(l), LoadMap.y(l) + 25);
								}
							}
							else
							{
								int i23 = l;
								sbyte b62 = b35;
								b35 = (sbyte)((int)b62 + 1);
								this.setPopup(i23, b62, 0);
								this.setTypeMap(l, 71, 47);
							}
							b35 = (sbyte)((int)b35 + 1);
							break;
						case 186:
							b12 = (sbyte)((int)b12 + 1);
							if ((int)b12 == 3)
							{
								LoadMap.addPopup(T.joinA, LoadMap.x(l), LoadMap.y(l) + 24);
							}
							this.setTypeMap(l, 94, 17);
							break;
						case 187:
							if ((int)b37 == 0 && FarmScr.idFarm == GameMidlet.avatar.IDDB)
							{
								LoadMap.treeLists.addElement(new SubObject(-10, LoadMap.x(l) + 20, LoadMap.y(l) + 20, FarmScr.imgBuyLant.getWidth(), FarmScr.imgBuyLant.getHeight()));
							}
							b37 = (sbyte)((int)b37 + 1);
							this.setTypeMap(l, (FarmScr.idFarm != GameMidlet.avatar.IDDB) ? 80 : 95, 4);
							break;
						case 188:
							if (FarmScr.idFarm == GameMidlet.avatar.IDDB)
							{
								LoadMap.treeLists.addElement(new SubObject(-10, LoadMap.x(l) + 20, LoadMap.y(l) + 20, FarmScr.imgBuyLant.getWidth(), FarmScr.imgBuyLant.getWidth()));
							}
							this.setTypeMap(l, (FarmScr.idFarm != GameMidlet.avatar.IDDB) ? 80 : 96, 4);
							break;
						case 189:
							FarmScr.starFruil.x = LoadMap.x(l) + 12;
							FarmScr.starFruil.y = LoadMap.y(l) + 12;
							LoadMap.type[l] = 97;
							if (GameMidlet.avatar.IDDB == FarmScr.idFarm)
							{
								FarmScr.starFruil.index = 97;
							}
							LoadMap.map[l] = 4;
							LoadMap.treeLists.addElement(FarmScr.starFruil);
							if (GameMidlet.avatar.IDDB != FarmScr.idFarm && FarmScr.isSteal && FarmScr.starFruil.numberFruit > 0)
							{
								LoadMap.addPopup("trom", LoadMap.x(l) + 12, LoadMap.y(l) + 24, -2);
							}
							break;
						case 190:
							LoadMap.type[l] = 98;
							LoadMap.map[l] = 4;
							if ((int)b38 == 0)
							{
								LoadMap.addObjTree(1029, LoadMap.x(l) + 36, LoadMap.y(l) + 20, 98);
								FarmScr.xPosCook = LoadMap.x(l) + 26;
								FarmScr.yPosCook = LoadMap.y(l) + 10;
								if (FarmScr.idFarm == GameMidlet.avatar.IDDB)
								{
									LoadMap.addPopup(T.joinA, LoadMap.x(l) + 36, LoadMap.y(l) + 24);
								}
							}
							if (FarmScr.idFarm != GameMidlet.avatar.IDDB)
							{
								LoadMap.type[l] = 88;
							}
							b38 = (sbyte)((int)b38 + 1);
							break;
						case 191:
							LoadMap.type[l] = 23;
							if (index - 1 == 104)
							{
								LoadMap.map[l] = 0;
								if ((int)b18 == 1)
								{
									LoadMap.addPopup(T.joinA, LoadMap.x(l) + 12, LoadMap.y(l) + 12);
								}
							}
							else
							{
								if ((int)b18 % 2 == 0)
								{
									LoadMap.map[l] = 46;
								}
								else
								{
									LoadMap.map[l] = 44;
								}
								if ((int)b18 == 1)
								{
									LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 12);
								}
							}
							b18 = (sbyte)((int)b18 + 1);
							break;
						case 192:
							LoadMap.type[l] = 99;
							LoadMap.map[l] = 4;
							b22 = (sbyte)((int)b22 + 1);
							break;
						case 193:
							LoadMap.type[l] = 100;
							LoadMap.map[l] = 4;
							if ((int)b21 == 1)
							{
								LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 30);
							}
							b21 = (sbyte)((int)b21 + 1);
							break;
						case 194:
							LoadMap.type[l] = 106;
							LoadMap.map[l] = 4;
							break;
						case 195:
							LoadMap.type[l] = 102;
							LoadMap.map[l] = 4;
							break;
						case 196:
							LoadMap.type[l] = 103;
							LoadMap.map[l] = 4;
							if ((int)b19 == 1)
							{
								LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 30);
							}
							b19 = (sbyte)((int)b19 + 1);
							break;
						case 197:
							LoadMap.type[l] = 104;
							LoadMap.map[l] = 4;
							if ((int)b20 == 1)
							{
								LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 30);
							}
							b20 = (sbyte)((int)b20 + 1);
							break;
						case 198:
							LoadMap.type[l] = 105;
							LoadMap.map[l] = 4;
							LoadMap.addObjTree(1036, LoadMap.x(l) + 12, LoadMap.y(l) + 20, 105);
							break;
						case 199:
							LoadMap.type[l] = 101;
							LoadMap.map[l] = 4;
							if ((int)b10 == 1)
							{
								LoadMap.addObjTree(1031, LoadMap.x(l) + 24, LoadMap.y(l) + 24, 101);
								LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 30);
							}
							b10 = (sbyte)((int)b10 + 1);
							break;
						case 200:
							LoadMap.type[l] = 107;
							if ((int)b23 == 1)
							{
								LoadMap.addObjTree(-1, LoadMap.x(l) + 24, LoadMap.y(l) + 24, 107);
								LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 30);
							}
							b23 = (sbyte)((int)b23 + 1);
							LoadMap.map[l] = 5;
							break;
						case 201:
							LoadMap.type[l] = 19;
							LoadMap.map[l] = 5;
							if ((int)b39 == 1)
							{
								LoadMap.addPopup(T.joinA, LoadMap.x(l) + 24, LoadMap.y(l) + 30);
							}
							b39 = (sbyte)((int)b39 + 1);
							break;
						case 202:
							this.setTypeMap(l, 88, 96);
							if ((int)b13 % 4 == 0)
							{
								LoadMap.addObjTree(4, LoadMap.x(l) + LoadMap.w * 2, LoadMap.y(l) + LoadMap.w, 88);
							}
							b13 = (sbyte)((int)b13 + 1);
							break;
						case 203:
						{
							int i24 = l;
							sbyte b63 = b34;
							b34 = (sbyte)((int)b63 + 1);
							this.setPopup(i24, b63, 0);
							this.setTypeMap(l, 110, 96);
							break;
						}
						case 204:
							LoadMap.map[l] = 43;
							LoadMap.type[l] = 10;
							if ((int)b39 == 1)
							{
								LoadMap.addPopup(T.joinA, LoadMap.x(l), LoadMap.y(l) + 30);
							}
							b39 = (sbyte)((int)b39 + 1);
							break;
						}
					}
					if ((int)LoadMap.type[l] == 80)
					{
						num++;
					}
				}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		AvCamera.gI().followPlayer = GameMidlet.avatar;
		this.setMapItem(index);
		LoadMap.orderVector(LoadMap.treeLists);
		if (LoadMap.TYPEMAP == 24 && FarmScr.idFarm != GameMidlet.avatar.IDDB)
		{
			LoadMap.TYPEMAP = 53;
		}
		int typemap = LoadMap.TYPEMAP;
		if (typemap != -1 && LoadMap.idTileImg != -1)
		{
			typemap = LoadMap.typeAny;
		}
		for (int n = 0; n < LoadMap.type.Length; n++)
		{
			if (LoadMap.isType(n % (int)LoadMap.wMap, n / (int)LoadMap.wMap, (short)typemap))
			{
				AvPosition avPosition3 = this.setPosPlayer(n);
				if (avPosition3 != null)
				{
					GameMidlet.avatar.x = avPosition3.x;
					GameMidlet.avatar.y = avPosition3.y;
				}
				break;
			}
		}
		if (LoadMap.typeTemp != -1)
		{
			LoadMap.typeAny = LoadMap.typeTemp;
		}
		AvCamera.gI().init(index);
		this.initFindPath();
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0004CCB4 File Offset: 0x0004B0B4
	public AvPosition setPosPlayer(int i)
	{
		if (i + 1 < LoadMap.type.Length && (int)LoadMap.type[i] == (int)LoadMap.type[i + 1])
		{
			for (int j = i; j < LoadMap.type.Length; j++)
			{
				if ((int)LoadMap.type[j] != (int)LoadMap.type[j + 1])
				{
					int num = LoadMap.w;
					if (i / (int)LoadMap.wMap == (int)(LoadMap.Hmap - 1) || i / (int)LoadMap.wMap == (int)(LoadMap.Hmap - 2))
					{
						num = -LoadMap.w;
					}
					return new AvPosition(LoadMap.x(i) + (j - i + 1) * LoadMap.w / 2, LoadMap.y(i) + LoadMap.w / 2 + num);
				}
			}
		}
		else if (i + (int)LoadMap.wMap < LoadMap.type.Length && (int)LoadMap.type[i] == (int)LoadMap.type[i + (int)LoadMap.wMap])
		{
			for (int k = i; k < LoadMap.type.Length; k += (int)LoadMap.wMap)
			{
				if ((int)LoadMap.type[k] != (int)LoadMap.type[k + (int)LoadMap.wMap])
				{
					int num2 = -LoadMap.w;
					if (i % (int)LoadMap.wMap == 0)
					{
						num2 = LoadMap.w;
					}
					return new AvPosition(LoadMap.x(i) + LoadMap.w / 2 + num2, LoadMap.y(i) + ((k - i) / (int)LoadMap.wMap + 1) * LoadMap.w / 2);
				}
			}
		}
		return null;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0004CE28 File Offset: 0x0004B228
	public static void addObjTree(int type, int x, int y, int index)
	{
		if (LoadMap.idTileImg != -1)
		{
			return;
		}
		SubObject subObject;
		if (type >= 0)
		{
			subObject = new ImageObj(type, x, y, 0, 0);
		}
		else
		{
			subObject = new SubObject(type, x, y, 0, 0);
		}
		subObject.index = (short)index;
		LoadMap.treeLists.addElement(subObject);
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0004CE78 File Offset: 0x0004B278
	public static void addObjTree(int type, int i, int index)
	{
		if (LoadMap.idTileImg != -1)
		{
			return;
		}
		ImageObj imageObj = new ImageObj(type, LoadMap.x(i) + LoadMap.getWTileImg(i, LoadMap.map), LoadMap.y(i) + LoadMap.w - 4, 0, 0);
		imageObj.index = (short)index;
		LoadMap.treeLists.addElement(imageObj);
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0004CECD File Offset: 0x0004B2CD
	public static int x(int i)
	{
		return i % (int)LoadMap.wMap * LoadMap.w;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0004CEDC File Offset: 0x0004B2DC
	public static int y(int i)
	{
		return i / (int)LoadMap.wMap * LoadMap.w;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0004CEEB File Offset: 0x0004B2EB
	private void setTypeMap(int i, sbyte t, int m)
	{
		LoadMap.type[i] = t;
		LoadMap.map[i] = (short)m;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x0004CEFE File Offset: 0x0004B2FE
	private void setTypePark(int i, sbyte t)
	{
		LoadMap.type[i] = t;
		if (i / (int)LoadMap.wMap == 0)
		{
			LoadMap.map[i] = 43;
		}
		else
		{
			LoadMap.map[i] = 6;
		}
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0004CF2A File Offset: 0x0004B32A
	public static void setType(int x, int y, sbyte t)
	{
		LoadMap.type[y * (int)LoadMap.wMap + x] = t;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0004CF3C File Offset: 0x0004B33C
	public static bool isType(int x, int y, short t)
	{
		return (int)LoadMap.type[y * (int)LoadMap.wMap + x] == (int)t;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0004CF51 File Offset: 0x0004B351
	public static void addPopup(string info, int x, int y)
	{
		if (Session_ME.connected)
		{
			LoadMap.treeLists.addElement(new PopupName(info, x, y));
		}
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0004CF70 File Offset: 0x0004B370
	public static void addPopup(string info, int x, int y, int type)
	{
		if (Session_ME.connected)
		{
			PopupName popupName = new PopupName(info, x, y);
			popupName.type = type;
			LoadMap.treeLists.addElement(popupName);
		}
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0004CFA4 File Offset: 0x0004B3A4
	public static MapItemType getMapItemTypeByID(int idType)
	{
		int num = LoadMap.mapItemType.size();
		for (int i = 0; i < num; i++)
		{
			MapItemType mapItemType = (MapItemType)LoadMap.mapItemType.elementAt(i);
			if ((int)mapItemType.idType == idType)
			{
				return mapItemType;
			}
		}
		return null;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0004CFF0 File Offset: 0x0004B3F0
	public void setMapItemType()
	{
		if (LoadMap.mapItem == null || LoadMap.mapItemType == null)
		{
			return;
		}
		for (int i = 0; i < LoadMap.mapItem.size(); i++)
		{
			MapItem mapItem = (MapItem)LoadMap.mapItem.elementAt(i);
			MapItemType mapItemTypeByID = LoadMap.getMapItemTypeByID((int)mapItem.typeID);
			LoadMap.setTypeSeat(mapItem, mapItemTypeByID);
			MapItem mapItem2 = new MapItem(mapItem.type, mapItem.x * LoadMap.w, mapItem.y * LoadMap.w, (int)mapItem.ID, mapItem.typeID);
			mapItem2.isGetImg = mapItem.isGetImg;
			LoadMap.treeLists.addElement(mapItem2);
		}
		LoadMap.orderVector(LoadMap.treeLists);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0004D0A4 File Offset: 0x0004B4A4
	private void setMapItem(int typ)
	{
		for (int i = 0; i < AvatarData.listMapItem.size(); i++)
		{
			MapItem mapItem = (MapItem)AvatarData.listMapItem.elementAt(i);
			if (mapItem.type == typ)
			{
				MapItemType mapItemTypeByID = AvatarData.getMapItemTypeByID((int)mapItem.typeID);
				LoadMap.setTypeSeat(mapItem, mapItemTypeByID);
				LoadMap.treeLists.addElement(new MapItem(mapItem.type, mapItem.x * LoadMap.w, mapItem.y * LoadMap.w, (int)mapItem.ID, mapItem.typeID));
			}
		}
		if (AvatarData.listAd != null)
		{
			for (int j = 0; j < AvatarData.listAd.size(); j++)
			{
				ObjAd objAd = (ObjAd)AvatarData.listAd.elementAt(j);
				for (int k = 0; k < objAd.listPoint.size(); k++)
				{
					AvPosition avPosition = (AvPosition)objAd.listPoint.elementAt(k);
					if (avPosition.anchor == typ)
					{
						if (avPosition.y * (int)LoadMap.wMap + avPosition.x >= 0 && avPosition.y * (int)LoadMap.wMap + avPosition.x < LoadMap.type.Length)
						{
							LoadMap.type[avPosition.y * (int)LoadMap.wMap + avPosition.x] = 83;
						}
						LoadMap.addPopup(objAd.title, avPosition.x * LoadMap.w + LoadMap.w / 2, avPosition.y * LoadMap.w + LoadMap.w / 2);
					}
				}
			}
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0004D240 File Offset: 0x0004B640
	public void getAd(int tx, int ty)
	{
		if (AvatarData.listAd == null)
		{
			return;
		}
		for (int i = 0; i < AvatarData.listAd.size(); i++)
		{
			ObjAd objAd = (ObjAd)AvatarData.listAd.elementAt(i);
			for (int j = 0; j < objAd.listPoint.size(); j++)
			{
				AvPosition avPosition = (AvPosition)objAd.listPoint.elementAt(j);
				if (avPosition.x == tx && avPosition.y == ty && LoadMap.TYPEMAP + 1 == avPosition.anchor)
				{
					Canvas.msgdlg.setInfoLR(objAd.text, new Command(T.OK, new LoadMap.IActionAd(objAd)), new Command(T.close, new LoadMap.IActionCloseAd()));
					return;
				}
			}
		}
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0004D30C File Offset: 0x0004B70C
	public void setClound()
	{
		this.clound = null;
		if (this.imgTreeBg == null || LoadMap.imgBG == null)
		{
			return;
		}
		this.clound = new AvPosition[6];
		int num = 0;
		if (LoadMap.rememBg == 0)
		{
			num = -10 * AvMain.hd;
		}
		else if (LoadMap.rememBg == 2)
		{
			num = 5 * AvMain.hd;
		}
		else if (LoadMap.rememBg == 3)
		{
			num = 25 * AvMain.hd;
		}
		else if (LoadMap.rememBg == 1)
		{
		}
		for (int i = 0; i < this.clound.Length; i++)
		{
			sbyte b = (sbyte)CRes.rnd(2);
			int x = CRes.rnd((int)LoadMap.wMap * LoadMap.w + LoadMap.imgBG.w) * 100;
			int y = -(LoadMap.imgBG.h / 2 + this.imgClound[(int)b].h + num + CRes.rnd(LoadMap.imgBG.h / 2));
			this.clound[i] = new AvPosition(x, y);
			this.clound[i].anchor = (int)b;
			if (this.clound[i].anchor == 1)
			{
				this.clound[i].index = (short)(10 + CRes.rnd(30));
			}
			else
			{
				this.clound[i].index = (short)(30 + CRes.rnd(30));
			}
		}
		CRes.rndaaa();
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0004D478 File Offset: 0x0004B878
	public static MyVector orderVector(MyVector obj)
	{
		try
		{
			int num = obj.size();
			for (int i = 0; i < num - 1; i++)
			{
				MyObject myObject = (MyObject)obj.elementAt(i);
				for (int j = i + 1; j < num; j++)
				{
					MyObject myObject2 = (MyObject)obj.elementAt(j);
					if (myObject.y > myObject2.y)
					{
						obj.setElementAt(myObject, j);
						obj.setElementAt(myObject2, i);
						myObject = myObject2;
					}
				}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		return obj;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0004D51C File Offset: 0x0004B91C
	public static void resetObject()
	{
		Canvas.currentEffect.removeAllElements();
		LoadMap.treeLists.removeAllElements();
		LoadMap.playerLists.removeAllElements();
		LoadMap.dynamicLists.removeAllElements();
		LoadMap.effBgList = null;
		LoadMap.effCameraList = null;
		LoadMap.effManager = null;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0004D558 File Offset: 0x0004B958
	public static bool isTrans(int x, int y)
	{
		int typeMap = LoadMap.getTypeMap(x, y);
		return typeMap == 80 || typeMap == 51 || (GameMidlet.avatar.task == -5 && (typeMap == 79 || typeMap == 92 || typeMap == 81 || typeMap == 67));
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0004D5B4 File Offset: 0x0004B9B4
	public static int getTypeMap(int vX, int vY)
	{
		if (vX < 0 || vX > (int)LoadMap.wMap * LoadMap.w || vY / LoadMap.w * (int)LoadMap.wMap + vX / LoadMap.w < 0 || vY / LoadMap.w * (int)LoadMap.wMap + vX / LoadMap.w >= LoadMap.type.Length)
		{
			return -2;
		}
		return (int)LoadMap.type[vY / LoadMap.w * (int)LoadMap.wMap + vX / LoadMap.w];
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0004D638 File Offset: 0x0004BA38
	public static int getposMap(int vX, int vY)
	{
		if (vX < 0 || vX > (int)LoadMap.wMap * LoadMap.w || vY / LoadMap.w * (int)LoadMap.wMap + vX / LoadMap.w >= LoadMap.type.Length)
		{
			return -1;
		}
		return vY / LoadMap.w * (int)LoadMap.wMap + vX / LoadMap.w;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0004D698 File Offset: 0x0004BA98
	public static Avatar getAvatar(int id)
	{
		for (int i = 0; i < LoadMap.playerLists.size(); i++)
		{
			MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
			if ((int)myObject.catagory == 0 && ((Base)myObject).IDDB == id)
			{
				return (Avatar)myObject;
			}
		}
		return null;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0004D6F8 File Offset: 0x0004BAF8
	public static void onWeather(sbyte weather2)
	{
		for (int i = 0; i < Canvas.currentEffect.size(); i++)
		{
			Effect effect = (Effect)Canvas.currentEffect.elementAt(i);
			effect.isStop = true;
		}
		if ((int)weather2 != -1)
		{
			AnimateEffect animateEffect = new AnimateEffect(weather2, true, 0);
			animateEffect.show();
		}
		LoadMap.weather = weather2;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0004D758 File Offset: 0x0004BB58
	public static void setPet(Avatar ava)
	{
		if (ava.idPet != -1)
		{
			Pet pet = new Pet(ava);
			LoadMap.playerLists.addElement(pet);
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0004D784 File Offset: 0x0004BB84
	public static Pet getPet(int id)
	{
		for (int i = 0; i < LoadMap.playerLists.size(); i++)
		{
			MyObject myObject = (MyObject)LoadMap.playerLists.elementAt(i);
			if ((int)myObject.catagory == 4 && ((Pet)myObject).follow.IDDB == id)
			{
				return (Pet)myObject;
			}
		}
		return null;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0004D7E8 File Offset: 0x0004BBE8
	public static void addPlayer(Avatar ava)
	{
		LoadMap.playerLists.addElement(ava);
		ava.setPet();
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0004D7FC File Offset: 0x0004BBFC
	public static void removePlayer(Avatar ava)
	{
		LoadMap.playerLists.removeElement(ava);
		Pet pet = LoadMap.getPet(ava.IDDB);
		if (pet != null)
		{
			LoadMap.playerLists.removeElement(pet);
		}
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0004D831 File Offset: 0x0004BC31
	public static void removePlayer(MyObject obj)
	{
		if (LoadMap.focusObj == obj)
		{
			LoadMap.focusObj = null;
		}
		LoadMap.playerLists.removeElement(obj);
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0004D84F File Offset: 0x0004BC4F
	public void onTileImg(sbyte idTileMap, sbyte[] arr)
	{
		LoadMap.idTileImg = (int)idTileMap;
		LoadMap.imgMap = new FrameImage(CRes.createImgByByteArray(ArrayCast.cast(arr)), LoadMap.w * AvMain.hd, AvMain.hd * LoadMap.w);
		this.setMapAny();
		Canvas.load = 0;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0004D88F File Offset: 0x0004BC8F
	private void setMapPaint(int i, short[] m)
	{
		if (i % (int)LoadMap.wMap == 0)
		{
			m[i] = m[i + 1];
		}
		else
		{
			m[i] = m[i - 1];
		}
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0004D8B4 File Offset: 0x0004BCB4
	public void setMapAny()
	{
		Bus.isRun = false;
		LoadMap.resetObject();
		LoadMap.addPlayer(GameMidlet.avatar);
		short[] array = new short[LoadMap.map.Length];
		LoadMap.type = new sbyte[LoadMap.map.Length];
		sbyte[] array2 = new sbyte[125];
		sbyte b = 0;
		sbyte b2 = 0;
		for (int i = 0; i < LoadMap.map.Length; i++)
		{
			array[i] = LoadMap.map[i];
		}
		LoadMap.isCasino = false;
		for (int j = 0; j < LoadMap.map.Length; j++)
		{
			if ((int)LoadMap.map[j] < LoadMap.imgMap.nFrame)
			{
				LoadMap.map[j] = -4;
			}
			else if ((int)LoadMap.map[j] < LoadMap.imgMap.nFrame * 2)
			{
				LoadMap.map[j] = -5;
			}
			else
			{
				int num = (int)LoadMap.map[j] - LoadMap.imgMap.nFrame * 2;
				switch (num)
				{
				case 12:
					LoadMap.map[j] = 150;
					break;
				case 13:
					LoadMap.map[j] = 151;
					break;
				case 14:
					LoadMap.isCasino = true;
					this.setPopup(j, b, 0);
					b = (sbyte)((int)b + 1);
					LoadMap.map[j] = 184;
					array[j] = 33;
					break;
				case 15:
					b2 = (sbyte)((int)b2 + 1);
					array[j] = 0;
					LoadMap.map[j] = 185;
					break;
				default:
					switch (num)
					{
					case 0:
					{
						LoadMap.map[j] = 98;
						ImageObj imageObj = new ImageObj(846, LoadMap.x(j) + LoadMap.w / 2, LoadMap.y(j) + LoadMap.w / 2, 0, 0);
						LoadMap.treeLists.addElement(imageObj);
						goto IL_214;
					}
					case 2:
						LoadMap.map[j] = 139;
						goto IL_214;
					case 3:
						LoadMap.map[j] = 152;
						goto IL_214;
					}
					this.setPopup(j, array2[num], 0);
					LoadMap.type[j] = (sbyte)(-125 + num);
					LoadMap.map[j] = -3;
					break;
				}
				IL_214:
				if (num > 0 && (int)array2[num] == 0 && num - 1 < MapScr.idImg.Length && MapScr.idImg[num - 1] != -1)
				{
					ImageObj imageObj2 = new ImageObj((int)MapScr.idImg[num - 1], LoadMap.x(j) + LoadMap.getWTileImg(j, array), LoadMap.y(j) + LoadMap.w - 4, 0, 0);
					LoadMap.treeLists.addElement(imageObj2);
				}
				if (num != 14)
				{
					this.setMapPaint(j, array);
				}
				sbyte[] array3 = array2;
				int num2 = num;
				array3[num2] = (sbyte)((int)array3[num2] + 1);
			}
		}
		AvCamera.disable = false;
		GameMidlet.avatar.action = 0;
		this.imgTreeBg = null;
		LoadMap.imgCreateMap = null;
		this.setMap(null, (int)MapScr.roomID + 1, false);
		LoadMap.x0_imgBG = 26;
		LoadMap.TYPEMAP = (int)MapScr.roomID;
		LoadMap.map = array;
		AvCamera.gI().init((int)MapScr.roomID + 1);
		Canvas.endDlg();
		LoadMap.rememBg = -1;
		LoadMap.rememMap = -1;
		this.setMapItemType();
		ParkService.gI().doJoinPark((int)MapScr.roomID, -1);
		Canvas.paint.setColorBar();
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0004DC04 File Offset: 0x0004C004
	public static int getWTileImg(int i, short[] m)
	{
		for (int j = i; j < m.Length; j++)
		{
			if (m[j] != m[j + 1])
			{
				return (j - i + 1) * LoadMap.w / 2;
			}
		}
		return 0;
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0004DC44 File Offset: 0x0004C044
	public void setPopup(int i, sbyte count, int type)
	{
		if ((int)count != 0)
		{
			return;
		}
		if (i + 1 < LoadMap.map.Length && LoadMap.map[i] == LoadMap.map[i + 1])
		{
			for (int j = i; j < LoadMap.map.Length; j++)
			{
				if (LoadMap.map[j] != LoadMap.map[j + 1])
				{
					LoadMap.addPopup((type == 1) ? T.exit : T.joinA, LoadMap.x(i) + (j - i + 1) * LoadMap.w / 2, LoadMap.y(i) + ((LoadMap.idTileImg != -1) ? LoadMap.w : (LoadMap.w / 2)) + ((type != 2) ? 0 : (LoadMap.w / 2)));
					return;
				}
			}
		}
		else if (i + (int)LoadMap.wMap < LoadMap.map.Length && LoadMap.map[i] == LoadMap.map[i + (int)LoadMap.wMap])
		{
			for (int k = i; k < LoadMap.map.Length; k += (int)LoadMap.wMap)
			{
				if (LoadMap.map[k] != LoadMap.map[k + (int)LoadMap.wMap])
				{
					LoadMap.addPopup((type == 1) ? T.exit : T.joinA, LoadMap.x(i) + 3, LoadMap.y(i) + ((k - i) / (int)LoadMap.wMap + 1) * LoadMap.w / 2);
					return;
				}
			}
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0004DDB8 File Offset: 0x0004C1B8
	public static void setTypeSeat(MapItem pos, MapItemType map)
	{
		sbyte b = 88;
		if (map.iconID == 1)
		{
			b = 79;
		}
		else if (map.iconID == 2)
		{
			b = 67;
		}
		for (int i = 0; i < map.listNotTrans.size(); i++)
		{
			AvPosition avPosition = (AvPosition)map.listNotTrans.elementAt(i);
			LoadMap.type[(pos.y + avPosition.y) * (int)LoadMap.wMap + (pos.x + avPosition.x)] = b;
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0004DE41 File Offset: 0x0004C241
	public void onDichChuyen(sbyte roomID, sbyte boardID, int xTe, int yTe)
	{
		LoadMap.xDichChuyen = xTe;
		LoadMap.yDichChuyen = yTe;
		LoadMap.idTileImg = -1;
		Canvas.startWaitDlg();
		if (GameMidlet.CLIENT_TYPE != 9)
		{
			GlobalService.gI().getHandler(9);
		}
		ParkService.gI().doJoinPark((int)roomID, (int)boardID);
	}

	// Token: 0x040009A5 RID: 2469
	public const sbyte T_PARK = 0;

	// Token: 0x040009A6 RID: 2470
	public const sbyte T_PARK_1 = 1;

	// Token: 0x040009A7 RID: 2471
	public const sbyte T_PARK_2 = 2;

	// Token: 0x040009A8 RID: 2472
	public const sbyte T_PARK_3 = 3;

	// Token: 0x040009A9 RID: 2473
	public const sbyte T_PARK_4 = 4;

	// Token: 0x040009AA RID: 2474
	public const sbyte T_PARK_5 = 5;

	// Token: 0x040009AB RID: 2475
	public const sbyte T_PARK_6 = 6;

	// Token: 0x040009AC RID: 2476
	public const sbyte T_PARK_7 = 7;

	// Token: 0x040009AD RID: 2477
	public const sbyte T_PARK_8 = 8;

	// Token: 0x040009AE RID: 2478
	public const sbyte T_CITY = 9;

	// Token: 0x040009AF RID: 2479
	public const sbyte T_CONG_SANH_CUOI = 10;

	// Token: 0x040009B0 RID: 2480
	public const sbyte T_PARK_PATH = 11;

	// Token: 0x040009B1 RID: 2481
	public const sbyte T_PARK_ADVANCED = 12;

	// Token: 0x040009B2 RID: 2482
	public const sbyte T_FISHING_ADVANCED = 13;

	// Token: 0x040009B3 RID: 2483
	public const sbyte T_FISING_1 = 14;

	// Token: 0x040009B4 RID: 2484
	public const sbyte T_FISING_2 = 15;

	// Token: 0x040009B5 RID: 2485
	public const sbyte T_FISING_3 = 16;

	// Token: 0x040009B6 RID: 2486
	public const sbyte T_SLUM = 17;

	// Token: 0x040009B7 RID: 2487
	public const sbyte T_PRISON = 18;

	// Token: 0x040009B8 RID: 2488
	public const sbyte T_LE_CUOI = 19;

	// Token: 0x040009B9 RID: 2489
	public const sbyte T_SAN_BAY = 20;

	// Token: 0x040009BA RID: 2490
	public const sbyte T_ROAD_HOUSE = 21;

	// Token: 0x040009BB RID: 2491
	public const sbyte T_KHU_MUA_SAM = 23;

	// Token: 0x040009BC RID: 2492
	public const sbyte T_FARM = 24;

	// Token: 0x040009BD RID: 2493
	public const sbyte T_FARMWAY = 25;

	// Token: 0x040009BE RID: 2494
	public const sbyte T_SHOP = 57;

	// Token: 0x040009BF RID: 2495
	public const sbyte T_BEAUTYSALON = 58;

	// Token: 0x040009C0 RID: 2496
	public const sbyte T_GIFT = 59;

	// Token: 0x040009C1 RID: 2497
	public const sbyte T_BOARD_WAIT_2 = 60;

	// Token: 0x040009C2 RID: 2498
	public const sbyte T_BOARD_WAIT_4 = 61;

	// Token: 0x040009C3 RID: 2499
	public const sbyte T_SHOP_2 = 62;

	// Token: 0x040009C4 RID: 2500
	public const sbyte T_BEAUTYSALON_2 = 63;

	// Token: 0x040009C5 RID: 2501
	public const sbyte T_GIFT_2 = 64;

	// Token: 0x040009C6 RID: 2502
	public const sbyte T_POPUP = 27;

	// Token: 0x040009C7 RID: 2503
	public const sbyte T_STORE = 28;

	// Token: 0x040009C8 RID: 2504
	public const sbyte T_CHECK_POINT = 29;

	// Token: 0x040009C9 RID: 2505
	public const sbyte T_JOIN_ANY = -125;

	// Token: 0x040009CA RID: 2506
	public const sbyte T_PLANT = 51;

	// Token: 0x040009CB RID: 2507
	public const sbyte T_CUAHANG = 52;

	// Token: 0x040009CC RID: 2508
	public const sbyte T_FARM_FRIEND = 53;

	// Token: 0x040009CD RID: 2509
	public const sbyte T_FISHING_CHAIR = 54;

	// Token: 0x040009CE RID: 2510
	public const sbyte T_BANK = 55;

	// Token: 0x040009CF RID: 2511
	public const sbyte T_JOIN = 56;

	// Token: 0x040009D0 RID: 2512
	public const sbyte T_BOARD_WAIT_5 = 65;

	// Token: 0x040009D1 RID: 2513
	public const sbyte T_NAM_NGHI = 67;

	// Token: 0x040009D2 RID: 2514
	public const sbyte T_HOUSE_1 = 68;

	// Token: 0x040009D3 RID: 2515
	public const sbyte T_HOUSE_2 = 69;

	// Token: 0x040009D4 RID: 2516
	public const sbyte T_HOUSE_3 = 70;

	// Token: 0x040009D5 RID: 2517
	public const sbyte T_VE_BAY = 71;

	// Token: 0x040009D6 RID: 2518
	public const sbyte T_TL = 72;

	// Token: 0x040009D7 RID: 2519
	public const sbyte T_P = 73;

	// Token: 0x040009D8 RID: 2520
	public const sbyte T_CT = 74;

	// Token: 0x040009D9 RID: 2521
	public const sbyte T_CR = 75;

	// Token: 0x040009DA RID: 2522
	public const sbyte T_DM = 76;

	// Token: 0x040009DB RID: 2523
	public const sbyte T_BC = 77;

	// Token: 0x040009DC RID: 2524
	public const sbyte T_FOOD_PET = 78;

	// Token: 0x040009DD RID: 2525
	public const sbyte T_CHAIR = 79;

	// Token: 0x040009DE RID: 2526
	public const sbyte T_EMPTY = 80;

	// Token: 0x040009DF RID: 2527
	public const sbyte T_BUS_STOP = 81;

	// Token: 0x040009E0 RID: 2528
	public const sbyte T_OBJECT = 82;

	// Token: 0x040009E1 RID: 2529
	public const sbyte T_AD = 83;

	// Token: 0x040009E2 RID: 2530
	public const sbyte T_PIG_TROUGH = 84;

	// Token: 0x040009E3 RID: 2531
	public const sbyte T_DOG_TROUGH = 85;

	// Token: 0x040009E4 RID: 2532
	public const sbyte T_MILK_BUCKET = 86;

	// Token: 0x040009E5 RID: 2533
	public const sbyte T_NEST = 87;

	// Token: 0x040009E6 RID: 2534
	public const sbyte T_ROCK = 88;

	// Token: 0x040009E7 RID: 2535
	public const sbyte T_TOP_FARM = 89;

	// Token: 0x040009E8 RID: 2536
	public const sbyte T_NOTENTER = 90;

	// Token: 0x040009E9 RID: 2537
	public const sbyte T_NOT_FISHING = 91;

	// Token: 0x040009EA RID: 2538
	public const sbyte T_CHAIR_RED = 92;

	// Token: 0x040009EB RID: 2539
	public const sbyte T_ICE_CREAM = 93;

	// Token: 0x040009EC RID: 2540
	public const sbyte T_FISH_SHOP = 94;

	// Token: 0x040009ED RID: 2541
	public const sbyte T_UPDATE_CATTLE = 95;

	// Token: 0x040009EE RID: 2542
	public const sbyte T_UPDATE_FISH = 96;

	// Token: 0x040009EF RID: 2543
	public const sbyte T_STAR_FRUIT = 97;

	// Token: 0x040009F0 RID: 2544
	public const sbyte T_COOKING = 98;

	// Token: 0x040009F1 RID: 2545
	public const sbyte T_QUAY_SO = 99;

	// Token: 0x040009F2 RID: 2546
	public const sbyte T_TASK = 100;

	// Token: 0x040009F3 RID: 2547
	public const sbyte T_DAU_GIA = 106;

	// Token: 0x040009F4 RID: 2548
	public const sbyte T_UPGRADE = 102;

	// Token: 0x040009F5 RID: 2549
	public const sbyte T_PRIMEUM_SHOP = 103;

	// Token: 0x040009F6 RID: 2550
	public const sbyte T_PET_SHOP = 104;

	// Token: 0x040009F7 RID: 2551
	public const sbyte T_THO_KHOA = 105;

	// Token: 0x040009F8 RID: 2552
	public const sbyte T_MR_DOOM = 101;

	// Token: 0x040009F9 RID: 2553
	public const sbyte T_RACE = 107;

	// Token: 0x040009FA RID: 2554
	public const sbyte T_CASINO = 108;

	// Token: 0x040009FB RID: 2555
	public const sbyte T_CASINO_2 = 109;

	// Token: 0x040009FC RID: 2556
	public const sbyte T_HOUSE_4 = 110;

	// Token: 0x040009FD RID: 2557
	public const sbyte T_FLOWER_LOVE = 111;

	// Token: 0x040009FE RID: 2558
	public const sbyte T_HO_HOUSE = 112;

	// Token: 0x040009FF RID: 2559
	public const short INSTANCE = 127;

	// Token: 0x04000A00 RID: 2560
	public const sbyte MAPMAIN = -1;

	// Token: 0x04000A01 RID: 2561
	public static int TYPEMAP = -1;

	// Token: 0x04000A02 RID: 2562
	public static Image imgDen;

	// Token: 0x04000A03 RID: 2563
	public static Image imgBG;

	// Token: 0x04000A04 RID: 2564
	public static FrameImage imgMap;

	// Token: 0x04000A05 RID: 2565
	public static short[] map;

	// Token: 0x04000A06 RID: 2566
	public static short wMap;

	// Token: 0x04000A07 RID: 2567
	public static short Hmap;

	// Token: 0x04000A08 RID: 2568
	public static sbyte[] type;

	// Token: 0x04000A09 RID: 2569
	public static sbyte[] bg = new sbyte[]
	{
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		0,
		1,
		1,
		3,
		3,
		3,
		3,
		3,
		2,
		-1,
		-1,
		-1,
		0,
		0,
		0,
		2,
		2
	};

	// Token: 0x04000A0A RID: 2570
	public static int w = 24;

	// Token: 0x04000A0B RID: 2571
	public static sbyte status = 0;

	// Token: 0x04000A0C RID: 2572
	public static sbyte weather = -1;

	// Token: 0x04000A0D RID: 2573
	public static MyVector treeLists = new MyVector();

	// Token: 0x04000A0E RID: 2574
	public static MyVector playerLists = new MyVector();

	// Token: 0x04000A0F RID: 2575
	public static MyVector dynamicLists = new MyVector();

	// Token: 0x04000A10 RID: 2576
	public static MyVector listImgAD;

	// Token: 0x04000A11 RID: 2577
	public static int fWint = 0;

	// Token: 0x04000A12 RID: 2578
	public static int star = 0;

	// Token: 0x04000A13 RID: 2579
	public AvPosition[] clound;

	// Token: 0x04000A14 RID: 2580
	public static MyVector listStar = new MyVector();

	// Token: 0x04000A15 RID: 2581
	public static int[] colorStar = new int[]
	{
		15853390,
		15006199,
		8183509,
		12254198
	};

	// Token: 0x04000A16 RID: 2582
	public static MyObject focusObj;

	// Token: 0x04000A17 RID: 2583
	public static Command cmdNext;

	// Token: 0x04000A18 RID: 2584
	public static Image imgShadow;

	// Token: 0x04000A19 RID: 2585
	public static FrameImage imgFocus;

	// Token: 0x04000A1A RID: 2586
	private static int[] colorBg = new int[]
	{
		6143735,
		21
	};

	// Token: 0x04000A1B RID: 2587
	public static Color colorBackGr;

	// Token: 0x04000A1C RID: 2588
	public static int rememMap = -1;

	// Token: 0x04000A1D RID: 2589
	public static int rememBg = -1;

	// Token: 0x04000A1E RID: 2590
	public static AvPosition posFocus;

	// Token: 0x04000A1F RID: 2591
	public static MyVector effBgList;

	// Token: 0x04000A20 RID: 2592
	public static MyVector effCameraList;

	// Token: 0x04000A21 RID: 2593
	public static MyVector effManager;

	// Token: 0x04000A22 RID: 2594
	public static int idTileImg = -1;

	// Token: 0x04000A23 RID: 2595
	public static Bus bus = new Bus();

	// Token: 0x04000A24 RID: 2596
	private Image[] imgClound = new Image[2];

	// Token: 0x04000A25 RID: 2597
	private int hBG;

	// Token: 0x04000A26 RID: 2598
	private Image imgTreeBg;

	// Token: 0x04000A27 RID: 2599
	private static int x0_imgTreeBg = 0;

	// Token: 0x04000A28 RID: 2600
	private static int x0_imgBG = 0;

	// Token: 0x04000A29 RID: 2601
	private int countRndSound;

	// Token: 0x04000A2A RID: 2602
	private long timeCurSound;

	// Token: 0x04000A2B RID: 2603
	public static MyVector listDeltaPosition = new MyVector();

	// Token: 0x04000A2C RID: 2604
	public static float zoom = 0f;

	// Token: 0x04000A2D RID: 2605
	public static float disTouch;

	// Token: 0x04000A2E RID: 2606
	public static bool trans;

	// Token: 0x04000A2F RID: 2607
	public static bool isGo;

	// Token: 0x04000A30 RID: 2608
	public static int dirFocus = -1;

	// Token: 0x04000A31 RID: 2609
	public static string test = string.Empty;

	// Token: 0x04000A32 RID: 2610
	private int pxLast;

	// Token: 0x04000A33 RID: 2611
	private int pyLast;

	// Token: 0x04000A34 RID: 2612
	public float pa;

	// Token: 0x04000A35 RID: 2613
	public float pb;

	// Token: 0x04000A36 RID: 2614
	public float dyTran;

	// Token: 0x04000A37 RID: 2615
	public float dxTran;

	// Token: 0x04000A38 RID: 2616
	public bool transY;

	// Token: 0x04000A39 RID: 2617
	public bool transX;

	// Token: 0x04000A3A RID: 2618
	private long count;

	// Token: 0x04000A3B RID: 2619
	private long timeDelay;

	// Token: 0x04000A3C RID: 2620
	private long timePointY;

	// Token: 0x04000A3D RID: 2621
	private long timePointX;

	// Token: 0x04000A3E RID: 2622
	private sbyte iTop;

	// Token: 0x04000A3F RID: 2623
	private sbyte iLeft;

	// Token: 0x04000A40 RID: 2624
	private int xFirFocus;

	// Token: 0x04000A41 RID: 2625
	private int yFirFocus;

	// Token: 0x04000A42 RID: 2626
	private int xfirDu;

	// Token: 0x04000A43 RID: 2627
	private int yfirDu;

	// Token: 0x04000A44 RID: 2628
	private int xLastDu;

	// Token: 0x04000A45 RID: 2629
	private int yLastDu;

	// Token: 0x04000A46 RID: 2630
	private bool[][] used;

	// Token: 0x04000A47 RID: 2631
	private short[] to;

	// Token: 0x04000A48 RID: 2632
	private short[] from;

	// Token: 0x04000A49 RID: 2633
	private short[] mPath;

	// Token: 0x04000A4A RID: 2634
	public static int nPath = 0;

	// Token: 0x04000A4B RID: 2635
	private static int wFocus = 3;

	// Token: 0x04000A4C RID: 2636
	public static int xJoinCasino;

	// Token: 0x04000A4D RID: 2637
	public static int yJoinCasino;

	// Token: 0x04000A4E RID: 2638
	private MyObject player;

	// Token: 0x04000A4F RID: 2639
	private MyObject obj;

	// Token: 0x04000A50 RID: 2640
	private MyObject dynamic;

	// Token: 0x04000A51 RID: 2641
	private Base temp1 = new Base();

	// Token: 0x04000A52 RID: 2642
	private SubObject temp2 = new SubObject(10000, 10000);

	// Token: 0x04000A53 RID: 2643
	private int p;

	// Token: 0x04000A54 RID: 2644
	private int o;

	// Token: 0x04000A55 RID: 2645
	private int d;

	// Token: 0x04000A56 RID: 2646
	private static int numF = 0;

	// Token: 0x04000A57 RID: 2647
	private Image imgDayDien0;

	// Token: 0x04000A58 RID: 2648
	private Image imgDayDien1;

	// Token: 0x04000A59 RID: 2649
	private Image imgDayDien2;

	// Token: 0x04000A5A RID: 2650
	public static Image[] imgCreateMap;

	// Token: 0x04000A5B RID: 2651
	public static int typeAny = 0;

	// Token: 0x04000A5C RID: 2652
	public static int typeTemp = -1;

	// Token: 0x04000A5D RID: 2653
	public static bool isCasino = false;

	// Token: 0x04000A5E RID: 2654
	public static MyVector mapItemType;

	// Token: 0x04000A5F RID: 2655
	public static MyVector mapItem;

	// Token: 0x04000A60 RID: 2656
	public static int xDichChuyen = -1;

	// Token: 0x04000A61 RID: 2657
	public static int yDichChuyen = -1;

	// Token: 0x02000119 RID: 281
	private class IActionNextFocus : IAction
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0004DFA7 File Offset: 0x0004C3A7
		public void perform()
		{
			LoadMap.NextFocus();
		}
	}

	// Token: 0x0200011A RID: 282
	private class IActionExitToCity : IAction
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x0004DFB6 File Offset: 0x0004C3B6
		public void perform()
		{
			Canvas.startWaitDlg();
			if (LoadMap.TYPEMAP == 108)
			{
				ParkService.gI().doJoinPark(9, -1);
			}
			else
			{
				Canvas.startWaitDlg();
				GlobalService.gI().getHandler(9);
			}
		}
	}

	// Token: 0x0200011B RID: 283
	private class IActionExitCity2 : IAction
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x0004DFF4 File Offset: 0x0004C3F4
		public void perform()
		{
			Canvas.startWaitDlg();
			if (LoadMap.TYPEMAP == 108)
			{
				ParkService.gI().doJoinPark(9, -1);
			}
			else
			{
				MapScr.gI().doExit();
			}
		}
	}

	// Token: 0x0200011C RID: 284
	private class IActionExitPark2 : IAction
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x0004E02B File Offset: 0x0004C42B
		public void perform()
		{
			Canvas.startWaitDlg();
			MapScr.gI().doExit();
		}
	}

	// Token: 0x0200011D RID: 285
	private class IActionCloseAd : IAction
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0004E044 File Offset: 0x0004C444
		public void perform()
		{
		}
	}

	// Token: 0x0200011E RID: 286
	private class IActionAd : IAction
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x0004E046 File Offset: 0x0004C446
		public IActionAd(ObjAd ob)
		{
			this.obj = ob;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0004E058 File Offset: 0x0004C458
		public void perform()
		{
			if (this.obj.typeShop != -1)
			{
				GlobalService.gI().requestShop(this.obj.typeShop);
				Canvas.startWaitDlg();
			}
			else if (this.obj.url != null && !this.obj.url.Equals(string.Empty))
			{
				GameMidlet.flatForm(this.obj.url);
			}
			else
			{
				GameMidlet.sendSMS(this.obj.sms, this.obj.to, null, null);
			}
		}

		// Token: 0x04000A62 RID: 2658
		private ObjAd obj;
	}
}
