using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject player = GameObject.Find("Player");
        GameObject canvas = GameObject.Find("Canvas");
        GameObject memManager = GameObject.Find("MementoManager");

        if(memManager) {
            memManager.GetComponent<MementoManager>().ClearSnapshot();
        }

        Destroy(player);
        Destroy(canvas);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
