using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundSettings {

	public AudioClip drop;
	public AudioClip line;
	public AudioClip gameOver;

	public AudioClip gameMusic;

	public AudioSource effects;
	public AudioSource music;

	public bool muted=false;

	public float overallVolume=1.0f;

	float volume=1.0f;

	public void initialize(bool _muted){
		Application.ExternalCall("console.log", _muted);
		if(_muted){
			Application.ExternalCall("console.log", "juhu");
		}else{
			Application.ExternalCall("console.log", "oh no");
		}
		
		volume=overallVolume;

		if(_muted){
			muted=true;
			volume=0f;
		}

		music.volume=volume;
		effects.volume=volume;
	}

	public void playMusic(){
		music.clip=gameMusic;
		music.Play();
	}

	public void toggleVolume(){
		if(muted){
			muted=false;

			fadeInMusic();
			volume=overallVolume;
		}else{
			muted=true;

			fadeOutMusic();
			volume=0f;
		}

		Application.ExternalCall("window.application.setSessionMute", (muted).ToString());
	}

	public void fadeOutMusic(){
		iTween.AudioTo(GameController.instance.gameObject, iTween.Hash(
			"audiosource", music,
			"volume", 0,
			"time", 0.7f,
			"easetype", "easeOutSine"
		));
	}

	public void fadeInMusic(){
		iTween.AudioTo(GameController.instance.gameObject, iTween.Hash(
			"audiosource", music,
			"volume", overallVolume,
			"time", 0.7f,
			"easetype", "easeOutSine"
		));
	}

	public void playDrop(){
		effects.PlayOneShot(drop, volume);
	}

	public void playLine(){
		effects.PlayOneShot(line, volume*0.4f);
	}

	public void playGameOver(){
		effects.PlayOneShot(gameOver, volume);
	}
}
