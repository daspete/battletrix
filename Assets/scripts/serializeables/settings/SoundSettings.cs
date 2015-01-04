using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundSettings {

	public AudioClip drop;
	public AudioClip line;

	public AudioClip gameMusic;

	public AudioSource effects;
	public AudioSource music;

	public void playMusic(){
		music.clip=gameMusic;
		music.Play();
	}

	public void playDrop(){
		effects.PlayOneShot(drop, 1f);
	}

	public void playLine(){
		effects.PlayOneShot(line, 1f);
	}
}
