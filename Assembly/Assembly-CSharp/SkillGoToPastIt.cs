using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class SkillGoToPastIt : SkillBaseIt
{
	// Token: 0x060026E0 RID: 9952 RVA: 0x00160EDC File Offset: 0x0015F0DC
	private void Start()
	{
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x00160EDF File Offset: 0x0015F0DF
	public override void ModelLoaded()
	{
		base.ModelLoaded();
		this.shanhuiqiu = base.gameplayer.userModelTps.transform.Find("shanxianguang").gameObject;
	}

	// Token: 0x060026E2 RID: 9954 RVA: 0x00160F10 File Offset: 0x0015F110
	public override void OneSkillAction(object __target, ArrayList parm2)
	{
		base.OneSkillAction(__target, parm2);
		this.qiu = UnityEngine.Object.Instantiate<GameObject>(this.shanhuiqiu, base.gameplayer.UserControl.transform.position, Quaternion.identity);
		this.qiu.transform.parent = fGameManager.allBaozhaFather.transform;
		this.qiu.SetActive(true);
		base.gameplayer.userModelTps.SetActive(false);
		base.gameplayer.charMove.notMove = true;
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x00160F9D File Offset: 0x0015F19D
	public override void OneSkillActionUpdate(ArrayList list)
	{
		base.gameplayer.tChar.moveByControlIt = true;
		base.StartCoroutine(this.SkillFinish());
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x00160FBF File Offset: 0x0015F1BF
	private IEnumerator SkillFinish()
	{
		yield return new WaitForSeconds(0.1f);
		this.qiu2 = UnityEngine.Object.Instantiate<GameObject>(this.shanhuiqiu, base.gameplayer.UserControl.transform.position, Quaternion.identity);
		this.qiu2.transform.parent = fGameManager.allBaozhaFather.transform;
		this.qiu2.SetActive(true);
		this.OneSkillFinish();
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(this.qiu);
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(this.qiu2);
		yield break;
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x00160FD0 File Offset: 0x0015F1D0
	public override void OneSkillFinish()
	{
		base.OneSkillFinish();
		base.gameplayer.userModelTps.SetActive(true);
		base.gameplayer.charMove.notMove = false;
		base.gameplayer.tChar.moveByControlIt = false;
		UnityEngine.Object.Destroy(this.qiu);
	}

	// Token: 0x04002251 RID: 8785
	private GameObject shanhuiqiu;

	// Token: 0x04002252 RID: 8786
	private GameObject qiu;

	// Token: 0x04002253 RID: 8787
	private GameObject qiu2;
}
