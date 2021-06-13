using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    void OnEnable()
    {        
        StartCoroutine(LoadYourAsyncScene());

        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(-51, 34, -207);
    }

    IEnumerator LoadYourAsyncScene()
    {
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(1);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}