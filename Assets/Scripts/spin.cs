﻿using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	public float speed = 40f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up, speed * Time.deltaTime);
	}
}
