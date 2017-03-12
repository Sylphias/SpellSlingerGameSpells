﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float moveForce = 0f;
	public GameObject fireBall, earthWall;
	public Transform gun;
	public float shootRate = 0f;
	public float fireBallCooldown = 0f;
	public float earthWallCooldown = 0f;
	public float rotationalForce = 0f;
	private float fireBallCooldownTimer,earthWallCooldownTimer,iceballCooldownTimer;
	// Use this for initialization
	void Start () {
		fireBallCooldownTimer = Time.time;
		earthWallCooldownTimer = Time.time;
		iceballCooldownTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw ("Horizontal") * moveForce;
		float v = Input.GetAxisRaw ("Vertical") * rotationalForce;
//		rbody.velocity = new Vector3 (h, v, 0);

		transform.Rotate(0, h, 0);
		transform.Translate(0, 0, v);
		if(Input.GetKey(KeyCode.Space)){
			if(Time.time > fireBallCooldownTimer){
				Instantiate(
					fireBall, gun.position, gun.rotation);
				fireBallCooldownTimer = Time.time + shootRate;
			}
		}
		if(Input.GetKey(KeyCode.E)){
			if (Time.time > earthWallCooldownTimer) {
				Instantiate (earthWall, gun.position, gun.rotation);
				earthWallCooldownTimer = Time.time + shootRate;
			}
		}
	}

	public void frost(float speedMultiplier){
		float oldMoveForce = moveForce;
		float debuffTimer = Time.time;
		moveForce *= speedMultiplier;
		if (moveForce < 0) {
			moveForce = 0;
		}
//		while
		Update();
	}
}
