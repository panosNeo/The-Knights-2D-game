using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

	public float respawnDelay; //metavlhth gia delay 
	private Player gamePlayer;	//antikeimeno klashs playerController

	//coins score, posa coins exoume 
	public int coins = 0;
	//orc score, posa orc skotose
	public int orcs = 0;

	//coin text score sto canva
	public Text coinText;
	//coin text sto game result panel
	public Text menuCoinText;
	//orc text score sto canva
	public Text orcText;
	//orc text sto game result panel
	public Text menuOrcText;


	// Use this for initialization
	void Start ()
	{
		gamePlayer = FindObjectOfType<Player> ();	//initialize to antikeimeno
		menuCoinText.text = ": " + coins + " / 450"; //ananewsh to score sto game lost panel
		coinText.text = ": " + coins; //ananewsh to score text
		menuOrcText.text = ": " + orcs;		//ananewsh to orc score sto result panel 
		orcText.text = ": " + orcs;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	public void Respawn(){
		StartCoroutine ("RespawnCoroutine");
	}

	public IEnumerator RespawnCoroutine(){
		gamePlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (respawnDelay);	//stelnei ta deuterolepta gia to delay
		gamePlayer.transform.position = gamePlayer.respawnPoint;
		gamePlayer.gameObject.SetActive (true);
	}

	public void CountOrcs(){
		orcs++;
		orcText.text = ": " + orcs;
		menuOrcText.text = ": " + orcs;
	}


	public void AddCoins(int numberOfCoins){
		coins += numberOfCoins;
		coinText.text = ": " + coins; //ananewsh to score text
		menuCoinText.text = ": " + coins + " / 450"; //ananewsh to score sto game lost panel
	}
}

