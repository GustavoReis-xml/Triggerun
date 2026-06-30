using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C6 RID: 198
public class Cross_Hair_Handle : MonoBehaviour
{
	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000758 RID: 1880 RVA: 0x00041114 File Offset: 0x0003F314
	// (set) Token: 0x06000759 RID: 1881 RVA: 0x0004114F File Offset: 0x0003F34F
	public RectTransform Cross
	{
		get
		{
			bool flag = this._Cross == null;
			if (flag)
			{
				this._Cross = base.gameObject.GetComponent<RectTransform>();
			}
			return this._Cross;
		}
		set
		{
			this._Cross = value;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x0600075A RID: 1882 RVA: 0x0004115C File Offset: 0x0003F35C
	// (set) Token: 0x0600075B RID: 1883 RVA: 0x000411A6 File Offset: 0x0003F3A6
	public RectTransform Cross_Top
	{
		get
		{
			bool flag = this._Cross_Top == null;
			if (flag)
			{
				this._Cross_Top = base.gameObject.transform.Find("Top").GetComponent<RectTransform>();
			}
			return this._Cross_Top;
		}
		set
		{
			this._Cross_Top = value;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600075C RID: 1884 RVA: 0x000411B0 File Offset: 0x0003F3B0
	// (set) Token: 0x0600075D RID: 1885 RVA: 0x000411F0 File Offset: 0x0003F3F0
	public Image Cross_Top_Image_Component
	{
		get
		{
			bool flag = this._Cross_Top_Image_Component == null;
			if (flag)
			{
				this._Cross_Top_Image_Component = this.Cross_Top.gameObject.GetComponent<Image>();
			}
			return this._Cross_Top_Image_Component;
		}
		set
		{
			this._Cross_Top_Image_Component = value;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x0600075E RID: 1886 RVA: 0x000411FC File Offset: 0x0003F3FC
	// (set) Token: 0x0600075F RID: 1887 RVA: 0x00041246 File Offset: 0x0003F446
	public RectTransform Cross_Down
	{
		get
		{
			bool flag = this._Cross_Down == null;
			if (flag)
			{
				this._Cross_Down = base.gameObject.transform.Find("Down").GetComponent<RectTransform>();
			}
			return this._Cross_Down;
		}
		set
		{
			this._Cross_Down = value;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000760 RID: 1888 RVA: 0x00041250 File Offset: 0x0003F450
	// (set) Token: 0x06000761 RID: 1889 RVA: 0x00041290 File Offset: 0x0003F490
	public Image Cross_Down_Image_Component
	{
		get
		{
			bool flag = this._Cross_Down_Image_Component == null;
			if (flag)
			{
				this._Cross_Down_Image_Component = this.Cross_Down.gameObject.GetComponent<Image>();
			}
			return this._Cross_Down_Image_Component;
		}
		set
		{
			this._Cross_Down_Image_Component = value;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000762 RID: 1890 RVA: 0x0004129C File Offset: 0x0003F49C
	// (set) Token: 0x06000763 RID: 1891 RVA: 0x000412E6 File Offset: 0x0003F4E6
	public RectTransform Cross_Left
	{
		get
		{
			bool flag = this._Cross_Left == null;
			if (flag)
			{
				this._Cross_Left = base.gameObject.transform.Find("Left").GetComponent<RectTransform>();
			}
			return this._Cross_Left;
		}
		set
		{
			this._Cross_Left = value;
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000764 RID: 1892 RVA: 0x000412F0 File Offset: 0x0003F4F0
	// (set) Token: 0x06000765 RID: 1893 RVA: 0x00041330 File Offset: 0x0003F530
	public Image Cross_Left_Image_Component
	{
		get
		{
			bool flag = this._Cross_Left_Image_Component == null;
			if (flag)
			{
				this._Cross_Left_Image_Component = this.Cross_Left.gameObject.GetComponent<Image>();
			}
			return this._Cross_Left_Image_Component;
		}
		set
		{
			this._Cross_Left_Image_Component = value;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000766 RID: 1894 RVA: 0x0004133C File Offset: 0x0003F53C
	// (set) Token: 0x06000767 RID: 1895 RVA: 0x00041386 File Offset: 0x0003F586
	public RectTransform Cross_Right
	{
		get
		{
			bool flag = this._Cross_Right == null;
			if (flag)
			{
				this._Cross_Right = base.gameObject.transform.Find("Right").GetComponent<RectTransform>();
			}
			return this._Cross_Right;
		}
		set
		{
			this._Cross_Right = value;
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000768 RID: 1896 RVA: 0x00041390 File Offset: 0x0003F590
	// (set) Token: 0x06000769 RID: 1897 RVA: 0x000413D0 File Offset: 0x0003F5D0
	public Image Cross_Right_Image_Component
	{
		get
		{
			bool flag = this._Cross_Right_Image_Component == null;
			if (flag)
			{
				this._Cross_Right_Image_Component = this.Cross_Right.gameObject.GetComponent<Image>();
			}
			return this._Cross_Right_Image_Component;
		}
		set
		{
			this._Cross_Right_Image_Component = value;
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000413DC File Offset: 0x0003F5DC
	public void Setting_Size(int _Width, int _Height)
	{
		this.Cross.sizeDelta = new Vector2((float)_Height, (float)_Height);
		this.Cross_Top.sizeDelta = new Vector2((float)_Width, (float)_Height);
		this.Cross_Down.sizeDelta = new Vector2((float)_Width, (float)_Height);
		this.Cross_Left.sizeDelta = new Vector2((float)_Width, (float)_Height);
		this.Cross_Right.sizeDelta = new Vector2((float)_Width, (float)_Height);
		Outline component = this.Cross_Top.gameObject.GetComponent<Outline>();
		bool flag = component == null;
		if (flag)
		{
			this.Cross_Top.gameObject.AddComponent<Outline>();
		}
		component = this.Cross_Down.gameObject.GetComponent<Outline>();
		bool flag2 = component == null;
		if (flag2)
		{
			this.Cross_Down.gameObject.AddComponent<Outline>();
		}
		component = this.Cross_Left.gameObject.GetComponent<Outline>();
		bool flag3 = component == null;
		if (flag3)
		{
			this.Cross_Left.gameObject.AddComponent<Outline>();
		}
		component = this.Cross_Right.gameObject.GetComponent<Outline>();
		bool flag4 = component == null;
		if (flag4)
		{
			this.Cross_Right.gameObject.AddComponent<Outline>();
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00041513 File Offset: 0x0003F713
	private void OnDisable()
	{
		this.Setting_Color(false);
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0004151E File Offset: 0x0003F71E
	private void Awake()
	{
		this.Setting_Color(false);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0004152C File Offset: 0x0003F72C
	public void Setting_Color(bool _State)
	{
		this._Delay_Setting_Color_Time = Time.time + 1.5f;
		if (_State)
		{
			this._Delay_Setting_Target_Color = Color.red;
		}
		else
		{
			this._Delay_Setting_Target_Color = Color.green;
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0004156C File Offset: 0x0003F76C
	private void Update()
	{
		this.Cross_Top_Image_Component.color = Color.Lerp(this.Cross_Top_Image_Component.color, this._Delay_Setting_Target_Color, Time.time);
		this.Cross_Down_Image_Component.color = Color.Lerp(this.Cross_Down_Image_Component.color, this._Delay_Setting_Target_Color, Time.time);
		this.Cross_Left_Image_Component.color = Color.Lerp(this.Cross_Left_Image_Component.color, this._Delay_Setting_Target_Color, Time.time);
		this.Cross_Right_Image_Component.color = Color.Lerp(this.Cross_Right_Image_Component.color, this._Delay_Setting_Target_Color, Time.time);
		bool flag = Time.time >= this._Delay_Setting_Color_Time;
		if (flag)
		{
			this.Setting_Color(false);
		}
	}

	// Token: 0x04000559 RID: 1369
	private RectTransform _Cross;

	// Token: 0x0400055A RID: 1370
	private RectTransform _Cross_Top;

	// Token: 0x0400055B RID: 1371
	private Image _Cross_Top_Image_Component;

	// Token: 0x0400055C RID: 1372
	private RectTransform _Cross_Down;

	// Token: 0x0400055D RID: 1373
	private Image _Cross_Down_Image_Component;

	// Token: 0x0400055E RID: 1374
	private RectTransform _Cross_Left;

	// Token: 0x0400055F RID: 1375
	private Image _Cross_Left_Image_Component;

	// Token: 0x04000560 RID: 1376
	private RectTransform _Cross_Right;

	// Token: 0x04000561 RID: 1377
	private Image _Cross_Right_Image_Component;

	// Token: 0x04000562 RID: 1378
	private Color _Delay_Setting_Target_Color = Color.white;

	// Token: 0x04000563 RID: 1379
	public float _Delay_Setting_Color_Time;
}
