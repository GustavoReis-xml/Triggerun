using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000489 RID: 1161
[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x0018AED0 File Offset: 0x001890D0
	public int frames
	{
		get
		{
			return this.szSprite.Count;
		}
	}

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x0018AEE0 File Offset: 0x001890E0
	// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x0018AEF8 File Offset: 0x001890F8
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x0018AF04 File Offset: 0x00189104
	// (set) Token: 0x06002CDA RID: 11482 RVA: 0x0018AF1C File Offset: 0x0018911C
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x06002CDB RID: 11483 RVA: 0x0018AF26 File Offset: 0x00189126
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x0018AF2E File Offset: 0x0018912E
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x0018AF38 File Offset: 0x00189138
	private void Update()
	{
		bool flag = !this.mActive || this.szSprite.Count <= 1 || !Application.isPlaying || (float)this.mFPS <= 0f;
		if (!flag)
		{
			this.mDelta += Time.deltaTime;
			float num = 1f / (float)this.mFPS;
			bool flag2 = num >= this.mDelta;
			if (!flag2)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				bool flag3 = Time.time - this.mDelayCount < this.mLoopDelay;
				if (!flag3)
				{
					int num2 = this.mIndex + 1;
					this.mIndex = num2;
					bool flag4 = num2 >= this.szSprite.Count;
					if (flag4)
					{
						this.mIndex = 0;
						this.mActive = this.loop;
						bool flag5 = this.mDelayCount == 0f;
						if (flag5)
						{
							this.mDelayCount = Time.time;
							this.mSprite.enabled = false;
						}
					}
					bool flag6 = this.mActive && Time.time - this.mDelayCount >= this.mLoopDelay;
					if (flag6)
					{
						this.mDelayCount = 0f;
						this.mSprite.enabled = true;
						this.mSprite.sprite = this.szSprite[this.mIndex];
						this.mSprite.SetNativeSize();
					}
				}
			}
		}
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x0018B0CC File Offset: 0x001892CC
	private void RebuildSpriteList()
	{
		bool flag = this.mSprite == null;
		if (flag)
		{
			this.mSprite = base.GetComponent<Image>();
		}
	}

	// Token: 0x06002CDF RID: 11487 RVA: 0x0018B0F8 File Offset: 0x001892F8
	public void Reset()
	{
		this.mActive = true;
		this.mIndex = 0;
		this.mDelayCount = 0f;
		bool flag = this.mSprite != null && this.szSprite.Count > 0;
		if (flag)
		{
			this.mSprite.sprite = this.szSprite[this.mIndex];
			this.mSprite.SetNativeSize();
		}
	}

	// Token: 0x0400260B RID: 9739
	public int mFPS = 30;

	// Token: 0x0400260C RID: 9740
	public bool mLoop = true;

	// Token: 0x0400260D RID: 9741
	public float mLoopDelay;

	// Token: 0x0400260E RID: 9742
	private Image mSprite;

	// Token: 0x0400260F RID: 9743
	private float mDelta;

	// Token: 0x04002610 RID: 9744
	private int mIndex;

	// Token: 0x04002611 RID: 9745
	private bool mActive = true;

	// Token: 0x04002612 RID: 9746
	private float mDelayCount;

	// Token: 0x04002613 RID: 9747
	public List<Sprite> szSprite = new List<Sprite>();
}
