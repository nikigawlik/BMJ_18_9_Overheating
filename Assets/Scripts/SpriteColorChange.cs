using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorChange : MonoBehaviour {
	public Gradient colorProgression;
	public float progress = 0f;

	private void Update() {
		this.GetComponent<SpriteRenderer>().color = colorProgression.Evaluate(progress);
	}
}
