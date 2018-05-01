using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerSelect : MonoBehaviour {
	Toggle myToggle;

	Stack controllerStack;

	void Start () {
		resetPlayerControllerToDefault();

		initPlayers();
		initControllerStack();
	}

	void initPlayers() {
		GameObject players = GameObject.Find("Players");
		int childCount = players.transform.childCount;

		for(int i = GameInfo.playerCount; i < childCount ; i++) {
			players.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	void initControllerStack() {
		const int NUM_CONTROLLERS = 8;

		controllerStack = new Stack();
		for(int i = NUM_CONTROLLERS; i > 0; i--) {
			controllerStack.Push("Controller" + i);
		}
	}

	void resetPlayerControllerToDefault() {
		GameInfo.inputMap.Clear();
		GameInfo.inputMap.Add("PlayerOne", "PlayerOne");
		GameInfo.inputMap.Add("PlayerTwo", "PlayerTwo");
		GameInfo.inputMap.Add("PlayerThree", "PlayerThree");
		GameInfo.inputMap.Add("PlayerFour", "PlayerFour");
		GameInfo.inputMap.Add("PlayerFive", "PlayerFive");
		GameInfo.inputMap.Add("PlayerSix", "PlayerSix");
		GameInfo.inputMap.Add("PlayerSeven", "PlayerSeven");
		GameInfo.inputMap.Add("PlayerEight", "PlayerEight");
	}

	public void Done() {
		Toggle[] allChildren = GetComponentsInChildren<Toggle>(true);

		foreach(Toggle child in allChildren) {
			if(child.GetComponent<Toggle>().isOn) {
				GameInfo.inputMap[child.name] = (string)controllerStack.Pop();
			}
		}

		SceneManager.LoadScene(GameInfo.playerCount);
	}

	void Update () {
	}
}
