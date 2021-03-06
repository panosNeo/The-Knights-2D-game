using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMenuController : MonoBehaviour
{
	//sprites me tis eikones gia to life
	public Sprite lifeImage;
	public Sprite lostLifeImage;
	public Sprite youLose;		//lose eikona
	public Sprite youWin;		//win eikona
	public Sprite oneStar;		//
	public Sprite twoStars;		//eikones me ta asteria sto kentro
	public Sprite threeStars;	//
	public Sprite zeroStars;	//
	//sprites gia ta buttons
	public Sprite pauseIcon;
	public Sprite continueIcon;
	public Sprite musicIcon;
	public Sprite stopMusicIcon;
	public Sprite soundIcon;
	public Sprite stopSoundIcon;


	//UI images me tis zwes tou player
	public Image lifeImage1, lifeImage2, lifeImage3;
	//UI win-lose sto result panel
	public Image headerImage;
	public Image starImage;	//gia ta asteria sto win
	public Text menuLifeText; //to textbox gia tis zwes sto result menou

	public Slider musicSlider;
	public Slider soundSlider;

	//panels me ta menu
	public GameObject resultPanel;
	//buttons
	public GameObject pauseButton;
	public GameObject musicButton;
	public GameObject soundButton;

	//game time pause
	private float timeInStart;
	//counter gia tis zwes
	private int lifes = 3;

	//other classes objects
	private LevelManager lvlManager;


	void Start(){
		timeInStart = Time.timeScale;

		lvlManager = FindObjectOfType<LevelManager> (); //init to object klashs
		menuLifeText.text = ": " + lifes + " / 3";
	}

	void Update(){
		FindObjectOfType<AudioManager> ().SetMusicVolume("GamePlay",musicSlider.value);
		FindObjectOfType<AudioManager> ().SetSoundsVolume(soundSlider.value);
	}

	//gia to pause button
	private void PauseGame(){
		if (pauseButton.GetComponent<Image> ().sprite == pauseIcon) {
			Time.timeScale = 0f;
			pauseButton.GetComponent<Image> ().sprite = continueIcon;
			FindObjectOfType<AudioManager> ().StopSound("GamePlay");
		} 
		else {
			Time.timeScale = timeInStart;
			pauseButton.GetComponent<Image> ().sprite = pauseIcon;
			FindObjectOfType<AudioManager> ().Play("GamePlay");
		}
	}

	//gia na elegxei th mousiki
	public void SetMusic(){
		if (musicButton.GetComponent<Image> ().sprite == musicIcon) {
			musicButton.GetComponent<Image> ().sprite = stopMusicIcon;
			FindObjectOfType<AudioManager> ().StopSound ("GamePlay");
			musicSlider.value = 0;
		} else {
			musicButton.GetComponent<Image> ().sprite = musicIcon;
			FindObjectOfType<AudioManager> ().Play ("GamePlay");
			musicSlider.value = 0.6f;
		}
	}

	//gia na elegxei ta sounds
	public void SetSounds(){
		if (soundButton.GetComponent<Image> ().sprite == soundIcon) {
			soundButton.GetComponent<Image> ().sprite = stopSoundIcon;
			FindObjectOfType<AudioManager> ().StopSounds ();
			soundSlider.value = 0;
		} else {
			soundButton.GetComponent<Image> ().sprite = soundIcon;
			FindObjectOfType<AudioManager> ().StartSounds ();
			soundSlider.value = 1;
		}
	}

	//epistrofh sto vasiko menou
	public void ReturnHome(){
		Time.timeScale = timeInStart; 			//kane unpause to game
		SceneManager.LoadScene ("Menu");		//fortose th skhnh menu
	}

	//xana kane to level
	public void RestartLevel(){
		Time.timeScale = timeInStart; 			//kane unpause to game
		SceneManager.LoadScene ("LevelOne");	//fortose thn skhnh levelone
		ResetSounds();
	}

	public void SetLifeImages(){	//methodos gia na elegxei tis zwes pou xanei o player
		if (lifeImage1.sprite == lifeImage) { //prwth zwh
			lifeImage1.sprite = lostLifeImage;
			--lifes;
		}else if (lifeImage1.sprite == lostLifeImage && lifeImage2.sprite == lifeImage){	//deuterh zwh
			lifeImage2.sprite = lostLifeImage;
			--lifes;
		}else if(lifeImage2.sprite == lostLifeImage && lifeImage3.sprite == lifeImage){		//trith zwh
			lifeImage3.sprite = lostLifeImage;
			--lifes;

			FindObjectOfType<AudioManager> ().Play("Lose");
			FindObjectOfType<AudioManager> ().StopSound("GamePlay");

			//settings sto Lose
			headerImage.sprite = youLose;
			starImage.sprite = zeroStars;
			menuLifeText.text = ": " + lifes + " / 3";


			resultPanel.SetActive (true);
			Time.timeScale = 0f;
		}
	}

	public void ShowWinPanel(){
		//show ta coins
		//show stars
		//show kills
		//win music
		headerImage.sprite = youWin;
		menuLifeText.text = ": " + lifes + " / 3";

		int coins = lvlManager.coins;

		if (coins == 450 && lifes == 3)
			starImage.sprite = threeStars;
		else if (coins == 450 && lifes < 3)
			starImage.sprite = twoStars;
		else if (coins < 450 && lifes == 3)
			starImage.sprite = twoStars;
		else
			starImage.sprite = oneStar;


		resultPanel.SetActive (true);
		Time.timeScale = 0f;
	}


	public void QuitGame(){
		//Debug.Log ("QUIT!");
		Application.Quit ();
	}

	private void ResetSounds(){
		FindObjectOfType<AudioManager> ().Play("GamePlay");
		FindObjectOfType<AudioManager> ().StopSound("Win");
		FindObjectOfType<AudioManager> ().StopSound("Lose");
	}

}

