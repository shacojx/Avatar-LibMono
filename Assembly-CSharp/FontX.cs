using System;

// Token: 0x0200010E RID: 270
public abstract class FontX
{
	// Token: 0x0600076C RID: 1900
	public abstract void drawString(MyGraphics g, string st, int x, int y, int align);

	// Token: 0x0600076D RID: 1901
	public abstract int getWidth(string st);

	// Token: 0x0600076E RID: 1902
	public abstract string[] splitFontBStrInLine(string src, int lineWidth);

	// Token: 0x0600076F RID: 1903
	public abstract MyVector splitFontBStrInLineV(string src, int lineWidth);

	// Token: 0x06000770 RID: 1904
	public abstract string replace(string _text, string _searchStr, string _replacementStr);

	// Token: 0x06000771 RID: 1905
	public abstract int getHeight();

	// Token: 0x06000772 RID: 1906
	public abstract int getWidthNotExact(string s);

	// Token: 0x0400096C RID: 2412
	public const sbyte LEFT = 0;

	// Token: 0x0400096D RID: 2413
	public const sbyte RIGHT = 1;

	// Token: 0x0400096E RID: 2414
	public const sbyte CENTER = 2;
}
