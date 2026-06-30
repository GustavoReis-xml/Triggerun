using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.Track;

// Token: 0x02000080 RID: 128
public class Chariot_View : Npc_View
{
	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000457 RID: 1111 RVA: 0x00021EEC File Offset: 0x000200EC
	// (set) Token: 0x06000458 RID: 1112 RVA: 0x00021F04 File Offset: 0x00020104
	public float Track_Total_Distance
	{
		get
		{
			return this._Track_Total_Distance;
		}
		set
		{
			this._Track_Total_Distance = value;
		}
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00021F10 File Offset: 0x00020110
	public void Get_Track_Data()
	{
		GameObject gameObject = GameObject.Find("Track");
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			string name = transform.name;
			int num = int.Parse(name.Split(new char[]
			{
				'_'
			})[1]);
			Track component = transform.GetComponent<Track>();
			bool flag = num == 0 || num == 99;
			if (flag)
			{
				component.outOfTrack = Track.EndOfLineBehavior.EndStop;
			}
			component.curveType = TrackCurve.CurveType.Hermite;
			component.acceleration.targetSpeed = 0f;
			component.acceleration.maxForce = 200f;
			component.brakes.targetSpeed = 0f;
			component.brakes.maxForce = 200f;
			this.Track_Total_Distance += component.Distance;
		}
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00022028 File Offset: 0x00020228
	public void Create_Node_Object()
	{
		Dictionary<int, ArrayList> dictionary = ((Chariot_Model)base.Controller_Component.Model_Component).Node_Data;
		foreach (int key in dictionary.Keys)
		{
			ArrayList arrayList = dictionary[key];
			BoxCollider boxCollider = new GameObject("Node_" + key.ToString())
			{
				tag = "Chariot_Node",
				transform = 
				{
					localPosition = (Vector3)arrayList[0],
					localEulerAngles = Vector3.zero,
					localScale = Vector3.one
				},
				layer = 0
			}.AddComponent<BoxCollider>();
			boxCollider.isTrigger = true;
		}
		dictionary = ((Chariot_Model)base.Controller_Component.Model_Component).Pass_Node_Data;
		foreach (int key2 in dictionary.Keys)
		{
			ArrayList arrayList = dictionary[key2];
			BoxCollider boxCollider = new GameObject("Pass_Node_" + key2.ToString())
			{
				tag = "Chariot_Pass_Node",
				transform = 
				{
					localPosition = (Vector3)arrayList[0],
					localEulerAngles = Vector3.zero,
					localScale = Vector3.one
				},
				layer = 0
			}.AddComponent<BoxCollider>();
			boxCollider.isTrigger = true;
		}
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x000221F8 File Offset: 0x000203F8
	public void Find_Nearest_Track(TrackCart _TrackCart)
	{
		Track track = null;
		float num = float.PositiveInfinity;
		Vector3 b = Vector3.zero;
		List<Track> track_Object_List = ((Chariot_Model)base.Controller_Component.Model_Component).Track_Object_List;
		for (int i = 0; i < track_Object_List.Count; i++)
		{
			Track track2 = track_Object_List[i];
			bool enabled = track2.enabled;
			if (enabled)
			{
				b = track2.NearestPointAbsolute(_TrackCart.transform.position).position;
				float num2 = Vector3.Distance(_TrackCart.transform.position, b);
				bool flag = num2 < num;
				if (flag)
				{
					track = track2;
					num = num2;
				}
			}
		}
		bool flag2 = !track;
		if (flag2)
		{
			Debug.LogWarning("Could not find a track to put this cart on", _TrackCart);
		}
		_TrackCart.CurrentTrack = track;
		TrackCurve curve = _TrackCart.CurrentTrack.Curve;
		float num3 = curve.GetFraction(_TrackCart.CurrentTrack.transform.InverseTransformPoint(_TrackCart.transform.position));
		num3 = Mathf.Max(0f, Mathf.Min(num3, 1f));
		SimpleTransform simpleTransform = _TrackCart.CurrentTrack.TrackAbsoluteStart * curve.GetPointAt(num3);
		bool cartReversed = _TrackCart.cartReversed;
		if (cartReversed)
		{
			simpleTransform.AboutFace();
		}
		_TrackCart.transform.position = simpleTransform.position;
		_TrackCart.transform.rotation = simpleTransform.rotation;
	}

	// Token: 0x04000359 RID: 857
	private float _Track_Total_Distance;
}
