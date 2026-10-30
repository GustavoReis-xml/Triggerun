using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// Native sysload invokes SetNativeSize after every asynchronous icon download.
// Keep art inside the native slot; the native cooldown/key labels stay siblings.
public class TriggerunLamsterHudLayout : MonoBehaviour
{
    private float scanAt;
    private readonly List<TriggerunLamsterIconFit> fits=new List<TriggerunLamsterIconFit>();
    private void RestoreAll(){foreach(TriggerunLamsterIconFit fit in fits)if(fit!=null)fit.Restore();fits.Clear();}
    private void OnDestroy(){RestoreAll();}
    private void LateUpdate(){
        if(Time.unscaledTime<scanAt)return;scanAt=Time.unscaledTime+.2f;
        HeroPlayer p=fGameManager.Heroplayer;
        bool lamster=p!=null&&!p.haveDestroy&&p.playerCommon!=null&&p.playerCommon.m_nModelIndex==2000;
        if(!lamster){RestoreAll();return;}
        foreach(OneSkilUI slot in FindObjectsOfType<OneSkilUI>()){
            Transform t=slot.transform.Find("up/skillpic");
            if(t!=null)Wrap(t.GetComponent<RawImage>(),58f);
        }
        if(NewHeroDaZhaoUi.Inst!=null){
            Transform t=NewHeroDaZhaoUi.Inst.transform.Find("dazhao/full/pic");
            if(t!=null)Wrap(t.GetComponent<RawImage>(),96f);
        }
    }
    private void Wrap(RawImage image,float size){
        if(image==null||image.GetComponent<TriggerunLamsterIconFit>()!=null)return;
        TriggerunLamsterIconFit fit=image.gameObject.AddComponent<TriggerunLamsterIconFit>();fit.Initialize(image,size);fits.Add(fit);
    }
}

public class TriggerunLamsterIconFit : MonoBehaviour
{
    private static Sprite circle;
    private RawImage image;
    private RectTransform oldParent,rect;
    private Vector2 anchorMin,anchorMax,pivot,position;
    private Vector3 scale;
    private int sibling;
    private GameObject mask;
    private float size;
    private static Sprite Circle {get{
        if(circle==null){
            Texture2D texture=new Texture2D(64,64,TextureFormat.RGBA32,false);texture.wrapMode=TextureWrapMode.Clamp;
            Color32[] pixels=new Color32[4096];
            for(int y=0;y<64;y++)for(int x=0;x<64;x++){
                float d=Vector2.Distance(new Vector2(x+.5f,y+.5f),new Vector2(32,32));
                pixels[y*64+x]=new Color32(255,255,255,(byte)(255*Mathf.Clamp01(32f-d)));
            }
            texture.SetPixels32(pixels);texture.Apply();circle=Sprite.Create(texture,new Rect(0,0,64,64),new Vector2(.5f,.5f),64f);
        }
        return circle;
    }}
    public void Initialize(RawImage value,float diameter){
        image=value;size=diameter;rect=image.rectTransform;oldParent=rect.parent as RectTransform;
        anchorMin=rect.anchorMin;anchorMax=rect.anchorMax;pivot=rect.pivot;position=rect.anchoredPosition;scale=rect.localScale;sibling=rect.GetSiblingIndex();
        mask=new GameObject("Lamster circular icon",typeof(RectTransform),typeof(CanvasRenderer),typeof(Image),typeof(Mask));
        RectTransform m=mask.GetComponent<RectTransform>();m.SetParent(oldParent,false);m.SetSiblingIndex(sibling);
        m.anchorMin=anchorMin;m.anchorMax=anchorMax;m.pivot=pivot;m.anchoredPosition=position;m.sizeDelta=new Vector2(size,size);m.localScale=scale;
        Image graphic=mask.GetComponent<Image>();graphic.sprite=Circle;graphic.raycastTarget=false;mask.GetComponent<Mask>().showMaskGraphic=false;
        rect.SetParent(m,false);rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(.5f,.5f);rect.anchoredPosition=Vector2.zero;rect.localScale=Vector3.one;
        Clamp();
    }
    private void Clamp(){if(rect!=null)rect.sizeDelta=new Vector2(size,size);}
    private void LateUpdate(){Clamp();}
    public void Restore(){
        if(rect!=null&&oldParent!=null){
            rect.SetParent(oldParent,false);rect.SetSiblingIndex(sibling);rect.anchorMin=anchorMin;rect.anchorMax=anchorMax;rect.pivot=pivot;rect.anchoredPosition=position;rect.localScale=scale;
            if(image!=null&&image.texture!=null)image.SetNativeSize();
        }
        if(mask!=null)Destroy(mask);Destroy(this);
    }
}
