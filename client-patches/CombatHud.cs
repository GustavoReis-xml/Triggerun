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

// Local help only: native input and server-confirmed combat remain authoritative.
public class TriggerunLamsterHud : MonoBehaviour
{
    private GUIStyle title, line;
    private void OnGUI()
    {
        HeroPlayer player=fGameManager.Heroplayer;
        if(player==null || player.haveDestroy || player.isDead || !player.canControl || player.playerCommon==null || player.playerCommon.m_nModelIndex!=2000 || !HeroPlayer.isPlayerControl) return;
        if(title==null){title=new GUIStyle(GUI.skin.label);title.fontSize=16;title.fontStyle=FontStyle.Bold;title.normal.textColor=new Color(.35f,1f,.7f);line=new GUIStyle(GUI.skin.label);line.fontSize=13;line.normal.textColor=Color.white;}
        float width=Mathf.Min(410f,Screen.width*.4f), x=Screen.width-width-18f,y=Screen.height-205f;
        GUI.Box(new Rect(x,y,width,163f),string.Empty);
        GUI.Label(new Rect(x+12,y+8,width-24,24),"LAMSTER  |  SUPORTE DE COMBATE",title);
        GUI.Label(new Rect(x+12,y+38,width-24,22),"ESQUERDO  •  Soco: 120 dano / 180 cura",line);
        GUI.Label(new Rect(x+12,y+61,width-24,22),"Alcance 2,2 m  •  Intervalo 1 s",line);
        GUI.Label(new Rect(x+12,y+84,width-24,22),"DIREITO  •  360 cura / 250 dano: 8 m / 6 s",line);
        GUI.Label(new Rect(x+12,y+107,width-24,22),"CORPO A CORPO  •  120 dano / 0,8 s",line);
        GUI.Label(new Rect(x+12,y+130,width-24,22),"Direito: +25% dano para você e aliados por 5 s.",line);
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


