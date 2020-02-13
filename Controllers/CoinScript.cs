using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {


	public int coinValue; //to score me ta coins 
	private LevelManager lvlManager;

	// Use this for initialization
	void Start () {
		lvlManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			FindObjectOfType<AudioManager> ().Play("Coin");
			Destroy (gameObject);
			lvlManager.AddCoins (coinValue);
		}
	}


}
