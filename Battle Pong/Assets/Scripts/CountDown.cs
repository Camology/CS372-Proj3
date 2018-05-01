using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
	public Text text;

	private int countDownTime = 2;

	void Update(){
		ResumeIn5 ();
	}

	void ResumeIn5(){
		StartCoroutine (Counter (countDownTime));
	}

	private IEnumerator Counter(int resumeTime){
		Time.timeScale = 0.0001f;

		float timeSinceLastNumber = Time.realtimeSinceStartup;
		int countDownNumber = resumeTime;

		while (countDownNumber > -1) {
			if (Time.realtimeSinceStartup - timeSinceLastNumber > 1.0f ) {
				countDownNumber -= 1;
				timeSinceLastNumber = Time.realtimeSinceStartup;
			}

			updateText(countDownNumber);

			yield return null;
		}

		text.gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	void updateText(int number) {
		if (countDownNumber == 0 || countDownNumber == -1) {
			text.text = "Start!";
		} else {
			text.text = countDownNumber.ToString();
		}
	}
}
