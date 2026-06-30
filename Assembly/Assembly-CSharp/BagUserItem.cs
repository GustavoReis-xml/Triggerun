using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002C RID: 44
public class BagUserItem : MonoBehaviour
{
	// Token: 0x06000192 RID: 402 RVA: 0x00008C98 File Offset: 0x00006E98
	public void showItemData(ArrayList aryOneItem, BagUserItem.ClickFunDelegate FunName, UnityEngine.Object ObjData)
	{
		this.ClickFun = FunName;
		this.ClickFunObj = ObjData;
		bool flag = aryOneItem != null && aryOneItem.Count > 0;
		if (flag)
		{
			this.showItemData(aryOneItem);
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00008CD4 File Offset: 0x00006ED4
	public void showItemData(ArrayList aryOneItem)
	{
		int key = (int)aryOneItem[0];
		this.itemData = fGameManager.mloadConfig.AllItemData[key];
		Transform transform = base.transform.Find("Text");
		int num = (int)aryOneItem[1];
		bool flag = num == 0;
		if (flag)
		{
			transform.gameObject.GetComponent<Text>().text = string.Empty;
		}
		else
		{
			transform.gameObject.GetComponent<Text>().text = this.itemData.m_sName + "*" + num.ToString();
		}
		RawImage component = base.transform.Find("RawImage").GetComponent<RawImage>();
		component.enabled = true;
		this.downokIco(this.itemData, component);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00008DA2 File Offset: 0x00006FA2
	private void downokIco(ItemData itemData, RawImage tex)
	{
		fUIManager.GetInstance().GetIconAsset(string.Format(ShopManager.GetIcoPath(itemData.m_nType), itemData.m_sIco), tex);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00008DC8 File Offset: 0x00006FC8
	private void ItemOnClick(GameObject Obj)
	{
		bool flag = this.ClickFun != null;
		if (flag)
		{
			this.ClickFun(this.ClickFunObj);
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00008DF7 File Offset: 0x00006FF7
	private void ItemOnHover(GameObject obj, bool isover)
	{
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00008DFA File Offset: 0x00006FFA
	public void showItemData(ArrayList aryOneItem, bool ShowType)
	{
		this.showItemData(aryOneItem);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00008E05 File Offset: 0x00007005
	public void ShowGuangXiao()
	{
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00008E08 File Offset: 0x00007008
	public void CloseGuangXiao()
	{
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00008E0C File Offset: 0x0000700C
	public void clearItem()
	{
		this.itemData = null;
		Transform transform = base.transform.Find("Text");
		transform.gameObject.GetComponent<Text>().text = string.Empty;
		RawImage component = base.transform.Find("RawImage").GetComponent<RawImage>();
		component.enabled = false;
	}

	// Token: 0x04000128 RID: 296
	public ItemData userItem;

	// Token: 0x04000129 RID: 297
	public bool islock;

	// Token: 0x0400012A RID: 298
	public GameObject gridobj;

	// Token: 0x0400012B RID: 299
	public int ntype;

	// Token: 0x0400012C RID: 300
	public GameObject texture;

	// Token: 0x0400012D RID: 301
	public ItemData itemData;

	// Token: 0x0400012E RID: 302
	public BagUserItem.ClickFunDelegate ClickFun;

	// Token: 0x0400012F RID: 303
	public UnityEngine.Object ClickFunObj;

	// Token: 0x0200051C RID: 1308
	// (Invoke) Token: 0x060032B1 RID: 12977
	public delegate void ClickFunDelegate(object Data);
}
