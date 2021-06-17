using UnityEngine;

public class Elevator : MonoBehaviour
{
    private GameObject _player;
    private GameObject leftDoor;
    private GameObject rightDoor;
    private Vector3 initRightDoorPosition;
    private Vector3 initLeftDoorPosition;
    private Vector3 endRightDoorPosition;
    private Vector3 endLeftDoorPosition;
    private bool opening;
    private bool closing;

    private float _smoothFactor = 2.5f;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        leftDoor = transform.Find("LeftDoor").gameObject;
        rightDoor = transform.Find("RightDoor").gameObject;

        initLeftDoorPosition = leftDoor.transform.position;
        endLeftDoorPosition = leftDoor.transform.position + new Vector3(0, 0, 10);

        initRightDoorPosition = rightDoor.transform.position;
        endRightDoorPosition = rightDoor.transform.position + new Vector3(0, 0, -10);

        closing = false;
        opening = false;
    }

    void Update()
    {
        if (opening)
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, endLeftDoorPosition, Time.deltaTime * _smoothFactor);
            rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, endRightDoorPosition, Time.deltaTime * _smoothFactor);
        }
        else if (closing)
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, initLeftDoorPosition, Time.deltaTime * _smoothFactor);
            rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, initRightDoorPosition, Time.deltaTime * _smoothFactor);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            opening = true;
            closing = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            closing = true;
            opening = false;
        }
    }
}