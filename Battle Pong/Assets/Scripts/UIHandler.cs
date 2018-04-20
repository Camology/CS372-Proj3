using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIHandler : MonoBehaviour {
public GameObject panel;
void Start()
{
        panel = GameObject.Find("Pause_Panel");
        panel.SetActive(false);
}
void Update(){
        ScanForKeyStroke();
}

void ScanForKeyStroke(){
        if (Input.GetKeyDown("escape")) TogglePauseMenu();
}
void TogglePauseMenu(){
        panel.SetActive(!panel.activeSelf);
        Time.timeScale = !panel.activeSelf ? 1.0f : 0f;
}
}
