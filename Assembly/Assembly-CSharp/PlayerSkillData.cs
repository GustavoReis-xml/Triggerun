using System;
using System.Xml;

// Token: 0x0200029B RID: 667
[Serializable]
public class PlayerSkillData
{
	// Token: 0x17000384 RID: 900
	// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0011A8DA File Offset: 0x00118ADA
	public int key
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x0011A8E4 File Offset: 0x00118AE4
	public void Init(XmlElement xml)
	{
		this.id = int.Parse(xml.GetAttribute("id"));
		this.name = xml.GetAttribute("name");
		this.skillclass = xml.GetAttribute("skillclass");
		this.type = int.Parse(xml.GetAttribute("type"));
		this.icon = xml.GetAttribute("icon");
		this.desc = xml.GetAttribute("desc");
		this.targettype = int.Parse(xml.GetAttribute("targettype"));
		this.taregetlock = int.Parse(xml.GetAttribute("taregetlock"));
		this.taregetradius = float.Parse(xml.GetAttribute("taregetradius"));
		this.dokey = xml.GetAttribute("dokey");
		this.xingtai = int.Parse(xml.GetAttribute("xingtai"));
		bool flag = xml.GetAttribute("triggerweapon") != "0";
		if (flag)
		{
			string[] array = xml.GetAttribute("triggerweapon").Split(new char[]
			{
				'|'
			});
			this.triggerWeapon = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.triggerWeapon[i] = int.Parse(array[i]);
			}
		}
		else
		{
			this.triggerWeapon = new int[0];
		}
		this.soundFriend = xml.GetAttribute("soundfriend");
		this.soundEnemy = xml.GetAttribute("soundenemy");
		this.soundRand = (float)int.Parse(xml.GetAttribute("soundrand")) * 0.0001f;
		this.animation = xml.GetAttribute("animation");
		this.maxSkillNumber = int.Parse(xml.GetAttribute("maxskillnumber"));
		this.maxItemNumber = int.Parse(xml.GetAttribute("maxitemnumber"));
		this.readySwitch = int.Parse(xml.GetAttribute("readyswitch"));
		this.readyItem = xml.GetAttribute("readyitem");
		this.readyItemType = int.Parse(xml.GetAttribute("readyitemtype"));
		this.readyItemRotation = int.Parse(xml.GetAttribute("readyitemrotation"));
		this.readyAnimation = xml.GetAttribute("readyanimation");
		this.soundBegin = xml.GetAttribute("soundbegin");
		this.soundEnd = xml.GetAttribute("soundend");
	}

	// Token: 0x04001BED RID: 7149
	public int id;

	// Token: 0x04001BEE RID: 7150
	public string name;

	// Token: 0x04001BEF RID: 7151
	public string skillclass;

	// Token: 0x04001BF0 RID: 7152
	public int buff_idx;

	// Token: 0x04001BF1 RID: 7153
	public int buff_idx_me;

	// Token: 0x04001BF2 RID: 7154
	public int mapeffect_idx;

	// Token: 0x04001BF3 RID: 7155
	public int mapeffect_at;

	// Token: 0x04001BF4 RID: 7156
	public int type;

	// Token: 0x04001BF5 RID: 7157
	public int maxSkillNumber;

	// Token: 0x04001BF6 RID: 7158
	public int maxItemNumber;

	// Token: 0x04001BF7 RID: 7159
	public string soundFriend;

	// Token: 0x04001BF8 RID: 7160
	public string soundEnemy;

	// Token: 0x04001BF9 RID: 7161
	public float soundRand = 1f;

	// Token: 0x04001BFA RID: 7162
	public string icon;

	// Token: 0x04001BFB RID: 7163
	public string desc;

	// Token: 0x04001BFC RID: 7164
	public int targettype;

	// Token: 0x04001BFD RID: 7165
	public int taregetlock;

	// Token: 0x04001BFE RID: 7166
	public float taregetradius;

	// Token: 0x04001BFF RID: 7167
	public int readySwitch;

	// Token: 0x04001C00 RID: 7168
	public string readyItem;

	// Token: 0x04001C01 RID: 7169
	public int readyItemType;

	// Token: 0x04001C02 RID: 7170
	public int readyItemRotation;

	// Token: 0x04001C03 RID: 7171
	public string dokey;

	// Token: 0x04001C04 RID: 7172
	public int triggerDo;

	// Token: 0x04001C05 RID: 7173
	public int[] triggerWeapon;

	// Token: 0x04001C06 RID: 7174
	public int xingtai;

	// Token: 0x04001C07 RID: 7175
	public string animation;

	// Token: 0x04001C08 RID: 7176
	public string readyAnimation;

	// Token: 0x04001C09 RID: 7177
	public string soundBegin;

	// Token: 0x04001C0A RID: 7178
	public string soundEnd;
}
