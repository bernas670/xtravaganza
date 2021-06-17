using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Respawn()
    {
        PauseController pauseController = gameObject.GetComponentInParent<PauseController>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseController.ResumeGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        gameObject.GetComponentInParent<PauseController>().ResumeGame();
    }
}
