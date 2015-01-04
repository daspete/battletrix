using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class UISettings {

	public GameObject UI;

	public Text levelValue;
	public Text linesValue;
	public Text pointsValue;

	public Text bonusHeadline;
	public Text bonusText;

	public GameObject bonusIndicator;


	public void initialize(){
		iTween.FadeTo(bonusIndicator, iTween.Hash(
			"alpha", 0,
			"time", 0.001
		));

		iTween.ScaleTo(bonusIndicator, iTween.Hash(
			"scale", Vector3.zero,
			"time", 0.001
		));
	}

	public void showBonus(Hashtable settings){
		bonusHeadline.text=(settings["headline"]).ToString();
		bonusText.text=(settings["text"]).ToString();

		iTween.FadeTo(bonusIndicator, iTween.Hash(
			"alpha", 1,
			"time", 0.3f
		));

		iTween.ScaleTo(bonusIndicator, iTween.Hash(
			"scale", new Vector3(1,1,1),
			"time", 0.3f,
			"easetype", "spring"
		));

		iTween.FadeTo(bonusIndicator, iTween.Hash(
			"alpha", 0,
			"time", 0.3f,
			"delay", 1f
		));

		iTween.ScaleTo(bonusIndicator, iTween.Hash(
			"scale", Vector3.zero,
			"time", 0.3f,
			"easetype", "spring",
			"delay", 1f
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

}
