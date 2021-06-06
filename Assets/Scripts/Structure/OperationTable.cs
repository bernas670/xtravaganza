using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationTable : MonoBehaviour
{
    // Start is called before the first frame update

    private Door _door;
    void Awake()
    {   
        GameObject go = GameObject.FindGameObjectWithTag("LockedDoor");
        if(go)
        _door = go.GetComponent<Door>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionStay(){
        if(Input.GetKeyDown(KeyCode.O)){
            Debug.Log("Unlocked Door");
            _door.unlockDoor();
        }
    }
}
