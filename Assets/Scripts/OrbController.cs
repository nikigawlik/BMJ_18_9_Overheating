using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour {

	public float acceleration = 2f;
	public float drag = 2f;
	public float initialSpeedMin = 2f;
	public float initialSpeedMax = 6f;
	
	private Vector3 velocity = Vector2.zero;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		velocity = transform.right * Random.Range(initialSpeedMin, initialSpeedMax);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject target = GameController.instance.player;
		if(target != null) {
			Vector3 delta = target.transform.position - transform.position;

			velocity += delta.normalized * acceleration * Time.deltaTime;
		}
		
		velocity += Vector3.ClampMagnitude(-velocity.normalized * drag * Time.deltaTime, velocity.magnitude);
		transform.position += velocity * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		PlayerController p = other.GetComponent<PlayerController>();
		if(p != null) {
			p.money += 1f;
			Destroy(this.gameObject);
		}
	}
}
