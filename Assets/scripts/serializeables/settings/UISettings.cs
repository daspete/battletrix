using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class UISettings {

	public GameObject UI;

	public Text pointsValue;
	public Text levelValue;

	public void addPoints(int val){
		pointsValue.text=(int.Parse(pointsValue.text)+val).ToString();
	}

	public void setLevel(int level){
		levelValue.text=level.ToString();
	}

}
