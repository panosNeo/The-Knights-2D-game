using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour
{

	public Sprite grassType3;
	public Sprite grassType2;
	//private SpriteRenderer checkpointSpriteRenderer;	//allazei ta sprites (full-empty)
	public bool checkpointReached; 	//true an ekane trigger apo ton player kai egine h allagh 

	// Use this for initialization
	void Start ()
	{
		//checkpointSpriteRenderer = GetComponent<SpriteRenderer> (); //init to sprite renderer
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			//checkpointSpriteRenderer.sprite = grassType2;	//allazei tis eikones 
			checkpointReached = true;
		}
	}



}

