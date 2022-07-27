using System;
using System.Threading;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class Main : MonoBehaviour
{
	// Token: 0x06000049 RID: 73 RVA: 0x0000406C File Offset: 0x0000246C
	private void Start()
	{
		if (!Main.started)
		{
			if (Application.platform == 8 && (Screen.height == 768 || Screen.height == 1536))
			{
				Screen.SetResolution(1024, 768, true);
			}
			Screen.orientation = 3;
			Application.runInBackground = true;
			Application.targetFrameRate = 30;
			base.useGUILayout = false;
			Main.isCompactDevice = Main.detectCompactDevice();
			if (Main.main == null)
			{
				Main.main = this;
			}
			Main.started = true;
			ScaleGUI.initScaleGUI();
			if (Thread.CurrentThread.Name != "Main")
			{
				Thread.CurrentThread.Name = "Main";
			}
			Main.mainThreadName = Thread.CurrentThread.Name;
			Debug.Log("Start main thread name: " + Main.mainThreadName);
			SoundManager.init();
			Out.println(string.Concat(new object[]
			{
				"aaa: ",
				ScaleGUI.WIDTH,
				"    ",
				ScaleGUI.HEIGHT
			}));
			Main.hdtype = 2;
			Main.g = new MyGraphics();
			Main.midlet = new GameMidlet();
			Main.loadLanguage();
			Main.canvas = new Canvas();
			Main.canvas.start();
			OptionScr.gI().load();
			AvatarData.loadIP();
			Main.canvas.sizeChanged((int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT);
			SplashScr.gI().switchToMe();
			AvatarData.loadMyAccount();
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000041F4 File Offset: 0x000025F4
	public static void loadLanguage()
	{
		DataInputStream dataInputStream = null;
		sbyte[] array = RMS.loadRMS("avlanguage");
		if (array != null)
		{
			dataInputStream = new DataInputStream(array);
		}
		if (dataInputStream == null)
		{
			return;
		}
		try
		{
			GameMidlet.saveLanguage = dataInputStream.readBoolean();
			GameMidlet.isEnglish = dataInputStream.readBoolean();
			dataInputStream.close();
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
		if (GameMidlet.isEnglish)
		{
			GameMidlet.loadEnglish = true;
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004270 File Offset: 0x00002670
	private void OnGUI()
	{
		this.checkInput();
		if (Event.current.type.Equals(7))
		{
			Main.canvas.onPaint(Main.g);
			this.paintCount++;
			Main.g.reset();
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000042D0 File Offset: 0x000026D0
	private void OnApplicationPause(bool paused)
	{
		if (Canvas.currentMyScreen != null)
		{
			if (paused)
			{
				this.timeLimit = Canvas.getTick();
				if (Canvas.msgdlg.isWaiting)
				{
					MapScr.gI().exitGame();
				}
			}
			else
			{
				GlobalLogicHandler.isAutoLogin = false;
				if (!Session_ME.gI().isConnected())
				{
					AvCamera.gI().xCam = (AvCamera.gI().xTo = 100f);
					MapScr.gI().exitGame();
					LoginScr.gI().switchToMe();
				}
				else if ((Canvas.getTick() - this.timeLimit) / 1000L > 60L)
				{
					Canvas.setPopupTime(T.disConnect);
					AvCamera.gI().xCam = (AvCamera.gI().xTo = 100f);
					MapScr.gI().exitGame();
					LoginScr.gI().switchToMe();
				}
			}
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000043B8 File Offset: 0x000027B8
	private void checkInput()
	{
		if (Input.anyKeyDown)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			if (num != 0)
			{
				Main.canvas.onKeyPressed(num);
			}
		}
		if (Event.current.type == 5)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			if (num2 != 0)
			{
				Main.canvas.onKeyReleased(num2);
			}
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00004424 File Offset: 0x00002824
	public void checkKey()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			Main.canvas.pointerPressed((int)ScaleGUI.scaleX(mousePosition.x), (int)ScaleGUI.scaleY((float)Screen.height - mousePosition.y));
			this.lastMousePos.x = mousePosition.x;
			this.lastMousePos.y = mousePosition.y;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			Main.canvas.pointerDragged((int)ScaleGUI.scaleX(mousePosition2.x), (int)ScaleGUI.scaleY((float)Screen.height - mousePosition2.y));
			this.lastMousePos.x = mousePosition2.x;
			this.lastMousePos.y = mousePosition2.y;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePosition3 = Input.mousePosition;
			this.lastMousePos.x = mousePosition3.x;
			this.lastMousePos.y = mousePosition3.y;
			long num = (long)Environment.TickCount;
			Main.canvas.pointerReleased((int)ScaleGUI.scaleX(mousePosition3.x), (int)ScaleGUI.scaleY((float)Screen.height - mousePosition3.y));
			this.lastReleased = num;
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000455F File Offset: 0x0000295F
	private void OnApplicationQuit()
	{
		Debug.LogWarning("APP QUIT");
		Canvas.bRun = false;
		Session_ME.gI().close();
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0000457B File Offset: 0x0000297B
	private void FixedUpdate()
	{
		Main.canvas.update();
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00004587 File Offset: 0x00002987
	private void Update()
	{
		this.checkKey();
		ipKeyboard.update();
		RMS.update();
		Image.update();
		DataInputStream.update();
		Player.update();
		SMS.update();
		Session_ME.update();
		Net.update();
		this.updateCount++;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000045C5 File Offset: 0x000029C5
	public static void exit()
	{
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000045C7 File Offset: 0x000029C7
	public static bool detectCompactDevice()
	{
		return true;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000045CA File Offset: 0x000029CA
	public static bool checkCanSendSMS()
	{
		return false;
	}

	// Token: 0x04000035 RID: 53
	public static Main main;

	// Token: 0x04000036 RID: 54
	public static Canvas canvas;

	// Token: 0x04000037 RID: 55
	public static MyGraphics g;

	// Token: 0x04000038 RID: 56
	public static GameMidlet midlet;

	// Token: 0x04000039 RID: 57
	public static string res = "sd";

	// Token: 0x0400003A RID: 58
	public static string mainThreadName;

	// Token: 0x0400003B RID: 59
	public static bool started;

	// Token: 0x0400003C RID: 60
	private long lastReleased;

	// Token: 0x0400003D RID: 61
	public static int hdtype;

	// Token: 0x0400003E RID: 62
	private int updateCount;

	// Token: 0x0400003F RID: 63
	private int paintCount;

	// Token: 0x04000040 RID: 64
	public static string text = string.Empty;

	// Token: 0x04000041 RID: 65
	private long timeLimit;

	// Token: 0x04000042 RID: 66
	public static string test = string.Empty;

	// Token: 0x04000043 RID: 67
	private Vector2 lastMousePos = default(Vector2);

	// Token: 0x04000044 RID: 68
	public string s;

	// Token: 0x04000045 RID: 69
	public static bool isCompactDevice = true;
}
