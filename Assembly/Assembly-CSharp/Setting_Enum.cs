using System;
using System.ComponentModel;
using System.Reflection;

// Token: 0x020002F9 RID: 761
public class Setting_Enum
{
	// Token: 0x06002441 RID: 9281 RVA: 0x00148A04 File Offset: 0x00146C04
	public static string Get_Description(Enum _Value, bool _State = false)
	{
		Type type = _Value.GetType();
		DescriptionAttribute descriptionAttribute = null;
		if (_State)
		{
			descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute));
		}
		else
		{
			string name = Enum.GetName(type, _Value);
			bool flag = name != null;
			if (flag)
			{
				FieldInfo field = type.GetField(name);
				bool flag2 = field != null;
				if (flag2)
				{
					descriptionAttribute = (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false) as DescriptionAttribute);
				}
			}
		}
		bool flag3 = descriptionAttribute != null && !string.IsNullOrEmpty(descriptionAttribute.Description);
		string result;
		if (flag3)
		{
			result = descriptionAttribute.Description;
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	// Token: 0x02000605 RID: 1541
	[Description("设置类型")]
	public enum Setting_Type
	{
		// Token: 0x04002F10 RID: 12048
		[Description("视频设置")]
		Video = 1,
		// Token: 0x04002F11 RID: 12049
		[Description("声音设置")]
		Voice,
		// Token: 0x04002F12 RID: 12050
		[Description("游戏设置")]
		Game,
		// Token: 0x04002F13 RID: 12051
		[Description("控制设置")]
		Control,
		// Token: 0x04002F14 RID: 12052
		[Description("社交设置")]
		Social
	}
}
