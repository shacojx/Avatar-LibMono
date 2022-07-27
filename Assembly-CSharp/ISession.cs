using System;

// Token: 0x0200007C RID: 124
public interface ISession
{
	// Token: 0x06000401 RID: 1025
	bool isConnected();

	// Token: 0x06000402 RID: 1026
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x06000403 RID: 1027
	void connect(string host, int port);

	// Token: 0x06000404 RID: 1028
	void sendMessage(Message message);

	// Token: 0x06000405 RID: 1029
	void close();
}
