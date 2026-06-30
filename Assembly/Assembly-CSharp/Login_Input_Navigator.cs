using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000223 RID: 547
public class Login_Input_Navigator : MonoBehaviour
{
	// Token: 0x060018A9 RID: 6313 RVA: 0x000E6D3D File Offset: 0x000E4F3D
	private void Start()
	{
		this.system = EventSystem.current;
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x000E6D4C File Offset: 0x000E4F4C
	private void Update()
	{
		bool flag = !Input.GetKeyDown(KeyCode.Tab);
		if (!flag)
		{
			bool flag2 = !(this.system.currentSelectedGameObject == null);
			if (flag2)
			{
				Selectable selectable = this.system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
				bool flag3 = !(selectable == null);
				if (flag3)
				{
					this.system.SetSelectedGameObject(selectable.gameObject, new BaseEventData(this.system));
				}
			}
		}
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x000E6DCA File Offset: 0x000E4FCA
	public void OnSelect(BaseEventData eventData)
	{
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x000E6DCD File Offset: 0x000E4FCD
	public void OnDeselect(BaseEventData eventData)
	{
	}

	// Token: 0x0400135E RID: 4958
	private EventSystem system;
}
