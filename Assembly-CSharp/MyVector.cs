using System;
using System.Collections;

// Token: 0x02000012 RID: 18
public class MyVector
{
	// Token: 0x06000084 RID: 132 RVA: 0x00005F09 File Offset: 0x00004309
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00005F1C File Offset: 0x0000431C
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00005F2B File Offset: 0x0000432B
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00005F3A File Offset: 0x0000433A
	public int size()
	{
		return this.a.Count;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00005F47 File Offset: 0x00004347
	public object elementAt(int i)
	{
		return this.a[i];
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00005F55 File Offset: 0x00004355
	public void removeElementAt(int i)
	{
		this.a.RemoveAt(i);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00005F63 File Offset: 0x00004363
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00005F71 File Offset: 0x00004371
	public void setElementAt(object o, int i)
	{
		this.a[i] = o;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00005F80 File Offset: 0x00004380
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00005F8D File Offset: 0x0000438D
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x04000062 RID: 98
	private ArrayList a;
}
