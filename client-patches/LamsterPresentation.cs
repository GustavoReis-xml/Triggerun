using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cosmetic only. Native launchers retain all collision, damage, healing and
// cooldown decisions. Callbacks run only after a successful native shot/cast.
public class TriggerunLamsterPresentation : MonoBehaviour
{
    private NetLiving actor;
    private int combo;
    private float shotAt=-10f,until;
    private AudioSource sound;
    private GameObject cachedTps,cachedFps;
    private Transform leftTps,rightTps,leftFps,rightFps;
    private static readonly Dictionary<int,AudioClip> clips=new Dictionary<int,AudioClip>();
    private static readonly Color emerald=new Color(.10f,.85f,.55f,.55f),gold=new Color(1f,.70f,.18f,.55f);

    private static bool Valid(NetLiving n){return n!=null&&!n.haveDestroy&&!n.isDead&&n.playerHaveCreate&&n.UserControl!=null&&n.playerCommon!=null&&n.playerCommon.m_nModelIndex==2000;}
    private static TriggerunLamsterPresentation Get(NetLiving n){
        if(!Valid(n))return null;
        TriggerunLamsterPresentation p=n.UserControl.GetComponent<TriggerunLamsterPresentation>();
        if(p==null)p=n.UserControl.AddComponent<TriggerunLamsterPresentation>();
        p.actor=n;return p;
    }
    public static void OnShot(CLauncher launcher,int result){
        try{
            if(result!=1||launcher==null||launcher.weaponData==null)return;
            TriggerunLamsterPresentation p=Get(launcher.NetObject);if(p==null)return;
            p.Shot(launcher.weaponData.shootname);
        }catch(Exception ex){Debug.LogWarning("[Lamster presentation] "+ex.Message);}
    }
    public static void OnSkill(NetLiving n,ArrayList data){
        try{
            if(data==null||data.Count<1)return;int id=Convert.ToInt32(data[0]);
            if(id<22001||id>22003)return;
            TriggerunLamsterPresentation p=Get(n);if(p==null)return;
            if(id==22001){p.Play("LamsterContain",.52f);p.PlaySound(2);p.Wave(6f,emerald,.42f);p.Trail(false,.34f,emerald);}
            else if(id==22002){p.Play("LamsterSprint",.36f);p.PlaySound(3);}
            else {p.Play("LamsterRally",.78f);p.PlaySound(4);p.Wave(10f,gold,.65f);p.Trail(true,.48f,gold);p.Trail(false,.48f,gold);}
        }catch(Exception ex){Debug.LogWarning("[Lamster presentation] "+ex.Message);}
    }
    private void Shot(int id){
        if(id!=1&&id!=2&&id!=4)return;
        if(Time.time-shotAt<.045f)return;shotAt=Time.time;
        if(id==2){Play("LamsterPulse",.60f);PlaySound(1);Wave(8f,gold,.48f);Trail(true,.36f,gold);Trail(false,.36f,gold);}
        else{
            bool left=id==1&&((combo++&1)==0);Play(left?"LamsterJab":"LamsterCross",left?.44f:.50f);
            PlaySound(0);Trail(left,.30f,emerald);
        }
    }
    private void Play(string name,float duration){
        if(!Valid(actor))return;
        bool played=false;
        if(actor.tCharAnimationTps!=null)played=PlayOne(actor.tCharAnimationTps.animator,"UpperBody."+name,1)||played;
        if(actor.tCharAnimationFps!=null)played=PlayOne(actor.tCharAnimationFps.animator,"Base Layer."+name,0)||played;
        until=played?Time.time+duration:0f;
    }
    private static bool PlayOne(Animator a,string name,int layer){
        if(a==null||a.runtimeAnimatorController==null)return false;
        int hash=Animator.StringToHash(name);
        if(a.HasState(layer,hash)){a.SetInteger("AttackState",0);a.CrossFade(hash,.06f,layer,0f);return true;}
        return false;
    }
    private static Transform Hand(GameObject model,string side){
        if(model==null)return null;
        foreach(Transform t in model.GetComponentsInChildren<Transform>(true))if(t.name=="Bip001 "+side+" Hand")return t;
        return null;
    }
    private void Trail(bool left,float duration,Color color){
        if(cachedTps!=actor.userModelTps){cachedTps=actor.userModelTps;leftTps=Hand(cachedTps,"L");rightTps=Hand(cachedTps,"R");}
        if(cachedFps!=actor.userModelFps){cachedFps=actor.userModelFps;leftFps=Hand(cachedFps,"L");rightFps=Hand(cachedFps,"R");}
        bool fps=actor==fGameManager.Heroplayer&&actor.FpsOrTps==1;
        Transform hand=fps?(left?leftFps:rightFps):(left?leftTps:rightTps);if(hand==null)return;
        if(TriggerunLamsterWave.Material==null)return;
        GameObject obj=new GameObject("Lamster fist energy");obj.layer=hand.gameObject.layer;obj.transform.SetParent(hand,false);
        TrailRenderer tr=obj.AddComponent<TrailRenderer>();tr.sharedMaterial=TriggerunLamsterWave.Material;
        tr.time=.11f;tr.minVertexDistance=.012f;tr.startWidth=.045f;tr.endWidth=.003f;
        tr.startColor=color;tr.endColor=new Color(color.r,color.g,color.b,0f);
        tr.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;tr.receiveShadows=false;
        Destroy(obj,duration);
    }
    private void Wave(float radius,Color color,float duration){
        Vector3 center=actor.UserControl.transform.position+Vector3.up*.06f;
        TriggerunLamsterWave.Spawn(center,radius,color,duration,actor);
    }
    private void PlaySound(int kind){
        if(sound==null){sound=gameObject.AddComponent<AudioSource>();sound.playOnAwake=false;sound.loop=false;sound.minDistance=1.5f;sound.maxDistance=24f;sound.rolloffMode=AudioRolloffMode.Linear;
            CSoundManager.Instance.SetEffectSoundMixer(sound,0,actor,20);}
        sound.spatialBlend=actor==fGameManager.Heroplayer?0f:1f;
        AudioClip clip;
        if(!clips.TryGetValue(kind,out clip)){clip=MakeSound(kind);clips[kind]=clip;}
        sound.PlayOneShot(clip,kind==0?.22f:.25f);
    }
    private static AudioClip MakeSound(int kind){
        const int rate=22050;float duration=kind==0?.18f:kind==1?.40f:kind==2?.28f:kind==3?.22f:.72f;
        int count=(int)(duration*rate);float[] data=new float[count];uint seed=(uint)(8317+kind);float low=0f,previous=0f,phase=0f;
        for(int i=0;i<count;i++){
            float t=(float)i/count;seed=1664525u*seed+1013904223u;float noise=((seed>>8)&65535)/32767.5f-1f;
            low+=.14f*(noise-low);float high=low-previous;previous=low;
            float env=(float)Math.Sin(Math.PI*t)*(float)Math.Pow(1f-t,kind==4?.7:1.3);
            float freq=kind==4?220f+260f*t:kind==0?360f-260f*t:520f-360f*t;
            phase+=2f*(float)Math.PI*freq/rate;
            float tone=(float)Math.Sin(phase)*.18f+(float)Math.Sin(phase*1.5f)*.08f;
            data[i]=Mathf.Clamp(env*((kind==0||kind==3?low*.65f:low*.25f)+high*.3f+tone),-.8f,.8f);
        }
        AudioClip clip=AudioClip.Create("Lamster kinetic "+kind,count,1,rate,false);clip.SetData(data,0);return clip;
    }
    private void Update(){
        if(!Valid(actor)){until=0f;if(sound!=null)sound.Stop();return;}
        if(Time.time<until){
            if(actor.tCharAnimationTps!=null&&actor.tCharAnimationTps.animator!=null)actor.tCharAnimationTps.animator.SetInteger("AttackState",0);
            if(actor.tCharAnimationFps!=null&&actor.tCharAnimationFps.animator!=null)actor.tCharAnimationFps.animator.SetInteger("AttackState",0);
        }
    }
    private void OnDisable(){until=0f;if(sound!=null)sound.Stop();}
    private void OnDestroy(){if(sound!=null)Destroy(sound);}
}

