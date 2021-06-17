using UnityEngine;

public class DeathAnimationController : MonoBehaviour
{
    public GameObject deathMenu;
    public PauseController pauseController;

    void DeathEnd()
    {
        pauseController.enabled = false;
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
