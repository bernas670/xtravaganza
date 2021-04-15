using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovementController : MonoBehaviour
{

    public float speed = 10f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.1f;

    public LayerMask groundLayer;

    private Rigidbody _body;
    private Transform _groundChecker;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    // temporary for debug purposes
    public TextMeshProUGUI velocityText;


    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        _isGrounded = IsGrounded();

        _inputs = Vector3.zero;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _inputs = (_body.transform.right * x + _body.transform.forward * z).normalized;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // temporary for debug purposes
        velocityText.text = _body.velocity.ToString();
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
