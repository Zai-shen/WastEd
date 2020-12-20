using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    GroundCheck groundCheck;
    new Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    public bool ableToDoubleJump = false;
    public bool mayDoubleJump = false;

    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        if (!groundCheck)
            groundCheck = GroundCheck.Create(transform);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (groundCheck.isGrounded && ableToDoubleJump && !mayDoubleJump)
        {
            mayDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
        else if (Input.GetButtonDown("Jump") && mayDoubleJump)
        {
            mayDoubleJump = false;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
