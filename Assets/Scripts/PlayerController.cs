using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 2f;
	public GameObject bulletPrefab;
	public float shootDelay = .2f;
	public GameObject explosion;

	public float maxHeat = 10f;
	public float heatDecay = 1f;

	public float bulletHeatIncrease = 1f;

	private float heat = 0f;

	private float shootTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// heat decay
		heat -= heatDecay * Time.deltaTime;

		// rotation
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// shooting		
		shootTimer -= Time.deltaTime * GetInverseHeatFactor();
		if(shootTimer <= 0 && Input.GetButton("Fire1")) {
			heat += bulletHeatIncrease;
			shootTimer = shootDelay;
			GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		}

		// heat display
		heat = Mathf.Clamp(heat, 0, maxHeat);
		GetComponentInChildren<SpriteColorChange>().progress = GetHeatFactor();
	}

	private float GetInverseHeatFactor()
    {
        return 1f - GetHeatFactor();
    }

    private float GetHeatFactor()
    {
        return Mathf.Clamp01(heat / maxHeat);
    }

    private void OnTriggerEnter2D(Collider2D other) {
		EnemyController enemy = other.GetComponent<EnemyController>();
		if(enemy != null) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			GameController.instance.GameOver();
			Destroy(this.gameObject);
		}
	}
}
