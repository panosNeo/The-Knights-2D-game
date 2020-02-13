using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {


	public Sound[] sounds;

	public static AudioManager instance;


	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			//s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	void Start()
	{
		Play ("Intro");
	}

	public void Play(string name){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if(s == null){
			return;
		}
		s.source.Play ();
	}

	public void StopSound(string name){
		Sound s = Array.Find (sounds,sound => sound.name == name);
		s.source.Stop ();
	}

	public void StopSounds(){
		foreach(Sound s in sounds){
			if (s.name == "Water" || s.name == "Coin" || s.name == "Treasure")
				s.source.volume = 0;
		}
	}

	public void StartSounds(){
		foreach(Sound s in sounds){
			if (s.name == "Water" || s.name == "Coin" || s.name == "Treasure")
				s.source.volume = 1;
		}
	}

	public void SetMusicVolume(string name,float value){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		s.source.volume = value;
	}

	public void SetSoundsVolume(float value){
		foreach(Sound s in sounds){
			if (s.name == "Water" || s.name == "Coin" || s.name == "Treasure")
				s.source.volume = value;
		}
	}
}
