using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState 
{
	private Enemy enemy;		//enemy object
	private float idleTimer;	//gia to patrol state
	private float idleDuration = 5; //gia na allaxeu state

	public void Execute(){
		Idle ();	//kalese methodo

		if(enemy.Target != null){
			enemy.ChangeState (new PatrolState());
		}
	}

	public void Enter(Enemy enemy){
		this.enemy = enemy;
	}

	public void Exit(){

	}

	public void OnTriggerEnter(Collider2D other){

	}

	private void Idle(){
		enemy.MyAnimator.SetFloat ("Speed",0);

		idleTimer += Time.deltaTime;

		if(idleTimer >= idleDuration){
			enemy.ChangeState (new PatrolState());
		}
	}

















}
