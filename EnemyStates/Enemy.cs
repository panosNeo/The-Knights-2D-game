using UnityEngine;
using System.Collections;
using System;

public class Enemy : Character
{
	private IEnemyState currentState;

	public GameObject Target { get; set;}

	[SerializeField]
	private float meleeRange; //metavliti gia to meleerange wste na mporei na kanei attack

	public bool InMeleeRange //elegxei an exei target mesa sto melee range gia na kanei attack 
	{
		get
		{
			if(Target != null){
				return Vector2.Distance (transform.position,Target.transform.position) <= meleeRange;
			}
			return false;
		}
	}

	[SerializeField]
	private float throwRange;

	public bool InThrowRange //elegxei an exei target mesa sto melee range gia na kanei attack 
	{
		get
		{
			if(Target != null){
				return Vector2.Distance (transform.position,Target.transform.position) <= throwRange;
			}
			return false;
		}
	}
	public override bool IsDead
	{
		get
		{
			return health <= 0;
		}
	}

	private LevelManager lvlManager;	//antikeimeno lvlmanager gia count ta dead orcs


	// Use this for initialization
	public override void Start ()
	{
		base.Start (); 					//kalese th start tou character
		ChangeState(new IdleState());	//ftiaxe new idle state me thn enarxh tou game
		lvlManager = FindObjectOfType<LevelManager>();	//init to object
	}

	// Update is called once per frame
	void Update ()
	{
		if(!IsDead){
			if(!TakingDamage){
				currentState.Execute ();	//kane execute to state
			}

			LookAtTarget ();	// gia na gurnaei pros to target tou 
		}

	}

	private void LookAtTarget(){
		if(Target != null){
			float xDir = Target.transform.position.x - transform.position.x;

			if(xDir < 0 && facingRight || xDir > 0 && !facingRight){
				ChangeDirection ();
			}
		}
	}

	//gia na allazw ta states (idle-melee-patrol)
	public void ChangeState(IEnemyState newState){
		if(currentState != null)	//an currentstate yparxei
		{
			currentState.Exit ();	//tote kleiste to currentState
		}
		currentState = newState;	//ftiaxe new state
		currentState.Enter (this);	// to new state einai value
	}

	public void Move(){
		if (!attack) {
			MyAnimator.SetFloat ("Speed",1);
			transform.Translate (GetDirection() * (movementSpeed * Time.deltaTime));
		}
	}

	public Vector2 GetDirection(){
		return facingRight ? Vector2.right : Vector2.left;
	}

	public override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
		currentState.OnTriggerEnter (other);
		/*
		if(other.tag == "Weapon" )
		{
			StartCoroutine (TakeDamage());
		}*/
	}


	public override IEnumerator TakeDamage(){
		health -= 10;
		if (!IsDead) {
			MyAnimator.SetTrigger ("Damage");
		}
		else 
		{
			MyAnimator.SetTrigger ("Death");
			if(health == 0)
				lvlManager.CountOrcs ();	//kalese ti methodo gia count
			yield return new WaitForSeconds (4);
			gameObject.SetActive (false);
			Destroy(gameObject);
			yield return null;
		}
	}


}

