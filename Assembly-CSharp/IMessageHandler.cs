using System;

// Token: 0x0200007B RID: 123
public interface IMessageHandler
{
	// Token: 0x060003FD RID: 1021
	void onMessage(Message message);

	// Token: 0x060003FE RID: 1022
	void onConnectionFail();

	// Token: 0x060003FF RID: 1023
	void onDisconnected();

	// Token: 0x06000400 RID: 1024
	void onConnectOK();
}
