using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class UISettings {

	public GameObject UI;

	public Text levelValue;
	public Text linesValue;
	public Text pointsValue;


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
