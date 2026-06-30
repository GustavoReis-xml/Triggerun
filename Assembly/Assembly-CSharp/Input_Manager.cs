using System;
using TeamUtility.IO;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public static class Input_Manager
{
	// Token: 0x0600161F RID: 5663 RVA: 0x000CDD80 File Offset: 0x000CBF80
	public static float Get_Axis(string _Axis_Name)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		float result;
		if (flag)
		{
			result = 0f;
		}
		else
		{
			result = InputManager.GetAxis(_Axis_Name, PlayerID.One);
		}
		return result;
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000CDDC0 File Offset: 0x000CBFC0
	public static float Get_Axis_Raw(string _Axis_Name)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		float result;
		if (flag)
		{
			result = 0f;
		}
		else
		{
			result = InputManager.GetAxisRaw(_Axis_Name, PlayerID.One);
		}
		return result;
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000CDE00 File Offset: 0x000CC000
	public static bool Get_Button(string _Button_Name)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool chatInput = ChatManager.ChatInput;
			if (chatInput)
			{
				result = false;
			}
			else
			{
				bool flag2 = fGameManager.IsSingle && PlayerLearnManager.GetInstance().IsInLimit(_Button_Name) && PlayerLearnManager.GetInstance().GetPlayerLimitByBtnName(_Button_Name);
				result = (!flag2 && InputManager.GetButton(_Button_Name, PlayerID.One));
			}
		}
		return result;
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000CDE78 File Offset: 0x000CC078
	public static bool Get_Button_Up(string _Button_Name)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool chatInput = ChatManager.ChatInput;
			if (chatInput)
			{
				result = false;
			}
			else
			{
				bool flag2 = fGameManager.IsSingle && PlayerLearnManager.GetInstance().IsInLimit(_Button_Name) && PlayerLearnManager.GetInstance().GetPlayerLimitByBtnName(_Button_Name);
				result = (!flag2 && InputManager.GetButtonUp(_Button_Name, PlayerID.One));
			}
		}
		return result;
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000CDEF0 File Offset: 0x000CC0F0
	public static bool Get_Button_Down(string _Button_Name)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool chatInput = ChatManager.ChatInput;
			if (chatInput)
			{
				result = false;
			}
			else
			{
				bool flag2 = fGameManager.IsSingle && PlayerLearnManager.GetInstance().IsInLimit(_Button_Name) && PlayerLearnManager.GetInstance().GetPlayerLimitByBtnName(_Button_Name);
				result = (!flag2 && InputManager.GetButtonDown(_Button_Name, PlayerID.One));
			}
		}
		return result;
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000CDF68 File Offset: 0x000CC168
	public static bool Get_Key(KeyCode _KeyCode)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		return !flag && InputManager.GetKey(_KeyCode);
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
	public static bool Get_Key_Up(KeyCode _KeyCode)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		return !flag && InputManager.GetKeyUp(_KeyCode);
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000CDFE0 File Offset: 0x000CC1E0
	public static bool Get_Key_Down(KeyCode _KeyCode)
	{
		bool flag = SettingCon.Instance != null && SettingCon.Instance.visible;
		return !flag && InputManager.GetKeyDown(_KeyCode);
	}
}
