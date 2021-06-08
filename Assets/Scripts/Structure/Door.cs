
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public bool isLocked;
    private GameObject _player;
    private Vector3 openDoor;
    private Vector3 closeDoor;

    private bool opening;
    private bool closing;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        openDoor = transform.position + new Vector3(0, 12, 0);
        closeDoor = transform.position;
        closing = true;
        opening = false;

        if (isLocked) lockDoor();
        else unlockDoor();
    }

    void Update() {
        if (opening) {
            transform.position = Vector3.Lerp(transform.position, openDoor, Time.deltaTime * 2.0f);
        } else if (closing) {
            transform.position = Vector3.Lerp(transform.position, closeDoor, Time.deltaTime * 2.0f);
        }
    }

    public void lockDoor()
    {
        isLocked = true;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.materials[1].color = Color.red;
        }
    }

    public void unlockDoor()
    {
        isLocked = false;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.materials[1].color = Color.blue;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (isLocked)
            return;

        if (col.gameObject.tag == "Player")
        {
            opening = true;
            closing = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (isLocked)
            return;

        if (col.gameObject.tag == "Player")
        {
            closing = true;
            opening = false;
        }

    }
}