using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Camera cam;
    public float sensitivity = 300f;

    public float minHeadRotation = -90f;
    public float maxHeadRotation = 90f;

    private float _headRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float rotX = Input.GetAxis("Mouse X") * sensitivity;
        float rotY = Input.GetAxis("Mouse Y") * sensitivity * -1f;

        transform.Rotate(0, rotX, 0);

        _headRotation += rotY;
        _headRotation = Mathf.Clamp(_headRotation, minHeadRotation, maxHeadRotation);
        cam.transform.localEulerAngles = new Vector3(_headRotation, 0, 0);
    }
}
