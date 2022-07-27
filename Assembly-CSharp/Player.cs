using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class Player
{
	// Token: 0x06000092 RID: 146 RVA: 0x00006064 File Offset: 0x00004464
	public static void update()
	{
		if (Player.status == 2)
		{
			Player.status = 1;
			Player.__load(Player.filenametemp, Player.postem);
			Player.status = 0;
		}
		if (Player.status == 3)
		{
			Player.status = 1;
			Player.__start(Player.volumetem, Player.postem);
			Player.status = 0;
		}
		if (Player.status == 4)
		{
			Player.status = 1;
			Player.__stop(Player.postem);
			Player.status = 0;
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000060DE File Offset: 0x000044DE
	public static void load(string filename, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Player.__load(filename, pos);
		}
		else
		{
			Player._load(filename, pos);
		}
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000610C File Offset: 0x0000450C
	private static void _load(string filename, int pos)
	{
		if (Player.status != 0)
		{
			Debug.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + Player.filenametemp);
			return;
		}
		Player.filenametemp = filename;
		Player.postem = pos;
		Player.status = 2;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Player.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR LOAD AUDIO " + filename);
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"Load Audio ",
				filename,
				" done in ",
				i * 5,
				"ms"
			}));
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x000061CC File Offset: 0x000045CC
	private static void __load(string filename, int pos)
	{
		Player.mysound[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
		GameObject.Find("Main Camera").AddComponent<AudioSource>();
		Player.music[pos] = GameObject.Find("Main Camera");
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000620B File Offset: 0x0000460B
	public static void start(float volume, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Player.__start(volume, pos);
		}
		else
		{
			Player._start(volume, pos);
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000623C File Offset: 0x0000463C
	public static void _start(float volume, int pos)
	{
		if (Player.status != 0)
		{
			Debug.LogError("CANNOT START AUDIO WHEN STARTING");
			return;
		}
		Player.volumetem = volume;
		Player.postem = pos;
		Player.status = 3;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Player.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR START AUDIO");
		}
		else
		{
			Debug.Log("Start Audio done in " + i * 5 + "ms");
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x000062CB File Offset: 0x000046CB
	public static void __start(float volume, int pos)
	{
		Player.music[pos].GetComponent<AudioSource>().PlayOneShot(Player.mysound[pos], volume);
	}

	// Token: 0x06000099 RID: 153 RVA: 0x000062E6 File Offset: 0x000046E6
	public static void stop(int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Player.__stop(pos);
		}
		else
		{
			Player._stop(pos);
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00006314 File Offset: 0x00004714
	public static void _stop(int pos)
	{
		if (Player.status != 0)
		{
			Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
			return;
		}
		Player.postem = pos;
		Player.status = 4;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Player.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR STOP AUDIO");
		}
		else
		{
			Debug.Log("Stop Audio done in " + i * 5 + "ms");
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000639D File Offset: 0x0000479D
	public static void __stop(int pos)
	{
		if (Player.music[pos] != null)
		{
			Player.music[pos].GetComponent<AudioSource>().Stop();
		}
	}

	// Token: 0x04000065 RID: 101
	private const int INTERVAL = 5;

	// Token: 0x04000066 RID: 102
	private const int MAXTIME = 100;

	// Token: 0x04000067 RID: 103
	private static int status;

	// Token: 0x04000068 RID: 104
	private static int postem;

	// Token: 0x04000069 RID: 105
	private static string filenametemp;

	// Token: 0x0400006A RID: 106
	private static float volumetem;

	// Token: 0x0400006B RID: 107
	public static AudioClip[] mysound = new AudioClip[20];

	// Token: 0x0400006C RID: 108
	public static GameObject[] music = new GameObject[20];
}
