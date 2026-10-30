using System;
using UnityEngine;
using UnityEngine.UI;
public class TriggerunCrosshair : MonoBehaviour
{
    private Cross_Hair_Handle cross;
    private RectTransform[] bars;
    private Vector2[] positions;
    private Image[] images;
    private Image dot;
    private int width,length;
    private bool changed;
    public void Initialize(Cross_Hair_Handle value,int w,int l)
    {
        cross=value;width=w;length=l;
        if(bars!=null)return;
        bars=new[]{cross.Cross_Top,cross.Cross_Down,cross.Cross_Left,cross.Cross_Right};
        images=new Image[4];positions=new Vector2[4];
        for(int i=0;i<4;i++){positions[i]=bars[i].anchoredPosition;images[i]=bars[i].GetComponent<Image>();}
        GameObject obj=new GameObject("CustomCrosshairDot",typeof(RectTransform),typeof(CanvasRenderer),typeof(Image));
        obj.transform.SetParent(cross.transform,false);dot=obj.GetComponent<Image>();dot.raycastTarget=false;
        RectTransform rect=obj.GetComponent<RectTransform>();rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(.5f,.5f);rect.anchoredPosition=Vector2.zero;
        dot.enabled=false;
    }
    private void LateUpdate()
    {
        if(cross==null || bars==null)return;
        if(!TriggerunHudSettings.custom){
            if(changed){for(int i=0;i<4;i++){bars[i].sizeDelta=new Vector2(width,length);bars[i].anchoredPosition=positions[i];images[i].enabled=true;}dot.enabled=false;changed=false;}
            return;
        }
        changed=true;
        Color color=TriggerunHudSettings.SelectedColor;
        Vector2[] direction={Vector2.up,Vector2.down,Vector2.left,Vector2.right};
        for(int i=0;i<4;i++){
            bars[i].sizeDelta=new Vector2(TriggerunHudSettings.thickness,TriggerunHudSettings.length);
            bars[i].anchoredPosition=positions[i]+direction[i]*TriggerunHudSettings.gap;
            images[i].enabled=TriggerunHudSettings.style!=1;images[i].color=color;
        }
        dot.enabled=TriggerunHudSettings.style!=0;dot.color=color;
        dot.rectTransform.sizeDelta=new Vector2(TriggerunHudSettings.thickness+1,TriggerunHudSettings.thickness+1);
    }
}

