using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAnimationController : MonoBehaviour
{
    void DeathEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
