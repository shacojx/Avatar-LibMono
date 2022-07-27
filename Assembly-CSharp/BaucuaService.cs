using System;

// Token: 0x02000086 RID: 134
public class BaucuaService
{
	// Token: 0x0600044F RID: 1103 RVA: 0x00027A91 File Offset: 0x00025E91
	public static BaucuaService gI()
	{
		return (BaucuaService.me != null) ? BaucuaService.me : (BaucuaService.me = new BaucuaService());
	}

	// Token: 0x04000767 RID: 1895
	private static BaucuaService me;

	// Token: 0x04000768 RID: 1896
	public ISession session = Session_ME.gI();
}
