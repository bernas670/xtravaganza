using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    
    void OnEnable()
    {        
        GameObject player = GameObject.Find("Player");

        StartCoroutine(LoadYourAsyncScene());
        
        player.transform.position = new Vector3(-53, 38, -207);

        MovementController mController = player.GetComponent<MovementController>();
        mController.enabled = true;
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