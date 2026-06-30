using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000133 RID: 307
public class Friend_UICon : Window
{
	// Token: 0x06000BA6 RID: 2982 RVA: 0x0006D508 File Offset: 0x0006B708
	protected override void Awake()
	{
		base.Awake();
		Friend_UICon.inst = this;
		this.btn1 = this.UIGameObj.Get_GameObj(1);
		this.btnimg1 = this.btn1.transform.FindChild("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.btn1).onClick = new EventTriggerListener.VoidDelegate(this.OnBtnClick);
		this.btn2 = this.UIGameObj.Get_GameObj(2);
		this.btnimg2 = this.btn2.transform.FindChild("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.btn2).onClick = new EventTriggerListener.VoidDelegate(this.OnBtnClick);
		this.btn3 = this.UIGameObj.Get_GameObj(3);
		this.btnimg3 = this.btn3.transform.FindChild("Image").GetComponent<Image>();
		EventTriggerListener.Get(this.btn3).onClick = new EventTriggerListener.VoidDelegate(this.OnBtnClick);
		this.btn4 = this.UIGameObj.Get_GameObj(4);
		EventTriggerListener.Get(this.btn4).onClick = new EventTriggerListener.VoidDelegate(this.OnBtnClick);
		this.wInput = this.UIGameObj.Get_GameObj(5).GetComponent<InputField>();
		EventTriggerListener.Get(this.wInput.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.OnFocus);
		EventTriggerListener.Get(this.wInput.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.OnFocusExit);
		this.frienduser = this.UIGameObj.Get_GameObj(8);
		this.frienduser.SetActive(false);
		this.latelyuser = this.UIGameObj.Get_GameObj(11);
		this.latelyuser.SetActive(false);
		this.msguser = this.UIGameObj.Get_GameObj(14);
		this.msguser.SetActive(false);
		this.FriendContent = this.UIGameObj.Get_GameObj(7);
		this.LatelyContent = this.UIGameObj.Get_GameObj(10);
		this.MsgContent = this.UIGameObj.Get_GameObj(13);
		this.rawimg = this.UIGameObj.Get_GameObj(16).GetComponent<RawImage>();
		this.sLevel = this.UIGameObj.Get_GameObj(17).GetComponent<Text>();
		this.sname = this.UIGameObj.Get_GameObj(18).GetComponent<Text>();
		this.tipobj = this.UIGameObj.Get_GameObj(15);
		this.tipobj.SetActive(false);
		this.scw1 = this.UIGameObj.Get_GameObj(6);
		this.scw2 = this.UIGameObj.Get_GameObj(9);
		this.scw2.SetActive(false);
		this.scw3 = this.UIGameObj.Get_GameObj(12);
		this.scw3.SetActive(false);
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x0006D7E5 File Offset: 0x0006B9E5
	private void OnFocus(GameObject obj)
	{
		ChatManager.ChatInput = true;
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x0006D7EE File Offset: 0x0006B9EE
	private void OnFocusExit(GameObject obj)
	{
		ChatManager.ChatInput = false;
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0006D7F7 File Offset: 0x0006B9F7
	private void Start()
	{
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0006D7FA File Offset: 0x0006B9FA
	private void Update()
	{
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0006D800 File Offset: 0x0006BA00
	public void InitFriend()
	{
		bool isInitFriend = this.IsInitFriend;
		if (!isInitFriend)
		{
			this.IsInitFriend = true;
			ArrayList szFriendData = PlayerDataManager.getInstance().HUserData.m_szFriendData;
			ArrayList userdata = new ArrayList();
for (int i = 1; i < szFriendData.Count; i += 2)
			{
				userdata = (ArrayList)szFriendData[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.frienduser);
				gameObject.name = userdata[0].ToString();
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.FriendContent.transform);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				Text component = gameObject.transform.FindChild("Textname").GetComponent<Text>();
				Text component2 = gameObject.transform.FindChild("Textlevel").GetComponent<Text>();
				Image component3 = gameObject.transform.FindChild("ImageLine").GetComponent<Image>();
				Button component4 = gameObject.transform.FindChild("Button").GetComponent<Button>();
				UnityEvent onClick = component4.onClick;
				onClick.AddListener(delegate() {
						ChatManager.GetInstance().OnMsgTip(userdata[1].ToString(), 1, null, 0);
					});
				EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnter);
				EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.onExit);
				component.text = userdata[1].ToString();
				component2.text = userdata[2].ToString();
				Text component5 = gameObject.transform.FindChild("face").GetComponent<Text>();
				component5.text = userdata[3].ToString();
				bool flag = (int)userdata[5] == 1;
				if (flag)
				{
					this.SetBackground(component3, "ab/friendatlas", "kongxian");
				}
				else
				{
					this.SetBackground(component3, "ab/friendatlas", "likai");
				}
			}
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0006DA4C File Offset: 0x0006BC4C
	public void InitLately()
	{
		bool isInitLately = this.IsInitLately;
		if (!isInitLately)
		{
			this.IsInitLately = true;
			ArrayList szFriendLately = PlayerDataManager.getInstance().HUserData.m_szFriendLately;
			ArrayList userdata = new ArrayList();
for (int i = 1; i < szFriendLately.Count; i += 2)
			{
				userdata = (ArrayList)szFriendLately[i];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.latelyuser);
				gameObject.name = userdata[0].ToString();
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.LatelyContent.transform);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				Text component = gameObject.transform.FindChild("Textname").GetComponent<Text>();
				Text component2 = gameObject.transform.FindChild("Textlevel").GetComponent<Text>();
				Button component3 = gameObject.transform.FindChild("Button").GetComponent<Button>();
				UnityEvent onClick = component3.onClick;
				onClick.AddListener(delegate() {
						ChatManager.GetInstance().OnMsgTip(userdata[1].ToString(), 1, null, 0);
					});
				component.text = userdata[1].ToString();
				component2.text = Fun.SecToFullTime((int)userdata[4]);
				Text component4 = gameObject.transform.FindChild("face").GetComponent<Text>();
				component4.text = userdata[3].ToString();
			}
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0006DC0C File Offset: 0x0006BE0C
	public void InitUnMsg()
	{
		bool isInitUnMsg = this.IsInitUnMsg;
		if (!isInitUnMsg)
		{
			this.IsInitUnMsg = true;
			ArrayList arr = PlayerDataManager.getInstance().HUserData.m_szFriendUnMsg;

for (int i = 0; i < arr.Count; i += 2)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.msguser);
				gameObject.name = arr[i].ToString();
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.MsgContent.transform);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				Text component = gameObject.transform.FindChild("Text").GetComponent<Text>();
				component.text = arr[0] + "请求加您为好友";
				Button component2 = gameObject.transform.FindChild("Button").GetComponent<Button>();
				UnityEvent onClick = component2.onClick;
				onClick.AddListener(delegate() {
						this.OnUnMsgClick(arr[0].ToString(), 1);
					});
				Button component3 = gameObject.transform.FindChild("Button1").GetComponent<Button>();
				UnityEvent onClick2 = component3.onClick;
				onClick2.AddListener(delegate() {
						this.OnUnMsgClick(arr[0].ToString(), 0);
					});
			}
		}
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0006DD9C File Offset: 0x0006BF9C
	public void UpDataOneFriend(string uname, int nState)
	{
		ArrayList arrayList = new ArrayList();
		int num = PlayerDataManager.getInstance().HUserData.m_szFriendData.LastIndexOf(uname);
		bool flag = num != -1;
		if (flag)
		{
			arrayList = (ArrayList)PlayerDataManager.getInstance().HUserData.m_szFriendData[num + 1];
		}
		string name = arrayList[0].ToString();
		GameObject gameObject = this.FriendContent.transform.FindChild(name).gameObject;
		bool flag2 = gameObject != null;
		if (flag2)
		{
			Image component = gameObject.transform.FindChild("ImageLine").GetComponent<Image>();
			bool flag3 = (int)arrayList[5] == 1;
			if (flag3)
			{
				this.SetBackground(component, "ab/friendatlas", "kongxian");
			}
			else
			{
				this.SetBackground(component, "ab/friendatlas", "likai");
			}
		}
		bool flag4 = nState == 1;
		if (flag4)
		{
			gameObject.transform.SetSiblingIndex(0);
		}
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0006DE98 File Offset: 0x0006C098
	public void Add_Del_User(ArrayList arr, int nstate)
	{
		bool flag = nstate == 1;
		if (flag)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.frienduser);
			gameObject.name = arr[0].ToString();
			gameObject.SetActive(true);
			gameObject.transform.SetParent(this.FriendContent.transform);
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = Vector3.zero;
			Text component = gameObject.transform.FindChild("Textname").GetComponent<Text>();
			Text component2 = gameObject.transform.FindChild("Textlevel").GetComponent<Text>();
			Image component3 = gameObject.transform.FindChild("ImageLine").GetComponent<Image>();
			Button component4 = gameObject.transform.FindChild("Button").GetComponent<Button>();
			component4.onClick.AddListener(delegate()
			{
				ChatManager.GetInstance().OnMsgTip(arr[1].ToString(), 1, null, 0);
			});
			EventTriggerListener.Get(gameObject).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnter);
			EventTriggerListener.Get(gameObject).onExit = new EventTriggerListener.VoidDelegate(this.onExit);
			component.text = arr[1].ToString();
			component2.text = arr[2].ToString();
			Text component5 = gameObject.transform.FindChild("face").GetComponent<Text>();
			component5.text = arr[3].ToString();
			bool flag2 = (int)arr[5] == 1;
			if (flag2)
			{
				this.SetBackground(component3, "ab/friendatlas", "kongxian");
			}
			else
			{
				this.SetBackground(component3, "ab/friendatlas", "likai");
			}
			gameObject.transform.SetSiblingIndex(0);
		}
		else
		{
			GameObject gameObject2 = this.FriendContent.transform.FindChild(arr[0].ToString()).gameObject;
			UnityEngine.Object.Destroy(gameObject2);
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0006E0B0 File Offset: 0x0006C2B0
	private void OnEnter(GameObject obj)
	{
		Text component = obj.transform.FindChild("face").GetComponent<Text>();
		Text component2 = obj.transform.FindChild("Textname").GetComponent<Text>();
		Text component3 = obj.transform.FindChild("Textlevel").GetComponent<Text>();
		this.sname.text = component2.text;
		this.sLevel.text = component3.text;
		bool flag = fGameManager.mloadConfig.AllFaceData.ContainsKey(int.Parse(component.text));
		if (flag)
		{
			PlayerFaceData playerFaceData = fGameManager.mloadConfig.AllFaceData[int.Parse(component.text)];
			fUIManager.GetInstance().GetIconAsset("userface/" + playerFaceData.sFaceIcon + ".txt", this.rawimg);
		}
		this.tipobj.SetActive(true);
		this.mousepos(this.tipobj);
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0006E1A1 File Offset: 0x0006C3A1
	private void onExit(GameObject obj)
	{
		this.tipobj.SetActive(false);
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
	private void OnBtnClick(GameObject obj)
	{
		string name = obj.name;
		string a = name;
		if (!(a == "Button"))
		{
			if (!(a == "Button1"))
			{
				if (!(a == "Button2"))
				{
					if (a == "Button3")
					{
						bool flag = !(this.wInput.text == string.Empty);
						if (flag)
						{
							FriendManager.GetInstance().AddFriend(this.wInput.text);
						}
					}
				}
				else
				{
					this.SetBackground(this.btnimg1, "ab/friendatlas", "sanjiaoshouqi");
					this.SetBackground(this.btnimg2, "ab/friendatlas", "sanjiaoshouqi");
					this.SetBackground(this.btnimg3, "ab/friendatlas", "sanjiaozhankai");
					this.btn2.transform.localPosition = new Vector3(this.btn2.transform.localPosition.x, 380f, this.btn2.transform.localPosition.z);
					this.btn3.transform.localPosition = new Vector3(this.btn3.transform.localPosition.x, 346f, this.btn3.transform.localPosition.z);
					this.scw1.SetActive(false);
					this.scw2.SetActive(false);
					this.scw3.SetActive(true);
					this.InitUnMsg();
				}
			}
			else
			{
				this.SetBackground(this.btnimg1, "ab/friendatlas", "sanjiaoshouqi");
				this.SetBackground(this.btnimg2, "ab/friendatlas", "sanjiaozhankai");
				this.SetBackground(this.btnimg3, "ab/friendatlas", "sanjiaoshouqi");
				this.btn2.transform.localPosition = new Vector3(this.btn2.transform.localPosition.x, 380f, this.btn2.transform.localPosition.z);
				this.btn3.transform.localPosition = new Vector3(this.btn3.transform.localPosition.x, -388f, this.btn3.transform.localPosition.z);
				this.scw1.SetActive(false);
				this.scw2.SetActive(true);
				this.scw3.SetActive(false);
				this.InitLately();
			}
		}
		else
		{
			this.SetBackground(this.btnimg1, "ab/friendatlas", "sanjiaozhankai");
			this.SetBackground(this.btnimg2, "ab/friendatlas", "sanjiaoshouqi");
			this.SetBackground(this.btnimg3, "ab/friendatlas", "sanjiaoshouqi");
			this.btn2.transform.localPosition = new Vector3(this.btn2.transform.localPosition.x, -354f, this.btn2.transform.localPosition.z);
			this.btn3.transform.localPosition = new Vector3(this.btn3.transform.localPosition.x, -388f, this.btn3.transform.localPosition.z);
			this.scw1.SetActive(true);
			this.scw2.SetActive(false);
			this.scw3.SetActive(false);
		}
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0006E540 File Offset: 0x0006C740
	private void OnUnMsgClick(string sName, int nType)
	{
		GameObject gameObject = this.MsgContent.transform.FindChild(sName).gameObject;
		bool flag = gameObject != null;
		if (flag)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0006E579 File Offset: 0x0006C779
	public void SetBackground(Image img, string url, string iconname)
	{
		img.sprite = Window.spriteName(url, iconname);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0006E58C File Offset: 0x0006C78C
	private void mousepos(GameObject obj)
	{
		Transform transform = obj.transform;
		Vector3 vector = Input.mousePosition;
		Vector3 zero = Vector3.zero;
		bool flag = fUIManager.uiCamera != null;
		if (flag)
		{
			vector.y = Mathf.Clamp01(vector.y / (float)Screen.height);
			float num = fUIManager.uiCamera.orthographicSize / transform.parent.lossyScale.y;
			float num2 = (float)Screen.height * 0.5f / num;
			Vector2 vector2 = new Vector2(num2 * zero.x / (float)Screen.width, num2 * zero.y / (float)Screen.height);
			vector.y = Mathf.Max(vector.y, vector2.y);
			transform.position = fUIManager.uiCamera.ViewportToWorldPoint(vector);
			vector = transform.localPosition;
			vector.z = 0f;
			vector.x = -349.3f;
			transform.localPosition = vector;
		}
	}

	// Token: 0x040009F7 RID: 2551
	public static Friend_UICon inst;

	// Token: 0x040009F8 RID: 2552
	private GameObject btn1;

	// Token: 0x040009F9 RID: 2553
	private GameObject btn2;

	// Token: 0x040009FA RID: 2554
	private GameObject btn3;

	// Token: 0x040009FB RID: 2555
	private GameObject btn4;

	// Token: 0x040009FC RID: 2556
	private GameObject scw1;

	// Token: 0x040009FD RID: 2557
	private GameObject scw2;

	// Token: 0x040009FE RID: 2558
	private GameObject scw3;

	// Token: 0x040009FF RID: 2559
	private GameObject FriendContent;

	// Token: 0x04000A00 RID: 2560
	private GameObject LatelyContent;

	// Token: 0x04000A01 RID: 2561
	private GameObject MsgContent;

	// Token: 0x04000A02 RID: 2562
	private GameObject frienduser;

	// Token: 0x04000A03 RID: 2563
	private GameObject latelyuser;

	// Token: 0x04000A04 RID: 2564
	private GameObject msguser;

	// Token: 0x04000A05 RID: 2565
	private GameObject tipobj;

	// Token: 0x04000A06 RID: 2566
	private RawImage rawimg;

	// Token: 0x04000A07 RID: 2567
	private Text sname;

	// Token: 0x04000A08 RID: 2568
	private Text sLevel;

	// Token: 0x04000A09 RID: 2569
	private Image btnimg1;

	// Token: 0x04000A0A RID: 2570
	private Image btnimg2;

	// Token: 0x04000A0B RID: 2571
	private Image btnimg3;

	// Token: 0x04000A0C RID: 2572
	private InputField wInput;

	// Token: 0x04000A0D RID: 2573
	private bool IsInitFriend;

	// Token: 0x04000A0E RID: 2574
	private bool IsInitLately;

	// Token: 0x04000A0F RID: 2575
	private bool IsInitUnMsg;
}
