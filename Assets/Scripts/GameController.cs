using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Button restartButton;
	public GameObject gameOverScreen;
	public GameObject player;
	public float gameOverDelay = 2f;

	public AudioSource generalSource;

	public static GameController instance;

	// Use this for initialization
	void Start () {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject);
		}

		restartButton.onClick.AddListener(GameRestart);
	}

	private void GameRestart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GameOver() {
		StartCoroutine(GameOverRoutine());
	}

	public IEnumerator GameOverRoutine() {
		yield return new WaitForSeconds(gameOverDelay);
		gameOverScreen.SetActive(true);
	}


}
