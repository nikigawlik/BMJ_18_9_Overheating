using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float minSpeed = 0.5f;
	public float maxSpeed = 2f;
	public float randomAngle = 20f;
	public float hp = 1f;

	public float maxSize = 3f;
	private float size = 1f;

	public GameObject explosion;
	public GameObject[] drops;

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

		float f = Random.Range(0f, 1f) * Random.Range(0f, 1f);
		size = Mathf.Floor(f * maxSize) + 1f;
		transform.localScale *= size;
		hp *= size;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Quaternion.Euler(0, 0, direction) * Vector3.right * speed * Time.deltaTime;

		// hp
		if(hp <= 0) {
			// die
			Instantiate(explosion, transform.position, transform.rotation);
			if(size >= 3f) {
				GameObject drop = drops[Random.Range(0, drops.Length)];
				Instantiate(drop, transform.position, Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
	}
}
