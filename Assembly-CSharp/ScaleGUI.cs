using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class ScaleGUI
{
	// Token: 0x060000B0 RID: 176 RVA: 0x000069B4 File Offset: 0x00004DB4
	public static void initScaleGUI()
	{
		Debug.Log(string.Concat(new object[]
		{
			"Init Scale GUI: Screen.w=",
			Screen.width,
			" Screen.h=",
			Screen.height
		}));
		ScaleGUI.WIDTH = (float)Screen.width;
		ScaleGUI.HEIGHT = (float)Screen.height;
		ScaleGUI.scaleScreen = false;
		if (Screen.height > 1080)
		{
			ScaleGUI.scaleScreen = true;
			ScaleGUI.WIDTH = (float)(Screen.width / 2);
			ScaleGUI.HEIGHT = (float)(Screen.height / 2);
			ScaleGUI.numScale = 2;
			ScaleGUI.scaleAndroid = 2;
		}
		else if (Screen.height > 480)
		{
			ScaleGUI.numScale = 2;
			ScaleGUI.scaleAndroid = 1;
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00006A74 File Offset: 0x00004E74
	public static void BeginGUI()
	{
		if (!ScaleGUI.scaleScreen)
		{
			return;
		}
		ScaleGUI.stack.Add(GUI.matrix);
		Matrix4x4 matrix4x = default(Matrix4x4);
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = num / num2;
		Vector3 zero = Vector3.zero;
		float num4;
		if (num3 < ScaleGUI.WIDTH / ScaleGUI.HEIGHT)
		{
			num4 = (float)Screen.width / ScaleGUI.WIDTH;
		}
		else
		{
			num4 = (float)Screen.height / ScaleGUI.HEIGHT;
		}
		matrix4x.SetTRS(zero, Quaternion.identity, Vector3.one * num4);
		GUI.matrix *= matrix4x;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00006B1F File Offset: 0x00004F1F
	public static void EndGUI()
	{
		if (!ScaleGUI.scaleScreen)
		{
			return;
		}
		GUI.matrix = ScaleGUI.stack[ScaleGUI.stack.Count - 1];
		ScaleGUI.stack.RemoveAt(ScaleGUI.stack.Count - 1);
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00006B5D File Offset: 0x00004F5D
	public static float scaleX(float x)
	{
		if (!ScaleGUI.scaleScreen)
		{
			return x;
		}
		x = x * ScaleGUI.WIDTH / (float)Screen.width;
		return x;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00006B7C File Offset: 0x00004F7C
	public static float scaleY(float y)
	{
		if (!ScaleGUI.scaleScreen)
		{
			return y;
		}
		y = y * ScaleGUI.HEIGHT / (float)Screen.height;
		return y;
	}

	// Token: 0x0400007C RID: 124
	public static bool scaleScreen;

	// Token: 0x0400007D RID: 125
	public static float WIDTH;

	// Token: 0x0400007E RID: 126
	public static float HEIGHT;

	// Token: 0x0400007F RID: 127
	private static List<Matrix4x4> stack = new List<Matrix4x4>();

	// Token: 0x04000080 RID: 128
	public static int numScale = 1;

	// Token: 0x04000081 RID: 129
	public static int scaleAndroid = 1;

	// Token: 0x04000082 RID: 130
	public static bool isAndroid = false;
}
