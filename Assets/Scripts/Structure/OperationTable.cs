using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationTable : MonoBehaviour
{
    private GameObject _door;

    void Awake()
    {
        _door = GameObject.FindGameObjectWithTag("LockedDoor");
    }

    private void OnCollisionStay()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_door)
            {
                Debug.Log("Unlocked Door");
                Renderer r = gameObject.GetComponent<MeshRenderer>();
                r.materials[2].color = Color.green;
                _door.GetComponent<Door>().unlockDoor();
            }
        }
    }
}
