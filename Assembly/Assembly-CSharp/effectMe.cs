using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class effectMe : effectBase
{
	// Token: 0x060009FC RID: 2556 RVA: 0x0005B490 File Offset: 0x00059690
	public override void AddOneEffect(string sFpsUrl, string sTpsUrl, DateTime m_dDate, int nEffectType)
	{
		bool flag = this.gameplayer.FpsOrTps == 1;
		if (flag)
		{
			bool flag2 = !(sFpsUrl == "0");
			if (flag2)
			{
				bool flag3 = !this.FirstStateEffectInfo.ContainsKey(sFpsUrl);
				if (flag3)
				{
					EffectData effectData = new EffectData();
					effectData.Init(sFpsUrl, sTpsUrl, m_dDate, nEffectType);
					this.FirstStateEffectInfo.Add(sFpsUrl, effectData);
				}
				else
				{
					EffectData effectData = this.FirstStateEffectInfo[sFpsUrl];
					effectData.Init(sFpsUrl, sTpsUrl, m_dDate, nEffectType);
				}
				this.gameplayer.playerZhunxingMe.OnPlayerFpsEffect(sFpsUrl, true);
			}
		}
		else
		{
			base.AddOneEffect(sFpsUrl, sTpsUrl, m_dDate, nEffectType);
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0005B540 File Offset: 0x00059740
	public override void DelOneEffect(string sFpsUrl, string sTpsUrl)
	{
		bool flag = this.gameplayer.FpsOrTps == 1;
		if (flag)
		{
			bool flag2 = !(sFpsUrl == "0");
			if (flag2)
			{
				this.DelFpsEffect(sFpsUrl);
			}
		}
		else
		{
			base.DelOneEffect(sFpsUrl, sTpsUrl);
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0005B58C File Offset: 0x0005978C
	private void DelFpsEffect(string sFpsUrl)
	{
		bool flag = this.FirstStateEffectInfo.ContainsKey(sFpsUrl);
		if (flag)
		{
			EffectData effectData = this.FirstStateEffectInfo[sFpsUrl];
			bool flag2 = effectData != null;
			if (flag2)
			{
				this.gameplayer.playerZhunxingMe.OnPlayerFpsEffect(effectData.m_sFpsUrl, false);
			}
			this.FirstStateEffectInfo.Remove(sFpsUrl);
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0005B5EC File Offset: 0x000597EC
	protected override void Update()
	{
		bool flag = Time.time < this.ftime;
		if (!flag)
		{
			this.ftime = Time.time + 0.1f;
			base.Update();
			bool flag2 = this.gameplayer.haveDestroy || !this.gameplayer.playerHaveCreate || this.FirstStateEffectInfo.Count <= 0;
			if (!flag2)
			{
				bool flag3 = this.mDelList.Count > 0;
				if (flag3)
				{
					this.mDelList.Clear();
				}
				foreach (string text in this.FirstStateEffectInfo.Keys)
				{
					EffectData effectData = this.FirstStateEffectInfo[text];
					bool flag4 = effectData.m_nType == 1;
					if (flag4)
					{
						float num = (float)(Fun.GetServerDate() - effectData.m_dDate).TotalMilliseconds;
						bool flag5 = num > 300f && !this.mDelList.Contains(text);
						if (flag5)
						{
							this.mDelList.Add(text);
						}
					}
				}
				bool flag6 = this.mDelList.Count > 0;
				if (flag6)
				{
					int count = this.mDelList.Count;
					for (int i = 0; i < count; i++)
					{
						string key = this.mDelList[i].ToString();
						EffectData effectData = this.FirstStateEffectInfo[key];
						this.DelOneEffect(effectData.m_sFpsUrl, effectData.m_sTpsUrl);
					}
				}
			}
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0005B7B0 File Offset: 0x000599B0
	public override void OnDeadClearEffect()
	{
		base.OnDeadClearEffect();
		ArrayList arrayList = new ArrayList();
		arrayList = new ArrayList(this.FirstStateEffectInfo.Keys);
		while (arrayList.Count > 0)
		{
			string sFpsUrl = arrayList[0].ToString();
			arrayList.RemoveAt(0);
			this.DelFpsEffect(sFpsUrl);
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0005B80C File Offset: 0x00059A0C
	public void ChangeBuffEffect()
	{
		List<string> list = new List<string>();
		bool flag = this.gameplayer.FpsOrTps == 1 && this.ThirdStateEffectInfo.Count > 0;
		if (flag)
		{
			foreach (string item in this.ThirdStateEffectInfo.Keys)
			{
				bool flag2 = !list.Contains(item);
				if (flag2)
				{
					list.Add(item);
				}
			}
			foreach (string key in list)
			{
				EffectData effectData = this.ThirdStateEffectInfo[key];
				base.DelOneEffect(effectData.m_sFpsUrl, effectData.m_sTpsUrl);
				this.AddOneEffect(effectData.m_sFpsUrl, effectData.m_sTpsUrl, effectData.m_dDate, effectData.m_nType);
			}
			list.Clear();
		}
		else
		{
			bool flag3 = this.FirstStateEffectInfo.Count <= 0;
			if (!flag3)
			{
				foreach (string item2 in this.FirstStateEffectInfo.Keys)
				{
					bool flag4 = !list.Contains(item2);
					if (flag4)
					{
						list.Add(item2);
					}
				}
				foreach (string text in list)
				{
					EffectData effectData = this.FirstStateEffectInfo[text];
					this.DelFpsEffect(text);
					this.AddOneEffect(effectData.m_sFpsUrl, effectData.m_sTpsUrl, effectData.m_dDate, effectData.m_nType);
				}
				list.Clear();
			}
		}
	}

	// Token: 0x0400081C RID: 2076
	public Dictionary<string, EffectData> FirstStateEffectInfo = new Dictionary<string, EffectData>();

	// Token: 0x0400081D RID: 2077
	private float ftime;

	// Token: 0x0400081E RID: 2078
	private List<string> mDelList = new List<string>();
}
