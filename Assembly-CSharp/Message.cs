using System;

// Token: 0x0200007D RID: 125
public class Message
{
	// Token: 0x06000406 RID: 1030 RVA: 0x00025EDB File Offset: 0x000242DB
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00025EF6 File Offset: 0x000242F6
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00025F09 File Offset: 0x00024309
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00025F23 File Offset: 0x00024323
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00025F3E File Offset: 0x0002433E
	public sbyte[] getData()
	{
		return this.dos.getData();
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00025F4B File Offset: 0x0002434B
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00025F53 File Offset: 0x00024353
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00025F5B File Offset: 0x0002435B
	public void cleanup()
	{
	}

	// Token: 0x0400066B RID: 1643
	public sbyte command;

	// Token: 0x0400066C RID: 1644
	private myReader dis;

	// Token: 0x0400066D RID: 1645
	private myWriter dos;
}
