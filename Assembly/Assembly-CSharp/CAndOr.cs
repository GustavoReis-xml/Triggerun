using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class CAndOr : MonoBehaviour
{
	// Token: 0x0600027A RID: 634 RVA: 0x00012724 File Offset: 0x00010924
	private void Start()
	{
		this.a = 1;
		this.b = 2;
		this.c = 4;
		this.d = 8;
		this.e = 16;
		this.f = 32;
		this.aa = (this.a | this.b | this.c | this.d | this.e | this.f);
		this.bb = (this.b | this.c | this.d | this.e | this.f);
		this.cc = (this.c | this.d | this.e | this.f);
		this.dd = (this.d | this.e | this.f);
		this.ee = (this.e | this.f);
		this.ff = this.f;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00012810 File Offset: 0x00010A10
	private void OnGUI()
	{
		string text = string.Format("a:{0},     b:{1},     c:{2},     d:{3},     e:{4},     f:{5}", new object[]
		{
			this.a,
			this.b,
			this.c,
			this.d,
			this.e,
			this.f
		});
		GUI.Label(new Rect(10f, 10f, 500f, 50f), text);
		string text2 = string.Format("aa:{0},     bb:{1},     cc:{2},     dd:{3},     ee:{4},     ff:{5}", new object[]
		{
			this.aa,
			this.bb,
			this.cc,
			this.dd,
			this.ee,
			this.ff
		});
		GUI.Label(new Rect(10f, 25f, 500f, 50f), text2);
		string text3 = string.Format("aa & a :{0} , aa & b :{1} , aa & c :{2} , aa & d :{3} , aa & e :{4} , aa & f :{5}", new object[]
		{
			this.aa & this.a,
			this.aa & this.b,
			this.aa & this.c,
			this.aa & this.d,
			this.aa & this.e,
			this.aa & this.f
		});
		GUI.Label(new Rect(10f, 50f, 500f, 50f), text3);
		string text4 = string.Format("bb & a :{0} , bb & b :{1} , bb & c :{2} , bb & d :{3} , bb & e :{4} , bb & f :{5}", new object[]
		{
			this.bb & this.a,
			this.bb & this.b,
			this.bb & this.c,
			this.bb & this.d,
			this.bb & this.e,
			this.bb & this.f
		});
		GUI.Label(new Rect(10f, 100f, 500f, 50f), text4);
		string text5 = string.Format("cc & a :{0} , cc & b :{1} , cc & c :{2} , cc & d :{3} , cc & e :{4} , cc & f :{5}", new object[]
		{
			this.cc & this.a,
			this.cc & this.b,
			this.cc & this.c,
			this.cc & this.d,
			this.cc & this.e,
			this.cc & this.f
		});
		GUI.Label(new Rect(10f, 150f, 500f, 50f), text5);
		string text6 = string.Format("dd & a :{0} , dd & b :{1} , dd & c :{2} , dd & d :{3} , dd & e :{4} , dd & f :{5}", new object[]
		{
			this.dd & this.a,
			this.dd & this.b,
			this.dd & this.c,
			this.dd & this.d,
			this.dd & this.e,
			this.dd & this.f
		});
		GUI.Label(new Rect(10f, 200f, 500f, 50f), text6);
		string text7 = string.Format("ee & a :{0} , ee & b :{1} , ee & c :{2} , ee & d :{3} , ee & e :{4} , ee & f :{5}", new object[]
		{
			this.ee & this.a,
			this.ee & this.b,
			this.ee & this.c,
			this.ee & this.d,
			this.ee & this.e,
			this.ee & this.f
		});
		GUI.Label(new Rect(10f, 250f, 500f, 50f), text7);
		string text8 = string.Format("ff & a :{0} , ff & b :{1} , ff & c :{2} , ff & d :{3} , ff & e :{4} , ff & f :{5}", new object[]
		{
			this.ff & this.a,
			this.ff & this.b,
			this.ff & this.c,
			this.ff & this.d,
			this.ff & this.e,
			this.ff & this.f
		});
		GUI.Label(new Rect(10f, 300f, 500f, 50f), text8);
		GUI.Label(new Rect(10f, 400f, 500f, 50f), (this.aa & (this.a | this.b)).ToString());
	}

	// Token: 0x04000233 RID: 563
	private int a;

	// Token: 0x04000234 RID: 564
	private int b;

	// Token: 0x04000235 RID: 565
	private int c;

	// Token: 0x04000236 RID: 566
	private int d;

	// Token: 0x04000237 RID: 567
	private int e;

	// Token: 0x04000238 RID: 568
	private int f;

	// Token: 0x04000239 RID: 569
	private int aa;

	// Token: 0x0400023A RID: 570
	private int bb;

	// Token: 0x0400023B RID: 571
	private int cc;

	// Token: 0x0400023C RID: 572
	private int dd;

	// Token: 0x0400023D RID: 573
	private int ee;

	// Token: 0x0400023E RID: 574
	private int ff;
}
