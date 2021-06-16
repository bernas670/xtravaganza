using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public static bool isPaused = false;

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    void PauseGame() {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

}
