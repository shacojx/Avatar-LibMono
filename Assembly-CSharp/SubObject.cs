using System;

// Token: 0x0200006F RID: 111
public class SubObject : MyObject
{
	// Token: 0x060003B7 RID: 951 RVA: 0x00010370 File Offset: 0x0000E770
	public SubObject()
	{
		this.catagory = 1;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001037F File Offset: 0x0000E77F
	public SubObject(int type, int x, int y, int w)
	{
		this.catagory = 1;
		this.type = type;
		this.x = x;
		this.y = y;
		this.w = (short)w;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000103AC File Offset: 0x0000E7AC
	public SubObject(int type, int x, int y, int w, int h)
	{
		this.catagory = 1;
		this.type = type;
		this.x = x;
		this.y = y;
		this.w = (short)w;
		this.h = (short)h;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000103E2 File Offset: 0x0000E7E2
	public SubObject(int i, int j)
	{
		this.catagory = 1;
		this.x = i;
		this.y = j;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00010400 File Offset: 0x0000E800
	public override void paint(MyGraphics g)
	{
		if (this.type < 0 && ((float)(this.x * MyObject.hd + (int)(this.w / 2)) < AvCamera.gI().xCam || (float)(this.x * MyObject.hd - (int)(this.w / 2)) > AvCamera.gI().xCam + (float)Canvas.w))
		{
			return;
		}
		int num = this.x * MyObject.hd;
		int num2 = this.y * MyObject.hd;
		int num3 = this.type;
		switch (num3 + 10)
		{
		case 0:
		case 7:
			g.drawImage(FarmScr.imgBuyLant, (float)num, (float)num2, MyGraphics.BOTTOM | MyGraphics.RIGHT);
			break;
		case 1:
			if (Canvas.welcome != null)
			{
				g.drawImage(LoadMap.imgShadow, (float)num, (float)num2, 3);
				AvatarData.paintImg(g, 900, num, num2 + (int)Canvas.welcome.num - 10, MyGraphics.BOTTOM | MyGraphics.HCENTER);
			}
			break;
		case 2:
			SubObject.paintNest(g, num, num2, FarmScr.listNest);
			break;
		case 3:
			SubObject.paintNest(g, num, num2, FarmScr.listBucket);
			break;
		case 4:
			SubObject.paintDogTr(g, num, num2);
			break;
		case 5:
			SubObject.paintTrough(g, num, num2, this.type);
			break;
		case 8:
			SubObject.paintDoing(g, num, num2);
			break;
		case 10:
			AvatarData.paintImg(g, 243, num, num2, 33);
			break;
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00010594 File Offset: 0x0000E994
	private static void paintNest(MyGraphics g, int x, int y, MyVector list)
	{
		for (int i = 0; i < list.size(); i++)
		{
			AvPosition avPosition = (AvPosition)list.elementAt(i);
			if (avPosition.x * MyObject.hd == x && avPosition.y * MyObject.hd == y)
			{
				AnimalInfo animalByID = FarmData.getAnimalByID(avPosition.anchor);
				if (animalByID.iconO != -1)
				{
					AvatarData.paintImg(g, (int)animalByID.iconO, x, y, 3);
				}
				for (int j = 0; j < FarmScr.animalLists.size(); j++)
				{
					Animal animal = (Animal)FarmScr.animalLists.elementAt(j);
					if ((int)animal.species == avPosition.anchor && animal.numEggOne > 0)
					{
						AvatarData.paintImg(g, (int)animalByID.iconProduct, x, y, 3);
						return;
					}
				}
			}
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001066D File Offset: 0x0000EA6D
	private static void paintDogTr(MyGraphics g, int x, int y)
	{
		FarmScr.imgDogTr.drawFrame(0, x, y, 0, 3, g);
		if (Dog.itemID != -1)
		{
			FarmScr.imgDogTr.drawFrame(1, x, y, 0, 3, g);
		}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001069A File Offset: 0x0000EA9A
	private static void paintTrough(MyGraphics g, int x, int y, int type)
	{
		FarmScr.imgTrough.drawFrame(0, x, y, 0, 3, g);
		if (Cattle.itemID != -1)
		{
			FarmScr.imgTrough.drawFrame(2, x, y, 0, 3, g);
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000106C7 File Offset: 0x0000EAC7
	public static void paintDoing(MyGraphics g, int x, int y)
	{
		if ((int)FarmScr.action != -1)
		{
			FarmScr.img.drawFrame((int)FarmScr.frame, x, y, ((int)GameMidlet.avatar.direct != (int)Base.LEFT) ? 0 : 2, 3, g);
		}
	}

	// Token: 0x040004CF RID: 1231
	public int type;
}
