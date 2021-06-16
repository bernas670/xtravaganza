using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public void Resume() {
        PauseController pauseController = gameObject.GetComponentInParent<PauseController>();

        pauseController.ResumeGame();
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
        Resume();
    }
}
