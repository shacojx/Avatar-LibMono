using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class HDFont : FontX
{
	// Token: 0x06000773 RID: 1907 RVA: 0x00044724 File Offset: 0x00042B24
	public HDFont(int type)
	{
		HDFont.getColor(12907498);
		string name = (Main.hdtype != 2) ? "arial" : "vo";
		if (type == 9)
		{
			name = "fontmenu" + AvMain.hd;
		}
		int size = HDFont.SIZE[type] * Main.hdtype;
		if (type == 5 || type == 6)
		{
			name = "vo2";
			size = HDFont.SIZE[type] * 3 / 2;
		}
		if (type == 3 && Main.hdtype == 2)
		{
			name = "vo";
		}
		this.fontB = new FontB(name, size, HDFont.STYLE[type], (sbyte)type, HDFont.COLOR1[type], HDFont.COLOR2[type]);
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x000447F8 File Offset: 0x00042BF8
	public HDFont(int type, string path)
	{
		int size = HDFont.SIZE[type] * Main.hdtype;
		if ((type == 5 || type == 6) && Main.hdtype == 2)
		{
			size = HDFont.SIZE[type] * 3 / 2;
		}
		this.fontB = new FontB(T.getPath() + "/font/" + path, size, HDFont.STYLE[type], (sbyte)type, HDFont.COLOR1[type], HDFont.COLOR2[type]);
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00044884 File Offset: 0x00042C84
	public HDFont(int type, string path, int color, int color2)
	{
		int size = HDFont.SIZE[type] * Main.hdtype;
		this.fontB = new FontB(T.getPath() + "/font/" + path, size, HDFont.STYLE[type], (sbyte)type, HDFont.getColor(color), HDFont.getColor(color2));
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x000448D7 File Offset: 0x00042CD7
	public HDFont(int type, string path, int size, int color, int color2)
	{
		this.fontB = new FontB("font/" + path, size, HDFont.STYLE[type], (sbyte)type, HDFont.getColor(color), HDFont.getColor(color2));
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00044910 File Offset: 0x00042D10
	public static Color getColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		return new Color(num6, num5, num4);
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00044961 File Offset: 0x00042D61
	public override void drawString(MyGraphics g, string st, int x, int y, int align)
	{
		this.fontB.drawString(g, st, x, y, align);
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00044975 File Offset: 0x00042D75
	public override int getWidth(string st)
	{
		return this.fontB.getWidthOf(st);
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00044983 File Offset: 0x00042D83
	public override string[] splitFontBStrInLine(string src, int lineWidth)
	{
		return this.fontB.splitStrInLine(src, lineWidth);
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00044994 File Offset: 0x00042D94
	public override MyVector splitFontBStrInLineV(string src, int lineWidth)
	{
		return this.fontB.splitStrInLineV(src, lineWidth);
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x000449B0 File Offset: 0x00042DB0
	public override string replace(string _text, string _searchStr, string _replacementStr)
	{
		if (_text.Equals(string.Empty) || _searchStr.Equals(string.Empty))
		{
			return _text;
		}
		string str = string.Empty;
		int num = _text.IndexOf(_searchStr);
		int num2 = 0;
		int length = _searchStr.Length;
		while (num != -1)
		{
			str = str + _text.Substring(num2, num - num2) + _replacementStr;
			num2 = num + length;
			num = _text.IndexOf(_searchStr, num2);
		}
		return str + _text.Substring(num2, _text.Length - num2);
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00044A38 File Offset: 0x00042E38
	public override int getHeight()
	{
		return this.fontB.getHeight();
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00044A45 File Offset: 0x00042E45
	public override int getWidthNotExact(string s)
	{
		return this.fontB.getWidthNotExactOf(s);
	}

	// Token: 0x0400096F RID: 2415
	public Image imgFont;

	// Token: 0x04000970 RID: 2416
	public string charList;

	// Token: 0x04000971 RID: 2417
	public sbyte[] charWidth;

	// Token: 0x04000972 RID: 2418
	private static int[] STYLE = new int[]
	{
		0,
		2,
		0,
		0,
		2,
		2,
		2,
		1,
		0,
		0,
		0,
		3,
		0,
		0,
		0,
		0,
		0,
		0,
		0
	};

	// Token: 0x04000973 RID: 2419
	private static Color[] COLOR1 = new Color[]
	{
		new Color(0f, 0.453125f, 0.546875f),
		new Color(0.9647059f, 1f, 0.380392164f),
		Color.white,
		Color.black,
		Color.yellow,
		new Color(1f, 0.5254902f, 0.5254902f),
		new Color(1f, 0.870588243f, 0.56078434f),
		Color.white,
		Color.black,
		Color.white,
		new Color(0.16f, 0.504f, 0.75f),
		Color.white,
		Color.black,
		Color.white,
		new Color(0f, 0.453125f, 0.5507813f),
		Color.white,
		Color.white,
		Color.white,
		Color.white
	};

	// Token: 0x04000974 RID: 2420
	private static Color[] COLOR2 = new Color[]
	{
		Color.gray,
		Color.black,
		Color.gray,
		Color.gray,
		Color.black,
		Color.black,
		Color.black,
		new Color(0.105f, 0.415f, 0.368f),
		Color.black,
		Color.white,
		new Color(0f, 0.453125f, 0.5507813f),
		Color.white,
		Color.black,
		Color.white,
		new Color(0f, 0.453125f, 0.5507813f),
		Color.white,
		Color.white,
		Color.white,
		Color.white
	};

	// Token: 0x04000975 RID: 2421
	private static int[] SIZE = new int[]
	{
		13,
		14,
		13,
		10,
		17,
		10,
		10,
		14,
		14,
		12,
		12,
		12,
		12,
		10,
		10,
		10,
		10,
		5,
		12
	};

	// Token: 0x04000976 RID: 2422
	private FontB fontB;
}
