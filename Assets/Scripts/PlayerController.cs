using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float acceleration = 2f;
	public float drag = 2f;
	public GameObject bulletPrefab;
	public float shootDelay = .2f;
	public GameObject explosion;

	public GameObject flame;
	public Image barImage;

	public float maxHeat = 10f;
	public float heatDecay = 1f;
	public float bulletHeatIncrease = 1f;
	public float flameHeatIncrease = 2f;

	public float flameShutoffHeat = 5f;

	private float heat = 0f;
	private float shootTimer = 0f;

	private Vector3 velocity = Vector2.zero;

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
			Instantiate(bulletPrefab, transform.position, transform.rotation);
		}

		// heat display
		heat = Mathf.Clamp(heat, 0, maxHeat);
		GetComponentInChildren<SpriteColorChange>().progress = GetHeatFactor();
		barImage.fillAmount = GetHeatFactor();

		// movement
		if(Input.GetButton("Fire2") && heat < flameShutoffHeat) {
			flame.SetActive(true);
			velocity += transform.right * acceleration * Time.deltaTime;
			heat += flameHeatIncrease * Time.deltaTime;
		} else {
			flame.SetActive(false);
		}

		velocity += Vector3.ClampMagnitude(-velocity.normalized * drag * Time.deltaTime, velocity.magnitude);
		transform.position += velocity * Time.deltaTime;
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
