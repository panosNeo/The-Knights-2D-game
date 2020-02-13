using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {

	private Enemy enemy;


	public void Execute(){
		if (enemy.InMeleeRange) {
			enemy.ChangeState (new MeleeState());
			enemy.MyAnimator.SetFloat ("Speed",0);
		}
		else if (enemy.Target != null) {
			enemy.Move ();
		}
		else
		{
			enemy.ChangeState (new IdleState());
		}
	}

	public void Enter(Enemy enemy){
		this.enemy = enemy;
	}

	public void Exit(){

	}
	public void OnTriggerEnter(Collider2D other){
		
	}
}
