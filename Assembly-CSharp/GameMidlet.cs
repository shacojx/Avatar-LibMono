using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class GameMidlet
{
	// Token: 0x06000168 RID: 360 RVA: 0x0000A6E0 File Offset: 0x00008AE0
	static GameMidlet()
	{
		GameMidlet.IP = new string[2][][];
		GameMidlet.PORT = new int[2][][];
		GameMidlet.IP[0] = new string[2][];
		GameMidlet.IP[1] = new string[1][];
		GameMidlet.PORT[0] = new int[2][];
		GameMidlet.PORT[1] = new int[1][];
		GameMidlet.IP[0][0] = new string[]
		{
			"avhm.teamobi.com",
			"avtk.teamobi.com",
			"avdk.teamobi.com"
		};
		GameMidlet.IP[0][1] = new string[]
		{
			"avbb.teamobi.com"
		};
		GameMidlet.PORT[0][0] = new int[]
		{
			19128,
			19128,
			19128,
			19128
		};
		GameMidlet.PORT[0][1] = new int[]
		{
			19128
		};
		GameMidlet.PORT[1][0] = new int[]
		{
			19128
		};
		GameMidlet.nameSV = T.getNameServer();
		GameMidlet.linkGetHost[0] = new string[]
		{
			"http://teamobi.com/srvips/avatarios.txt",
			"http://teamobi.com/srvips/avatarios.txt"
		};
		GameMidlet.linkGetHost[1] = new string[]
		{
			"http://teamobi.com/srvips/avatarios.txt",
			"http://teamobi.com/srvips/avatarios.txt"
		};
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000A888 File Offset: 0x00008C88
	public GameMidlet()
	{
		GameMidlet.instance = this;
		if ((int)GameMidlet.PROVIDER != 0)
		{
			GameMidlet.VERSIONCODE = 0;
		}
		Debug.Log(string.Concat(new object[]
		{
			"PROVIDER: ",
			GameMidlet.PROVIDER,
			"    ",
			GameMidlet.VERSIONCODE
		}));
		Debug.Log("AGENT: " + GameMidlet.AGENT);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000A900 File Offset: 0x00008D00
	public static void sendSMS(string data, string to, IAction successAction, IAction failAction)
	{
		if (to.Contains("sms://"))
		{
			to = to.Remove(0, 6);
		}
		if (SMS.send(data, to) == -1)
		{
			if (failAction != null)
			{
				failAction.perform();
			}
			else
			{
				Canvas.startOKDlg(T.canNotSendMsg);
			}
		}
		else if (successAction != null)
		{
			successAction.perform();
		}
		else
		{
			Canvas.startOKDlg(T.sentMsg);
		}
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000A96F File Offset: 0x00008D6F
	public static void flatForm(string url)
	{
		Out.println("flatForm: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x04000132 RID: 306
	public static string gameID = "12";

	// Token: 0x04000133 RID: 307
	public static bool isEnglish = false;

	// Token: 0x04000134 RID: 308
	public static bool loadEnglish = false;

	// Token: 0x04000135 RID: 309
	public static bool saveLanguage = false;

	// Token: 0x04000136 RID: 310
	public static string IPEng = "112.78.1.25";

	// Token: 0x04000137 RID: 311
	public static int PORTEng = 19128;

	// Token: 0x04000138 RID: 312
	public static string[] nameSVEng = new string[]
	{
		"International Server",
		"Aries City"
	};

	// Token: 0x04000139 RID: 313
	public static string[][][] IP;

	// Token: 0x0400013A RID: 314
	public static int[][][] PORT;

	// Token: 0x0400013B RID: 315
	public static string[][][] nameSV;

	// Token: 0x0400013C RID: 316
	public static string[][] linkGetHost = new string[2][];

	// Token: 0x0400013D RID: 317
	public static int CLIENT_TYPE = 8;

	// Token: 0x0400013E RID: 318
	public static sbyte PROVIDER = 0;

	// Token: 0x0400013F RID: 319
	public static string AGENT = "0";

	// Token: 0x04000140 RID: 320
	public const string version = "2.5.8";

	// Token: 0x04000141 RID: 321
	public static sbyte VERSIONCODE = 13;

	// Token: 0x04000142 RID: 322
	public const int GAMEID_COTUONG = 5;

	// Token: 0x04000143 RID: 323
	public const int GAMEID_XITO = 6;

	// Token: 0x04000144 RID: 324
	public const int GAMEID_CARO = 1;

	// Token: 0x04000145 RID: 325
	public const int GAMEID_TIENLEN = 3;

	// Token: 0x04000146 RID: 326
	public const int GAMEID_PHOM = 7;

	// Token: 0x04000147 RID: 327
	public const int GAMEID_RACE = 12;

	// Token: 0x04000148 RID: 328
	public const int GAMEID_DAIMOND = 21;

	// Token: 0x04000149 RID: 329
	public const int GAMEID_BAUCUA = 22;

	// Token: 0x0400014A RID: 330
	public static GameMidlet instance;

	// Token: 0x0400014B RID: 331
	public static Avatar avatar = new Avatar();

	// Token: 0x0400014C RID: 332
	public static IndexPlayer myIndexP = new IndexPlayer();

	// Token: 0x0400014D RID: 333
	public static MyVector listContainer;
}
