using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotePoseHUD : MonoBehaviour
{
	public static EmotePoseHUD Instance;
	public static bool isOpen = false;

	private Texture2D bgTexture;
	private Texture2D btnTexture;
	private Texture2D btnHoverTexture;
	private Texture2D btnPoseTexture;
	private Texture2D btnStopTexture;

	private GUIStyle titleStyle;
	private GUIStyle subtitleStyle;
	private GUIStyle sectionHeaderStyle;
	private GUIStyle btnEmoteStyle;
	private GUIStyle btnPoseStyle;
	private GUIStyle btnStopStyle;
	private GUIStyle btnCloseStyle;

	private void Awake()
	{
		EmotePoseHUD.Instance = this;
	}

	private void Start()
	{
		this.InitTextures();
	}

	private void InitTextures()
	{
		this.bgTexture = this.MakeTex(2, 2, new Color(0.06f, 0.08f, 0.12f, 0.92f));
		this.btnTexture = this.MakeTex(2, 2, new Color(0.12f, 0.18f, 0.26f, 0.85f));
		this.btnHoverTexture = this.MakeTex(2, 2, new Color(0.0f, 0.65f, 0.95f, 0.90f));
		this.btnPoseTexture = this.MakeTex(2, 2, new Color(0.22f, 0.18f, 0.05f, 0.85f));
		this.btnStopTexture = this.MakeTex(2, 2, new Color(0.50f, 0.12f, 0.15f, 0.85f));
	}

	private Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width * height];
		for (int i = 0; i < pix.Length; ++i)
		{
			pix[i] = col;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}

	public static void Toggle()
	{
		EmotePoseHUD.isOpen = !EmotePoseHUD.isOpen;
		HeroPlayer.isMouseShow = EmotePoseHUD.isOpen;
		if (EmotePoseHUD.isOpen)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public static void Close()
	{
		EmotePoseHUD.isOpen = false;
		HeroPlayer.isMouseShow = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) && !ChatManager.ChatInput)
		{
			if (fGameManager.Heroplayer != null && !fGameManager.Heroplayer.isDead)
			{
				EmotePoseHUD.Toggle();
			}
		}

		if (EmotePoseHUD.isOpen)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				EmotePoseHUD.Close();
				return;
			}

			if (fGameManager.Heroplayer == null || fGameManager.Heroplayer.isDead)
			{
				EmotePoseHUD.Close();
				return;
			}
		}
	}

	private void InitStyles()
	{
		if (this.titleStyle == null)
		{
			this.titleStyle = new GUIStyle(GUI.skin.label)
			{
				fontSize = 20,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleCenter
			};
			this.titleStyle.normal.textColor = new Color(0f, 0.85f, 1f, 1f);

			this.subtitleStyle = new GUIStyle(GUI.skin.label)
			{
				fontSize = 13,
				fontStyle = FontStyle.Normal,
				alignment = TextAnchor.MiddleCenter
			};
			this.subtitleStyle.normal.textColor = new Color(0.75f, 0.80f, 0.88f, 1f);

			this.sectionHeaderStyle = new GUIStyle(GUI.skin.label)
			{
				fontSize = 14,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleLeft
			};
			this.sectionHeaderStyle.normal.textColor = new Color(1f, 0.80f, 0.20f, 1f);

			this.btnEmoteStyle = new GUIStyle(GUI.skin.button)
			{
				fontSize = 13,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleLeft
			};
			this.btnEmoteStyle.normal.background = this.btnTexture;
			this.btnEmoteStyle.normal.textColor = Color.white;
			this.btnEmoteStyle.hover.background = this.btnHoverTexture;
			this.btnEmoteStyle.hover.textColor = Color.white;

			this.btnPoseStyle = new GUIStyle(GUI.skin.button)
			{
				fontSize = 13,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleLeft
			};
			this.btnPoseStyle.normal.background = this.btnPoseTexture;
			this.btnPoseStyle.normal.textColor = new Color(1f, 0.90f, 0.50f, 1f);
			this.btnPoseStyle.hover.background = this.btnHoverTexture;
			this.btnPoseStyle.hover.textColor = Color.white;

			this.btnStopStyle = new GUIStyle(GUI.skin.button)
			{
				fontSize = 13,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleCenter
			};
			this.btnStopStyle.normal.background = this.btnStopTexture;
			this.btnStopStyle.normal.textColor = Color.white;
			this.btnStopStyle.hover.background = this.btnHoverTexture;
			this.btnStopStyle.hover.textColor = Color.white;

			this.btnCloseStyle = new GUIStyle(GUI.skin.button)
			{
				fontSize = 13,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.MiddleCenter
			};
			this.btnCloseStyle.normal.background = this.btnTexture;
			this.btnCloseStyle.normal.textColor = Color.white;
			this.btnCloseStyle.hover.background = this.btnHoverTexture;
			this.btnCloseStyle.hover.textColor = Color.white;
		}
	}

	private void OnGUI()
	{
		if (!EmotePoseHUD.isOpen)
		{
			return;
		}

		HeroPlayer heroplayer = fGameManager.Heroplayer;
		if (heroplayer == null || heroplayer.playerCommon == null)
		{
			return;
		}

		this.InitStyles();

		int heroId = heroplayer.playerCommon.m_nModelIndex;
		string heroName = "Herói " + heroId;
		if (fGameManager.mloadConfig != null && fGameManager.mloadConfig.AllHeroList != null && fGameManager.mloadConfig.AllHeroList.ContainsKey(heroId))
		{
			heroName = fGameManager.mloadConfig.AllHeroList[heroId].hname;
		}

		float windowWidth = 620f;
		float windowHeight = 440f;
		float posX = ((float)Screen.width - windowWidth) / 2f;
		float posY = ((float)Screen.height - windowHeight) / 2f;

		Rect winRect = new Rect(posX, posY, windowWidth, windowHeight);
		GUI.DrawTexture(winRect, this.bgTexture);

		GUI.Label(new Rect(posX, posY + 12f, windowWidth, 26f), "🎭 MENU DE EXPRESSÕES & POSES", this.titleStyle);
		GUI.Label(new Rect(posX, posY + 38f, windowWidth, 20f), "Herói Atual: " + heroName + " · Clique ou aperte [1..9] para executar", this.subtitleStyle);

		float colWidth = (windowWidth - 40f) / 2f;
		float col1X = posX + 15f;
		float col2X = posX + 25f + colWidth;
		float curY = posY + 65f;

		// --- COLUNA 1: EMOTES & DANÇAS ---
		GUI.Label(new Rect(col1X, curY, colWidth, 22f), "💃 EMOTES & DANÇAS", this.sectionHeaderStyle);
		float eY = curY + 26f;

		List<int> emoteParams = new List<int>();
		List<string> emoteNames = new List<string>();

		if (fGameManager.mloadConfig != null && fGameManager.mloadConfig.AllHeroUnlock != null && fGameManager.mloadConfig.AllHeroUnlock.ContainsKey(heroId))
		{
			HeroUnlockData hData = fGameManager.mloadConfig.AllHeroUnlock[heroId];
			if (hData != null && hData.szEmotionUnlock != null)
			{
				foreach (EmotionUnlock emo in hData.szEmotionUnlock.Values)
				{
					int p = 50;
					if (int.TryParse(emo.animParm, out p))
					{
						string cleanName = emo.sName;
						if (!string.IsNullOrEmpty(cleanName))
						{
							cleanName = cleanName.Replace("<size=20>", string.Empty).Replace("</size>", string.Empty).Replace("<size=22>", string.Empty).Replace("(Emoji)", string.Empty).Trim();
						}
						else
						{
							cleanName = "Emote " + emo.nId;
						}
						emoteParams.Add(p);
						emoteNames.Add(cleanName);
					}
				}
			}
		}

		if (emoteParams.Count == 0)
		{
			emoteParams.Add(50);
			emoteNames.Add("Provocação Principal");
			emoteParams.Add(201);
			emoteNames.Add("Dança");
		}

		int keyShortcut = 1;
		for (int i = 0; i < emoteParams.Count && i < 5; i++)
		{
			int animparm = emoteParams[i];
			string btnText = "[" + keyShortcut + "] " + emoteNames[i] + " (" + animparm + ")";
			if (GUI.Button(new Rect(col1X, eY, colWidth, 42f), btnText, this.btnEmoteStyle) || this.CheckNumericKey(keyShortcut))
			{
				this.ExecutePose(animparm);
			}
			eY += 48f;
			keyShortcut++;
		}

		// --- COLUNA 2: POSES SECRETAS DE VITÓRIA (300+) ---
		GUI.Label(new Rect(col2X, curY, colWidth, 22f), "🏆 POSES DE VITÓRIA & MVP", this.sectionHeaderStyle);
		float pY = curY + 26f;

		int[] secretPoses = new int[] { 300, 301, 302, 303, 304 };
		string[] poseTitles = new string[] {
			"Pose de Vitória #1",
			"Pose de Vitória #2",
			"Pose de Destaque MVP",
			"Pose de Vitrine Loja",
			"Pose de Combate #4"
		};

		for (int j = 0; j < secretPoses.Length; j++)
		{
			int animparm2 = secretPoses[j];
			string btnText2 = "[" + keyShortcut + "] " + poseTitles[j] + " (" + animparm2 + ")";
			if (GUI.Button(new Rect(col2X, pY, colWidth, 42f), btnText2, this.btnPoseStyle) || (keyShortcut <= 9 && this.CheckNumericKey(keyShortcut)))
			{
				this.ExecutePose(animparm2);
			}
			pY += 48f;
			keyShortcut++;
		}

		// --- BOTÕES INFERIORES ---
		float botY = posY + windowHeight - 50f;
		float botBtnWidth = (windowWidth - 40f) / 2f;

		if (GUI.Button(new Rect(col1X, botY, botBtnWidth, 38f), "[0] ⏹ CANCELAR / VOLTAR AO COMBATE", this.btnStopStyle) || Input.GetKeyDown(KeyCode.Alpha0))
		{
			this.ExecutePose(0);
		}

		if (GUI.Button(new Rect(col2X, botY, botBtnWidth, 38f), "❌ FECHAR MENU (ESC / P)", this.btnCloseStyle))
		{
			EmotePoseHUD.Close();
		}
	}

	private bool CheckNumericKey(int num)
	{
		switch (num)
		{
			case 1: return Input.GetKeyDown(KeyCode.Alpha1);
			case 2: return Input.GetKeyDown(KeyCode.Alpha2);
			case 3: return Input.GetKeyDown(KeyCode.Alpha3);
			case 4: return Input.GetKeyDown(KeyCode.Alpha4);
			case 5: return Input.GetKeyDown(KeyCode.Alpha5);
			case 6: return Input.GetKeyDown(KeyCode.Alpha6);
			case 7: return Input.GetKeyDown(KeyCode.Alpha7);
			case 8: return Input.GetKeyDown(KeyCode.Alpha8);
			case 9: return Input.GetKeyDown(KeyCode.Alpha9);
			default: return false;
		}
	}

	private void ExecutePose(int animparm)
	{
		ChatManager.GetInstance().SendPoseNetMsg(animparm);
		EmotePoseHUD.Close();
	}
}
