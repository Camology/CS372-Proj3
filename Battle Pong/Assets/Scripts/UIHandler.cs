using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIHandler : MonoBehaviour {
void Start()
{
        GetComponentInChildren<Canvas>().enabled = false;
}
void Update(){
        ScanForKeyStroke();
}

void ScanForKeyStroke(){
        if (Input.GetKeyDown("escape")) TogglePauseMenu();
}
void TogglePauseMenu(){
        GetComponentInChildren<Canvas>().enabled = !(GetComponentInChildren<Canvas>().enabled);
        Time.timeScale = !GetComponentInChildren<Canvas>().enabled ? 1.0f : 0f;
}
}
