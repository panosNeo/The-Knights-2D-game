using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState 
{
	private float attackTimer;
	private float attackCoolDown = 3;
	private bool canAttack = true;

	private Enemy enemy;


	public void Execute(){
		Attack (); //start to attack
		if(enemy.InThrowRange && !enemy.InMeleeRange){
			enemy.ChangeState (new RangedState());
			enemy.MyAnimator.SetFloat ("Speed",1);
		}
		else if(enemy.Target == null){
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

	private void Attack(){
		attackTimer += Time.deltaTime;
		if(attackTimer >= attackCoolDown){
			canAttack = true;
			attackTimer = 0;
		}
		if(canAttack){
			canAttack = false;
			enemy.MyAnimator.SetTrigger ("Attack");
		}
	}







}
