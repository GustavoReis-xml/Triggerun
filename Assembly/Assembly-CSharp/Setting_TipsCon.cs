using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000301 RID: 769
public class Setting_TipsCon : Window
{
	// Token: 0x170004CC RID: 1228
	// (get) Token: 0x06002493 RID: 9363 RVA: 0x0014A8C4 File Offset: 0x00148AC4
	// (set) Token: 0x06002494 RID: 9364 RVA: 0x0014A8DC File Offset: 0x00148ADC
	public GameObject Up
	{
		get
		{
			return this._Up;
		}
		set
		{
			this._Up = value;
		}
	}

	// Token: 0x170004CD RID: 1229
	// (get) Token: 0x06002495 RID: 9365 RVA: 0x0014A8E8 File Offset: 0x00148AE8
	// (set) Token: 0x06002496 RID: 9366 RVA: 0x0014A900 File Offset: 0x00148B00
	public GameObject Down
	{
		get
		{
			return this._Down;
		}
		set
		{
			this._Down = value;
		}
	}

	// Token: 0x170004CE RID: 1230
	// (get) Token: 0x06002497 RID: 9367 RVA: 0x0014A90C File Offset: 0x00148B0C
	// (set) Token: 0x06002498 RID: 9368 RVA: 0x0014A924 File Offset: 0x00148B24
	public Button Ignore_Button
	{
		get
		{
			return this._Ignore_Button;
		}
		set
		{
			this._Ignore_Button = value;
		}
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x0014A92E File Offset: 0x00148B2E
	protected override void Awake()
	{
		base.Awake();
		Setting_TipsCon.Instance = this;
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x0014A940 File Offset: 0x00148B40
	public void Initialize(GameObject _UI_Object, object _Parameters)
	{
		this.Up = _UI_Object.transform.Find("Up").gameObject;
		this.Down = _UI_Object.transform.Find("Down").gameObject;
		string text = _Parameters.ToString();
		bool flag = text == "Up";
		if (flag)
		{
			string @string = PlayerPrefs.GetString("Ignore_Setting_Up_Tips");
			bool flag2 = @string != string.Empty;
			if (flag2)
			{
				DateTime t = Convert.ToDateTime(@string);
				bool flag3 = DateTime.Now < t;
				if (flag3)
				{
					Setting_TipsCon.Instance.Close();
					return;
				}
			}
		}
		Text component = _UI_Object.transform.Find(text + "/Image/Background/lbl_Key").GetComponent<Text>();
		Setting_Entity_Control setting_Entity_Control = (fGameManager.GetInstance().getPlayer() != null && fGameManager.GetInstance().getPlayer().playerCommon != null) ? GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(fGameManager.GetInstance().getPlayer().playerCommon.m_nModelIndex) : GameRoot.inst.Setting.Hero_Control_Class.Get_Hero_Control_Data(0);
		string text2 = text;
		string a = text2;
		if (!(a == "Up"))
		{
			if (a == "Down")
			{
				this.Up.SetActive(false);
				this.Down.SetActive(true);
				component.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Reduce_Quality);
			}
		}
		else
		{
			this.Up.SetActive(true);
			this.Down.SetActive(false);
			component.text = GameRoot.inst.Setting.Get_Key_Describe(setting_Entity_Control.Key_Improve_Quality);
			bool flag4 = this.Ignore_Button == null;
			if (flag4)
			{
				this.Ignore_Button = this.Up.transform.Find("btn_Ignore").gameObject.GetComponent<Button>();
				this.Ignore_Button.gameObject.SetActive(true);
				EventTriggerListener.Get(this.Ignore_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(GameRoot.inst.Setting.Ignore);
			}
		}
	}

	// Token: 0x04002097 RID: 8343
	public static Setting_TipsCon Instance;

	// Token: 0x04002098 RID: 8344
	private GameObject _Up;

	// Token: 0x04002099 RID: 8345
	private GameObject _Down;

	// Token: 0x0400209A RID: 8346
	private Button _Ignore_Button;
}
