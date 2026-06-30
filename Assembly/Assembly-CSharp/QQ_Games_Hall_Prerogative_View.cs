using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BD RID: 701
public class QQ_Games_Hall_Prerogative_View : MonoBehaviour
{
	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06002055 RID: 8277 RVA: 0x00128A3C File Offset: 0x00126C3C
	// (set) Token: 0x06002056 RID: 8278 RVA: 0x00128A9A File Offset: 0x00126C9A
	public QQ_Games_Hall_Prerogative_UI UI_Component
	{
		get
		{
			bool flag = this._UI_Component == null;
			if (flag)
			{
				this._UI_Component = base.gameObject.GetComponent<QQ_Games_Hall_Prerogative_UI>();
				bool flag2 = this._UI_Component == null;
				if (flag2)
				{
					this._UI_Component = base.gameObject.AddComponent<QQ_Games_Hall_Prerogative_UI>();
				}
			}
			return this._UI_Component;
		}
		set
		{
			this._UI_Component = value;
		}
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x00128AA4 File Offset: 0x00126CA4
	public void Initialize()
	{
		EventTriggerListener.Get(this.UI_Component.Top_Left_Get_Novice_Reward_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Get_Novice_Reward);
		EventTriggerListener.Get(this.UI_Component.Lower_Left_Get_Day_Reward_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Get_Day_Reward);
		EventTriggerListener.Get(this.UI_Component.Top_Right_Get_Growth_Reward_Button.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.Get_Growth_Reward);
		this.Display_Data();
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x00128B2B File Offset: 0x00126D2B
	private void Get_Novice_Reward(GameObject _GameObject)
	{
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			6,
			0
		});
	}