public class TriggerunHudSettings : MonoBehaviour
{
    public static bool custom;
    public static int thickness=2,length=12,gap=2,style,colorIndex;
    private static TriggerunHudSettings instance;
    private static readonly Color[] colors={Color.green,Color.white,Color.cyan,Color.yellow,new Color(1,.2f,.7f)};
    public static Color SelectedColor {get{return colors[Mathf.Clamp(colorIndex,0,colors.Length-1)];}}
    private bool open,oldControl,oldCursor,oldMouseShow;
    private CursorLockMode oldLock;
    private HeroPlayer owner;
    public static void Ensure(){if(instance!=null)return;GameObject obj=new GameObject("TriggerunHudSettings");DontDestroyOnLoad(obj);instance=obj.AddComponent<TriggerunHudSettings>();obj.AddComponent<TriggerunLamsterHud>();obj.AddComponent<TriggerunRoundSkinPortrait>();obj.AddComponent<TriggerunSkinProximity>();}
    private void Awake(){custom=PlayerPrefs.GetInt("tr.hud.custom",0)!=0;thickness=Mathf.Clamp(PlayerPrefs.GetInt("tr.hud.width",2),1,8);length=Mathf.Clamp(PlayerPrefs.GetInt("tr.hud.length",12),3,30);gap=Mathf.Clamp(PlayerPrefs.GetInt("tr.hud.gap",2),0,20);style=Mathf.Clamp(PlayerPrefs.GetInt("tr.hud.style",0),0,2);colorIndex=Mathf.Clamp(PlayerPrefs.GetInt("tr.hud.color",0),0,4);}
    private void Save(){PlayerPrefs.SetInt("tr.hud.custom",custom?1:0);PlayerPrefs.SetInt("tr.hud.width",thickness);PlayerPrefs.SetInt("tr.hud.length",length);PlayerPrefs.SetInt("tr.hud.gap",gap);PlayerPrefs.SetInt("tr.hud.style",style);PlayerPrefs.SetInt("tr.hud.color",colorIndex);PlayerPrefs.Save();}
    private void Close(bool restore=true){if(!open)return;open=false;Save();if(restore){if(fGameManager.Heroplayer==owner && owner!=null && !owner.haveDestroy){HeroPlayer.isPlayerControl=oldControl;HeroPlayer.isMouseShow=oldMouseShow;}Cursor.visible=oldCursor;Cursor.lockState=oldLock;}owner=null;}
    private void Update(){
        if(open&&(owner==null||owner.haveDestroy||owner.isDead||fGameManager.Heroplayer!=owner)){Close();return;}
        if(Input.GetKeyDown(KeyCode.F2)){
            if(open){Close();return;}
            HeroPlayer player=fGameManager.Heroplayer;
            if(player==null||player.haveDestroy||player.isDead||!player.playerHaveCreate||!HeroPlayer.isPlayerControl||ChatManager.ChatInput)return;
            owner=player;oldControl=HeroPlayer.isPlayerControl;oldMouseShow=HeroPlayer.isMouseShow;oldCursor=Cursor.visible;oldLock=Cursor.lockState;
            HeroPlayer.isPlayerControl=false;HeroPlayer.isMouseShow=true;Cursor.visible=true;Cursor.lockState=CursorLockMode.None;open=true;
        }
        if(open&&Input.GetKeyDown(KeyCode.Escape))Close(false);
    }
    private void OnDestroy(){Close();if(instance==this)instance=null;}
    private void OnGUI(){if(!open)return;Rect panel=new Rect(24,24,360,390);GUI.Box(panel,"Mira personalizada · F2");GUILayout.BeginArea(new Rect(40,58,328,340));
        custom=GUILayout.Toggle(custom,"Usar mira personalizada");
        GUILayout.Label("Formato");style=GUILayout.SelectionGrid(style,new[]{"Cruz","Ponto","Cruz + ponto"},3);
        GUILayout.Label("Cor");colorIndex=GUILayout.SelectionGrid(colorIndex,new[]{"Verde","Branca","Ciano","Amarela","Rosa"},3);
        GUILayout.Label("Espessura: "+thickness);thickness=Mathf.RoundToInt(GUILayout.HorizontalSlider(thickness,1,8));
        GUILayout.Label("Comprimento: "+length);length=Mathf.RoundToInt(GUILayout.HorizontalSlider(length,3,30));
        GUILayout.Label("Espaço central: "+gap);gap=Mathf.RoundToInt(GUILayout.HorizontalSlider(gap,0,20));
        if(GUILayout.Button("Restaurar mira original")){custom=false;thickness=2;length=12;gap=2;style=0;colorIndex=0;Save();}
        if(GUILayout.Button("Salvar e fechar"))Close();GUILayout.EndArea();
    }
}

