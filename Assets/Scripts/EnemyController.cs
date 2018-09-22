using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float minSpeed = 0.5f;
	public float maxSpeed = 2f;
	public float randomAngle = 20f;
	public float hp = 2;

	public GameObject explosion;

	private float speed;
	private float direction;

	// Use this for initialization
	void Start () {
		speed = Random.Range(minSpeed, maxSpeed);
		GameObject target = GameController.instance.player;
		if(target != null) {
			Vector3 delta = target.transform.position - transform.position;
			direction = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
			direction += Random.Range(-randomAngle, randomAngle);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Quaternion.Euler(0, 0, direction) * Vector3.right * speed * Time.deltaTime;

		// hp
		if(hp <= 0) {
			// die
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
	}
}