public class TriggerunLamsterWave : MonoBehaviour
{
    private static Material material;
    public static Material Material {get{
        if(material==null){Shader s=Shader.Find("Particles/Additive");if(s==null)s=Shader.Find("Sprites/Default");if(s==null)return null;material=new Material(s);material.hideFlags=HideFlags.DontSave;}
        return material;
    }}
    private LineRenderer outer,inner;
    private float started,duration,radius;
    private Color color;
    private NetLiving owner;
    public static void Spawn(Vector3 pos,float radius,Color color,float duration,NetLiving owner){
        if(Material==null)return;
        GameObject go=new GameObject("Lamster kinetic wave");go.transform.position=pos;
        if(fGameManager.allBulletFather!=null)go.transform.SetParent(fGameManager.allBulletFather.transform,true);
        TriggerunLamsterWave w=go.AddComponent<TriggerunLamsterWave>();w.radius=radius;w.color=color;w.duration=duration;w.owner=owner;w.started=Time.time;
        w.outer=w.Line("outer",.038f);w.inner=w.Line("inner",.018f);
    }
    private LineRenderer Line(string name,float width){
        GameObject go=new GameObject(name);go.transform.SetParent(transform,false);LineRenderer r=go.AddComponent<LineRenderer>();
        r.sharedMaterial=Material;r.useWorldSpace=false;r.SetVertexCount(65);r.startWidth=width;r.endWidth=width;
        r.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;r.receiveShadows=false;return r;
    }
    private void Update(){
        float u=(Time.time-started)/duration;
        if(u>=1f||owner==null||owner.haveDestroy||owner.isDead||owner.playerCommon==null||owner.playerCommon.m_nModelIndex!=2000){Destroy(gameObject);return;}
        float r=Mathf.Lerp(.12f,radius,Mathf.SmoothStep(0f,1f,u));Color c=color;c.a*=1f-u;
        outer.startColor=outer.endColor=c;c.a*=.45f;inner.startColor=inner.endColor=c;
        for(int i=0;i<=64;i++){float a=i*(Mathf.PI*2f/64f);Vector3 p=new Vector3(Mathf.Cos(a)*r,0f,Mathf.Sin(a)*r);outer.SetPosition(i,p);inner.SetPosition(i,p*.91f);}
    }
}
