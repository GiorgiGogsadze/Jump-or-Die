using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EndUIManager : MonoBehaviour {

		public Text curJumps;
		public Text curDeaths;
		public Text curTime;
		public Text recJumps;
		public Text recDeaths;
		public Text recTime;

		public Text isRecord;

		public AudioSource VictoryAudio;

	void Start () {
		VictoryAudio.Play();

		Dictionary<string, int> curLevelInfo = GameManager.levelInfo[GameManager.currentLevel];

		curJumps.text = "" + GameManager.numberJumps;
		curDeaths.text =  "" + GameManager.numberDeaths + "";

		int timeUsed = curLevelInfo["time"] - Mathf.CeilToInt(GameManager.remainingTime);
		curTime.text = "" + timeUsed;

		if(curLevelInfo["jumpsR"] == -1) {
			curLevelInfo["jumpsR"] = GameManager.numberJumps;
			curLevelInfo["deathsR"] = GameManager.numberDeaths;
			curLevelInfo["timeR"] = timeUsed;
		}else{
			bool checkRecord = false;

			if(curLevelInfo["jumpsR"] > GameManager.numberJumps){
				curLevelInfo["jumpsR"] = GameManager.numberJumps;
				checkRecord = true;
			}
			if(curLevelInfo["deathsR"] > GameManager.numberDeaths){
				curLevelInfo["deathsR"] = GameManager.numberDeaths;
				checkRecord = true;
			}
			if(curLevelInfo["timeR"] > timeUsed){
				curLevelInfo["timeR"] = timeUsed;
				checkRecord = true;
			}
			
			if(!checkRecord){
				isRecord.text = "";
			}
		}

		recJumps.text = "" + curLevelInfo["jumpsR"];
		recDeaths.text = "" + curLevelInfo["deathsR"];
		recTime.text = "" + curLevelInfo["timeR"];

		if(GameManager.levelInfo.Count > GameManager.currentLevel){
			GameManager.currentLevel++;
			GameManager.levelInfo[GameManager.currentLevel]["unlocked"] = 1;
    }
	}
	
	void Update () {
	
	}

	public void ClickBack() {
		SceneManager.LoadScene("Scenes/Start");
	}
}
