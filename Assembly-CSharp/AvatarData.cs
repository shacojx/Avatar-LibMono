using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class AvatarData
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x000353D2 File Offset: 0x000337D2
	public static void delRMS()
	{
		RMS.deleteAll();
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x000353DC File Offset: 0x000337DC
	public void checkDataAvatar(MyVector bigInfo, int verBigImg, int verPart2, int verBigItemImg, int vItemType, int vItem, int vObj)
	{
		Out.println("checkDataAvatar");
		try
		{
			AvatarData.playing = 0;
			AvatarData.loadVersion();
			if (!AvatarData.loadImgBig())
			{
				AvatarData.bigImgInfo = bigInfo;
				int num = bigInfo.size();
				for (int i = 0; i < num; i++)
				{
					BigImgInfo bigImgInfo = (BigImgInfo)bigInfo.elementAt(i);
					AvatarService.gI().getBigImage(bigImgInfo.id);
					AvatarData.playing++;
				}
			}
			else
			{
				int num2 = bigInfo.size();
				for (int j = 0; j < num2; j++)
				{
					BigImgInfo bigImgInfo2 = (BigImgInfo)bigInfo.elementAt(j);
					BigImgInfo bigImgInfoList = AvatarData.getBigImgInfoList((int)bigImgInfo2.id);
					if (bigImgInfoList == null)
					{
						AvatarData.bigImgInfo.addElement(bigImgInfo2);
						AvatarService.gI().getBigImage(bigImgInfo2.id);
						AvatarData.playing++;
					}
					else if (bigImgInfo2.ver != bigImgInfoList.ver)
					{
						AvatarService.gI().getBigImage(bigImgInfo2.id);
						AvatarData.playing++;
					}
				}
			}
			if (!AvatarData.loadImageData())
			{
				AvatarData.verImg = verBigImg;
				AvatarService.gI().getImageData();
				AvatarData.playing++;
			}
			else if (AvatarData.verImg != verBigImg)
			{
				AvatarData.verImg = verBigImg;
				AvatarService.gI().getImageData();
				AvatarData.playing++;
			}
			if (!AvatarData.loadAvatarPart())
			{
				AvatarData.verPart = verPart2;
				AvatarService.gI().getAvatarPart();
				AvatarData.playing++;
			}
			else if (AvatarData.verPart != verPart2)
			{
				AvatarData.verPart = verPart2;
				AvatarService.gI().getAvatarPart();
				AvatarData.playing++;
			}
			else
			{
				AvatarData.setFollowAvatarPart();
			}
			if (!AvatarData.loadItemInfo())
			{
				AvatarData.verItemImg = verBigItemImg;
				AvatarService.gI().getItemInfo();
				AvatarData.playing++;
			}
			else if (AvatarData.verItemImg != verBigItemImg)
			{
				AvatarData.verItemImg = verBigItemImg;
				AvatarService.gI().getItemInfo();
				AvatarData.playing++;
			}
			if (!AvatarData.loadMapItemType())
			{
				AvatarData.verItemType = vItemType;
				AvatarService.gI().getMapItemType();
				AvatarData.playing++;
			}
			else if (AvatarData.verItemType != vItemType)
			{
				AvatarData.verItemType = vItemType;
				AvatarService.gI().getMapItemType();
				AvatarData.playing++;
			}
			if (!AvatarData.loadMapItem())
			{
				AvatarData.verItem = vItem;
				AvatarService.gI().getMapItem();
				AvatarData.playing++;
			}
			else if (AvatarData.verItem != vItem)
			{
				AvatarData.verItem = vItem;
				AvatarService.gI().getMapItem();
				AvatarData.playing++;
			}
			AvatarData.setPlaying();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x000356D8 File Offset: 0x00033AD8
	public static void addDataBig(BigImgInfo big)
	{
		AvatarData.playing--;
		int num = AvatarData.bigImgInfo.size();
		for (int i = 0; i < num; i++)
		{
			BigImgInfo bigImgInfo = (BigImgInfo)AvatarData.bigImgInfo.elementAt(i);
			if (bigImgInfo.id == big.id)
			{
				bigImgInfo.data = big.data;
				bigImgInfo.ver = big.ver;
				bigImgInfo.follow = big.follow;
				break;
			}
		}
		AvatarData.setPlaying();
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00035760 File Offset: 0x00033B60
	public static void saveImgBig()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeShort((short)AvatarData.bigImgInfo.size());
			for (int i = 0; i < AvatarData.bigImgInfo.size(); i++)
			{
				BigImgInfo bigImgInfo = (BigImgInfo)AvatarData.bigImgInfo.elementAt(i);
				dataOutputStream.writeShort(bigImgInfo.id);
				dataOutputStream.writeShort(bigImgInfo.follow);
				dataOutputStream.writeInt(bigImgInfo.data.Length);
				dataOutputStream.write(bigImgInfo.data);
				dataOutputStream.writeShort(bigImgInfo.ver);
			}
			RMS.saveRMS(AvatarData.sImgBig, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00035828 File Offset: 0x00033C28
	public static bool loadImgBig()
	{
		DataInputStream dataInputStream = AvatarData.initLoad(AvatarData.sImgBig);
		if (dataInputStream == null)
		{
			return false;
		}
		try
		{
			int num = (int)dataInputStream.readShort();
			AvatarData.bigImgInfo = new MyVector();
			for (int i = 0; i < num; i++)
			{
				BigImgInfo bigImgInfo = new BigImgInfo();
				bigImgInfo.id = dataInputStream.readShort();
				bigImgInfo.follow = dataInputStream.readShort();
				int num2 = dataInputStream.readInt();
				bigImgInfo.data = new sbyte[num2];
				dataInputStream.read(ref bigImgInfo.data);
				bigImgInfo.ver = dataInputStream.readShort();
				AvatarData.bigImgInfo.addElement(bigImgInfo);
			}
			dataInputStream.close();
		}
		catch (Exception ex)
		{
			AvatarData.delErrorRms(AvatarData.sImgBig);
		}
		return true;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000358F0 File Offset: 0x00033CF0
	private static Part[] setArrayPart(MyVector list)
	{
		int num = 0;
		for (int i = 0; i < list.size(); i++)
		{
			Part part = (Part)list.elementAt(i);
			if ((int)part.IDPart > num)
			{
				num = (int)part.IDPart;
			}
		}
		Part[] array = new Part[num + 1];
		for (int j = 0; j < list.size(); j++)
		{
			Part part2 = (Part)list.elementAt(j);
			array[(int)part2.IDPart] = part2;
		}
		return array;
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00035978 File Offset: 0x00033D78
	public static MyVector readAvatarPart(sbyte[] array, bool isSimple)
	{
		DataInputStream dataInputStream = new DataInputStream(array);
		short num = 1;
		if (!isSimple)
		{
			num = dataInputStream.readShort();
		}
		MyVector myVector = new MyVector();
		for (int i = 0; i < (int)num; i++)
		{
			short idpart = dataInputStream.readShort();
			int num2 = dataInputStream.readInt();
			int num3 = (int)dataInputStream.readShort();
			short num4 = dataInputStream.readShort();
			if (num4 == -2)
			{
				PartSmall partSmall = new PartSmall();
				partSmall.IDPart = idpart;
				partSmall.price[0] = num2;
				partSmall.price[1] = num3;
				partSmall.follow = num4;
				partSmall.name = dataInputStream.readUTF();
				partSmall.sell = dataInputStream.readByte();
				partSmall.idIcon = dataInputStream.readShort();
				myVector.addElement(partSmall);
			}
			else if (num4 == -1)
			{
				APartInfo apartInfo = new APartInfo();
				apartInfo.IDPart = idpart;
				apartInfo.price[0] = num2;
				apartInfo.price[1] = num3;
				apartInfo.follow = num4;
				apartInfo.name = dataInputStream.readUTF();
				apartInfo.sell = dataInputStream.readByte();
				apartInfo.zOrder = dataInputStream.readByte();
				apartInfo.gender = dataInputStream.readByte();
				apartInfo.level = dataInputStream.readByte();
				apartInfo.idIcon = dataInputStream.readShort();
				apartInfo.imgID = new short[15];
				apartInfo.dx = new sbyte[15];
				apartInfo.dy = new sbyte[15];
				for (int j = 0; j < 15; j++)
				{
					apartInfo.imgID[j] = dataInputStream.readShort();
					apartInfo.dx[j] = dataInputStream.readByte();
					apartInfo.dy[j] = dataInputStream.readByte();
				}
				myVector.addElement(apartInfo);
			}
			else
			{
				PartFollow partFollow = new PartFollow();
				partFollow.IDPart = idpart;
				partFollow.price[0] = num2;
				partFollow.price[1] = num3;
				partFollow.follow = num4;
				partFollow.color = dataInputStream.readShort();
				myVector.addElement(partFollow);
			}
		}
		return myVector;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00035B87 File Offset: 0x00033F87
	public static void saveAvatarPart(sbyte[] arr)
	{
		AvatarData.playing--;
		AvatarData.listPart = AvatarData.setArrayPart(AvatarData.readAvatarPart(arr, false));
		RMS.saveRMS(AvatarData.sAvaPart, arr);
		AvatarData.setFollowAvatarPart();
		AvatarData.setPlaying();
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00035BBC File Offset: 0x00033FBC
	public static bool loadAvatarPart()
	{
		sbyte[] array = RMS.loadRMS(AvatarData.sAvaPart);
		if (array == null)
		{
			return false;
		}
		AvatarData.listPart = AvatarData.setArrayPart(AvatarData.readAvatarPart(array, false));
		return true;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00035BF0 File Offset: 0x00033FF0
	private static void saveVersion()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeInt(AvatarData.verPart);
			dataOutputStream.writeInt(AvatarData.verItemType);
			dataOutputStream.writeInt(AvatarData.verImg);
			dataOutputStream.writeInt(AvatarData.verItemImg);
			dataOutputStream.writeInt(AvatarData.verItem);
			RMS.saveRMS(AvatarData.sVer, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00035C6C File Offset: 0x0003406C
	private static void loadVersion()
	{
		sbyte[] array = RMS.loadRMS(AvatarData.sVer);
		if (array == null)
		{
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		AvatarData.verPart = dataInputStream.readInt();
		AvatarData.verItemType = dataInputStream.readInt();
		AvatarData.verImg = dataInputStream.readInt();
		AvatarData.verItemImg = dataInputStream.readInt();
		AvatarData.verItem = dataInputStream.readInt();
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00035CCC File Offset: 0x000340CC
	public static void setFollowAvatarPart()
	{
		for (int i = 0; i < AvatarData.listPart.Length; i++)
		{
			if (AvatarData.listPart[i].follow >= 0)
			{
				Part part = AvatarData.listPart[(int)AvatarData.listPart[i].follow];
				Part part2 = AvatarData.listPart[i];
				part2.name = part.name;
				part2.sell = part.sell;
				part2.zOrder = part.zOrder;
				part2.idIcon = part.idIcon;
			}
		}
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00035D50 File Offset: 0x00034150
	private static void readItemDataInfo(sbyte[] arr)
	{
		DataInputStream dataInputStream = new DataInputStream(arr);
		short num = dataInputStream.readShort();
		AvatarData.listItemInfo = new MyVector();
		for (int i = 0; i < (int)num; i++)
		{
			Item item = new Item();
			item.ID = dataInputStream.readShort();
			item.name = dataInputStream.readUTF();
			item.des = dataInputStream.readUTF();
			item.price[0] = dataInputStream.readInt();
			item.shopType = dataInputStream.readByte();
			item.idIcon = dataInputStream.readShort();
			AvatarData.listItemInfo.addElement(item);
		}
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00035DE2 File Offset: 0x000341E2
	public static void saveItemData(sbyte[] arr)
	{
		AvatarData.playing--;
		AvatarData.readItemDataInfo(arr);
		RMS.saveRMS(AvatarData.sItemInfo, arr);
		AvatarData.setPlaying();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00035E08 File Offset: 0x00034208
	public static bool loadItemInfo()
	{
		sbyte[] array = RMS.loadRMS(AvatarData.sItemInfo);
		if (array == null)
		{
			return false;
		}
		AvatarData.readItemDataInfo(array);
		return true;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00035E30 File Offset: 0x00034230
	private static ImageInfo[] readImageData(sbyte[] arr)
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
			imageInfo.x0 = (short)dataInputStream.readUnsignedByte();
			imageInfo.y0 = (short)dataInputStream.readUnsignedByte();
			imageInfo.w = (short)dataInputStream.readByte();
			imageInfo.h = (short)dataInputStream.readByte();
			myVector.addElement(imageInfo);
		}
		ImageInfo[] array = new ImageInfo[num2 + 1];
		for (int j = 0; j < myVector.size(); j++)
		{
			ImageInfo imageInfo2 = (ImageInfo)myVector.elementAt(j);
			array[(int)imageInfo2.ID] = imageInfo2;
		}
		return array;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00035F21 File Offset: 0x00034321
	public static void saveImageData(sbyte[] arr)
	{
		AvatarData.playing--;
		AvatarData.listImgInfo = AvatarData.readImageData(arr);
		RMS.saveRMS(AvatarData.sImage, arr);
		AvatarData.setPlaying();
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00035F4C File Offset: 0x0003434C
	public static bool loadImageData()
	{
		sbyte[] array = RMS.loadRMS(AvatarData.sImage);
		if (array == null)
		{
			return false;
		}
		AvatarData.listImgInfo = AvatarData.readImageData(array);
		return true;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00035F78 File Offset: 0x00034378
	private static MyVector readMapItemType(sbyte[] arr)
	{
		DataInputStream dataInputStream = new DataInputStream(arr);
		short num = dataInputStream.readShort();
		MyVector myVector = new MyVector();
		sbyte b = 0;
		while ((int)b < (int)num)
		{
			MapItemType mapItemType = new MapItemType();
			mapItemType.idType = dataInputStream.readShort();
			mapItemType.name = dataInputStream.readUTF();
			mapItemType.des = dataInputStream.readUTF();
			mapItemType.imgID = dataInputStream.readShort();
			mapItemType.iconID = dataInputStream.readShort();
			mapItemType.dx = (short)dataInputStream.readByte();
			mapItemType.dy = (short)dataInputStream.readByte();
			mapItemType.priceXu = (int)dataInputStream.readShort();
			if (mapItemType.priceXu == 32767)
			{
				mapItemType.priceXu = -1;
			}
			if (mapItemType.priceXu > -1)
			{
				mapItemType.priceXu *= 1000;
			}
			mapItemType.priceLuong = dataInputStream.readShort();
			mapItemType.buy = dataInputStream.readByte();
			mapItemType.listNotTrans = new MyVector();
			sbyte b2 = dataInputStream.readByte();
			sbyte b3 = 0;
			while ((int)b3 < (int)b2)
			{
				AvPosition avPosition = new AvPosition();
				avPosition.x = (int)dataInputStream.readByte();
				avPosition.y = (int)dataInputStream.readByte();
				mapItemType.listNotTrans.addElement(avPosition);
				b3 = (sbyte)((int)b3 + 1);
			}
			myVector.addElement(mapItemType);
			b = (sbyte)((int)b + 1);
		}
		return myVector;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000360DB File Offset: 0x000344DB
	public static void saveMapItemType(sbyte[] arr)
	{
		AvatarData.playing--;
		AvatarData.listMapItemType.removeAllElements();
		AvatarData.listMapItemType = AvatarData.readMapItemType(arr);
		RMS.saveRMS(AvatarData.sMapItemType, arr);
		AvatarData.setPlaying();
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00036110 File Offset: 0x00034510
	public static bool loadMapItemType()
	{
		sbyte[] array = RMS.loadRMS(AvatarData.sMapItemType);
		if (array == null)
		{
			return false;
		}
		AvatarData.listMapItemType = AvatarData.readMapItemType(array);
		return true;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0003613C File Offset: 0x0003453C
	public static void loadMyAccount()
	{
		sbyte[] array = RMS.loadRMS("my_account");
		if (array == null)
		{
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		Canvas.user = dataInputStream.readUTF();
		Canvas.pass = dataInputStream.readUTF();
		ServerListScr.selected = (int)dataInputStream.readByte();
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00036184 File Offset: 0x00034584
	public static void saveMyAccount()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF(Canvas.user);
			dataOutputStream.writeUTF(Canvas.pass);
			dataOutputStream.writeByte(ServerListScr.selected);
			RMS.saveRMS("my_account", dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x000361F0 File Offset: 0x000345F0
	private static void readMapItem(sbyte[] arr)
	{
		DataInputStream dataInputStream = new DataInputStream(arr);
		AvatarData.listMapItem = new MyVector();
		short num = dataInputStream.readShort();
		byte b = 0;
		while ((short)b < num)
		{
			MapItem mapItem = new MapItem();
			mapItem.ID = dataInputStream.readShort();
			mapItem.typeID = dataInputStream.readShort();
			mapItem.type = (int)dataInputStream.readByte();
			mapItem.x = (int)dataInputStream.readByte();
			mapItem.y = (int)dataInputStream.readByte();
			AvatarData.listMapItem.addElement(mapItem);
			b += 1;
		}
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00036278 File Offset: 0x00034678
	public static void saveMapItem(sbyte[] arr)
	{
		AvatarData.playing--;
		AvatarData.listMapItem.removeAllElements();
		AvatarData.readMapItem(arr);
		RMS.saveRMS(AvatarData.sMapType, arr);
		AvatarData.setPlaying();
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x000362A8 File Offset: 0x000346A8
	public static bool loadMapItem()
	{
		sbyte[] array = RMS.loadRMS(AvatarData.sMapType);
		if (array == null)
		{
			return false;
		}
		AvatarData.readMapItem(array);
		return true;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x000362D0 File Offset: 0x000346D0
	public static void setPlaying()
	{
		if (AvatarData.playing != 0)
		{
			return;
		}
		AvatarData.saveVersion();
		AvatarData.saveImgBig();
		int num = AvatarData.bigImgInfo.size();
		for (int i = 0; i < num; i++)
		{
			BigImgInfo bigImgInfo = (BigImgInfo)AvatarData.bigImgInfo.elementAt(i);
			if (bigImgInfo.follow != -1)
			{
				sbyte[] data = AvatarData.getBigImgInfoList((int)bigImgInfo.follow).data;
				Array.Copy(bigImgInfo.data, 0, data, 0, bigImgInfo.data.Length);
				bigImgInfo.data = data;
			}
			bigImgInfo.img = CRes.createImgByByteArray(ArrayCast.cast(bigImgInfo.data));
		}
		for (int j = 0; j < AvatarData.bigImgInfo.size(); j++)
		{
			BigImgInfo bigImgInfo2 = (BigImgInfo)AvatarData.bigImgInfo.elementAt(j);
			bigImgInfo2.data = null;
			AvatarData.listBigImg.put(string.Empty + bigImgInfo2.id, bigImgInfo2);
		}
		AvatarData.bigImgInfo.removeAllElements();
		AvatarData.bigImgInfo = null;
		GameMidlet.avatar.orderSeriesPath();
		MapScr.gI().joinCitymap();
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000363F0 File Offset: 0x000347F0
	private static void setBigImgBB(BigImgInfo big)
	{
		Image image = new Image();
		image.w = big.img.getWidth();
		image.h = big.img.getHeight();
		image.texture = new Texture2D(image.w, image.h);
		for (int i = 0; i < AvatarData.listImgInfo.Length; i++)
		{
			if (big.id == AvatarData.listImgInfo[i].bigID)
			{
				Color[] pixels = AvatarData.getBigImgInfo((int)big.id).img.texture.GetPixels((int)AvatarData.listImgInfo[i].x0 * AvMain.hd, (int)AvatarData.listImgInfo[i].y0 * AvMain.hd, (int)AvatarData.listImgInfo[i].w * AvMain.hd, (int)AvatarData.listImgInfo[i].h * AvMain.hd);
				int num = (int)AvatarData.listImgInfo[i].w * AvMain.hd;
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < (int)AvatarData.listImgInfo[i].h * AvMain.hd; k++)
					{
						Color color;
						color..ctor(pixels[k * num + j].a, pixels[k * num + j].b, pixels[k * num + j].g);
						pixels[k * num + j] = new Color(pixels[num - j].a, pixels[num - j].b, pixels[num - j].g);
						pixels[num - j] = new Color(color.a, color.b, color.g);
						pixels[k * num + j] = new Color(0f, 0f, 0f, 0f);
					}
				}
				image.texture.SetPixels((int)AvatarData.listImgInfo[i].x0 * AvMain.hd, (int)AvatarData.listImgInfo[i].y0 * AvMain.hd, (int)AvatarData.listImgInfo[i].w * AvMain.hd, (int)AvatarData.listImgInfo[i].h * AvMain.hd, pixels);
			}
		}
		for (int l = 0; l < AvatarData.listPart.Length; l++)
		{
			if (AvatarData.listPart[l].follow > -1)
			{
				APartInfo apartInfo = (APartInfo)AvatarData.getPart(AvatarData.listPart[l].follow);
				for (int m = 0; m < apartInfo.imgID.Length; m++)
				{
					ImageInfo imageInfo = AvatarData.listImgInfo[(int)apartInfo.imgID[m]];
					if (((PartFollow)AvatarData.listPart[l]).color == big.id)
					{
						Color[] pixels2 = AvatarData.getBigImgInfo((int)big.id).img.texture.GetPixels((int)imageInfo.x0 * AvMain.hd, (int)imageInfo.y0 * AvMain.hd, (int)imageInfo.w * AvMain.hd, (int)imageInfo.h * AvMain.hd);
						image.texture.SetPixels((int)imageInfo.x0 * AvMain.hd, (int)imageInfo.y0 * AvMain.hd, (int)imageInfo.w * AvMain.hd, (int)imageInfo.h * AvMain.hd, pixels2);
					}
				}
			}
		}
		image.texture.Apply(false, false);
		BigImgInfo bigImgInfo = new BigImgInfo();
		bigImgInfo.follow = big.follow;
		bigImgInfo.id = big.id;
		bigImgInfo.img = image;
		bigImgInfo.ver = big.ver;
		AvatarData.listBigImgBB.put(string.Empty + bigImgInfo.id, bigImgInfo);
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x000367D2 File Offset: 0x00034BD2
	public static BigImgInfo getBigImgInfoBB(int id)
	{
		return (BigImgInfo)AvatarData.listBigImgBB.get(string.Empty + id);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x000367F4 File Offset: 0x00034BF4
	public static void paintPart(MyGraphics g, int bigID, int x0, int y0, int w, int h, int x, int y, int direct, int arthor)
	{
		g.drawRegion(AvatarData.getBigImgInfo(bigID).img, (float)(x0 * AvMain.hd), (float)(y0 * AvMain.hd), w * AvMain.hd, h * AvMain.hd, direct, (float)x, (float)y, arthor);
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0003683C File Offset: 0x00034C3C
	public static DataInputStream initLoad(string name)
	{
		sbyte[] array = RMS.loadRMS(name);
		if (array == null)
		{
			return null;
		}
		return new DataInputStream(array);
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0003685E File Offset: 0x00034C5E
	public static void saveIP()
	{
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00036860 File Offset: 0x00034C60
	public static void loadIP()
	{
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00036864 File Offset: 0x00034C64
	public static BigImgInfo getBigImgInfoList(int id)
	{
		int num = AvatarData.bigImgInfo.size();
		for (int i = 0; i < num; i++)
		{
			BigImgInfo bigImgInfo = (BigImgInfo)AvatarData.bigImgInfo.elementAt(i);
			if ((int)bigImgInfo.id == id)
			{
				return bigImgInfo;
			}
		}
		return null;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x000368AE File Offset: 0x00034CAE
	public static BigImgInfo getBigImgInfo(int id)
	{
		return (BigImgInfo)AvatarData.listBigImg.get(string.Empty + id);
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x000368D0 File Offset: 0x00034CD0
	public static MapItemType getMapItemTypeByID(int idType)
	{
		int num = AvatarData.listMapItemType.size();
		for (int i = 0; i < num; i++)
		{
			if ((int)((MapItemType)AvatarData.listMapItemType.elementAt(i)).idType == idType)
			{
				return (MapItemType)AvatarData.listMapItemType.elementAt(i);
			}
		}
		return null;
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00036927 File Offset: 0x00034D27
	public static void onMapAd(MyVector listAd)
	{
		AvatarData.listAd = listAd;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0003692F File Offset: 0x00034D2F
	public static bool isZOrderMain(int zOrder)
	{
		return zOrder == 10 || zOrder == 20 || zOrder == 30 || zOrder == 40 || zOrder == 50;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0003695C File Offset: 0x00034D5C
	public static APartInfo getPartByZ(MyVector seri, int z)
	{
		if (seri != null)
		{
			for (int i = 0; i < seri.size(); i++)
			{
				SeriPart seriPart = (SeriPart)seri.elementAt(i);
				Part part = AvatarData.getPart(seriPart.idPart);
				if (seriPart != null && part is APartInfo && (int)((APartInfo)part).zOrder == z)
				{
					return (APartInfo)part;
				}
			}
		}
		return null;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x000369CC File Offset: 0x00034DCC
	public static SeriPart getSeriByIdPart(MyVector listSeri, int idPart)
	{
		int num = listSeri.size();
		for (int i = 0; i < num; i++)
		{
			SeriPart seriPart = (SeriPart)listSeri.elementAt(i);
			if ((int)seriPart.idPart == idPart)
			{
				return seriPart;
			}
		}
		return null;
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00036A10 File Offset: 0x00034E10
	public static SeriPart getSeriByZ(int zOrder, MyVector listSeri)
	{
		int num = listSeri.size();
		for (int i = 0; i < num; i++)
		{
			SeriPart seriPart = (SeriPart)listSeri.elementAt(i);
			Part part = AvatarData.getPart(seriPart.idPart);
			if ((int)part.zOrder == zOrder)
			{
				return seriPart;
			}
		}
		return null;
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00036A60 File Offset: 0x00034E60
	public static Part getPartDinamic(short idPart)
	{
		Part part = (Part)AvatarData.listPartDynamic.get(string.Empty + idPart);
		if (part == null)
		{
			part = new APartInfo();
			part.IDPart = -1;
			AvatarData.listPartDynamic.put(string.Empty + idPart, part);
			GlobalService.gI().requestPartDynaMic(idPart);
		}
		return part;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00036AC7 File Offset: 0x00034EC7
	public static Part getPart(short idPart)
	{
		if (idPart >= 2000)
		{
			return AvatarData.getPartDinamic(idPart);
		}
		return AvatarData.listPart[(int)idPart];
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00036AE2 File Offset: 0x00034EE2
	public static string getName(Part part)
	{
		if (part.follow >= 0)
		{
			return AvatarData.getPart(part.follow).name;
		}
		return part.name;
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00036B07 File Offset: 0x00034F07
	public static void paintImg(MyGraphics g, int id, int x, int y, int anthor)
	{
		if (AvatarData.getImgIcon((short)id).count != -1)
		{
			g.drawImage(AvatarData.getImgIcon((short)id).img, (float)x, (float)y, anthor);
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00036B34 File Offset: 0x00034F34
	public static ImageIcon getImagePart(short id)
	{
		ImageIcon imageIcon = (ImageIcon)AvatarData.listImgPart.get(string.Empty + id);
		if (imageIcon == null)
		{
			imageIcon = new ImageIcon();
			AvatarData.listImgPart.put(string.Empty + id, imageIcon);
			GlobalService.gI().requestImagePart(id);
		}
		else if (imageIcon.count >= 0)
		{
			imageIcon.count = Environment.TickCount / 1000;
		}
		return imageIcon;
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00036BB8 File Offset: 0x00034FB8
	public static ImageIcon getImgIcon(short id)
	{
		ImageIcon imageIcon = (ImageIcon)AvatarData.listImgIcon.get(string.Empty + id);
		if (imageIcon == null)
		{
			imageIcon = new ImageIcon();
			AvatarData.listImgIcon.put(string.Empty + id, imageIcon);
			AvatarService.gI().doGetImgIcon(id);
		}
		else if (imageIcon.count >= 0)
		{
			imageIcon.count = Environment.TickCount / 1000;
		}
		return imageIcon;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00036C3C File Offset: 0x0003503C
	public static void setLimitImage()
	{
		try
		{
			if (AvatarData.listImgIcon.size() > 5)
			{
				IDictionaryEnumerator enumerator = AvatarData.listImgIcon.GetEnumerator();
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
							AvatarData.listImgIcon.h.Remove(key);
							return;
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
			if (AvatarData.listImgPart.size() > 5)
			{
				IDictionaryEnumerator enumerator2 = AvatarData.listImgPart.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						ImageIcon imageIcon2 = (ImageIcon)dictionaryEntry2.Value;
						if (imageIcon2.count != -1 && Environment.TickCount / 1000 - imageIcon2.count > 200)
						{
							string k = (string)dictionaryEntry2.Key;
							AvatarData.listImgPart.remove(k);
						}
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator2 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00036DF8 File Offset: 0x000351F8
	public static int getLevel(Part part)
	{
		int result;
		if (part.follow >= 0)
		{
			result = (int)((APartInfo)AvatarData.getPart(part.follow)).level;
		}
		else
		{
			result = (int)((APartInfo)part).level;
		}
		return result;
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00036E40 File Offset: 0x00035240
	public static EffectData getEffect(short id)
	{
		for (int i = 0; i < AvatarData.effectList.size(); i++)
		{
			EffectData effectData = (EffectData)AvatarData.effectList.elementAt(i);
			if (effectData.ID == id)
			{
				return effectData;
			}
		}
		return null;
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00036E88 File Offset: 0x00035288
	public static void delErrorRms(string path)
	{
		try
		{
			PlayerPrefs.DeleteKey("2.5.8" + path);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x040007E0 RID: 2016
	public static string sAvaPart = "avatarPart";

	// Token: 0x040007E1 RID: 2017
	public static string sImage = "avatarImgData";

	// Token: 0x040007E2 RID: 2018
	public static string sData = "avatarData";

	// Token: 0x040007E3 RID: 2019
	public static string sItemInfo = "avatarItemInfo";

	// Token: 0x040007E4 RID: 2020
	public static string sImgBig = "avatarImgBig";

	// Token: 0x040007E5 RID: 2021
	public static string sMapItemType = "avatarMapItemType";

	// Token: 0x040007E6 RID: 2022
	public static string sMapType = "avatarMapType";

	// Token: 0x040007E7 RID: 2023
	public static string sObject = "avatarObject";

	// Token: 0x040007E8 RID: 2024
	public static string sTile = "avatarTile";

	// Token: 0x040007E9 RID: 2025
	public static string sVer = "avatarVs";

	// Token: 0x040007EA RID: 2026
	public static int verImg;

	// Token: 0x040007EB RID: 2027
	public static int verPart;

	// Token: 0x040007EC RID: 2028
	public static int verItemImg;

	// Token: 0x040007ED RID: 2029
	public static int verObj;

	// Token: 0x040007EE RID: 2030
	public static ImageInfo[] listImgInfo;

	// Token: 0x040007EF RID: 2031
	public static Part[] listPart;

	// Token: 0x040007F0 RID: 2032
	public static MyVector listItemInfo;

	// Token: 0x040007F1 RID: 2033
	private static MyVector bigImgInfo = new MyVector();

	// Token: 0x040007F2 RID: 2034
	public static MyHashTable listBigImg = new MyHashTable();

	// Token: 0x040007F3 RID: 2035
	public static MyHashTable listBigImgBB;

	// Token: 0x040007F4 RID: 2036
	public static int playing = -1;

	// Token: 0x040007F5 RID: 2037
	public static MyVector listMapItemType = new MyVector();

	// Token: 0x040007F6 RID: 2038
	public static int verItemType;

	// Token: 0x040007F7 RID: 2039
	public static int verItem;

	// Token: 0x040007F8 RID: 2040
	public static MyVector listMapItem = new MyVector();

	// Token: 0x040007F9 RID: 2041
	public static MyVector listAd;

	// Token: 0x040007FA RID: 2042
	public static MyHashTable listImgIcon = new MyHashTable();

	// Token: 0x040007FB RID: 2043
	public static MyHashTable listImgPart = new MyHashTable();

	// Token: 0x040007FC RID: 2044
	public static MyHashTable listPartDynamic = new MyHashTable();

	// Token: 0x040007FD RID: 2045
	public static MyVector effectList = new MyVector();
}
