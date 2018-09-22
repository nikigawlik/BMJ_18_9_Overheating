using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public float enemiesPerSecond = 4f;
	public float outOfScreenPadding = 20f;
	public GameObject enemyPrefab;

	private float spawnTimer = 0f;

	// Use this for initialization
	void Start () {
		spawnTimer = 1f / enemiesPerSecond;
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnTimer < 0) {
			spawnTimer = 1f / enemiesPerSecond;

			// spawn enemy
			float vertExtent = Camera.main.orthographicSize / 2f + outOfScreenPadding;
			float horzExtent = (vertExtent * Screen.width / Screen.height) / 2f + outOfScreenPadding;
			bool horizontal = Random.Range(0f, 1f) < .5f; 
			bool positive = Random.Range(0f, 1f) < .5f;
			Vector2 spawnPosition;
			if(horizontal) {
				spawnPosition = new Vector2(
					Random.Range(-horzExtent, horzExtent),
					vertExtent * (positive? 1f : -1f)
				);
			} else {
				spawnPosition = new Vector2(
					horzExtent * (positive? 1f : -1f),
					Random.Range(-vertExtent, vertExtent)
				);
			}
			Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		}
		spawnTimer -= Time.deltaTime;
	}
}
