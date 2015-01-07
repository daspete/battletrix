using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class UISettings {

	public GameObject UI;
	public GameObject preview;

	public Text levelValue;
	public Text linesValue;
	public Text pointsValue;

	public Text bonusHeadline;
	public Text bonusText;

	public GameObject bonusIndicator;

	public GameObject audioOn;
	public GameObject audioOff;


	public void initialize(bool _muted){
		toggleMute(_muted);
	}

	public void showBonus(Hashtable settings){
		bonusHeadline.text=(settings["headline"]).ToString();
		bonusText.text=(settings["text"]).ToString();

		iTween.ScaleTo(bonusIndicator, iTween.Hash(
			"scale", new Vector3(1,1,1),
			"time", 0.4f,
			"easetype", "easeInSine"
		));

		iTween.ScaleTo(bonusIndicator, iTween.Hash(
			"scale", Vector3.zero,
			"time", 0.4f,
			"easetype", "easeOutSine",
			"delay", 0.9f
		));
	}

	public void setPoints(int val){
		pointsValue.text=val.ToString();
	}

	public void setLevel(int level){
		levelValue.text=level.ToString();
	}

	public void setLines(int lines){
		linesValue.text=lines.ToString();
	}

	public void toggleMute(bool mute){
		if(mute){
			audioOn.SetActive(true);
			audioOff.SetActive(false);
		}else{
			audioOn.SetActive(false);
			audioOff.SetActive(true);
		}
	}

}
