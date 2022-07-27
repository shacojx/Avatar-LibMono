using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class FarmData
{
	// Token: 0x0600061C RID: 1564 RVA: 0x000394DD File Offset: 0x000378DD
	public static void init()
	{
		FarmData.playing = -1;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x000394E8 File Offset: 0x000378E8
	public static void checkDataFarm(sbyte number1, short[] id, short[] version, int verBigImg, int verPart2)
	{
		FarmData.loadVersion();
		FarmData.playing = 0;
		FarmData.imgBig = new Image[(int)number1];
		if (!FarmData.loadDataBig())
		{
			FarmData.numImgBig = number1;
			FarmData.versionBig = version;
			FarmData.verImg = -1;
			FarmData.verPart = -1;
			for (int i = 0; i < (int)number1; i++)
			{
				FarmService.gI().getBigImage((short)i);
				FarmData.playing++;
			}
		}
		else if ((int)FarmData.numImgBig > 0)
		{
			for (int j = 0; j < (int)FarmData.numImgBig; j++)
			{
				sbyte[] data = FarmData.loadImgBig(j);
				FarmData.imgBig[j] = CRes.createImgByByteArray(ArrayCast.cast(data));
			}
		}
		for (int k = 0; k < (int)FarmData.numImgBig; k++)
		{
			if (version[k] != FarmData.versionBig[k])
			{
				FarmService.gI().getBigImage((short)k);
				FarmData.playing++;
			}
		}
		int num = (int)number1 - (int)FarmData.numImgBig;
		if (num > 0)
		{
			short[] array = FarmData.versionBig;
			FarmData.versionBig = new short[version.Length];
			for (int l = 0; l < array.Length; l++)
			{
				FarmData.versionBig[l] = array[l];
			}
			for (int m = (int)FarmData.numImgBig; m < (int)number1; m++)
			{
				FarmService.gI().getBigImage((short)m);
				FarmData.playing++;
			}
		}
		if (!FarmData.loadImageData())
		{
			FarmData.verImg = verBigImg;
			FarmService.gI().getImageData();
			FarmData.playing++;
		}
		else if (FarmData.verImg != verBigImg)
		{
			FarmData.verImg = verBigImg;
			FarmService.gI().getImageData();
			FarmData.playing++;
		}
		if (!FarmData.loadTreeInfo())
		{
			FarmData.verPart = verPart2;
			FarmService.gI().getTreeInfo();
			FarmData.playing++;
		}
		else if (FarmData.verPart != verPart2)
		{
			FarmData.verPart = verPart2;
			FarmService.gI().getTreeInfo();
			FarmData.playing++;
		}
		if (FarmData.playing == 0)
		{
			FarmService.gI().getInventory();
		}
		CRes.rndaaa();
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0003971C File Offset: 0x00037B1C
	public static void addDataBig(short index, short version, sbyte[] data)
	{
		FarmData.playing--;
		FarmData.versionBig[(int)index] = version;
		FarmData.imgBig[(int)index] = CRes.createImgByByteArray(ArrayCast.cast(data));
		FarmData.saveImgBig(data, (int)index);
		FarmData.saveDataBig(FarmData.numImgBig, FarmData.versionBig, FarmData.verImg, FarmData.verPart);
		if (FarmData.playing == 0)
		{
			FarmService.gI().getInventory();
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00039784 File Offset: 0x00037B84
	public static void saveImgBig(sbyte[] imgData, int index)
	{
		try
		{
			RMS.saveRMS("avatarImgBigFarm" + index, imgData);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x000397C8 File Offset: 0x00037BC8
	public static sbyte[] loadImgBig(int index)
	{
		return RMS.loadRMS("avatarImgBigFarm" + index);
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x000397E0 File Offset: 0x00037BE0
	public static void saveVersion()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeInt(FarmData.verPart);
			dataOutputStream.writeInt(FarmData.verImg);
			RMS.saveRMS("avatarVSFarm", dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message + "\n" + ex.StackTrace);
		}
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00039858 File Offset: 0x00037C58
	private static void loadVersion()
	{
		sbyte[] array = RMS.loadRMS("avatarVSFarm");
		if (array == null)
		{
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		try
		{
			FarmData.verPart = dataInputStream.readInt();
			FarmData.verImg = dataInputStream.readInt();
		}
		catch (IOException ex)
		{
			Debug.LogError(ex.Message + "\n" + ex.StackTrace);
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x000398CC File Offset: 0x00037CCC
	private static void readTreeInfo(sbyte[] arr)
	{
		DataInputStream dataInputStream = new DataInputStream(arr);
		short num = dataInputStream.readShort();
		TreeInfo[] array = new TreeInfo[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = new TreeInfo();
			array[i].ID = (short)dataInputStream.readByte();
			array[i].name = dataInputStream.readUTF();
			array[i].name1 = array[i].name.ToLower();
			array[i].Phase = new sbyte[2];
			array[i].Phase[0] = dataInputStream.readByte();
			array[i].Phase[1] = dataInputStream.readByte();
			array[i].harvestTime = dataInputStream.readShort();
			array[i].dieTime = dataInputStream.readShort();
			array[i].priceSeed[0] = dataInputStream.readShort();
			array[i].priceProduct = dataInputStream.readShort();
			array[i].numProduct = dataInputStream.readShort();
			array[i].idImg = new short[8];
			for (int j = 0; j < array[i].idImg.Length; j++)
			{
				array[i].idImg[j] = dataInputStream.readShort();
			}
		}
		short num2 = dataInputStream.readShort();
		FarmData.vatPhamInfo = new Item[(int)num2];
		for (int k = 0; k < (int)num2; k++)
		{
			FarmData.vatPhamInfo[k] = new Item();
			FarmData.vatPhamInfo[k].ID = (short)dataInputStream.readByte();
			FarmData.vatPhamInfo[k].price[0] = (int)dataInputStream.readShort();
		}
		for (int l = 0; l < (int)num; l++)
		{
			array[l].priceSeed[1] = dataInputStream.readShort();
		}
		for (int m = 0; m < (int)num2; m++)
		{
			FarmData.vatPhamInfo[m].price[1] = (int)dataInputStream.readShort();
		}
		short num3 = dataInputStream.readShort();
		FarmData.listAnimalInfo = new MyVector();
		for (int n = 0; n < (int)num3; n++)
		{
			AnimalInfo animalInfo = new AnimalInfo();
			animalInfo.species = dataInputStream.readByte();
			animalInfo.name = dataInputStream.readUTF();
			animalInfo.des = dataInputStream.readUTF();
			animalInfo.price[0] = dataInputStream.readInt();
			animalInfo.price[1] = (int)dataInputStream.readShort();
			animalInfo.harvestTime = (int)dataInputStream.readShort();
			animalInfo.priceProduct = dataInputStream.readShort();
			for (int num4 = 0; num4 < 3; num4++)
			{
				animalInfo.idImg[num4] = dataInputStream.readShort();
			}
			animalInfo.frame = dataInputStream.readByte();
			for (int num5 = 0; num5 < 3; num5++)
			{
				for (int num6 = 0; num6 < 12; num6++)
				{
					animalInfo.arrFrame[num5][num6] = dataInputStream.readByte();
				}
			}
			animalInfo.area = dataInputStream.readByte();
			animalInfo.iconID = dataInputStream.readShort();
			animalInfo.iconProduct = dataInputStream.readShort();
			animalInfo.iconO = dataInputStream.readShort();
			FarmData.listAnimalInfo.addElement(animalInfo);
		}
		FarmData.listItemFarm = new MyVector();
		sbyte b = dataInputStream.readByte();
		for (int num7 = 0; num7 < (int)b; num7++)
		{
			FarmItem farmItem = new FarmItem();
			farmItem.isItem = true;
			farmItem.ID = dataInputStream.readShort();
			farmItem.IDImg = dataInputStream.readShort();
			farmItem.type = dataInputStream.readByte();
			farmItem.action = dataInputStream.readByte();
			farmItem.des = dataInputStream.readUTF();
			farmItem.priceXu = (int)dataInputStream.readShort();
			farmItem.priceLuong = (int)dataInputStream.readShort();
			FarmData.listItemFarm.addElement(farmItem);
		}
		sbyte b2 = dataInputStream.readByte();
		for (int num8 = 0; num8 < (int)b2; num8++)
		{
			FarmItem farmItem2 = new FarmItem();
			farmItem2.isItem = false;
			farmItem2.ID = dataInputStream.readShort();
			farmItem2.IDImg = dataInputStream.readShort();
			farmItem2.des = dataInputStream.readUTF();
			farmItem2.priceXu = (int)dataInputStream.readShort();
			farmItem2.priceLuong = (int)dataInputStream.readShort();
		}
		short num9 = (short)dataInputStream.readByte();
		TreeInfo[] array2 = new TreeInfo[(int)num9];
		for (int num10 = 0; num10 < (int)num9; num10++)
		{
			array2[num10] = new TreeInfo();
			array2[num10].isDynamic = true;
			array2[num10].ID = dataInputStream.readShort();
			array2[num10].name = dataInputStream.readUTF();
			array2[num10].name1 = array2[num10].name.ToLower();
			array2[num10].harvestTime = dataInputStream.readShort();
			array2[num10].priceSeed[0] = dataInputStream.readShort();
			array2[num10].priceSeed[1] = dataInputStream.readShort();
			array2[num10].productID = dataInputStream.readShort();
			array2[num10].numProduct = dataInputStream.readShort();
			array2[num10].lv = dataInputStream.readByte();
			array2[num10].idImg = new short[8];
			for (int num11 = 0; num11 < array2[num10].idImg.Length; num11++)
			{
				array2[num10].idImg[num11] = dataInputStream.readShort();
			}
		}
		short num12 = dataInputStream.readShort();
		for (int num13 = 0; num13 < (int)num12; num13++)
		{
			Food food = new Food();
			food.ID = dataInputStream.readShort();
			food.text = dataInputStream.readUTF();
			food.productID = dataInputStream.readShort();
			food.cookTime = dataInputStream.readShort();
			short num14 = dataInputStream.readShort();
			food.material = new short[(int)num14];
			food.numberMaterial = new short[(int)num14];
			for (int num15 = 0; num15 < (int)num14; num15++)
			{
				food.material[num15] = dataInputStream.readShort();
				food.numberMaterial[num15] = dataInputStream.readShort();
			}
			FarmData.listFood.addElement(food);
		}
		sbyte b3 = dataInputStream.readByte();
		for (int num16 = 0; num16 < (int)b3; num16++)
		{
			FarmItem farmItem3 = new FarmItem();
			farmItem3.isItem = false;
			farmItem3.ID = dataInputStream.readShort();
			farmItem3.IDImg = dataInputStream.readShort();
			farmItem3.des = dataInputStream.readUTF();
			farmItem3.priceXu = dataInputStream.readInt();
			farmItem3.priceLuong = dataInputStream.readInt();
			FarmData.listItemFarm.addElement(farmItem3);
		}
		FarmData.treeInfo = new TreeInfo[(int)(num + num9)];
		for (int num17 = 0; num17 < (int)num; num17++)
		{
			FarmData.treeInfo[num17] = array[num17];
		}
		for (int num18 = (int)num; num18 < (int)(num9 + num); num18++)
		{
			FarmData.treeInfo[num18] = array2[num18 - (int)num];
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00039FA8 File Offset: 0x000383A8
	public static void saveTreeInfo(sbyte[] arr)
	{
		FarmData.playing--;
		FarmData.readTreeInfo(arr);
		RMS.saveRMS("avatarTreeInfoFarm", arr);
		if (FarmData.playing == 0)
		{
			FarmService.gI().getInventory();
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00039FDC File Offset: 0x000383DC
	public static bool loadTreeInfo()
	{
		sbyte[] array = RMS.loadRMS("avatarTreeInfoFarm");
		if (array == null)
		{
			return false;
		}
		try
		{
			FarmData.readTreeInfo(array);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message + "\n" + ex.StackTrace);
		}
		return true;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0003A03C File Offset: 0x0003843C
	private static void readImageData(sbyte[] arr)
	{
		DataInputStream dataInputStream = new DataInputStream(arr);
		short num = dataInputStream.readShort();
		MyVector myVector = new MyVector();
		int num2 = 0;
		for (int i = 0; i < (int)num; i++)
		{
			ImageInfo imageInfo = new ImageInfo();
			imageInfo.ID = dataInputStream.readShort();
			if ((int)imageInfo.ID > num2)
			{
				num2 = (int)imageInfo.ID;
			}
			imageInfo.bigID = dataInputStream.readShort();
			imageInfo.x0 = (short)dataInputStream.readByte();
			imageInfo.y0 = (short)dataInputStream.readByte();
			imageInfo.w = (short)dataInputStream.readByte();
			imageInfo.h = (short)dataInputStream.readByte();
			myVector.addElement(imageInfo);
		}
		FarmData.listImgInfo = new ImageInfo[num2 + 1];
		for (int j = 0; j < (int)num; j++)
		{
			ImageInfo imageInfo2 = (ImageInfo)myVector.elementAt(j);
			FarmData.listImgInfo[(int)imageInfo2.ID] = imageInfo2;
		}
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0003A12C File Offset: 0x0003852C
	public static void saveImageData(sbyte[] arr)
	{
		FarmData.playing--;
		FarmData.readImageData(arr);
		RMS.saveRMS("avatarImgFarm", arr);
		if (FarmData.playing == 0)
		{
			FarmService.gI().getInventory();
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0003A160 File Offset: 0x00038560
	public static bool loadImageData()
	{
		sbyte[] array = RMS.loadRMS("avatarImgFarm");
		if (array == null)
		{
			return false;
		}
		try
		{
			FarmData.readImageData(array);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message + "\n" + ex.StackTrace);
		}
		return true;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0003A1C0 File Offset: 0x000385C0
	public static void saveDataBig(sbyte number1, short[] version, int verImg, int verPart)
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(number1);
			dataOutputStream.writeInt(verImg);
			dataOutputStream.writeInt(verPart);
			for (int i = 0; i < (int)number1; i++)
			{
				dataOutputStream.writeShort(version[i]);
			}
			sbyte[] data = dataOutputStream.toByteArray();
			try
			{
				RMS.saveRMS("avatarDataFarm", data);
			}
			catch (Exception e)
			{
				Out.logError(e);
			}
			dataOutputStream.close();
		}
		catch (IOException e2)
		{
			Out.logError(e2);
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0003A25C File Offset: 0x0003865C
	public static bool loadDataBig()
	{
		DataInputStream dataInputStream = AvatarData.initLoad("avatarDataFarm");
		if (dataInputStream == null)
		{
			return false;
		}
		try
		{
			FarmData.numImgBig = dataInputStream.readByte();
			FarmData.verImg = dataInputStream.readInt();
			FarmData.verPart = dataInputStream.readInt();
			FarmData.versionBig = new short[(int)FarmData.numImgBig];
			for (int i = 0; i < (int)FarmData.numImgBig; i++)
			{
				FarmData.versionBig[i] = dataInputStream.readShort();
			}
			dataInputStream.close();
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		return true;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0003A2FC File Offset: 0x000386FC
	public static Item getVPbyID(int id)
	{
		for (int i = 0; i < FarmData.vatPhamInfo.Length; i++)
		{
			if ((int)FarmData.vatPhamInfo[i].ID == id)
			{
				return FarmData.vatPhamInfo[i];
			}
		}
		return null;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0003A33C File Offset: 0x0003873C
	public static TreeInfo getTreeByID(int id)
	{
		for (int i = 0; i < FarmData.treeInfo.Length; i++)
		{
			if (id == (int)FarmData.treeInfo[i].ID)
			{
				return FarmData.treeInfo[i];
			}
		}
		return null;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0003A37C File Offset: 0x0003877C
	public static AnimalInfo getAnimalByID(int id)
	{
		int num = FarmData.listAnimalInfo.size();
		for (int i = 0; i < num; i++)
		{
			AnimalInfo animalInfo = (AnimalInfo)FarmData.listAnimalInfo.elementAt(i);
			if ((int)animalInfo.species == id)
			{
				return animalInfo;
			}
		}
		return null;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0003A3C8 File Offset: 0x000387C8
	public static TreeInfo getTreeInfoByID(int id)
	{
		for (int i = 0; i < FarmData.treeInfo.Length; i++)
		{
			if ((int)FarmData.treeInfo[i].ID == id)
			{
				return FarmData.treeInfo[i];
			}
		}
		return null;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0003A408 File Offset: 0x00038808
	public static void paintImg(MyGraphics g, int id, int x, int y, int anthor)
	{
		if (FarmData.getImgIcon((short)id).count != -1)
		{
			g.drawImage(FarmData.getImgIcon((short)id).img, (float)x, (float)y, anthor);
		}
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0003A434 File Offset: 0x00038834
	public static ImageIcon getImgIcon(short id)
	{
		ImageIcon imageIcon = (ImageIcon)FarmData.listImgIcon.get(string.Empty + id);
		if (imageIcon == null)
		{
			imageIcon = new ImageIcon();
			FarmData.listImgIcon.put(string.Empty + id, imageIcon);
			FarmService.gI().doGetImgIcon(id);
		}
		else if (imageIcon.count >= 0)
		{
			imageIcon.count = Environment.TickCount / 1000;
		}
		return imageIcon;
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0003A4B8 File Offset: 0x000388B8
	public static void setLimitImage()
	{
		if (FarmData.listImgIcon.size() > 50)
		{
			IDictionaryEnumerator enumerator = FarmData.listImgIcon.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					ImageIcon imageIcon = (ImageIcon)dictionaryEntry.Value;
					if (imageIcon.count != -1 && Canvas.getTick() / 1000L - (long)imageIcon.count > 200L)
					{
						string key = (string)dictionaryEntry.Key;
						FarmData.listImgIcon.h.Remove(key);
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0003A584 File Offset: 0x00038984
	public static Food getFoodByID(short id)
	{
		for (int i = 0; i < FarmData.listFood.size(); i++)
		{
			Food food = (Food)FarmData.listFood.elementAt(i);
			if (food.ID == id)
			{
				return food;
			}
		}
		return null;
	}

	// Token: 0x0400084B RID: 2123
	public const string dataBigFarm = "avatarDataFarm";

	// Token: 0x0400084C RID: 2124
	public const string imageData = "avatarImgFarm";

	// Token: 0x0400084D RID: 2125
	public const string sTreeInfoFarm = "avatarTreeInfoFarm";

	// Token: 0x0400084E RID: 2126
	public const string sImgBigFarm = "avatarImgBigFarm";

	// Token: 0x0400084F RID: 2127
	public const string sVer = "avatarVSFarm";

	// Token: 0x04000850 RID: 2128
	public static sbyte numImgBig;

	// Token: 0x04000851 RID: 2129
	public static short[] versionBig;

	// Token: 0x04000852 RID: 2130
	public static int verImg;

	// Token: 0x04000853 RID: 2131
	public static int verPart;

	// Token: 0x04000854 RID: 2132
	public static ImageInfo[] listImgInfo;

	// Token: 0x04000855 RID: 2133
	public static TreeInfo[] treeInfo;

	// Token: 0x04000856 RID: 2134
	public static Image[] imgBig;

	// Token: 0x04000857 RID: 2135
	public static Item[] vatPhamInfo;

	// Token: 0x04000858 RID: 2136
	public static MyVector listAnimalInfo = new MyVector();

	// Token: 0x04000859 RID: 2137
	public static MyVector listItemFarm = new MyVector();

	// Token: 0x0400085A RID: 2138
	public static MyVector listFood = new MyVector();

	// Token: 0x0400085B RID: 2139
	public static int playing = -1;

	// Token: 0x0400085C RID: 2140
	public static MyHashTable listImgIcon = new MyHashTable();
}
