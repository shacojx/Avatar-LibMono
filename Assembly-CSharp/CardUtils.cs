using System;

// Token: 0x020000B9 RID: 185
public class CardUtils
{
	// Token: 0x060005E6 RID: 1510 RVA: 0x00037338 File Offset: 0x00035738
	public static bool checkForceFinish(MyVector cards, bool newgame)
	{
		sbyte[] array = new sbyte[cards.size()];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = ((Card)cards.elementAt(i)).cardID;
		}
		int j;
		for (j = 9; j < 13; j++)
		{
			if ((int)array[j] / 4 != 12)
			{
				break;
			}
		}
		if (j == 13)
		{
			return true;
		}
		for (j = 0; j < 4; j++)
		{
			if ((int)array[j] / 4 != 0)
			{
				break;
			}
		}
		if (j == 4 && newgame)
		{
			return true;
		}
		int num = 0;
		for (j = 0; j < 12; j++)
		{
			if ((int)array[j] / 4 == (int)array[j + 1] / 4 - 1 && (int)array[j + 1] / 4 != 12)
			{
				num++;
			}
			else if ((int)array[j] / 4 != (int)array[j + 1] / 4)
			{
				break;
			}
		}
		if (num >= 11)
		{
			return true;
		}
		num = 0;
		for (j = 0; j < 12; j++)
		{
			if ((int)array[j] / 4 == (int)array[j + 1] / 4)
			{
				num++;
				j++;
			}
		}
		return num >= 6 || CardUtils.demDoiThong(array) >= 5;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00037488 File Offset: 0x00035888
	private static int demDoiThong(sbyte[] bai)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < bai.Length - 1; i++)
		{
			if (bai.Length >= 48)
			{
				break;
			}
			if (num2 == 0 && (int)bai[i] / 4 == (int)bai[i + 1] / 4)
			{
				num2 = 1;
			}
			else if (num2 % 2 == 1)
			{
				if ((int)bai[i] / 4 == (int)bai[i + 1] / 4 - 1)
				{
					num2++;
				}
				else if ((int)bai[i] / 4 != (int)bai[i + 1] / 4)
				{
					if (num2 > num)
					{
						num = num2;
					}
					num2 = 0;
				}
			}
			else if ((int)bai[i] / 4 == (int)bai[i + 1] / 4)
			{
				num2++;
			}
			else
			{
				if (num2 > num)
				{
					num = num2;
				}
				num2 = 0;
			}
		}
		if (num2 > num)
		{
			num = num2;
		}
		return (num + 1) / 2;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00037560 File Offset: 0x00035960
	public static void sort(sbyte[] bai)
	{
		for (int i = 0; i < bai.Length - 1; i++)
		{
			for (int j = i + 1; j < bai.Length; j++)
			{
				if ((int)bai[i] > (int)bai[j])
				{
					sbyte b = bai[i];
					bai[i] = bai[j];
					bai[j] = b;
				}
			}
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x000375B4 File Offset: 0x000359B4
	public static sbyte getType(sbyte[] bai)
	{
		if (CardUtils.is1(bai))
		{
			return 0;
		}
		if (CardUtils.is2(bai))
		{
			return 2;
		}
		if (CardUtils.is3(bai))
		{
			return 3;
		}
		if (CardUtils.is3DoiThong(bai))
		{
			return 4;
		}
		if (CardUtils.is4DoiThong(bai))
		{
			return 5;
		}
		if (CardUtils.isTuQuy(bai))
		{
			return 6;
		}
		if (CardUtils.isSanh(bai))
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0003761D File Offset: 0x00035A1D
	public static bool is1(sbyte[] bai)
	{
		return bai.Length == 1;
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00037628 File Offset: 0x00035A28
	public static bool isSanh(sbyte[] bai)
	{
		if (bai.Length < 3)
		{
			return false;
		}
		for (int i = 1; i < bai.Length; i++)
		{
			if ((int)bai[i - 1] / 4 != (int)bai[i] / 4 - 1)
			{
				return false;
			}
		}
		return (int)bai[bai.Length - 1] / 4 != 12;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0003767F File Offset: 0x00035A7F
	public static bool is2(sbyte[] bai)
	{
		return bai.Length == 2 && (int)bai[0] / 4 == (int)bai[1] / 4;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0003769E File Offset: 0x00035A9E
	public static bool is3(sbyte[] bai)
	{
		return bai.Length == 3 && (int)bai[0] / 4 == (int)bai[1] / 4 && (int)bai[1] / 4 == (int)bai[2] / 4;
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x000376D0 File Offset: 0x00035AD0
	public static bool isTuQuy(sbyte[] bai)
	{
		return bai.Length == 4 && (int)bai[0] / 4 == (int)bai[1] / 4 && (int)bai[1] / 4 == (int)bai[2] / 4 && (int)bai[2] / 4 == (int)bai[3] / 4;
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0003771C File Offset: 0x00035B1C
	public static bool is3DoiThong(sbyte[] bai)
	{
		if (bai.Length != 6)
		{
			return false;
		}
		for (int i = 1; i < bai.Length; i++)
		{
			if (i % 2 == 1 && (int)bai[i - 1] / 4 != (int)bai[i] / 4)
			{
				return false;
			}
			if (i % 2 == 0 && (int)bai[i - 1] / 4 != (int)bai[i] / 4 - 1)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00037788 File Offset: 0x00035B88
	public static bool is4DoiThong(sbyte[] bai)
	{
		if (bai.Length != 8)
		{
			return false;
		}
		for (int i = 1; i < bai.Length; i++)
		{
			if (i % 2 == 1 && (int)bai[i - 1] / 4 != (int)bai[i] / 4)
			{
				return false;
			}
			if (i % 2 == 0 && (int)bai[i - 1] / 4 != (int)bai[i] / 4 - 1)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x000377F4 File Offset: 0x00035BF4
	public static bool available(sbyte[] bai_sapdanh, sbyte type_bai_sapdanh, sbyte[] baidanh, sbyte type_baidanh)
	{
		CardUtils.penalty = 0;
		switch (type_baidanh + 1)
		{
		case 0:
			if ((int)type_bai_sapdanh != -1)
			{
				return true;
			}
			break;
		case 1:
			if ((int)type_bai_sapdanh == 0 && (int)bai_sapdanh[0] > (int)baidanh[0])
			{
				return true;
			}
			if ((int)baidanh[0] / 4 == 12 && ((int)type_bai_sapdanh == 4 || (int)type_bai_sapdanh == 5 || (int)type_bai_sapdanh == 6))
			{
				if ((int)baidanh[0] % 4 < 2)
				{
					CardUtils.penalty = CardUtils.money / 2;
					CardUtils.penaltyDes = T.chatHeo[0];
				}
				else
				{
					CardUtils.penalty = CardUtils.money;
					CardUtils.penaltyDes = T.chatHeo[1];
				}
				return true;
			}
			break;
		case 2:
			if ((int)type_bai_sapdanh == 1 && bai_sapdanh.Length == baidanh.Length && (int)bai_sapdanh[bai_sapdanh.Length - 1] > (int)baidanh[baidanh.Length - 1])
			{
				return true;
			}
			break;
		case 3:
			if ((int)type_bai_sapdanh == 2 && (int)bai_sapdanh[1] > (int)baidanh[1])
			{
				return true;
			}
			if ((int)baidanh[0] / 4 == 12 && ((int)type_bai_sapdanh == 6 || (int)type_bai_sapdanh == 5))
			{
				if ((int)baidanh[1] % 4 < 2)
				{
					CardUtils.penalty = CardUtils.money;
					CardUtils.penaltyDes = T.chatHeo[2];
				}
				else if ((int)baidanh[0] % 4 >= 2)
				{
					CardUtils.penalty = 2 * CardUtils.money;
					CardUtils.penaltyDes = T.chatHeo[3];
				}
				else
				{
					CardUtils.penalty = CardUtils.money + CardUtils.money / 2;
					CardUtils.penaltyDes = T.chatHeo[4];
				}
				return true;
			}
			break;
		case 4:
			if ((int)type_bai_sapdanh == 3 && (int)bai_sapdanh[2] > (int)baidanh[2])
			{
				return true;
			}
			break;
		case 5:
			if (((int)type_bai_sapdanh == 4 && (int)bai_sapdanh[5] > (int)baidanh[5]) || (int)type_bai_sapdanh == 6 || (int)type_bai_sapdanh == 5)
			{
				CardUtils.penalty = CardUtils.money;
				CardUtils.penaltyDes = T.chatHeo[5];
				return true;
			}
			break;
		case 6:
			if ((int)type_bai_sapdanh == 5 && (int)bai_sapdanh[7] > (int)baidanh[7])
			{
				CardUtils.penalty = 2 * CardUtils.money;
				CardUtils.penaltyDes = T.chatHeo[7];
				return true;
			}
			break;
		case 7:
			if (((int)type_bai_sapdanh == 6 && (int)bai_sapdanh[3] > (int)baidanh[3]) || (int)type_bai_sapdanh == 5)
			{
				CardUtils.penalty = CardUtils.money + CardUtils.money / 2;
				CardUtils.penaltyDes = T.chatHeo[6];
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x04000810 RID: 2064
	public const sbyte TYPE_1 = 0;

	// Token: 0x04000811 RID: 2065
	public const sbyte TYPE_SANH = 1;

	// Token: 0x04000812 RID: 2066
	public const sbyte TYPE_2 = 2;

	// Token: 0x04000813 RID: 2067
	public const sbyte TYPE_3 = 3;

	// Token: 0x04000814 RID: 2068
	public const sbyte TYPE_3DOITHONG = 4;

	// Token: 0x04000815 RID: 2069
	public const sbyte TYPE_4DOITHONG = 5;

	// Token: 0x04000816 RID: 2070
	public const sbyte TYPE_TUQUY = 6;

	// Token: 0x04000817 RID: 2071
	public static string[] cardValueName = new string[]
	{
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9",
		"10",
		"J",
		"Q",
		"K",
		"A",
		"Heo"
	};

	// Token: 0x04000818 RID: 2072
	public static int penalty;

	// Token: 0x04000819 RID: 2073
	public static int money;

	// Token: 0x0400081A RID: 2074
	public static string penaltyDes;
}
