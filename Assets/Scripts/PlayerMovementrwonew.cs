using UnityEngine;
using System.Collections;

public class PlayerMovementtrwonew : MonoBehaviour {

	private GameObject moveJoy;



	public bool isAndroid;

	public GameObject goal;
	public GameManager manager;
	public float moveSpeed;

//Particles
	public GameObject enemyParticles;
	public GameObject tokenParticles;
	public GameObject fallParticles;
	public GameObject goalFX;

	public AudioClip[] audioClip;

	private float maxSpeed = 7f;
	private Vector3 input;

	private Vector3 spawn;
	private Vector3 goalPosition;


	// Use this for initialization
	void Start () {
		moveJoy = GameObject.Find("RightJoystick");
		goal.GetComponent<Collider>().isTrigger = false; //goal can not be used until tokens are collected
		goalPosition = goal.transform.position;
		spawn = transform.position;
		manager = manager.GetComponent<GameManager>();
	}



	void FixedUpdate () {
		if (isAndroid) 
		{

			input = new Vector3 (Input.acceleration.x, 0, (-Input.acceleration.z - 0.3f ));

		} else {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		}

		if(GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
		{
			GetComponent<Rigidbody>().AddForce(input * moveSpeed);
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
				goal.GetComponent<Collider>().isTrigger = true; //Goal can be entered now
				PlaySound (4); //flame/ rocket sound
				Instantiate(goalFX, goalPosition, Quaternion.Euler(270,0,0));
			}

		}
		if (other.transform.tag == "Goal")
		{
			//Freeze the player from moving because the level has been won!
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			PlaySound (2);
			manager.CompleteLevel();
		}
	}

	void PlaySound(int clip)
	{
		GetComponent<AudioSource>().clip = audioClip [clip];
			GetComponent<AudioSource>().Play ();
		
	}

	void Die()
	{
		if (manager.lives == 1)
		{
			Debug.Log ("Lost!");
			Application.LoadLevel(0);
		}else 
		{
			Debug.Log (manager.lives);
			manager.lives -= 1;
		}
		//Handheld.Vibrate ();
		Instantiate(enemyParticles, transform.position, Quaternion.Euler(270,0,0));
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		transform.position = spawn;
	}

	
}
