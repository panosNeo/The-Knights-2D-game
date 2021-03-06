using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Player : Character
{
	
	private Rigidbody2D myRigidbody;	//to rigidbody tou player

	private float horizontal;		//get axis

	[SerializeField]				//gia metavlhth apo ton editor
	private Transform[] groundPoints;//gia na elegxo an akoumpaei edafos

	[SerializeField]
	private float groundRadius;	

	[SerializeField]
	private LayerMask whatIsGround;	//ti exei apo katw

	private bool isGrounded;	//pataei edafos;
	private bool jump;			//true gia na phdh3ei

	[SerializeField]
	private bool airControl;	//elegxos gia otan einai ston aera

	[SerializeField]
	private float jumpForce;	//poso psila na phdaei

	private static Player instance; 	//instance tou player

	public static Player Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<Player> ();
			}

			return instance;
		}
	}

	public override bool IsDead 
	{
		get
		{
			return health <= 0;
		}
	}

	public Vector3 respawnPoint;
	private LevelManager gameLevelManager;
	private GameMenuController gameMenuController;


	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
		myRigidbody = GetComponent<Rigidbody2D> ();		//arxikopoihsh 

		respawnPoint = transform.position; 								//pare thn arxikh thesh tou player
		gameLevelManager = FindObjectOfType<LevelManager>(); 			//init to antikeimeno ths klashs gamelevelmanager
		gameMenuController = FindObjectOfType<GameMenuController>();	//init to antikeimeno ths klashs gameMenuController
	}

	// Update is called once per frame
	void Update(){
		if(!TakingDamage && !IsDead){
			HandleInput ();	//kalei th methodo gia na elegxei an o user pathsei to key gia to attack
		}
	}


	// Update is called once per frame
	void FixedUpdate ()
	{
		if(!TakingDamage && !IsDead){
			horizontal = Input.GetAxis ("Horizontal");	//get axis sth metavlhth horizontal

			isGrounded = IsGrounded ();					//elegxos gia to an pataei edafos

			HandleMovement (horizontal);				//kalese th methodo HandleMovement gia na dwseis timi axis
			Flip (horizontal);							//kalese Flip() gia na dwseis timh axis wste na kanei flipping o player
			HandleAttacks();							//kalei th methodo gia na ektelesei to attack animation
			HandleLayer();								//gia na elegxei ta layers me ta animations

			ResetValues();								//kane reset oles tis times
		}
	}

	private void HandleMovement(float horizontal){
		//kanei set true to land gia na ginetai to jump animation
		if(myRigidbody.velocity.y < 0){
			MyAnimator.SetBool ("Land",true);
		}

		//elegxos gia na mporei na kanei jump
		if(isGrounded && jump){
			isGrounded = false;	//vale false giati ekane jump
			myRigidbody.AddForce(new Vector2(0,jumpForce)); //dinei thn timh tou jumpForce gia timi y wste na phdaei o player
			MyAnimator.SetTrigger("Jump");
		}


		if(!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
			myRigidbody.velocity = new Vector2 (horizontal * movementSpeed,myRigidbody.velocity.y);
		}
		MyAnimator.SetFloat ("Speed",Mathf.Abs(horizontal));
	}

	//pote na kanei attack
	private void HandleAttacks(){
		if (attack && isGrounded && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
			MyAnimator.SetTrigger ("Attack");		//activated trigger
			myRigidbody.velocity = Vector2.zero;	// gia na stamataei na trexei otan kanei attack
		}
	}
	private void HandleInput(){
		if(Input.GetKeyDown(KeyCode.Space)){
			jump = true;
		}
		if(Input.GetKey("w")){
			attack = true;
		}
	}

	private void Flip(float horizontal){
		if (horizontal > 0f && !facingRight || horizontal < 0f && facingRight) {
			
			ChangeDirection ();
		}
	}

	private bool IsGrounded(){
		if (myRigidbody.velocity.y <= 0) {
			foreach(Transform point in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position,groundRadius,whatIsGround);

				for(int i=0; i<colliders.Length; i++)
				{
					if (colliders [i].gameObject != gameObject) {
						MyAnimator.ResetTrigger ("Jump");	//kanei reset ton trigger
						MyAnimator.SetBool ("Land",false);	//kane reset th land se false
						return true;
					}
				}
			}
		}
		return false;
	}

	private void ResetValues(){	//kane reset oles tis metavlhtes pou xreiazonte 
		attack = false;
		jump = false;
	}

	private void HandleLayer(){
		if (!isGrounded) {
			MyAnimator.SetLayerWeight (1, 1);
		}
		else {
			MyAnimator.SetLayerWeight (1,0);
		}
	}


	public override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
	
		/*
		if(other.tag == "EnemyWeapon" )
		{
			StartCoroutine (TakeDamage());
		}*/

		if(other.tag == "FallDetector"){
			//ti tha ginei otan o player pathsei to fallDetector
			gameLevelManager.Respawn();	//afou valw delay

			gameMenuController.SetLifeImages ();	//gia kathe zwh pou xanei
		}
		if(other.tag == "Checkpoint"){
			//tha parei to teleutaio checkpoint gia na ginei to respawn
			respawnPoint = other.transform.position;
		}
		if(other.tag == "WinSign"){
			gameMenuController.ShowWinPanel ();
			FindObjectOfType<AudioManager> ().Play("Win");
			FindObjectOfType<AudioManager> ().StopSound("GamePlay");
		}
		if(other.tag == "Water"){
			FindObjectOfType<AudioManager> ().Play("Water");
		}
	}

	public override IEnumerator TakeDamage(){
		health -= 10;
		if (!IsDead) {
			MyAnimator.SetTrigger ("Damage");
		}
		else 
		{
			MyAnimator.SetTrigger ("Death");
			if (health == 0) {
				gameMenuController.SetLifeImages ();
				StartCoroutine ("Respawn");
			}
			health = 50;
		}
		yield return null;
	}

	private IEnumerator Respawn(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
		transform.position = respawnPoint;
		gameObject.SetActive (true);
	}




}

