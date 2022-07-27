using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class SoundManager
{
	// Token: 0x06000114 RID: 276 RVA: 0x000070E3 File Offset: 0x000054E3
	public static void init()
	{
		if (!Main.isCompactDevice)
		{
			return;
		}
		SoundManager.soundPoolMap = new Hashtable();
		Out.println("SoundManager init");
		SoundManager.loadSoundsEffect();
		SoundManager.loadSoundsAva();
		SoundManager.loadSoundsBG();
		SoundManager.loadSoundsAnimal();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00007118 File Offset: 0x00005518
	public static void onRequestOpenSound(string des, sbyte id)
	{
		if (SoundManager.isPlaying == 0)
		{
			return;
		}
		IAction action = new SoundManager.IActionYes(id);
		if (SoundManager.isPlaying == 1)
		{
			action.perform();
			return;
		}
		Canvas.startOKDlg(des, action);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00007150 File Offset: 0x00005550
	public static int setSound(string na)
	{
		if (SoundManager.name == null)
		{
			SoundManager.name = new MyVector();
		}
		for (int i = 0; i < SoundManager.name.size(); i++)
		{
			string text = (string)SoundManager.name.elementAt(i);
			if (text.Equals(na))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000071AC File Offset: 0x000055AC
	public static void onSoundData(byte[] dataS, sbyte id)
	{
		if (SoundManager.sound == null)
		{
			SoundManager.sound = new MyVector();
			SoundManager.name = new MyVector();
		}
		SoundManager.name.addElement(string.Empty + id);
		SoundManager.sound.addElement(dataS);
		SoundManager.playSoundData(dataS);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00007204 File Offset: 0x00005604
	private static void playSoundData(byte[] arr)
	{
		float[] array = SoundManager.ConvertByteToFloat(arr);
		AudioClip audioClip = AudioClip.Create("testSound", array.Length, 1, 44100, false, false);
		audioClip.SetData(array, 0);
		Out.println("playSoundData");
		AudioSource.PlayClipAtPoint(audioClip, new Vector3(100f, 100f, 0f), 1f);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00007260 File Offset: 0x00005660
	private static float[] ConvertByteToFloat(byte[] array)
	{
		float[] array2 = new float[array.Length / 4];
		for (int i = 0; i < array2.Length; i++)
		{
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array, i * 4, 4);
			}
			array2[i] = BitConverter.ToSingle(array, i * 4) / 2.14748365E+09f;
		}
		return array2;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000072B4 File Offset: 0x000056B4
	public static void playSound(int index)
	{
		if (!Main.isCompactDevice)
		{
			return;
		}
		if (OptionScr.instance != null && OptionScr.instance.volume == 0)
		{
			return;
		}
		SoundManager.isOpen = true;
		((MyAudioClip)SoundManager.soundPoolMap[index]).Play();
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00007306 File Offset: 0x00005706
	public static void setVolume(float volume)
	{
		Main.main.GetComponent<AudioSource>().volume = volume;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00007318 File Offset: 0x00005718
	public static void stop()
	{
		SoundManager.isStop = true;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00007320 File Offset: 0x00005720
	public static void playSoundBG(int index)
	{
		if (!Main.isCompactDevice)
		{
			return;
		}
		if (OptionScr.instance != null && OptionScr.instance.volume == 0)
		{
			return;
		}
		SoundManager.isOpen = true;
		Main.main.GetComponent<AudioSource>().volume = 1f;
		MyAudioClip myAudioClip = (MyAudioClip)SoundManager.soundPoolMap[index];
		if (myAudioClip.isPlaying())
		{
			return;
		}
		SoundManager.stopALLBGSound();
		myAudioClip.Play();
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000739C File Offset: 0x0000579C
	public static void stopALLBGSound()
	{
		if (!Main.isCompactDevice)
		{
			return;
		}
		Main.main.GetComponent<AudioSource>().Stop();
		((MyAudioClip)SoundManager.soundPoolMap[80]).timeStart = 0L;
		((MyAudioClip)SoundManager.soundPoolMap[82]).timeStart = 0L;
		((MyAudioClip)SoundManager.soundPoolMap[84]).timeStart = 0L;
		((MyAudioClip)SoundManager.soundPoolMap[83]).timeStart = 0L;
		((MyAudioClip)SoundManager.soundPoolMap[81]).timeStart = 0L;
		((MyAudioClip)SoundManager.soundPoolMap[85]).timeStart = 0L;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00007474 File Offset: 0x00005874
	public static void loadSoundsEffect()
	{
		SoundManager.soundPoolMap.Add(0, new MyAudioClip("sound/snd_effect_fishing_reel"));
		SoundManager.soundPoolMap.Add(1, new MyAudioClip("sound/snd_effect_fish"));
		SoundManager.soundPoolMap.Add(2, new MyAudioClip("sound/snd_effect_buy"));
		SoundManager.soundPoolMap.Add(3, new MyAudioClip("sound/snd_effect_earned_money"));
		SoundManager.soundPoolMap.Add(4, new MyAudioClip("sound/snd_effect_dao_dat"));
		SoundManager.soundPoolMap.Add(5, new MyAudioClip("sound/snd_effect_tuoi_nuoc"));
		SoundManager.soundPoolMap.Add(6, new MyAudioClip("sound/snd_effect_thu_hoach"));
		SoundManager.soundPoolMap.Add(7, new MyAudioClip("sound/snd_effect_touch"));
		SoundManager.soundPoolMap.Add(8, new MyAudioClip("sound/snd_effect_pig"));
		SoundManager.soundPoolMap.Add(9, new MyAudioClip("sound/snd_effect_chicken"));
		SoundManager.soundPoolMap.Add(10, new MyAudioClip("sound/snd_effect_cow"));
		SoundManager.soundPoolMap.Add(11, new MyAudioClip("sound/snd_effect_dog"));
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000075BC File Offset: 0x000059BC
	public static void loadSoundsAva()
	{
		SoundManager.soundPoolMap.Add(30, new MyAudioClip("sound/snd_ava_angry_b"));
		SoundManager.soundPoolMap.Add(31, new MyAudioClip("sound/snd_ava_angry_g"));
		SoundManager.soundPoolMap.Add(32, new MyAudioClip("sound/snd_ava_cry_b"));
		SoundManager.soundPoolMap.Add(33, new MyAudioClip("sound/snd_ava_cry_b_2"));
		SoundManager.soundPoolMap.Add(34, new MyAudioClip("sound/snd_ava_cry_g"));
		SoundManager.soundPoolMap.Add(35, new MyAudioClip("sound/snd_ava_cry_g_2"));
		SoundManager.soundPoolMap.Add(36, new MyAudioClip("sound/snd_ava_fight"));
		SoundManager.soundPoolMap.Add(37, new MyAudioClip("sound/snd_ava_fight_2"));
		SoundManager.soundPoolMap.Add(38, new MyAudioClip("sound/snd_ava_fight_3"));
		SoundManager.soundPoolMap.Add(39, new MyAudioClip("sound/snd_ava_fight_4"));
		SoundManager.soundPoolMap.Add(40, new MyAudioClip("sound/snd_ava_jump_b"));
		SoundManager.soundPoolMap.Add(41, new MyAudioClip("sound/snd_ava_jump_b_2"));
		SoundManager.soundPoolMap.Add(42, new MyAudioClip("sound/snd_ava_jump_g"));
		SoundManager.soundPoolMap.Add(43, new MyAudioClip("sound/snd_ava_jump_g_2"));
		SoundManager.soundPoolMap.Add(44, new MyAudioClip("sound/snd_ava_kiss"));
		SoundManager.soundPoolMap.Add(45, new MyAudioClip("sound/snd_ava_kiss_2"));
		SoundManager.soundPoolMap.Add(46, new MyAudioClip("sound/snd_ava_kiss_3"));
		SoundManager.soundPoolMap.Add(47, new MyAudioClip("sound/snd_ava_kiss_b"));
		SoundManager.soundPoolMap.Add(48, new MyAudioClip("sound/snd_ava_laugh_b"));
		SoundManager.soundPoolMap.Add(49, new MyAudioClip("sound/snd_ava_laugh_b_2"));
		SoundManager.soundPoolMap.Add(50, new MyAudioClip("sound/snd_ava_laugh_b_3"));
		SoundManager.soundPoolMap.Add(51, new MyAudioClip("sound/snd_ava_laugh_g"));
		SoundManager.soundPoolMap.Add(52, new MyAudioClip("sound/snd_ava_laugh_g_2"));
		SoundManager.soundPoolMap.Add(53, new MyAudioClip("sound/snd_ava_laugh_g_3"));
		SoundManager.soundPoolMap.Add(54, new MyAudioClip("sound/snd_ava_leuleu_b"));
		SoundManager.soundPoolMap.Add(56, new MyAudioClip("sound/snd_ava_leuleu_g"));
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00007888 File Offset: 0x00005C88
	public static void loadSoundsBG()
	{
		SoundManager.soundPoolMap.Add(80, new MyAudioClip("sound/snd_bg_crow"));
		SoundManager.soundPoolMap.Add(81, new MyAudioClip("sound/snd_bg_fishing"));
		SoundManager.soundPoolMap.Add(82, new MyAudioClip("sound/snd_bg_wedding"));
		SoundManager.soundPoolMap.Add(83, new MyAudioClip("sound/snd_bg_house"));
		SoundManager.soundPoolMap.Add(84, new MyAudioClip("sound/snd_bg_shop"));
		SoundManager.soundPoolMap.Add(85, new MyAudioClip("sound/snd_bg_city"));
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00007938 File Offset: 0x00005D38
	public static void loadSoundsAnimal()
	{
		SoundManager.soundPoolMap.Add(70, new MyAudioClip("sound/snd_ani_cow"));
		SoundManager.soundPoolMap.Add(71, new MyAudioClip("sound/snd_ani_dog"));
		SoundManager.soundPoolMap.Add(72, new MyAudioClip("sound/snd_ani_pig"));
		SoundManager.soundPoolMap.Add(73, new MyAudioClip("sound/snd_ani_chicken"));
	}

	// Token: 0x04000095 RID: 149
	public static Hashtable soundPoolMap;

	// Token: 0x04000096 RID: 150
	public static bool isStop;

	// Token: 0x04000097 RID: 151
	public static bool isOpen;

	// Token: 0x04000098 RID: 152
	private static MyVector sound;

	// Token: 0x04000099 RID: 153
	private static MyVector name;

	// Token: 0x0400009A RID: 154
	private static int isPlaying = -1;

	// Token: 0x0400009B RID: 155
	public static int currentBGMusic;

	// Token: 0x0400009C RID: 156
	public static bool isBGplay;

	// Token: 0x0400009D RID: 157
	public const int snd_effect_fishing_reel = 0;

	// Token: 0x0400009E RID: 158
	public const int snd_effect_fish = 1;

	// Token: 0x0400009F RID: 159
	public const int snd_effect_buy = 2;

	// Token: 0x040000A0 RID: 160
	public const int snd_effect_earned_money = 3;

	// Token: 0x040000A1 RID: 161
	public const int snd_effect_dao_dat = 4;

	// Token: 0x040000A2 RID: 162
	public const int snd_effect_tuoi_nuoc = 5;

	// Token: 0x040000A3 RID: 163
	public const int snd_effect_thu_hoach = 6;

	// Token: 0x040000A4 RID: 164
	public const int snd_effect_touch = 7;

	// Token: 0x040000A5 RID: 165
	public const int snd_effect_pig = 8;

	// Token: 0x040000A6 RID: 166
	public const int snd_effect_chicken = 9;

	// Token: 0x040000A7 RID: 167
	public const int snd_effect_cow = 10;

	// Token: 0x040000A8 RID: 168
	public const int snd_effect_dog = 11;

	// Token: 0x040000A9 RID: 169
	public const int snd_ava_angry_b = 30;

	// Token: 0x040000AA RID: 170
	public const int snd_ava_angry_g = 31;

	// Token: 0x040000AB RID: 171
	public const int snd_ava_cry_b = 32;

	// Token: 0x040000AC RID: 172
	public const int snd_ava_cry_b_2 = 33;

	// Token: 0x040000AD RID: 173
	public const int snd_ava_cry_g = 34;

	// Token: 0x040000AE RID: 174
	public const int snd_ava_cry_g_2 = 35;

	// Token: 0x040000AF RID: 175
	public const int snd_ava_fight = 36;

	// Token: 0x040000B0 RID: 176
	public const int snd_ava_fight_2 = 37;

	// Token: 0x040000B1 RID: 177
	public const int snd_ava_fight_3 = 38;

	// Token: 0x040000B2 RID: 178
	public const int snd_ava_fight_4 = 39;

	// Token: 0x040000B3 RID: 179
	public const int snd_ava_jump_b = 40;

	// Token: 0x040000B4 RID: 180
	public const int snd_ava_jump_b_2 = 41;

	// Token: 0x040000B5 RID: 181
	public const int snd_ava_jump_g = 42;

	// Token: 0x040000B6 RID: 182
	public const int snd_ava_jump_g_2 = 43;

	// Token: 0x040000B7 RID: 183
	public const int snd_ava_kiss = 44;

	// Token: 0x040000B8 RID: 184
	public const int snd_ava_kiss_2 = 45;

	// Token: 0x040000B9 RID: 185
	public const int snd_ava_kiss_3 = 46;

	// Token: 0x040000BA RID: 186
	public const int snd_ava_kiss_b = 47;

	// Token: 0x040000BB RID: 187
	public const int snd_ava_laugh_b = 48;

	// Token: 0x040000BC RID: 188
	public const int snd_ava_laugh_b_2 = 49;

	// Token: 0x040000BD RID: 189
	public const int snd_ava_laugh_b_3 = 50;

	// Token: 0x040000BE RID: 190
	public const int snd_ava_laugh_g = 51;

	// Token: 0x040000BF RID: 191
	public const int snd_ava_laugh_g_2 = 52;

	// Token: 0x040000C0 RID: 192
	public const int snd_ava_laugh_g_3 = 53;

	// Token: 0x040000C1 RID: 193
	public const int snd_ava_leuleu_b = 54;

	// Token: 0x040000C2 RID: 194
	public const int snd_ava_leuleu_b_2 = 55;

	// Token: 0x040000C3 RID: 195
	public const int snd_ava_leuleu_g = 56;

	// Token: 0x040000C4 RID: 196
	public const int snd_ava_leuleu_g_2 = 57;

	// Token: 0x040000C5 RID: 197
	public const int snd_ani_cow = 70;

	// Token: 0x040000C6 RID: 198
	public const int snd_ani_dog = 71;

	// Token: 0x040000C7 RID: 199
	public const int snd_ani_pig = 72;

	// Token: 0x040000C8 RID: 200
	public const int snd_ani_chicken = 73;

	// Token: 0x040000C9 RID: 201
	public const int snd_bg_crow = 80;

	// Token: 0x040000CA RID: 202
	public const int snd_bg_fishing = 81;

	// Token: 0x040000CB RID: 203
	public const int snd_bg_wedding = 82;

	// Token: 0x040000CC RID: 204
	public const int snd_bg_house = 83;

	// Token: 0x040000CD RID: 205
	public const int snd_bg_shop = 84;

	// Token: 0x040000CE RID: 206
	public const int snd_bg_city = 85;

	// Token: 0x0200001F RID: 31
	private class IActionYes : IAction
	{
		// Token: 0x06000124 RID: 292 RVA: 0x000079B9 File Offset: 0x00005DB9
		public IActionYes(sbyte id)
		{
			this.id = id;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000079C8 File Offset: 0x00005DC8
		public void perform()
		{
			int num = SoundManager.setSound(string.Empty + this.id);
			if (num == -1)
			{
				GlobalService.gI().doRequestSoundData(this.id);
			}
			else
			{
				SoundManager.playSoundData((byte[])SoundManager.sound.elementAt(num));
			}
			SoundManager.isPlaying = 1;
		}

		// Token: 0x040000CF RID: 207
		private sbyte id;
	}
}
