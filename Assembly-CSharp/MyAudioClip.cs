using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class MyAudioClip
{
	// Token: 0x0600005A RID: 90 RVA: 0x0000462C File Offset: 0x00002A2C
	public MyAudioClip(string filename)
	{
		this.clip = (AudioClip)Resources.Load(filename);
		this.name = filename;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000464C File Offset: 0x00002A4C
	public void Play()
	{
		Out.println("PLAY: " + this.name);
		if (this.isPlaying())
		{
			Debug.LogWarning("Skip " + this.name);
			return;
		}
		Main.main.GetComponent<AudioSource>().PlayOneShot(this.clip);
		this.timeStart = (long)Environment.TickCount;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000046B0 File Offset: 0x00002AB0
	public bool isPlaying()
	{
		return this.timeStart != 0L && (long)Environment.TickCount - this.timeStart < (long)(this.clip.length * 1000f);
	}

	// Token: 0x04000046 RID: 70
	public string name;

	// Token: 0x04000047 RID: 71
	public AudioClip clip;

	// Token: 0x04000048 RID: 72
	public long timeStart;
}
