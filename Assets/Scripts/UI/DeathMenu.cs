using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class DeathMenu : MonoBehaviour
{
    public void Respawn()
    {
        PauseController pauseController = gameObject.GetComponentInParent<PauseController>();
        FMOD.Studio.Bus masterBus = RuntimeManager.GetBus("Bus:/");
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseController.ResumeGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        gameObject.GetComponentInParent<PauseController>().ResumeGame();
    }
}
