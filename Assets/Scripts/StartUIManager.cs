using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartUIManager : MonoBehaviour {

	public Text textLevel;
	public GameObject targetCanvas;
	public GameObject menuContent;
	public GameObject LevelStats;
	public Text toggleText;

	private bool isVisible = false;
	private AudioSource clickAudio;

	void Start () {
		toggleText.text = isVisible ? "Close" : "Levels";
		clickAudio = GetComponent<AudioSource>();
		targetCanvas.SetActive(isVisible);
		FillMenu();
		FillRecords();


		textLevel.text = "Level " + GameManager.currentLevel;
		GameManager.remainingTime = GameManager.levelInfo[ GameManager.currentLevel]["time"];
		GameManager.ResetCurrentStats();
	}
	
	void Update () {
	
	}

	public void ClickPlay() {
		SceneManager.LoadScene("Scenes/Level"+GameManager.currentLevel);
		clickAudio.Play();
	}

	public void ClickLevels(){
		isVisible = !isVisible;
		targetCanvas.SetActive(isVisible);
		toggleText.text = isVisible ? "Close" : "Levels"; 
		clickAudio.Play();
	}

	private void ClickLevel(int level){
		Debug.Log("going to Level " + level); 
		GameManager.currentLevel = level; 
		SceneManager.LoadScene("Scenes/Start");
		clickAudio.Play();
	}

	private void FillMenu(){
		for(int i=1; i<=GameManager.levelInfo.Count; i++){
			if(GameManager.levelInfo[i]["unlocked"] == 1){
				LevelUI(i);
			}
		}
	}

	private void LevelUI(int level){
		int topPos = level * -25 + 10;

		GameObject textObj = new GameObject("Text" + level, typeof(Text));
		textObj.transform.SetParent(menuContent.transform, false);
		Text text = textObj.GetComponent<Text>();
		text.text = "Level " + level;
		text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
		text.fontSize = 16;
		text.alignment = TextAnchor.MiddleLeft;
		if(level == GameManager.currentLevel){
			text.color = Color.red;
		}else{
			text.color = Color.black;
		}
		RectTransform rectT = text.GetComponent<RectTransform>();
		rectT.anchorMin = new Vector2(0,1);
		rectT.anchorMax = new Vector2(0,1);
		rectT.pivot = new Vector2(0,1);
		rectT.sizeDelta = new Vector2(90, 20);
		rectT.anchoredPosition = new Vector2(20, topPos);

		GameObject btn = new GameObject("Button" + level, typeof(Button), typeof(Image));
		btn.transform.SetParent(menuContent.transform, false);
		if(level == GameManager.currentLevel){
			btn.GetComponent<Image>().color =  new Color(0.39f, 0f, 0f);;
		}else{
			btn.GetComponent<Image>().color =  new Color(0f, 0.39f, 0f);;
		}
		RectTransform rectB = btn.GetComponent<RectTransform>();
		rectB.anchorMin = new Vector2(0,1);
		rectB.anchorMax = new Vector2(0,1);
		rectB.pivot = new Vector2(0,1);
		rectB.sizeDelta = new Vector2(20, 20);
		rectB.anchoredPosition = new Vector2(100, topPos);

		GameObject btnTextObj = new GameObject("BtnText" + level, typeof(Text));
		btnTextObj.transform.SetParent(btn.transform, false);
		Text btnText = btnTextObj.GetComponent<Text>();
		btnText.text = "▶";
		btnText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
		btnText.fontSize = 16;
		btnText.alignment = TextAnchor.MiddleCenter;
		RectTransform btnTextrect = btnText.GetComponent<RectTransform>();
		btnTextrect.sizeDelta = new Vector2(20, 20);

		if(level != GameManager.currentLevel){
			Button btnB = btn.GetComponent<Button>();
			btnB.onClick.AddListener(() => ClickLevel(level));
		}

		if(level == GameManager.levelInfo.Count){
			RectTransform contRect = menuContent.GetComponent<RectTransform>();
			contRect.sizeDelta = new Vector2(contRect.sizeDelta.x, -1 * topPos + 30);
		}
	}

	private void FillRecords(){
		Dictionary<string, int> curLevelInfo = GameManager.levelInfo[GameManager.currentLevel];
		if(curLevelInfo["jumpsR"] == -1) {
			LevelStats.SetActive(false);
		}else{
			LevelStats.transform.Find("Numbers/recJumps").GetComponent<Text>().text = ""+ curLevelInfo["jumpsR"];
			LevelStats.transform.Find("Numbers/recDeaths").GetComponent<Text>().text = ""+ curLevelInfo["deathsR"];
			LevelStats.transform.Find("Numbers/recTime").GetComponent<Text>().text = ""+ curLevelInfo["timeR"];
		}
	}
}
