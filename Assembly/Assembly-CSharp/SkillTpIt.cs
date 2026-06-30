using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class SkillTpIt : SkillBaseIt
{
	// Token: 0x06002888 RID: 10376 RVA: 0x0016B244 File Offset: 0x00169444
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		string url = string.Format("EffectRes/{0}.ef", "skill_yanluo_aybmz");
		WWW resWWW = GameRoot.GetResWWW(url);
		bool flag = resWWW == null;
		if (flag)
		{
			GameRoot.LoadAsset(url, new fResourceManager.DownFinishDelegate(this.loadReayPrefab), 0);
		}
		else
		{
			this.loadReayPrefab(resWWW, 0);
		}
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x0016B2A8 File Offset: 0x001694A8
	private void loadReayPrefab(WWW www, object sStyle)
	{
		bool flag = www != null && www.error == null;
		if (flag)
		{
			this.perfab = www.assetBundle.LoadAsset(www.assetBundle.GetAllAssetNames()[0]);
		}
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x0016B2EC File Offset: 0x001694EC
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		ArrayList arrayList = (ArrayList)__target;
		ArrayList arrayList2 = (ArrayList)arrayList[1];
		Vector3 vector = new Vector3((float)((int)arrayList2[0]) * 0.01f, (float)((int)arrayList2[1]) * 0.01f, (float)((int)arrayList2[2]) * 0.01f);
		bool flag = this.perfab != null;
		if (flag)
		{
			this.eff = CEffectPools.Instance.CreateGameObject(this.perfab, vector, Quaternion.identity);
			this.eff.transform.parent = fGameManager.allBaozhaFather.transform;
			EffectAutoDestroy effectAutoDestroy = this.eff.AddComponent<EffectAutoDestroy>();
			effectAutoDestroy.PutInPools = false;
		}
		base.gameplayer.charMove.notMove = true;
		this.needCheck = true;
		base.StartCoroutine(this.checkMove(vector));
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x0016B3DB File Offset: 0x001695DB
	private IEnumerator checkMove(Vector3 target)
	{
		float addtime = 0f;
		while (this.needCheck)
		{
			addtime += Time.deltaTime;
			yield return new WaitForSeconds(0.1f);
			bool flag = base.gameplayer.GetAnimatorBool("SkillChuansong_do") || addtime > 5f;
			if (flag)
			{
				this.needCheck = false;
			}
		}
		yield return new WaitForSeconds(2f);
		base.gameplayer.setPlayerNewPos(target);
		base.gameplayer.charMove.notMove = false;
		yield break;
	}

	// Token: 0x04002317 RID: 8983
	private UnityEngine.Object perfab;

	// Token: 0x04002318 RID: 8984
	private GameObject eff;

	// Token: 0x04002319 RID: 8985
	private bool needCheck;
}
