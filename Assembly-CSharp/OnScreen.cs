using System;

// Token: 0x02000180 RID: 384
public class OnScreen : MyScreen
{
	// Token: 0x06000A20 RID: 2592 RVA: 0x00063458 File Offset: 0x00061858
	public void addCmd(int type, int indexImg)
	{
		Command command = new Command(string.Empty, type);
		command.subIndex = (sbyte)indexImg;
		this.listCmd.addElement(command);
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00063488 File Offset: 0x00061888
	public override void updateKey()
	{
		base.updateKey();
		Canvas.paint.updateKeyOn(this.left, this.center, this.right);
		if (Canvas.isPointerClick)
		{
			for (int i = 0; i < this.listCmd.size(); i++)
			{
				if (Canvas.isPoint(4 + OnScreen.imgButtomSmall.frameWidth / 2 + (OnScreen.imgButtomSmall.frameHeight + 4) * i - OnScreen.imgButtomSmall.frameWidth / 2, Canvas.hCan - PaintPopup.hButtonSmall / 2 + 1 - OnScreen.imgButtomSmall.frameHeight / 2, OnScreen.imgButtomSmall.frameWidth, OnScreen.imgButtomSmall.frameHeight))
				{
					Command command = (Command)this.listCmd.elementAt(i);
					command.indexImage = 1;
					Canvas.isPointerClick = false;
					this.isTranCmd = true;
					break;
				}
			}
		}
		if (this.isTranCmd)
		{
			if (Canvas.isPointerDown)
			{
				for (int j = 0; j < this.listCmd.size(); j++)
				{
					Command command2 = (Command)this.listCmd.elementAt(j);
					if ((int)command2.indexImage == 1 && !Canvas.isPoint(4 + OnScreen.imgButtomSmall.frameWidth / 2 + (OnScreen.imgButtomSmall.frameHeight + 4) * j - OnScreen.imgButtomSmall.frameWidth / 2, Canvas.hCan - PaintPopup.hButtonSmall / 2 + 1 - OnScreen.imgButtomSmall.frameHeight / 2, OnScreen.imgButtomSmall.frameWidth, OnScreen.imgButtomSmall.frameHeight))
					{
						command2.indexImage = 0;
						break;
					}
				}
			}
			if (Canvas.isPointerRelease)
			{
				for (int k = 0; k < this.listCmd.size(); k++)
				{
					Command command3 = (Command)this.listCmd.elementAt(k);
					if ((int)command3.indexImage == 1 && Canvas.isPoint(4 + OnScreen.imgButtomSmall.frameWidth / 2 + (OnScreen.imgButtomSmall.frameHeight + 4) * k - OnScreen.imgButtomSmall.frameWidth / 2, Canvas.hCan - PaintPopup.hButtonSmall / 2 + 1 - OnScreen.imgButtomSmall.frameHeight / 2, OnScreen.imgButtomSmall.frameWidth, OnScreen.imgButtomSmall.frameHeight))
					{
						command3.indexImage = 0;
						command3.perform();
						Canvas.isPointerRelease = false;
						this.isTranCmd = false;
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x000636FE File Offset: 0x00061AFE
	public override void update()
	{
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00063700 File Offset: 0x00061B00
	public override void paint(MyGraphics g)
	{
		OnScreen.paintBar(g, this.left, this.center, this.right, this.listCmd);
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00063720 File Offset: 0x00061B20
	public static void paintBar(MyGraphics g, Command left, Command center, Command right, MyVector listCmd)
	{
		Canvas.resetTrans(g);
		Canvas.paint.paintTabSoft(g);
		if (((Canvas.currentDialog == null && !ChatTextField.isShow) || Canvas.currentDialog == TransMoneyDlg.me) && !ChatTextField.isShow)
		{
			Canvas.paint.paintCmdBar(g, left, center, right);
			if (listCmd != null)
			{
				for (int i = 0; i < listCmd.size(); i++)
				{
					Command command = (Command)listCmd.elementAt(i);
					OnScreen.imgButtomSmall.drawFrame((int)command.indexImage, 4 + OnScreen.imgButtomSmall.frameWidth / 2 + (OnScreen.imgButtomSmall.frameHeight + 4) * i, Canvas.hCan - PaintPopup.hButtonSmall / 2 + 1, 0, 3, g);
					OnScreen.imgIconButton.drawFrame((int)command.subIndex, 4 + OnScreen.imgButtomSmall.frameWidth / 2 + (OnScreen.imgButtomSmall.frameHeight + 4) * i, Canvas.hCan - PaintPopup.hButtonSmall / 2 + 1, 0, 3, g);
				}
			}
		}
	}

	// Token: 0x04000D19 RID: 3353
	public static FrameImage imgButtomSmall;

	// Token: 0x04000D1A RID: 3354
	public static FrameImage imgIconButton;

	// Token: 0x04000D1B RID: 3355
	public MyVector listCmd = new MyVector();

	// Token: 0x04000D1C RID: 3356
	private bool isTranCmd;
}
