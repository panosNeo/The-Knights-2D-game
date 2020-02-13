using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public GameObject player;  	//gia na pw poion player tha akolouthei h kamera

	public float offset = 5f;	//gia th diafora ths kameras analoga me thn kateu8hnsh tou player
	private Vector3 playerPosition;	//gia na parw tis suntetagmenes tou player
	public float offsetSmoothing = 2f;	//poso xrono na kanei h kamera gia na gurisei



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
	
		playerPosition = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z); //gia na oristikopoihsw tis suntetagmenes tou player
		if (player.transform.localScale.x > 0f) {
			//an einai gurismenos pros ta dexia tote gurise kamera dexia
			playerPosition = new Vector3 (playerPosition.x + offset, playerPosition.y, playerPosition.z);
		}
		else {
			//an einai gurmismenos pros ta aristera na paei pros ta pisw tote gurise kamera aristera
			playerPosition = new Vector3 (playerPosition.x - offset, playerPosition.y, playerPosition.z);
		}

		transform.position = Vector3.Lerp (transform.position,playerPosition, offsetSmoothing * Time.deltaTime);
		//transform.position = playerPosition;
	}











}
