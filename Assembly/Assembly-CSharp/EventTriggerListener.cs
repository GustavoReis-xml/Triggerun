using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200011C RID: 284
public class EventTriggerListener : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x0005CA84 File Offset: 0x0005AC84
	public static EventTriggerListener Get(GameObject go)
	{
		bool flag = go == null;
		EventTriggerListener result;
		if (flag)
		{
			result = null;
		}
		else
		{
			EventTriggerListener eventTriggerListener = go.GetComponent<EventTriggerListener>();
			bool flag2 = eventTriggerListener == null;
			if (flag2)
			{
				eventTriggerListener = go.AddComponent<EventTriggerListener>();
			}
			result = eventTriggerListener;
		}
		return result;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0005CAC2 File Offset: 0x0005ACC2
	public void Awake()
	{
		this.slcComp = base.gameObject.GetComponent<Selectable>();
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0005CAD8 File Offset: 0x0005ACD8
	public void Update()
	{
		bool flag = !GameRoot.inst.m_bIsFocus;
		if (flag)
		{
			bool flag2 = this.onExit != null;
			if (flag2)
			{
				this.onExit(base.gameObject);
			}
			bool flag3 = this.onUp != null;
			if (flag3)
			{
				this.onUp(base.gameObject);
			}
		}
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0005CB3B File Offset: 0x0005AD3B
	public void SetSoundStr(string sover = "", string sclick = "")
	{
		this.sOverSound = sover;
		this.sClickSound = sclick;
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0005CB4C File Offset: 0x0005AD4C
	public void OnPointerClick(PointerEventData eventData)
	{
		bool flag = !MatchManager.GetInstance().isEnterFb && (!(this.slcComp != null) || this.slcComp.IsInteractable());
		if (flag)
		{
			bool flag2 = !string.IsNullOrEmpty(this.sClickSound);
			if (flag2)
			{
				fUISoundManager.instance.PlayUISound(this.sClickSound, 0, null, 20);
			}
			bool flag3 = this.onClick != null;
			if (flag3)
			{
				this.onClick(base.gameObject);
			}
		}
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0005CBD8 File Offset: 0x0005ADD8
	public void OnPointerDown(PointerEventData eventData)
	{
		bool flag = !(this.slcComp != null) || this.slcComp.IsInteractable();
		if (flag)
		{
			bool flag2 = this.onDown != null;
			if (flag2)
			{
				this.onDown(base.gameObject);
			}
			this.t2 = DateTime.Now;
			bool flag3 = this.t2 - this.t1 < new TimeSpan(0, 0, 0, 0, 500) && this.onDoubleClick != null;
			if (flag3)
			{
				this.onDoubleClick(base.gameObject);
			}
			this.t1 = this.t2;
		}
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0005CC90 File Offset: 0x0005AE90
	public void OnPointerEnter(PointerEventData eventData)
	{
		bool flag = !(this.slcComp != null) || this.slcComp.IsInteractable();
		if (flag)
		{
			bool flag2 = !string.IsNullOrEmpty(this.sOverSound);
			if (flag2)
			{
				fUISoundManager.instance.PlayUISound(this.sOverSound, 0, null, 20);
			}
			bool flag3 = this.onEnter != null;
			if (flag3)
			{
				this.onEnter(base.gameObject);
			}
		}
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0005CD0C File Offset: 0x0005AF0C
	public void OnPointerExit(PointerEventData eventData)
	{
		bool flag = this.onExit != null;
		if (flag)
		{
			this.onExit(base.gameObject);
		}
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0005CD3C File Offset: 0x0005AF3C
	public void OnPointerUp(PointerEventData eventData)
	{
		bool flag = this.onUp != null;
		if (flag)
		{
			this.onUp(base.gameObject);
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0005CD6C File Offset: 0x0005AF6C
	public void OnSelect(BaseEventData eventData)
	{
		bool flag = (!(this.slcComp != null) || this.slcComp.IsInteractable()) && this.onSelect != null;
		if (flag)
		{
			this.onSelect(base.gameObject);
		}
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0005CDBC File Offset: 0x0005AFBC
	public void OnUpdateSelected(BaseEventData eventData)
	{
		bool flag = (!(this.slcComp != null) || this.slcComp.IsInteractable()) && this.onUpdateSelect != null;
		if (flag)
		{
			this.onUpdateSelect(base.gameObject);
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0005CE0C File Offset: 0x0005B00C
	public void OnDrag(PointerEventData eventData)
	{
		bool flag = this.onDrag != null;
		if (flag)
		{
			this.onDrag(base.gameObject, eventData);
		}
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0005CE3C File Offset: 0x0005B03C
	public void OnEndDrag(PointerEventData eventData)
	{
		bool flag = this.onEndDrag != null;
		if (flag)
		{
			this.onEndDrag(base.gameObject, eventData);
		}
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0005CE6C File Offset: 0x0005B06C
	public void OnBeginDrag(PointerEventData eventData)
	{
		bool flag = this.onBeginDrag != null;
		if (flag)
		{
			this.onBeginDrag(base.gameObject, eventData);
		}
	}

	// Token: 0x0400084A RID: 2122
	public EventTriggerListener.VoidDelegate onClick;

	// Token: 0x0400084B RID: 2123
	public EventTriggerListener.VoidDelegate onDown;

	// Token: 0x0400084C RID: 2124
	public EventTriggerListener.VoidDelegate onEnter;

	// Token: 0x0400084D RID: 2125
	public EventTriggerListener.VoidDelegate onExit;

	// Token: 0x0400084E RID: 2126
	public EventTriggerListener.VoidDelegate onUp;

	// Token: 0x0400084F RID: 2127
	public EventTriggerListener.VoidDelegate onSelect;

	// Token: 0x04000850 RID: 2128
	public EventTriggerListener.VoidDelegate onUpdateSelect;

	// Token: 0x04000851 RID: 2129
	public EventTriggerListener.VoidDelegate onDoubleClick;

	// Token: 0x04000852 RID: 2130
	public EventTriggerListener.DataDelegate onBeginDrag;

	// Token: 0x04000853 RID: 2131
	public EventTriggerListener.DataDelegate onDrag;

	// Token: 0x04000854 RID: 2132
	public EventTriggerListener.DataDelegate onEndDrag;

	// Token: 0x04000855 RID: 2133
	private DateTime t1;

	// Token: 0x04000856 RID: 2134
	private DateTime t2;

	// Token: 0x04000857 RID: 2135
	private string sOverSound = string.Empty;

	// Token: 0x04000858 RID: 2136
	private string sClickSound = string.Empty;

	// Token: 0x04000859 RID: 2137
	private Selectable slcComp;

	// Token: 0x0200055C RID: 1372
	// (Invoke) Token: 0x060033D3 RID: 13267
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x0200055D RID: 1373
	// (Invoke) Token: 0x060033D7 RID: 13271
	public delegate void DataDelegate(GameObject go, PointerEventData eventData);
}
