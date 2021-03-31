using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.1f;

    public LayerMask groundLayer;

    private Rigidbody _body;
    private Transform _groundChecker;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = IsGrounded();

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * speed * Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(_groundChecker.position, groundDistance, groundLayer, QueryTriggerInteraction.Ignore);
    }
}
