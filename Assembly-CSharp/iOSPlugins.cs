using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class iOSPlugins
{
	// Token: 0x060000B7 RID: 183
	[DllImport("__Internal")]
	private static extern void _SMSsend(string tophone, string withtext, int n);

	// Token: 0x060000B8 RID: 184
	[DllImport("__Internal")]
	private static extern int _unpause();

	// Token: 0x060000B9 RID: 185
	[DllImport("__Internal")]
	private static extern int _checkRotation();

	// Token: 0x060000BA RID: 186
	[DllImport("__Internal")]
	private static extern int _back();

	// Token: 0x060000BB RID: 187
	[DllImport("__Internal")]
	private static extern int _Send();

	// Token: 0x060000BC RID: 188
	[DllImport("__Internal")]
	private static extern void _purchaseItem(string itemID, string userName, string gameID);

	// Token: 0x060000BD RID: 189
	[DllImport("__Internal")]
	private static extern int _getProvider();

	// Token: 0x060000BE RID: 190
	[DllImport("__Internal")]
	private static extern string _getAgent();

	// Token: 0x060000BF RID: 191 RVA: 0x00006BC4 File Offset: 0x00004FC4
	public static int Check()
	{
		string a = string.Empty + iOSPlugins.devide[2];
		if (a == "h" && iOSPlugins.devide.Length > 6)
		{
			iOSPlugins.Myname = SystemInfo.operatingSystem.ToString();
			string a2 = string.Empty + iOSPlugins.Myname[10];
			if (a2 != "2" && a2 != "3")
			{
				return 0;
			}
			return 1;
		}
		else
		{
			if (iOSPlugins.devide == "Unknown" && ScaleGUI.WIDTH * ScaleGUI.HEIGHT < 786432f)
			{
				return 0;
			}
			return -1;
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00006C87 File Offset: 0x00005087
	public static void SMSsend(string phonenumber, string bodytext, int n)
	{
		if (Application.platform != null)
		{
			iOSPlugins._SMSsend(phonenumber, bodytext, n);
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00006C9B File Offset: 0x0000509B
	public static void back()
	{
		if (Application.platform != null)
		{
			iOSPlugins._back();
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00006CAD File Offset: 0x000050AD
	public static void Send()
	{
		if (Application.platform != null)
		{
			iOSPlugins._Send();
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00006CBF File Offset: 0x000050BF
	public static int unpause()
	{
		if (Application.platform != null)
		{
			return iOSPlugins._unpause();
		}
		return 0;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00006CD2 File Offset: 0x000050D2
	public static int checkRotation()
	{
		if (Application.platform != null)
		{
			return iOSPlugins._checkRotation();
		}
		return 0;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00006CE5 File Offset: 0x000050E5
	public static int getProvider()
	{
		if (Application.platform == 8)
		{
			return iOSPlugins._getProvider();
		}
		return 0;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00006CF9 File Offset: 0x000050F9
	public static string getAgent()
	{
		if (Application.platform == 8)
		{
			return iOSPlugins._getAgent();
		}
		return "0";
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00006D11 File Offset: 0x00005111
	public static void purchaseItem(string itemID, string userName, string gameID)
	{
		if (Application.platform != null)
		{
			iOSPlugins._purchaseItem(itemID, userName, gameID);
		}
	}

	// Token: 0x04000083 RID: 131
	public static string devide;

	// Token: 0x04000084 RID: 132
	public static string Myname;
}
