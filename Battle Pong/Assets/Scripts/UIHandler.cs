using UnityEngine;

public class UIHandler : MonoBehaviour {
void Start()
{
}
void Update(){
        ScanForKeyStroke();
}

void ScanForKeyStroke(){
        if (Input.GetKeyDown("escape")) TogglePauseMenu();
}
void TogglePauseMenu(){
        GetComponentInChildren<Canvas>().enabled = !(GetComponentInChildren<Canvas>().enabled);
        Time.timeScale = GetComponentInChildren<Canvas>().enabled ? 1.0f : 0f;
}
}
