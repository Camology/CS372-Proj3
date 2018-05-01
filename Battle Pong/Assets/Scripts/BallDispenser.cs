using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDispenser : MonoBehaviour {
	public GameObject ball;
	private int ballCount;
	private bool ballWait;

	private int playerCount;

	void Awake () {
		Instantiate (ball, transform.position, transform.rotation);
		playerCount = GameInfo.playerCount;
	}

	void Update () {
		ballCount = GameObject.FindGameObjectsWithTag ("Ball").Length;

		if (ballCount == 0) {
			Instantiate (ball, transform.position, transform.rotation);
		}

		if (ballCount < playerCount && !ballWait) {
			ballWait = true;
			StartCoroutine ("SpawnBall");
		}
	}

	IEnumerator SpawnBall() {
		float randTime = Random.Range (1.0f, 5.0f);

		yield return new WaitForSeconds (randTime);

		Instantiate (ball, transform.position, transform.rotation);
		Debug.Log (ballCount);
		ballWait = false;
	}
}
