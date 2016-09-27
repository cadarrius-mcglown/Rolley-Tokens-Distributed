using UnityEngine;
using System.Collections;

public class PlayerMovementtwonew : MonoBehaviour {

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

	}



	void FixedUpdate () {

		input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));


		if(rigidbody.velocity.magnitude < maxSpeed)
		{

			rigidbody.AddForce(input * moveSpeed);
			Debug.Log(input+"Fs");
		}


	}









	
}
