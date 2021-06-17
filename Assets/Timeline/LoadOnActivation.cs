using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActivation : MonoBehaviour
{
    
    void OnEnable()
    {        
        GameObject player = GameObject.Find("Player");
        GameObject mManager = GameObject.Find("MementoManager");
        if(!mManager) return;

        MementoManager mementoManagerScript = mManager.GetComponent<MementoManager>();
        Player playerScript = player.GetComponent<Player>();
        
        WeaponSwitchController weaponSwitchController = playerScript.getGunContainer().GetComponent<WeaponSwitchController>();
        mementoManagerScript.CreateSnapshot(playerScript, weaponSwitchController);

        playerScript.GetRigController().clearRigWeaponReference();

        StartCoroutine(LoadAsyncScene(weaponSwitchController.weapons));
    }

    IEnumerator LoadAsyncScene(List<GameObject> weapons)
    {
        // Scene currentScene = SceneManager.GetActiveScene();
        
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(2);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);//, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // foreach(GameObject weapon in weapons){
        //     weapon.transform.parent = null;
        //     SceneManager.MoveGameObjectToScene(weapon, SceneManager.GetSceneByName(sceneName));
            
        //     GameObject gContainer = GameObject.Find("GunContainer");
        //     weapon.transform.parent = gContainer.transform;
        //     weapon.transform.localPosition = Vector3.zero;
        //     weapon.transform.localRotation = Quaternion.identity;
        // }

        // SceneManager.UnloadSceneAsync(currentScene);
    }
}