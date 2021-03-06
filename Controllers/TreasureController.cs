using UnityEngine;
using System.Collections;

public class TreasureController : MonoBehaviour
{
	public Sprite fullChest;
	public Sprite emptyChest;
	private SpriteRenderer checkpointSpriteRenderer;	//allazei ta sprites (full-empty)
	public bool checkpointReached; 	//true an ekane trigger apo ton player kai egine h allagh 

	//levelmanager gia to score
	private LevelManager lvlManager;
	public int chestCoins;


	// Use this for initialization
	void Start ()
	{
		checkpointSpriteRenderer = GetComponent<SpriteRenderer> (); //arxikopoihsh to sprite renderer
		lvlManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !checkpointReached) {
			FindObjectOfType<AudioManager> ().Play("Treasure");
			checkpointSpriteRenderer.sprite = emptyChest;	//allagh eikonas apo gemath se adeia
			checkpointReached = true;		//true oti egine h allagh 

			lvlManager.AddCoins (chestCoins); //kane add sto score to poso twn coins ana chest
		}
	}

}

