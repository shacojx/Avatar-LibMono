using System;

// Token: 0x02000050 RID: 80
public class Item
{
	// Token: 0x060002F1 RID: 753 RVA: 0x000185DC File Offset: 0x000169DC
	public static Item getItemByList(MyVector list, int id)
	{
		int num = list.size();
		for (int i = 0; i < num; i++)
		{
			Item item = (Item)list.elementAt(i);
			if ((int)item.ID == id)
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x0400037A RID: 890
	public short ID;

	// Token: 0x0400037B RID: 891
	public short idIcon;

	// Token: 0x0400037C RID: 892
	public sbyte shopType;

	// Token: 0x0400037D RID: 893
	public int[] price = new int[2];

	// Token: 0x0400037E RID: 894
	public int number;

	// Token: 0x0400037F RID: 895
	public string name = string.Empty;

	// Token: 0x04000380 RID: 896
	public string des;
}
