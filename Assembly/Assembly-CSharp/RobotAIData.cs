using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class RobotAIData
{
	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x06002182 RID: 8578 RVA: 0x00137B0C File Offset: 0x00135D0C
	public int key
	{
		get
		{
			return this.indexId;
		}
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x00137B14 File Offset: 0x00135D14
	public void Init(XmlElement xml)
	{
		this.movePos = new List<AIMovePos>();
		this.indexId = int.Parse(xml.GetAttribute("Index"));
		this.SightRange = (float)int.Parse(xml.GetAttribute("Eye_Range"));
		this.ShootRange = (float)int.Parse(xml.GetAttribute("Shoot_Range"));
		this.safeRange = (float)int.Parse(xml.GetAttribute("safeRange")) / 1000f;
		this.ChaseEnemyRange = (float)int.Parse(xml.GetAttribute("Follow_Range"));
		this.moveAttack = int.Parse(xml.GetAttribute("MoveAttack"));
		this.moveType = int.Parse(xml.GetAttribute("Move_Type"));
		this.EyeAngle = (float)int.Parse(xml.GetAttribute("Eye_Angle"));
		this.EarRange = (float)int.Parse(xml.GetAttribute("Ear_Range"));
		this.ReactionTime = (float)int.Parse(xml.GetAttribute("Reaction_time")) / 1000f;
		this.turnRotationSpeed = (float)int.Parse(xml.GetAttribute("Turn_ration")) / 1000f;
		this.DodgeRate = (float)int.Parse(xml.GetAttribute("Dodge_Rate")) / 1000f;
		string attribute = xml.GetAttribute("Move_Paths");
		bool flag = !(attribute != string.Empty) || !(attribute != "0");
		if (!flag)
		{
			string[] array = attribute.Split(new char[]
			{
				';'
			});
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag2 = !(array[i] == string.Empty);
				if (flag2)
				{
					string[] array2 = array[i].Split(new char[]
					{
						','
					});
					Vector3 pos = new Vector3((float)int.Parse(array2[1]) * 0.01f, (float)int.Parse(array2[2]) * 0.01f, (float)int.Parse(array2[3]) * 0.01f);
					AIMovePos item = new AIMovePos(pos, (float)int.Parse(array2[4]), (float)int.Parse(array2[5]), int.Parse(array2[6]));
					this.movePos.Add(item);
				}
			}
		}
	}

	// Token: 0x04001E7C RID: 7804
	public int indexId;

	// Token: 0x04001E7D RID: 7805
	public ArrayList szAward = new ArrayList();

	// Token: 0x04001E7E RID: 7806
	public List<AIMovePos> movePos;

	// Token: 0x04001E7F RID: 7807
	public float SightRange;

	// Token: 0x04001E80 RID: 7808
	public float ShootRange;

	// Token: 0x04001E81 RID: 7809
	public float ChaseEnemyRange;

	// Token: 0x04001E82 RID: 7810
	public int moveAttack;

	// Token: 0x04001E83 RID: 7811
	public int moveType;

	// Token: 0x04001E84 RID: 7812
	public float EyeAngle;

	// Token: 0x04001E85 RID: 7813
	public float EarRange;

	// Token: 0x04001E86 RID: 7814
	public float ReactionTime;

	// Token: 0x04001E87 RID: 7815
	public float turnRotationSpeed;

	// Token: 0x04001E88 RID: 7816
	public float DodgeRate;

	// Token: 0x04001E89 RID: 7817
	public float safeRange;
}
