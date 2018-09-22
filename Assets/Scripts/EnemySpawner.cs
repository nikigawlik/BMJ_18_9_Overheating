using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public float enemiesPerSecond = 4f;
	public float enemyRadius = 20f;
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
			float spawnAngle = Random.Range(0f, 360f);
			Vector2 spawnPosition = new Vector2(Mathf.Cos(spawnAngle * Mathf.Deg2Rad), Mathf.Sin(spawnAngle * Mathf.Deg2Rad)) * enemyRadius;
			Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		}
		spawnTimer -= Time.deltaTime;
	}
}
