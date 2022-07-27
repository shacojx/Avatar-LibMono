using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class MyKeyMap
{
	// Token: 0x0600007D RID: 125 RVA: 0x00005A08 File Offset: 0x00003E08
	static MyKeyMap()
	{
		MyKeyMap.h.Add(97, 97);
		MyKeyMap.h.Add(98, 98);
		MyKeyMap.h.Add(99, 99);
		MyKeyMap.h.Add(100, 100);
		MyKeyMap.h.Add(101, 101);
		MyKeyMap.h.Add(102, 102);
		MyKeyMap.h.Add(103, 103);
		MyKeyMap.h.Add(104, 104);
		MyKeyMap.h.Add(105, 105);
		MyKeyMap.h.Add(106, 106);
		MyKeyMap.h.Add(107, 107);
		MyKeyMap.h.Add(108, 108);
		MyKeyMap.h.Add(109, 109);
		MyKeyMap.h.Add(110, 110);
		MyKeyMap.h.Add(111, 111);
		MyKeyMap.h.Add(112, 112);
		MyKeyMap.h.Add(113, 113);
		MyKeyMap.h.Add(114, 114);
		MyKeyMap.h.Add(115, 115);
		MyKeyMap.h.Add(116, 116);
		MyKeyMap.h.Add(117, 117);
		MyKeyMap.h.Add(118, 118);
		MyKeyMap.h.Add(119, 119);
		MyKeyMap.h.Add(120, 120);
		MyKeyMap.h.Add(121, 121);
		MyKeyMap.h.Add(122, 122);
		MyKeyMap.h.Add(48, 48);
		MyKeyMap.h.Add(49, 49);
		MyKeyMap.h.Add(50, 50);
		MyKeyMap.h.Add(51, 51);
		MyKeyMap.h.Add(52, 52);
		MyKeyMap.h.Add(53, 53);
		MyKeyMap.h.Add(54, 54);
		MyKeyMap.h.Add(55, 55);
		MyKeyMap.h.Add(56, 56);
		MyKeyMap.h.Add(57, 57);
		MyKeyMap.h.Add(32, 32);
		MyKeyMap.h.Add(282, -21);
		MyKeyMap.h.Add(283, -22);
		MyKeyMap.h.Add(273, -1);
		MyKeyMap.h.Add(274, -2);
		MyKeyMap.h.Add(276, -3);
		MyKeyMap.h.Add(275, -4);
		MyKeyMap.h.Add(8, -8);
		MyKeyMap.h.Add(13, -5);
		MyKeyMap.h.Add(46, 46);
		MyKeyMap.h.Add(64, 64);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00005EA0 File Offset: 0x000042A0
	public static int map(KeyCode k)
	{
		object obj = MyKeyMap.h[k];
		if (obj == null)
		{
			return 0;
		}
		return (int)obj;
	}

	// Token: 0x04000060 RID: 96
	private static Hashtable h = new Hashtable();
}
