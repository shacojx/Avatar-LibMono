using System;

// Token: 0x02000083 RID: 131
public class AvatarMsgHandler : IMiniGameMsgHandler
{
	// Token: 0x0600042C RID: 1068 RVA: 0x00026C44 File Offset: 0x00025044
	public static void onHandler()
	{
		GlobalMessageHandler.gI().miniGameMessageHandler = AvatarMsgHandler.instance;
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00026C58 File Offset: 0x00025058
	public void onMessage(Message msg)
	{
		try
		{
			sbyte command = msg.command;
			switch (command + 16)
			{
			case 0:
			{
				sbyte[] arr = new sbyte[msg.reader().available()];
				msg.reader().read(ref arr);
				AvatarData.saveAvatarPart(arr);
				break;
			}
			case 1:
			{
				sbyte[] arr2 = new sbyte[msg.reader().available()];
				msg.reader().read(ref arr2);
				AvatarData.saveImageData(arr2);
				break;
			}
			case 2:
			{
				BigImgInfo bigImgInfo = new BigImgInfo();
				bigImgInfo.id = msg.reader().readShort();
				bigImgInfo.ver = msg.reader().readShort();
				int num = (int)msg.reader().readUnsignedShort();
				bigImgInfo.data = new sbyte[num];
				for (int i = 0; i < num; i++)
				{
					bigImgInfo.data[i] = msg.reader().readByte();
				}
				bigImgInfo.follow = -1;
				if (msg.reader().available() >= 2)
				{
					bigImgInfo.follow = msg.reader().readShort();
				}
				AvatarData.addDataBig(bigImgInfo);
				break;
			}
			default:
				switch (command + 41)
				{
				case 0:
				{
					sbyte[] arr3 = new sbyte[msg.reader().available()];
					msg.reader().read(ref arr3);
					AvatarData.saveMapItem(arr3);
					break;
				}
				case 1:
				{
					sbyte[] arr4 = new sbyte[msg.reader().available()];
					msg.reader().read(ref arr4);
					AvatarData.saveMapItemType(arr4);
					break;
				}
				case 4:
				{
					sbyte[] arr5 = new sbyte[msg.reader().available()];
					msg.reader().read(ref arr5);
					AvatarData.saveItemData(arr5);
					break;
				}
				}
				break;
			case 5:
			{
				MyVector myVector = new MyVector();
				sbyte b = msg.reader().readByte();
				for (int j = 0; j < (int)b; j++)
				{
					myVector.addElement(new BigImgInfo
					{
						id = msg.reader().readShort(),
						ver = msg.reader().readShort()
					});
				}
				int verBigImg = (int)msg.reader().readShort();
				int verPart = (int)msg.reader().readShort();
				int verBigItemImg = (int)msg.reader().readShort();
				int vItemType = (int)msg.reader().readShort();
				int vItem = (int)msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				for (int k = 0; k < (int)b2; k++)
				{
					myVector.addElement(new BigImgInfo
					{
						id = msg.reader().readShort(),
						ver = msg.reader().readShort()
					});
				}
				int vObj = msg.reader().readInt();
				Canvas.avataData.checkDataAvatar(myVector, verBigImg, verPart, verBigItemImg, vItemType, vItem, vObj);
				break;
			}
			}
		}
		catch (Exception e)
		{
			Out.logError(e);
		}
	}

	// Token: 0x04000764 RID: 1892
	private static AvatarMsgHandler instance = new AvatarMsgHandler();
}
