using System;
using System.IO;

// Token: 0x0200008F RID: 143
public class FarmService
{
	// Token: 0x0600048F RID: 1167 RVA: 0x0002AF96 File Offset: 0x00029396
	public static FarmService gI()
	{
		if (FarmService.instance == null)
		{
			FarmService.instance = new FarmService();
		}
		return FarmService.instance;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0002AFB1 File Offset: 0x000293B1
	public void send(Message m)
	{
		Session_ME.gI().sendMessage(m);
		m.cleanup();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0002AFC4 File Offset: 0x000293C4
	public void setBigData()
	{
		Message m = new Message(51);
		this.send(m);
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0002AFE0 File Offset: 0x000293E0
	public void getBigImage(short id)
	{
		Message message = new Message(54);
		try
		{
			message.writer().writeShort(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		Canvas.startWaitDlg(T.getFarmData);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0002B034 File Offset: 0x00029434
	public void getImageData()
	{
		Message m = new Message(55);
		this.send(m);
		Canvas.startWaitDlg(T.getFarmData);
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0002B05C File Offset: 0x0002945C
	public void getTreeInfo()
	{
		Message m = new Message(56);
		this.send(m);
		Canvas.startWaitDlg(T.getFarmData);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0002B084 File Offset: 0x00029484
	public void getInventory()
	{
		Message m = new Message(60);
		this.send(m);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0002B0A0 File Offset: 0x000294A0
	public void doJoinFarm(int id)
	{
		Message message = new Message(61);
		try
		{
			message.writer().writeInt(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0002B0EC File Offset: 0x000294EC
	public void doBuyItem(short id, sbyte n, int typeShop, int money)
	{
		Message message = new Message(62);
		try
		{
			message.writer().writeShort(id);
			message.writer().writeByte(n);
			message.writer().writeByte((sbyte)typeShop);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
		Canvas.endDlg();
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0002B154 File Offset: 0x00029554
	public void doSellItem(short idItem)
	{
		Message message = new Message(63);
		try
		{
			message.writer().writeShort(idItem);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0002B1A0 File Offset: 0x000295A0
	public void doPlantSeed(int idUser, int indexCell, int idSeed)
	{
		Message message = new Message(64);
		try
		{
			message.writer().writeInt(idUser);
			message.writer().writeByte((sbyte)indexCell);
			message.writer().writeByte((sbyte)idSeed);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002B204 File Offset: 0x00029604
	public void doUsingItem(int idUser, int indexCell, int idItem)
	{
		Message message = new Message(65);
		try
		{
			message.writer().writeInt(idUser);
			message.writer().writeByte((sbyte)indexCell);
			message.writer().writeShort((short)idItem);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0002B268 File Offset: 0x00029668
	public void doHervest(int idFarm, int indexCell)
	{
		Message message = new Message(66);
		try
		{
			message.writer().writeInt(idFarm);
			message.writer().writeByte((sbyte)indexCell);
			this.send(message);
		}
		catch (IOException e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0002B2C0 File Offset: 0x000296C0
	public void doOpenLand(int idFarm, int typeBuy)
	{
		Message message = new Message(70);
		try
		{
			message.writer().writeInt(idFarm);
			message.writer().writeByte((sbyte)typeBuy);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0002B318 File Offset: 0x00029718
	public void doRequestPricePlant(int idFarm)
	{
		Message message = new Message(69);
		try
		{
			message.writer().writeInt(idFarm);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0002B364 File Offset: 0x00029764
	public void doHarvestAnimal(int idFarm, int index)
	{
		Message message = new Message(74);
		try
		{
			message.writer().writeInt(idFarm);
			message.writer().writeByte((sbyte)index);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0002B3BC File Offset: 0x000297BC
	public void doSellAnimal(int idFarm, sbyte iddb)
	{
		Message message = new Message(73);
		try
		{
			message.writer().writeInt(idFarm);
			message.writer().writeByte(iddb);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0002B414 File Offset: 0x00029814
	public void doBuyAnimal(AnimalInfo animal, int typeBuy)
	{
		FarmScr.cell = null;
		Canvas.endDlg();
		Message message = new Message(71);
		try
		{
			message.writer().writeByte(animal.species);
			message.writer().writeByte((sbyte)typeBuy);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0002B47C File Offset: 0x0002987C
	public void doRequestPriceAnimal(int idFarm, int iddb)
	{
		Message message = new Message(72);
		try
		{
			message.writer().writeInt(idFarm);
			message.writer().writeByte((sbyte)iddb);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0002B4D4 File Offset: 0x000298D4
	public void doTransMoney(int money, int type)
	{
		Message message = new Message(75);
		try
		{
			message.writer().writeInt(money);
			message.writer().writeByte((sbyte)type);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0002B52C File Offset: 0x0002992C
	public void doUpdateFarm(int type, int typeMoney)
	{
		Message message = new Message(80);
		try
		{
			message.writer().writeByte(type);
			if (type == 1)
			{
				message.writer().writeByte(typeMoney);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0002B588 File Offset: 0x00029988
	public void doUpdateFish(int type, int typeMoney)
	{
		Message message = new Message(81);
		try
		{
			message.writer().writeByte(type);
			if (type == 1)
			{
				message.writer().writeByte(typeMoney);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0002B5E4 File Offset: 0x000299E4
	public void doGetImgIcon(short id)
	{
		Message message = new Message(82);
		try
		{
			message.writer().writeShort(id);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0002B630 File Offset: 0x00029A30
	public void getInfoStarFruit()
	{
		Message m = new Message(87);
		this.send(m);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002B64C File Offset: 0x00029A4C
	public void doUpdateStarFruil(int i)
	{
		Message message = new Message(84);
		try
		{
			message.writer().writeByte(i);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002B698 File Offset: 0x00029A98
	public void doHarvestStarFruit()
	{
		Message m = new Message(85);
		this.send(m);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0002B6B4 File Offset: 0x00029AB4
	public void doUpdateStarFruitByMoney(int i)
	{
		Message message = new Message(86);
		try
		{
			message.writer().writeByte(i);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002B700 File Offset: 0x00029B00
	public void doUpdateLand(int step, int typeMoney)
	{
		Message message = new Message(90);
		try
		{
			message.writer().writeByte(step);
			if (step == 1)
			{
				message.writer().writeByte(typeMoney);
			}
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0002B75C File Offset: 0x00029B5C
	public void doUpdateStore(int step, int typeMoney)
	{
		Message message = new Message(94);
		try
		{
			message.writer().writeByte(step);
			if (step == 1)
			{
				message.writer().writeByte(typeMoney);
			}
		}
		catch (Exception)
		{
		}
		this.send(message);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0002B7B4 File Offset: 0x00029BB4
	public void doCooking(short iD)
	{
		Canvas.startWaitDlg();
		Message message = new Message(91);
		try
		{
			message.writer().writeShort(iD);
			this.send(message);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0002B7FC File Offset: 0x00029BFC
	public void nauNhanh(int step)
	{
		Message message = new Message(93);
		try
		{
			message.writer().writeByte(step);
			this.send(message);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0002B840 File Offset: 0x00029C40
	public void doHarvestCook()
	{
		Message m = new Message(92);
		this.send(m);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0002B85C File Offset: 0x00029C5C
	public void doFinishStarFruit()
	{
		Message m = new Message(83);
		this.send(m);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0002B878 File Offset: 0x00029C78
	public void doStealInfo()
	{
		Message m = new Message(95);
		this.send(m);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0002B894 File Offset: 0x00029C94
	public void doSteal(int step)
	{
		Message message = new Message(96);
		try
		{
			message.writer().writeByte(step);
			this.send(message);
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0002B8E0 File Offset: 0x00029CE0
	public void doStealStore()
	{
		Message m = new Message(97);
		this.send(m);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0002B8FC File Offset: 0x00029CFC
	public void doLichSuAnTrom()
	{
		Message m = new Message(98);
		this.send(m);
	}

	// Token: 0x04000772 RID: 1906
	private static FarmService instance;
}
