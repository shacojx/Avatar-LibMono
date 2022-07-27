using System;

// Token: 0x0200004A RID: 74
public interface IPaint
{
	// Token: 0x0600029E RID: 670
	void paintTextBox(MyGraphics g, int x, int y, int width, int height, TField tf, bool isFocus, sbyte indexEraser);

	// Token: 0x0600029F RID: 671
	void paintBoxTab(MyGraphics g, int x, int y, int h, int w, int focusTab, int wSub, int wTab, int hTab, int numTab, int maxTab, int[] count, int[] colorTab, string name, sbyte countCloseAll, sbyte countCloseSmall, bool isMenu, bool isFull, string[] subName, float cmx, Image[][] imgIcon);

	// Token: 0x060002A0 RID: 672
	void paintCmd(MyGraphics g, Command left, Command center, Command right);

	// Token: 0x060002A1 RID: 673
	void init();

	// Token: 0x060002A2 RID: 674
	void initImgCard();

	// Token: 0x060002A3 RID: 675
	void paintHalf(MyGraphics g, Card c);

	// Token: 0x060002A4 RID: 676
	void paintHalfBackFull(MyGraphics g, Card c);

	// Token: 0x060002A5 RID: 677
	void paintFull(MyGraphics g, Card c);

	// Token: 0x060002A6 RID: 678
	void paintSmall(MyGraphics g, Card c, bool isCh);

	// Token: 0x060002A7 RID: 679
	void paintMSG(MyGraphics g);

	// Token: 0x060002A8 RID: 680
	void initPos();

	// Token: 0x060002A9 RID: 681
	int collisionCmdBar(Command left, Command center, Command right);

	// Token: 0x060002AA RID: 682
	void initPosLogin(LoginScr lg, int h);

	// Token: 0x060002AB RID: 683
	void getSound(string path, int loop);

	// Token: 0x060002AC RID: 684
	void setAnimalSound(MyVector animalLists);

	// Token: 0x060002AD RID: 685
	void setSoundAnimalFarm();

	// Token: 0x060002AE RID: 686
	void clickSound();

	// Token: 0x060002AF RID: 687
	void paintBGCMD(MyGraphics g, int x, int y, int w, int h);

	// Token: 0x060002B0 RID: 688
	void paintCheckBox(MyGraphics g, int x, int y, int focus, bool isCheck);

	// Token: 0x060002B1 RID: 689
	void paintSelected(MyGraphics g, int x, int y, int w, int h);

	// Token: 0x060002B2 RID: 690
	void paintArrow(MyGraphics g, int index, int x, int y, int w, int indLeft, int indRight);

	// Token: 0x060002B3 RID: 691
	void paintNormalFont(MyGraphics g, string str, int x, int y, int anthor, bool isSelect);

	// Token: 0x060002B4 RID: 692
	int getWNormalFont(string str);

	// Token: 0x060002B5 RID: 693
	void paintSelected_2(MyGraphics g, int x, int y, int w, int h);

	// Token: 0x060002B6 RID: 694
	void paintTransBack(MyGraphics g);

	// Token: 0x060002B7 RID: 695
	void paintKeyArrow(MyGraphics g, int x, int y);

	// Token: 0x060002B8 RID: 696
	int updateKeyArr(int x, int y);

	// Token: 0x060002B9 RID: 697
	void setVirtualKeyFish(int index);

	// Token: 0x060002BA RID: 698
	void initPosPhom();

	// Token: 0x060002BB RID: 699
	int initShop();

	// Token: 0x060002BC RID: 700
	bool selectedPointer(int xF, int yF);

	// Token: 0x060002BD RID: 701
	string doJoinGo(int x, int y);

	// Token: 0x060002BE RID: 702
	void setDrawPointer(Command left, Command center, Command right);

	// Token: 0x060002BF RID: 703
	void setBack();

	// Token: 0x060002C0 RID: 704
	void paintList(MyGraphics g, int w, int maxW, int maxH, bool isHide, int selected, int[] listBoard);

	// Token: 0x060002C1 RID: 705
	void setLanguage();

	// Token: 0x060002C2 RID: 706
	void paintDefaultBg(MyGraphics g);

	// Token: 0x060002C3 RID: 707
	void paintLogo(MyGraphics g, int x, int y);

	// Token: 0x060002C4 RID: 708
	void paintDefaultScrList(MyGraphics g, string title, string subTitle, string check);

	// Token: 0x060002C5 RID: 709
	void initImgBoard(int type);

	// Token: 0x060002C6 RID: 710
	void setColorBar();

	// Token: 0x060002C7 RID: 711
	void initOngame();

	// Token: 0x060002C8 RID: 712
	void resetOngame();

	// Token: 0x060002C9 RID: 713
	void initString(int type);

	// Token: 0x060002CA RID: 714
	void paintRoomList(MyGraphics g, MyVector roomList, int hSmall, int cmy);

	// Token: 0x060002CB RID: 715
	void setName(int index, BoardScr board);

	// Token: 0x060002CC RID: 716
	void paintPlayer(MyGraphics g, int index, int male, int countLeft, int countRight);

	// Token: 0x060002CD RID: 717
	void updateKeyRegister();

	// Token: 0x060002CE RID: 718
	void initReg();

	// Token: 0x060002CF RID: 719
	void paintPopupBack(MyGraphics g, int x, int y, int w, int h, int countClose, bool isFull);

	// Token: 0x060002D0 RID: 720
	void paintButton(MyGraphics g, int x, int y, int index, string text);

	// Token: 0x060002D1 RID: 721
	void resetCasino();

	// Token: 0x060002D2 RID: 722
	void paintMoney(MyGraphics g, int x, int y);

	// Token: 0x060002D3 RID: 723
	void paintTabSoft(MyGraphics g);

	// Token: 0x060002D4 RID: 724
	void paintCmdBar(MyGraphics g, Command left, Command center, Command right);

	// Token: 0x060002D5 RID: 725
	void paintDefaultPopup(MyGraphics g, int x, int y, int w, int h);

	// Token: 0x060002D6 RID: 726
	void paintLineRoom(MyGraphics g, int x, int y, int xTo, int yTo);

	// Token: 0x060002D7 RID: 727
	void paintSelect(MyGraphics g, int x, int y, int w, int h);

	// Token: 0x060002D8 RID: 728
	void updateKeyOn(Command left, Command center, Command right);

	// Token: 0x060002D9 RID: 729
	void paintBorderTitle(MyGraphics g, int x, int y, int w, int h);

	// Token: 0x060002DA RID: 730
	void clearImgAvatar();

	// Token: 0x060002DB RID: 731
	void loadImgAvatar();

	// Token: 0x060002DC RID: 732
	void paintTransMoney(MyGraphics g, int x, int y, int w, int h);
}
