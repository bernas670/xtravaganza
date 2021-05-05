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

    private float _tilt;
    private float _camTiltTime = 5;
    private float _tiltError = 0.000001f;

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
        cam.transform.localEulerAngles = new Vector3(_headRotation, 0, _tilt);
    }

    public void Tilt(float max) {
        _tilt = Mathf.Lerp(_tilt, max, _camTiltTime * Time.deltaTime);
    }

    public IEnumerator AsyncTilt(float target)
    {
        float _lastTilt = _tilt;
        while(Mathf.Abs(_tilt - target) >= _tiltError) {
            if(Mathf.Abs(_lastTilt) < Mathf.Abs(_tilt)) break;

            _tilt = Mathf.Lerp(_tilt, target, _camTiltTime * Time.deltaTime);
            _lastTilt = _tilt;
            yield return null;
        }

    }
}
