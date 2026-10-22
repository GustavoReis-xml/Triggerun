using System;
using UnityEngine;

// Observational only: no writes to player transforms, physics or animation.
public class TriggerunSkinProximity : MonoBehaviour
{
    private float nextScan;
    private void Update()
    {
        if(Time.time<nextScan)return;
        nextScan=Time.time+1f;
        TBUserControl[] controls=UnityEngine.Object.FindObjectsOfType<TBUserControl>();
        System.Collections.Generic.List<GamePlayer> players=new System.Collections.Generic.List<GamePlayer>();
        foreach(TBUserControl control in controls){GamePlayer value=control.hero as GamePlayer;if(value!=null && !players.Contains(value))players.Add(value);}
        foreach(GamePlayer player in players){
            if(player==null || player.haveDestroy || player.playerCommon==null || player.userModelTps==null || player.UserControl==null)continue;
            try{
                SkinUnlock skin=HeroDataManager.GetInstance().GetUseSkinData(player.playerCommon.m_nModelIndex,player.playerCommon.m_nCid);
                if(skin==null || (skin.tpsModel!="male_tps_hercules" && skin.tpsModel!="meidusha_tps_rider" && skin.tpsModel!="xingjilieren_tps_scarlet"))continue;
                TriggerunSkinContact trace=player.UserControl.GetComponent<TriggerunSkinContact>();
                if(trace==null)trace=player.UserControl.AddComponent<TriggerunSkinContact>();
                trace.owner=player;trace.skin=skin.tpsModel;
                GamePlayer nearest=null;float distance=3f;
                foreach(GamePlayer other in players){
                    if(other==player || other==null || other.haveDestroy || other.isDead || other.UserControl==null)continue;
                    float d=Vector3.Distance(player.UserControl.transform.position,other.UserControl.transform.position);
                    if(d<distance){distance=d;nearest=other;}
                }
                if(nearest==null)continue;
                Rigidbody body=player.UserControl.GetComponent<Rigidbody>();
                TBUserControlIt remote=player.UserControl.GetComponent<TBUserControlIt>();
                Debug.Log("[TRIG SkinNear] cid="+player.playerCommon.m_nCid+" skin="+skin.tpsModel+" dead="+player.isDead+" peer="+nearest.playerCommon.m_nCid+" distance="+distance.ToString("F3")+" control="+player.UserControl.transform.position.ToString("F3")+" modelLocal="+player.userModelTps.transform.localPosition.ToString("F3")+" bipLocal="+(player.userBip==null?"null":player.userModelTps.transform.InverseTransformPoint(player.userBip.position).ToString("F3"))+" velocity="+(body==null?"null":body.velocity.ToString("F3"))+" remoteTarget="+(remote==null?"local":remote.moveTarget.ToString("F3")));
            }catch(Exception error){Debug.Log("[TRIG SkinNear] diagnostic="+error.GetType().Name);}
        }
    }
}

public class TriggerunSkinContact : MonoBehaviour
{
    public GamePlayer owner;
    public string skin;
    private float nextLog;
    private void OnCollisionStay(Collision contact)
    {
        if(Time.time<nextLog || owner==null || owner.playerCommon==null || contact.collider==null)return;
        TBUserControl peerControl=contact.collider.GetComponentInParent<TBUserControl>();
        GamePlayer peer=peerControl==null?null:peerControl.hero as GamePlayer;
        if(peer==null || peer==owner)return;
        nextLog=Time.time+1f;
        Debug.Log("[TRIG SkinContact] cid="+owner.playerCommon.m_nCid+" skin="+skin+" collider="+contact.collider.name+" root="+contact.collider.transform.root.name+" velocity="+contact.relativeVelocity.ToString("F3")+" impulse="+contact.impulse.ToString("F3"));
    }
}

