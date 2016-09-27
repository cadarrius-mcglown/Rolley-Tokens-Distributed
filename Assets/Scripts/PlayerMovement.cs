using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	//Movespeed should be 80 for android and 70 for pc
	public float moveSpeed= 10f;
	private float maxSpeed = 2f;
	public bool isAndroid;

	public GameObject goal;
	public GameManager manager;


//Particles
	public GameObject enemyParticles;
	public GameObject tokenParticles;
	public GameObject fallParticles;
	public GameObject goalFX;
//Audio
	public AudioClip[] audioClip;


	private Vector3 input;

//Positions
	private Vector3 spawn;
	private Vector3 goalPosition;


// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.Android) 
		{
			rigidbody.drag = 1;
		} else {
			rigidbody.drag = 0;
		}

		goal.collider.isTrigger = false; //goal can not be used until tokens are collected
		goalPosition = goal.transform.position;
		spawn = transform.position;
		manager = manager.GetComponent<GameManager>();
	}


	void FixedUpdate () 
	{
		if (Application.platform == RuntimePlatform.Android) 
		{

			input = new Vector3 (Input.acceleration.x*3, 0, (Input.acceleration.y +.5f )*3);
			rigidbody.AddForce (input * moveSpeed);
			Debug.Log(input);
		}else {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			rigidbody.AddForce (input * (moveSpeed+12f));
		}

		if (transform.position.y < -2)
		{
			PlaySound (0);
			Die ();
		}
	}



	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy")
		{
			PlaySound (0);
			Die ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			PlaySound (3);
			Die ();
		}
		if (other.transform.tag == "Token")
		{
			PlaySound (1);
			Destroy(other.gameObject);
			Instantiate(tokenParticles, transform.position, Quaternion.Euler(270,0,0));
			manager.tokenCount += 1;

			//if all tokens are collected
			if(manager.tokenCount == manager.totalTokenCount)
			{
				goal.collider.isTrigger = true; //Goal can be entered now
				PlaySound (4); //flame/ rocket sound
				Instantiate(goalFX, goalPosition, Quaternion.Euler(270,0,0));
			}

		}
		if (other.transform.tag == "Goal")
		{
			//Freeze the player from moving because the level has been won!
			rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			PlaySound (2);
			manager.CompleteLevel();
		}
	}

	void PlaySound(int clip)
	{
		audio.clip = audioClip [clip];
			audio.Play ();
		
	}

	void Die()
	{
		if (manager.lives == 1)
		{
			Debug.Log ("Lost!");
			rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			manager.LoseLevel();
			//Application.LoadLevel(0);
		}else 
		{
			Debug.Log (manager.lives);
			manager.lives -= 1;
		}
		//Handheld.Vibrate ();
		Instantiate(enemyParticles, transform.position, Quaternion.Euler(270,0,0));
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		transform.position = spawn;
	}

	
}
