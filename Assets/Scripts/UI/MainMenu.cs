using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu() {

    }

    public void QuitGame() {
        Application.Quit();
    }
}
