
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isClosed;
    private GameObject _player;

    void Awake(){
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(_player.GetComponent<Collider>(), GetComponent<Collider>());
    }    
    void OnTriggerEnter(Collider col){
        Debug.Log("entrei");
    }
}