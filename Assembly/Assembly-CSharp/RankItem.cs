using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C6 RID: 710
public class RankItem : MonoBehaviour
{
	// Token: 0x06002077 RID: 8311 RVA: 0x001299D4 File Offset: 0x00127BD4
	private void Awake()
	{
		this.rankText = base.transform.Find("rank").GetComponent<Text>();
		this.rankImg = base.transform.Find("rankimg").gameObject;
		this.nLevelTex = base.transform.Find("level/num").GetComponent<Text>();
		this.nNickTex = base.transform.Find("unick").GetComponent<Text>();
		this.matchNumTex = base.transform.Find("matchnum").GetComponent<Text>();
		this.heroTimeTex = base.transform.Find("alltime").GetComponent<Text>();
		this.slText = base.transform.Find("shenglv").GetComponent<Text>();
		this.m_szRaw = base.transform.GetComponentsInChildren<RawImage>();
		this.heroList = base.transform.Find("herolistitem").gameObject;
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x00129ACC File Offset: 0x00127CCC
	public void Init()
	{
		this.rankImg.SetActive(false);
		this.rankText.text = string.Empty;
		this.nLevelTex.text = string.Empty;
		this.nNickTex.text = string.Empty;
		this.matchNumTex.text = string.Empty;
		this.heroTimeTex.text = string.Empty;
		this.slText.text = string.Empty;
		this.heroList.SetActive(false);
		this.DestroyTexture(this.m_szRaw);
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x00129B68 File Offset: 0x00127D68
	private void DestroyTexture(RawImage[] szRaw)
	{
		for (int i = 0; i < szRaw.Length; i++)
		{
			bool flag = szRaw[i].texture != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(szRaw[i].texture);
				szRaw[i].texture = null;
				szRaw[i].enabled = false;
				szRaw[i].GetComponent<GetIconComponent>()._Url = string.Empty;
			}
		}
	}

	// Token: 0x04001D89 RID: 7561
	private GameObject rankImg;

	// Token: 0x04001D8A RID: 7562
	private Text rankText;

	// Token: 0x04001D8B RID: 7563
	private Text nLevelTex;

	// Token: 0x04001D8C RID: 7564
	private Text nNickTex;

	// Token: 0x04001D8D RID: 7565
	private Text matchNumTex;

	// Token: 0x04001D8E RID: 7566
	private Text heroTimeTex;

	// Token: 0x04001D8F RID: 7567
	private Text slText;

	// Token: 0x04001D90 RID: 7568
	private RawImage[] m_szRaw;

	// Token: 0x04001D91 RID: 7569
	private GameObject heroList;
}
