using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour
{

	[SerializeField]				//gia na valw metavlhth apo ton editor
	protected float movementSpeed;	//speed tou player

	[SerializeField]
	protected int health;	//health xaraktirwn 

	[SerializeField]
	private BoxCollider2D weaponCollider;

	public BoxCollider2D WeaponCollider
	{
		get
		{
			return weaponCollider;
		}
	}

	public abstract bool IsDead { get;}

	protected bool facingRight;		//gia na kanei flipping o player

	protected bool attack;			//pote tha kanei to attack

	public bool Attack{
		get 
		{
			return attack;
		}
		set { 
			this.attack = value;
		}
	}

	public bool TakingDamage { get; set;}

	public Animator MyAnimator { get; private set; }  


	// Use this for initialization
	public virtual void Start ()
	{
		facingRight = true;			//arxikopoihsh

		MyAnimator = GetComponent<Animator> (); //arxikopoihsh
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public abstract IEnumerator TakeDamage();

	public void ChangeDirection(){
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
	}
		

	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Weapon" || other.tag == "EnemyWeapon")
		{
			StartCoroutine (TakeDamage());
		}
	}

	public void MeleeAttack(){
		weaponCollider.enabled = true;
	}







}

