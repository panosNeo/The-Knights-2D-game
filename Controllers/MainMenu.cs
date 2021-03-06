using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Slider musicSlider;
	public Slider soundSlider;

	void Update(){
		FindObjectOfType<AudioManager> ().SetMusicVolume("Intro",musicSlider.value);
		FindObjectOfType<AudioManager> ().SetSoundsVolume(soundSlider.value);
	}


	public void PlayGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex +1);
		FindObjectOfType<AudioManager> ().Play("GamePlay");
		FindObjectOfType<AudioManager> ().StopSound("Intro");
	}

	public void QuitGame(){
		//Debug.Log ("QUIT!");
		Application.Quit ();
	}




}

