using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class SMS
{
	// Token: 0x060000A9 RID: 169 RVA: 0x000066BC File Offset: 0x00004ABC
	public static int send(string content, string to)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return SMS.__send(content, to);
		}
		return SMS._send(content, to);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000066E8 File Offset: 0x00004AE8
	private static int _send(string content, string to)
	{
		if (SMS.status != 0)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (SMS.status == 0)
				{
					break;
				}
			}
			if (SMS.status != 0)
			{
				Debug.LogError("CANNOT SEND SMS " + content + " WHEN SENDING " + SMS._content);
				return -1;
			}
		}
		SMS._content = content;
		SMS._to = to;
		SMS._result = -1;
		SMS.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			if (SMS.status == 0)
			{
				break;
			}
		}
		if (j == 500)
		{
			Debug.LogError("TOO LONG FOR SEND SMS " + content);
			SMS.status = 0;
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"Send SMS ",
				content,
				" done in ",
				j * 5,
				"ms"
			}));
		}
		return SMS._result;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000067F8 File Offset: 0x00004BF8
	private static int __send(string content, string to)
	{
		int num = iOSPlugins.Check();
		if (num >= 0)
		{
			SMS.f = true;
			SMS.sendEnable = true;
			iOSPlugins.SMSsend(to, content, num);
			Screen.orientation = 5;
		}
		return num;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00006830 File Offset: 0x00004C30
	public static void update()
	{
		float num = Time.time;
		if (num - (float)SMS.time > 1f)
		{
			SMS.time++;
		}
		if (SMS.f)
		{
			SMS.OnSMS();
		}
		if (SMS.status == 2)
		{
			SMS.status = 1;
			try
			{
				SMS._result = SMS.__send(SMS._content, SMS._to);
			}
			catch (Exception ex)
			{
				Debug.Log("CANNOT SEND SMS");
			}
			SMS.status = 0;
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000068C0 File Offset: 0x00004CC0
	private static void OnSMS()
	{
		if (SMS.sendEnable)
		{
			if (iOSPlugins.checkRotation() == 1)
			{
				Screen.orientation = 3;
			}
			else if (iOSPlugins.checkRotation() == -1)
			{
				Screen.orientation = 1;
			}
			else if (iOSPlugins.checkRotation() == 0)
			{
				Screen.orientation = 5;
			}
			else if (iOSPlugins.checkRotation() == 2)
			{
				Screen.orientation = 4;
			}
			else if (iOSPlugins.checkRotation() == 3)
			{
				Screen.orientation = 2;
			}
			if (SMS.time0 < 5)
			{
				SMS.time0++;
			}
			else
			{
				iOSPlugins.Send();
				SMS.sendEnable = false;
				SMS.time0 = 0;
			}
		}
		if (iOSPlugins.unpause() == 1)
		{
			Screen.orientation = 3;
			if (SMS.time0 < 5)
			{
				SMS.time0++;
			}
			else
			{
				SMS.f = false;
				iOSPlugins.back();
				SMS.time0 = 0;
			}
		}
	}

	// Token: 0x04000072 RID: 114
	private const int INTERVAL = 5;

	// Token: 0x04000073 RID: 115
	private const int MAXTIME = 500;

	// Token: 0x04000074 RID: 116
	private static int status;

	// Token: 0x04000075 RID: 117
	private static int _result;

	// Token: 0x04000076 RID: 118
	private static string _to;

	// Token: 0x04000077 RID: 119
	private static string _content;

	// Token: 0x04000078 RID: 120
	private static bool f;

	// Token: 0x04000079 RID: 121
	private static int time;

	// Token: 0x0400007A RID: 122
	public static bool sendEnable;

	// Token: 0x0400007B RID: 123
	private static int time0;
}
