using System;
using System.Collections;

// Token: 0x0200000F RID: 15
public class MyHashTable
{
	// Token: 0x06000076 RID: 118 RVA: 0x00005989 File Offset: 0x00003D89
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00005997 File Offset: 0x00003D97
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000059A4 File Offset: 0x00003DA4
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000059B1 File Offset: 0x00003DB1
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000059BE File Offset: 0x00003DBE
	public void put(object k, object v)
	{
		if (this.h.ContainsKey(k))
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000059EA File Offset: 0x00003DEA
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000059F8 File Offset: 0x00003DF8
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x0400005F RID: 95
	public Hashtable h = new Hashtable();
}
