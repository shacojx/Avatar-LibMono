using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
internal class Net
{
	// Token: 0x0600008F RID: 143 RVA: 0x00005FA4 File Offset: 0x000043A4
	public static void update()
	{
		if (Net.www != null && Net.www.isDone)
		{
			string text = string.Empty;
			if (Net.www.error == null || Net.www.error.Equals(string.Empty))
			{
				text = Net.www.text;
			}
			Net.www = null;
			Net.h.onGetText(text);
			RMS.saveRMS("avServerList", mSystem.convertToSbyte(text));
		}
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00006024 File Offset: 0x00004424
	public static void connectHTTP(string link, HTTPHandler h)
	{
		if (Net.www != null)
		{
			Debug.LogError("GET HTTP BUSY");
		}
		Debug.LogWarning("REQUEST " + link);
		Net.www = new WWW(link);
		Net.h = h;
	}

	// Token: 0x04000063 RID: 99
	public static WWW www;

	// Token: 0x04000064 RID: 100
	public static HTTPHandler h;
}
