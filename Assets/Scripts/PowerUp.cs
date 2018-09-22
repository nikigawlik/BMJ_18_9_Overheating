﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public enum PowerUpType {
		COOLANT
	}

	public PowerUpType powerUpType = PowerUpType.COOLANT;
	public float rotationSpeed = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		PlayerController pc = other.GetComponent<PlayerController>();
		if(pc != null) {
			switch(powerUpType) {
				case PowerUpType.COOLANT:
					pc.Heat = 0;
				break;
			}
			Destroy(this.gameObject);
		}
	}
}
