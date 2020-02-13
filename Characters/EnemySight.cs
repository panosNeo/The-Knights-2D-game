using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	[SerializeField]
	private Enemy enemy;

	//an mpei sto pedio tou collider tote kane target ton player
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.Target = other.gameObject;
		}
	}

	//an fugei apo to pedio tote kane to target null
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.Target = null;
		}
	}









}
