using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 2f;
	public GameObject bulletPrefab;
	public float shootDelay = .2f;
	public GameObject explosion;

	private float shootTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// rotation
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// shooting		
		shootTimer -= Time.deltaTime;
		if(shootTimer <= 0 && Input.GetButton("Fire1")) {
			shootTimer = shootDelay;
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		EnemyController enemy = other.GetComponent<EnemyController>();
		if(enemy != null) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
