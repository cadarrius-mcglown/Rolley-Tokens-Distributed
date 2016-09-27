using UnityEngine;
using System.Collections;

public class PlayerMovementFollower : MonoBehaviour {

	public bool isAndroid;

	public GameManager manager;
	public float moveSpeed;

//Particles
	public GameObject enemyParticles;
	public GameObject tokenParticles;
	public GameObject fallParticles;

	public AudioClip[] audioClip;

	private float maxSpeed = 40f;
	private Vector3 input;

	private Vector3 spawn;


	// Use this for initialization
	void Start () {
		spawn = transform.position;
		manager = manager.GetComponent<GameManager>();
	}



	void FixedUpdate () {
		if (isAndroid) 
		{
			input = new Vector3 (Input.acceleration.x, 0, -Input.acceleration.z - 0.7f);
		} else {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		}

		if(rigidbody.velocity.magnitude < maxSpeed)
		{
			rigidbody.AddForce(input * moveSpeed);
		}

		if (transform.position.y < -2)
		{
			//PlaySound (0);
			//Die ();
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

		}
		if (other.transform.tag == "Goal")
		{
			PlaySound (2);
			manager.CompleteLevel();
		}
	}

	void PlaySound(int clip)
	{
		audio.clip = audioClip [clip];
		audio.Play();
	}

	void Die()
	{
		Instantiate(enemyParticles, transform.position, Quaternion.Euler(270,0,0));
		transform.position = spawn;
	}
}
