
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isLocked;
    private GameObject _player;
    private Vector3 openDoor;
    private Vector3 closeDoor;


    void Awake(){
        _player = GameObject.FindGameObjectWithTag("Player");
        openDoor = new Vector3(0, 12, 0);
        closeDoor = new Vector3(0, -12, 0);

        if(isLocked) lockDoor();
        else unlockDoor();
    }   


    public void lockDoor(){
        isLocked = true;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.materials[1].color = Color.red;
        }
    }
    public void unlockDoor(){
        isLocked = false;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.materials[1].color = Color.blue;
        }
    }
    private void OnTriggerEnter(Collider col)
    {   
        if(isLocked)
            return;
        
        
        if(col.gameObject.tag == "Player"){
            gameObject.transform.Translate(openDoor);
        }
    }
    private void OnTriggerExit(Collider col)
    {   
        if(isLocked)
            return;
        if(col.gameObject.tag == "Player"){
            gameObject.transform.Translate(closeDoor);
        }
        
    }
}