// Lamster now uses native OneSkilUI slots, F1 descriptions and the Q meter.
// Reconcile only the new movement buffs: native buffMe overwrites buffSpeed
// on Add and clears it on Delete, even if another speed effect remains active.
public class TriggerunLamsterHud : MonoBehaviour
{
    private void Start(){if(GetComponent<TriggerunLamsterHudLayout>()==null)gameObject.AddComponent<TriggerunLamsterHudLayout>();}
    private HeroPlayer previous;
    private bool ownedSpeed;
    private readonly System.Collections.Generic.Dictionary<int,GameObject> localFx=new System.Collections.Generic.Dictionary<int,GameObject>();
    private readonly System.Collections.Generic.Dictionary<string,GameObject> prefabs=new System.Collections.Generic.Dictionary<string,GameObject>();
    private readonly System.Collections.Generic.Dictionary<string,bool> pending=new System.Collections.Generic.Dictionary<string,bool>();
    private readonly System.Collections.Generic.Dictionary<string,float> retryAfter=new System.Collections.Generic.Dictionary<string,float>();
    private static readonly int[] fxIds={22012};
    private static readonly string[] fxNames={"lamster_sprint_buff"};
    private void ClearFx(){foreach(GameObject fx in localFx.Values)if(fx!=null)Destroy(fx);localFx.Clear();}
    private void LoadFx(string name){
        if(prefabs.ContainsKey(name)||pending.ContainsKey(name))return;
        float retry;if(retryAfter.TryGetValue(name,out retry)&&Time.time<retry)return;
        string url="EffectRes/"+name+".ef";WWW www=GameRoot.GetResWWW(url);
        if(www!=null){OnFxLoaded(www,name);return;}
        pending.Add(name,true);GameRoot.LoadAsset(url,new fResourceManager.DownFinishDelegate(OnFxLoaded),name);
    }
    private void OnFxLoaded(WWW www,object tag){
        string name=(string)tag;pending.Remove(name);
        if(this==null)return;
        if(www==null||www.error!=null||www.assetBundle==null){retryAfter[name]=Time.time+15f;return;}
        string[] names=www.assetBundle.GetAllAssetNames();
        if(names.Length>0)prefabs[name]=www.assetBundle.LoadAsset(names[0]) as GameObject;
    }
    private void LocalEffects(HeroPlayer player){
        for(int i=0;i<fxIds.Length;i++){
            int id=fxIds[i];bool active=player.FpsOrTps==1&&player.playerBuffMe.GetIsBuff(id);
            if(player.playerCommon.m_nModelIndex==2000||active)LoadFx(fxNames[i]);
            GameObject obj;localFx.TryGetValue(id,out obj);
            if(!active){if(obj!=null)Destroy(obj);localFx.Remove(id);continue;}
            GameObject prefab;
            if(obj==null&&prefabs.TryGetValue(fxNames[i],out prefab)&&prefab!=null){
                obj=(GameObject)Instantiate(prefab);localFx[id]=obj;
                CObjectRender render=obj.GetComponent<CObjectRender>();
                if(render!=null)render.SetEnemyFriendRenderParticle(false,false);
            }
            if(obj!=null&&player.userSpine!=null)obj.transform.position=player.userSpine.transform.position;
        }
    }
    private void OnDestroy(){ClearFx();}
    private void LateUpdate()
    {
        HeroPlayer player=fGameManager.Heroplayer;
        if(player!=previous){ClearFx();previous=player;ownedSpeed=false;}
        if(player==null || player.haveDestroy || player.isDead || !player.playerHaveCreate || player.playerCommon==null || player.playerBuffMe==null){ClearFx();return;}
        LocalEffects(player);
        bool custom=player.playerBuffMe.GetIsBuff(22011)||player.playerBuffMe.GetIsBuff(22012);
        if(!custom && !ownedSpeed)return;
        ownedSpeed=custom;
        float positive=0f,negative=0f;
        foreach(BuffInfo info in player.playerBuffMe.mBuffMeData.Values){
            if(info==null || info.buffdata==null || info.buffdata.bType!=8)continue;
            float value=info.buffdata.m_nBuffChangeValueSe*.01f;
            positive=Mathf.Max(positive,value);negative=Mathf.Min(negative,value);
        }
        float speed=Mathf.Clamp(positive+negative,-.9f,2f);
        if(Mathf.Abs(player.playerCommon.buffSpeed-speed)>.001f){
            player.playerCommon.buffSpeed=speed;player.ChangeSpeed();
        }
    }
}


// Keep the Scarlet circular asset square in the native portrait slot.
public class TriggerunRoundSkinPortrait : MonoBehaviour
{
    private RectTransform adjusted;
    private Vector2 original;
    private void Restore(){if(adjusted!=null){adjusted.sizeDelta=original;adjusted=null;}}
    private void LateUpdate()
    {
        HeroPlayer player=fGameManager.Heroplayer;
        if(player==null || player.haveDestroy || player.playerCommon==null || NewHeroHpUi.Inst==null){Restore();return;}
        SkinUnlock skin=HeroDataManager.GetInstance().GetUseSkinData(player.playerCommon.m_nModelIndex,player.playerCommon.m_nCid);
        if(skin==null || (skin.sIconName!="celina_scarlet" && skin.sIconName!="lamster_hercules" && skin.sIconName!="gaya_rider" && skin.sIconName!="celina_red")){Restore();return;}
        Transform image=NewHeroHpUi.Inst.transform.Find("user/img");
        if(image==null){Restore();return;}
        RectTransform rect=image.GetComponent<RectTransform>();
        if(rect==null)return;
        if(adjusted!=rect){Restore();adjusted=rect;original=rect.sizeDelta;}
        rect.sizeDelta=new Vector2(original.x,original.x);
    }
    private void OnDisable(){Restore();}
}


