using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OperationTable : MonoBehaviour
{
    private GameObject _door;
    public GameObject text;

    void Awake()
    {
        _door = GameObject.FindGameObjectWithTag("LockedDoor");
    }

    private void OnCollisionEnter(Collision col){
        if(col.transform.tag == "Player"){
            text.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision col){
        if(col.transform.tag == "Player"){
            text.SetActive(false);
        }
    }

    private void OnCollisionStay()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_door)
            {
                Renderer r = gameObject.GetComponent<MeshRenderer>();
                r.materials[2].color = Color.green;
                _door.GetComponent<Door>().unlockDoor();
                TMPro.TextMeshProUGUI mText = text.GetComponent<TMPro.TextMeshProUGUI>();
                mText.text = "The door is already unlocked...";
            }
        }
    }
}