	// Token: 0x06002059 RID: 8281 RVA: 0x00128B58 File Offset: 0x00126D58
	private void Get_Growth_Reward(GameObject _GameObject)
	{
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			8,
			0
		});
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x00128B85 File Offset: 0x00126D85
	private void Get_Day_Reward(GameObject _GameObject)
	{
		fGameManager.SendMsgToHallServer(3020001, new ArrayList
		{
			7,
			0
		});
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x00128BB4 File Offset: 0x00126DB4
	public void Display_Button()
	{
		bool interactable = false;
		string text = string.Empty;
		switch (Prerogative_Data.Instance.QQ_Games_Hall_Reward_State_New)
		{
		case 0:
			interactable = false;
			text = "不可领取";
			break;
		case 1:
			interactable = true;
			text = "领取";
			break;
		case 2:
			interactable = false;
			text = "已领取";
			break;
		}
		this.UI_Component.Top_Left_Get_Novice_Reward_Button.interactable = interactable;
		this.UI_Component.Top_Left_Get_Novice_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = text;
		switch (Prerogative_Data.Instance.QQ_Games_Hall_Reward_State_Day)
		{
		case 0:
			interactable = false;
			text = "不可领取";
			break;
		case 1:
			interactable = true;
			text = "领取";
			break;
		case 2:
			interactable = false;
			text = "已领取";
			break;
		}
		this.UI_Component.Lower_Left_Get_Day_Reward_Button.interactable = interactable;
		this.UI_Component.Lower_Left_Get_Day_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = text;
		this.UI_Component.Top_Right_Get_Growth_Reward_Button.interactable = false;
		this.UI_Component.Top_Right_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "不可领取";
		ArrayList qq_Games_Hall_Reward_State_Level_Up = Prerogative_Data.Instance.QQ_Games_Hall_Reward_State_Level_Up;
		for (int i = 0; i < qq_Games_Hall_Reward_State_Level_Up.Count; i++)
		{
			bool flag = int.Parse(qq_Games_Hall_Reward_State_Level_Up[i].ToString()) == 0;
			if (flag)
			{
				this.UI_Component.Top_Right_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "已领取";
			}
			bool flag2 = int.Parse(qq_Games_Hall_Reward_State_Level_Up[i].ToString()) == 1;
			if (flag2)
			{
				this.UI_Component.Top_Right_Get_Growth_Reward_Button.interactable = true;
				this.UI_Component.Top_Right_Get_Growth_Reward_Button.transform.FindChild("Text").GetComponent<Text>().text = "领取";
				break;
			}
		}
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x00128DC9 File Offset: 0x00126FC9
	public void Display_Data()
	{
		this.Display_Button();
		this.Display_Novice_Reward();
		this.Display_Day_Reward();
		this.Display_Growth_Level_Reward();
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x00128DE8 File Offset: 0x00126FE8
	private void Display_Novice_Reward()
	{
		this.Fill_Item_Data(fGameManager.mloadConfig.GetItemData(6), this.UI_Component.Top_Left_Grid, this.UI_Component.Top_Left_Item);
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x00128E13 File Offset: 0x00127013
	private void Display_Day_Reward()
	{
		this.Fill_Item_Data(fGameManager.mloadConfig._Prerogative_Xml_Data[7].Item_Data, this.UI_Component.Lower_Left_Grid, this.UI_Component.Lower_Left_Item);
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x00128E48 File Offset: 0x00127048
	private void Display_Growth_Level_Reward()
	{
		Vector2 vector = Vector2.zero;
		this.Fill_Item_Data(fGameManager.mloadConfig._Prerogative_Xml_Data[8].Item_Data, this.UI_Component.Top_Right_Grid, this.UI_Component.Top_Right_Item, this.UI_Component.Top_Right_Image_Item);
		vector = this.UI_Component.Top_Right_Content_RectTransform.sizeDelta;
		this.UI_Component.Top_Right_Content_RectTransform.sizeDelta = new Vector2(vector.x, (float)(fGameManager.mloadConfig._Prerogative_Xml_Data[8].Item_Data.Keys.Count * 73 + 20));
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x00128EEC File Offset: 0x001270EC
	private void Fill_Item_Data(Dictionary<int, object> _Data, GameObject _Parent_Object, GameObject _Item_Object)
	{
		List<int> list = new List<int>(_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			int key = list[i];
			bool flag = _Parent_Object.transform.FindChild((i + 1).ToString()) != null;
			GameObject gameObject;
			if (flag)
			{
				gameObject = _Parent_Object.transform.FindChild((i + 1).ToString()).gameObject;
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(_Item_Object);
				gameObject.name = (i + 1).ToString();
				gameObject.transform.SetParent(_Parent_Object.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
			}
			gameObject.SetActive(true);
			RawImage component = gameObject.transform.Find("img_Icon").gameObject.GetComponent<RawImage>();
			Text component2 = gameObject.transform.Find("lbl_Name").gameObject.GetComponent<Text>();
			ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
			fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component);
			Text text = component2;
			string sName = itemData.m_sName;
			string str = "* ";
			object obj = _Data[key];
			text.text = sName + str + ((obj != null) ? obj.ToString() : null);
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x00129094 File Offset: 0x00127294
	private void Fill_Item_Data(Dictionary<int, object> _Data, GameObject _Parent_Object, GameObject _Item_Object, GameObject _Image_Item_Object)
	{
		List<int> list = new List<int>(_Data.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			int num = list[i];
			Dictionary<int, int> dictionary = (Dictionary<int, int>)_Data[num];
			bool flag = _Parent_Object.transform.FindChild((i + 1).ToString()) != null;
			GameObject gameObject;
			if (flag)
			{
				gameObject = _Parent_Object.transform.FindChild((i + 1).ToString()).gameObject;
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(_Item_Object);
				gameObject.name = (i + 1).ToString();
				gameObject.transform.SetParent(_Parent_Object.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
			}
			gameObject.SetActive(true);
			Text component = gameObject.transform.Find("lbl_Describe").GetComponent<Text>();
			component.text = num.ToString() + "0级";
			List<int> list2 = new List<int>(dictionary.Keys);
			for (int j = 0; j < list2.Count; j++)
			{
				int key = list2[j];
				bool flag2 = gameObject.transform.Find("Grid").transform.FindChild((j + 1).ToString()) != null;
				GameObject gameObject2;
				if (flag2)
				{
					gameObject2 = gameObject.transform.Find("Grid").transform.FindChild((j + 1).ToString()).gameObject;
				}
				else
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(_Image_Item_Object);
					gameObject2.name = (j + 1).ToString();
					gameObject2.transform.SetParent(gameObject.transform.Find("Grid"));
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.localEulerAngles = Vector3.zero;
					gameObject2.transform.localScale = Vector3.one;
				}
				gameObject2.SetActive(true);
				RawImage component2 = gameObject2.transform.Find("img_Icon").gameObject.GetComponent<RawImage>();
				component = gameObject2.transform.Find("lbl_Amount").gameObject.GetComponent<Text>();
				component.text = "* " + dictionary[key].ToString();
				ItemData itemData = fGameManager.mloadConfig.AllItemData[key];
				fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), component2);
			}
			bool flag3 = !(gameObject.transform.Find("lbl_State") != null);
			if (!flag3)
			{
				component = gameObject.transform.Find("lbl_State").gameObject.GetComponent<Text>();
				bool flag4 = num - 1 >= Prerogative_Data.Instance.QQ_Games_Hall_Reward_State_Level_Up.Count;
				if (flag4)
				{
					component.color = Color.grey;
					component.text = string.Empty;
				}
				else
				{
					switch (int.Parse(Prerogative_Data.Instance.QQ_Games_Hall_Reward_State_Level_Up[num - 1].ToString()))
					{
					case 0:
						component.color = Color.white;
						component.text = "已领取";
						break;
					case 1:
						component.color = Color.green;
						component.text = "可领取";
						break;
					case 2:
						component.color = Color.grey;
						component.text = string.Empty;
						break;
					}
				}
			}
		}
	}

	// Token: 0x04001D82 RID: 7554
	private QQ_Games_Hall_Prerogative_UI _UI_Component;
}
