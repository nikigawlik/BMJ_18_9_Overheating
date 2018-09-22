using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	public float speed = 5f;
	public float damage = 1f;
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * (speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		EnemyController enemy = other.GetComponent<EnemyController>();

		if(enemy != null) {
			enemy.hp -= damage;
			Destroy(this.gameObject);
		}
	}
}
