using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class Session_ME : ISession
{
	// Token: 0x0600040E RID: 1038 RVA: 0x00025F5D File Offset: 0x0002435D
	public Session_ME()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00025F6F File Offset: 0x0002436F
	public void clearSendingMessage()
	{
		Session_ME.sender.sendingMessage.Clear();
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00025F80 File Offset: 0x00024380
	public static Session_ME gI()
	{
		if (Session_ME.instance == null)
		{
			Session_ME.instance = new Session_ME();
		}
		return Session_ME.instance;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00025F9B File Offset: 0x0002439B
	public bool isConnected()
	{
		return Session_ME.connected;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00025FA2 File Offset: 0x000243A2
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME.messageHandler = msgHandler;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00025FAC File Offset: 0x000243AC
	public void connect(string host, int port)
	{
		Debug.Log(string.Concat(new object[]
		{
			"connect ...!",
			Session_ME.connected,
			"  ::  ",
			Session_ME.connecting
		}));
		if (Session_ME.connected || Session_ME.connecting)
		{
			return;
		}
		this.host = host;
		this.port = port;
		Session_ME.getKeyComplete = false;
		Session_ME.sc = null;
		Debug.Log("connecting...!");
		Debug.Log("host: " + host);
		Debug.Log("port: " + port);
		Session_ME.initThread = new Thread(new ThreadStart(this.NetworkInit));
		Session_ME.initThread.Start();
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00026074 File Offset: 0x00024474
	private void NetworkInit()
	{
		Out.println("NetworkInit");
		Session_ME.isCancel = false;
		Session_ME.connecting = true;
		Thread.CurrentThread.Priority = ThreadPriority.Highest;
		Session_ME.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME.messageHandler.onConnectOK();
		}
		catch (Exception)
		{
			if (Session_ME.messageHandler != null)
			{
				this.close();
				Session_ME.messageHandler.onConnectionFail();
			}
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x000260FC File Offset: 0x000244FC
	public void doConnect(string host, int port)
	{
		try
		{
			Session_ME.isStart = true;
			Session_ME.timeStart = Canvas.getTick();
			Session_ME.sc = new TcpClient();
			Session_ME.sc.Connect(host, port);
			Session_ME.dataStream = Session_ME.sc.GetStream();
			Session_ME.isStart = false;
			Session_ME.dis = new BinaryReader(Session_ME.dataStream, new UTF8Encoding());
			Session_ME.dos = new BinaryWriter(Session_ME.dataStream, new UTF8Encoding());
			new Thread(new ThreadStart(Session_ME.sender.run)).Start();
			Session_ME.MessageCollector @object = new Session_ME.MessageCollector();
			Out.println("new -----");
			Session_ME.collectorThread = new Thread(new ThreadStart(@object.run));
			Session_ME.collectorThread.Start();
			Session_ME.timeConnected = Session_ME.currentTimeMillis();
			Session_ME.connecting = false;
			Session_ME.doSendMessage(new Message(-27));
		}
		catch (Exception)
		{
			if (Session_ME.messageHandler != null)
			{
				Session_ME.messageHandler.onConnectionFail();
			}
		}
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00026204 File Offset: 0x00024604
	public void sendMessage(Message message)
	{
		Session_ME.sender.AddMessage(message);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00026214 File Offset: 0x00024614
	private static void doSendMessage(Message m)
	{
		Out.println("---------->>> send: " + m.command);
		sbyte[] data = m.getData();
		int num = 0;
		try
		{
			Session_ME.test = 1 + " " + m.command;
			if (Session_ME.getKeyComplete)
			{
				sbyte value = Session_ME.writeKey(m.command);
				Session_ME.dos.Write(value);
			}
			else
			{
				Session_ME.dos.Write(m.command);
			}
			if (data != null)
			{
				Session_ME.test = 2 + " " + m.command;
				int num2 = data.Length;
				if (Session_ME.getKeyComplete)
				{
					int num3 = (int)Session_ME.writeKey((sbyte)(num2 >> 8));
					Session_ME.dos.Write((sbyte)num3);
					int num4 = (int)Session_ME.writeKey((sbyte)(num2 & 255));
					Session_ME.dos.Write((sbyte)num4);
				}
				else
				{
					Session_ME.dos.Write((ushort)num2);
				}
				Session_ME.test = 3 + " " + m.command;
				if (Session_ME.getKeyComplete)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME.writeKey(data[i]);
						Session_ME.dos.Write(value2);
					}
				}
				Session_ME.test = 4 + " " + m.command;
				Session_ME.sendByteCount += 5 + data.Length;
			}
			else
			{
				Session_ME.test = 5 + " " + m.command;
				if (Session_ME.getKeyComplete)
				{
					int num5 = 0;
					int num6 = (int)Session_ME.writeKey((sbyte)(num5 >> 8));
					Session_ME.dos.Write((sbyte)num6);
					int num7 = (int)Session_ME.writeKey((sbyte)(num5 & 255));
					Session_ME.dos.Write((sbyte)num7);
				}
				else
				{
					Session_ME.dos.Write(0);
				}
				Session_ME.sendByteCount += 5;
				Session_ME.test = 6 + " " + m.command;
			}
			Session_ME.dos.Flush();
			Session_ME.test = 7 + " " + m.command;
		}
		catch (Exception ex)
		{
			Out.println(string.Concat(new object[]
			{
				"ERROR SEND MSG: ",
				num,
				"   ",
				m.command
			}));
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x000264DC File Offset: 0x000248DC
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curR;
		Session_ME.curR = (sbyte)((int)b2 + 1);
		sbyte result = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME.curR >= Session_ME.key.Length)
		{
			Session_ME.curR = (sbyte)((int)Session_ME.curR % (int)((sbyte)Session_ME.key.Length));
		}
		return result;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0002653C File Offset: 0x0002493C
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curW;
		Session_ME.curW = (sbyte)((int)b2 + 1);
		sbyte result = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME.curW >= Session_ME.key.Length)
		{
			Session_ME.curW = (sbyte)((int)Session_ME.curW % (int)((sbyte)Session_ME.key.Length));
		}
		return result;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0002659A File Offset: 0x0002499A
	public static void onRecieveMsg(Message msg)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Session_ME.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x000265D0 File Offset: 0x000249D0
	public static void update()
	{
		while (Session_ME.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME.recieveMsg.elementAt(0);
			if (message == null)
			{
				Session_ME.recieveMsg.removeElementAt(0);
				return;
			}
			Session_ME.messageHandler.onMessage(message);
			Session_ME.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0002662B File Offset: 0x00024A2B
	public void close()
	{
		Session_ME.recieveMsg.removeAllElements();
		Session_ME.cleanNetwork();
		Session_ME.isStart = false;
		Session_ME.messageHandler = null;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00026648 File Offset: 0x00024A48
	private static void cleanNetwork()
	{
		Session_ME.key = null;
		Session_ME.curR = 0;
		Session_ME.curW = 0;
		try
		{
			Session_ME.connected = false;
			Session_ME.connecting = false;
			if (Session_ME.sc != null)
			{
				Session_ME.sc.Close();
				Session_ME.sc = null;
			}
			if (Session_ME.dataStream != null)
			{
				Session_ME.dataStream.Close();
				Session_ME.dataStream = null;
			}
			if (Session_ME.dos != null)
			{
				Session_ME.dos.Close();
				Session_ME.dos = null;
			}
			if (Session_ME.dis != null)
			{
				Session_ME.dis.Close();
				Session_ME.dis = null;
			}
			Session_ME.sendThread = null;
			Session_ME.collectorThread = null;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00026704 File Offset: 0x00024B04
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0002670B File Offset: 0x00024B0B
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00026724 File Offset: 0x00024B24
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0400066E RID: 1646
	protected static Session_ME instance = new Session_ME();

	// Token: 0x0400066F RID: 1647
	private static NetworkStream dataStream;

	// Token: 0x04000670 RID: 1648
	private static BinaryReader dis;

	// Token: 0x04000671 RID: 1649
	private static BinaryWriter dos;

	// Token: 0x04000672 RID: 1650
	public static IMessageHandler messageHandler;

	// Token: 0x04000673 RID: 1651
	private static TcpClient sc;

	// Token: 0x04000674 RID: 1652
	public static bool connected;

	// Token: 0x04000675 RID: 1653
	public static bool connecting;

	// Token: 0x04000676 RID: 1654
	public static bool isStart;

	// Token: 0x04000677 RID: 1655
	private static Session_ME.Sender sender = new Session_ME.Sender();

	// Token: 0x04000678 RID: 1656
	public static Thread initThread;

	// Token: 0x04000679 RID: 1657
	public static Thread collectorThread;

	// Token: 0x0400067A RID: 1658
	public static Thread sendThread;

	// Token: 0x0400067B RID: 1659
	public static int sendByteCount;

	// Token: 0x0400067C RID: 1660
	public static int recvByteCount;

	// Token: 0x0400067D RID: 1661
	private static bool getKeyComplete;

	// Token: 0x0400067E RID: 1662
	public static sbyte[] key = null;

	// Token: 0x0400067F RID: 1663
	private static sbyte curR;

	// Token: 0x04000680 RID: 1664
	private static sbyte curW;

	// Token: 0x04000681 RID: 1665
	private static int timeConnected;

	// Token: 0x04000682 RID: 1666
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04000683 RID: 1667
	public static bool isCancel;

	// Token: 0x04000684 RID: 1668
	private string host;

	// Token: 0x04000685 RID: 1669
	private int port;

	// Token: 0x04000686 RID: 1670
	public static long timeStart = 0L;

	// Token: 0x04000687 RID: 1671
	private static string test = string.Empty;

	// Token: 0x04000688 RID: 1672
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x0200007F RID: 127
	public class Sender
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x000267C0 File Offset: 0x00024BC0
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000267D3 File Offset: 0x00024BD3
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000267E4 File Offset: 0x00024BE4
		public void run()
		{
			while (Session_ME.connected)
			{
				try
				{
					if (Session_ME.getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message m = this.sendingMessage[0];
							Session_ME.doSendMessage(m);
							this.sendingMessage.RemoveAt(0);
						}
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception e)
					{
						Out.logError(e);
					}
				}
				catch (Exception e2)
				{
					Debug.Log("error send message!: " + Session_ME.test);
					Out.logError(e2);
				}
			}
		}

		// Token: 0x04000689 RID: 1673
		public List<Message> sendingMessage;
	}

	// Token: 0x02000080 RID: 128
	private class MessageCollector
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x000268A0 File Offset: 0x00024CA0
		public void run()
		{
			try
			{
				while (Session_ME.connected)
				{
					Message message = this.readMessage();
					if (message == null)
					{
						break;
					}
					try
					{
						if ((int)message.command == -27)
						{
							this.getKey(message);
						}
						else
						{
							Out.println("<<<---------- recieve: " + message.command);
							Session_ME.onRecieveMsg(message);
						}
					}
					catch (Exception e)
					{
						Out.logError(e);
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception e2)
					{
						Out.logError(e2);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log("error read message!");
				Debug.Log(ex.Message.ToString());
			}
			if (Session_ME.connected)
			{
				if (Session_ME.messageHandler != null)
				{
					if (Session_ME.currentTimeMillis() - Session_ME.timeConnected > 500)
					{
						Session_ME.messageHandler.onDisconnected();
					}
					else
					{
						Session_ME.messageHandler.onConnectionFail();
					}
				}
				if (Session_ME.sc != null)
				{
					Session_ME.cleanNetwork();
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000269D4 File Offset: 0x00024DD4
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME.key;
					int num = j + 1;
					key[num] = (sbyte)((int)key[num] ^ (int)Session_ME.key[j]);
				}
				Session_ME.getKeyComplete = true;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00026A74 File Offset: 0x00024E74
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME.dis.ReadSByte();
				if (Session_ME.getKeyComplete)
				{
					b = Session_ME.readKey(b);
				}
				if ((int)b == 50)
				{
					Canvas.text = Canvas.text + "  read: " + b;
				}
				int num;
				if (Session_ME.getKeyComplete)
				{
					sbyte b2 = Session_ME.dis.ReadSByte();
					sbyte b3 = Session_ME.dis.ReadSByte();
					num = (((int)Session_ME.readKey(b2) & 255) << 8 | ((int)Session_ME.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME.dis.ReadSByte();
					sbyte b5 = Session_ME.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME.recvByteCount += 5 + num;
				int num2 = Session_ME.recvByteCount + Session_ME.sendByteCount;
				Session_ME.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				if (Session_ME.getKeyComplete)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME.readKey(array[i]);
					}
				}
				return new Message(b, array);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.StackTrace.ToString());
			}
			return null;
		}
	}
}
