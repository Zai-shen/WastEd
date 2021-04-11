using System.Collections;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public float dashDistance = 5f;
    public float dashSpeed = 1f;
    public float shieldDuration = 3f;
    Vector2 velocity;
    public bool ableToShield = false;
    public bool ableToSecondLife = false;
    public bool ableToDash = false;

    public bool mayShield = false;
    public bool mayDash = false;

    public float shieldCD = 10f;
    private float nextShieldTime = 0f;
    public float dashCD = 5f;
    private float nextDashTime = 0f;

    private PlayerHPHandler pHPHandler;

    private void Start()
    {
        pHPHandler = GetComponent<PlayerHPHandler>();
    }

    void FixedUpdate()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ableToShield && Time.time > nextShieldTime)
        {
            //Debug.Log("Shielding!" + Time.time);
            //Disable damage, display shield
            StartCoroutine(pHPHandler.PowerUpShield(shieldDuration));
            
            nextShieldTime = Time.time + shieldCD;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && ableToDash && Time.time > nextDashTime)
        {
            //Debug.Log("Dashing!" + Time.time);
            //Do the dashing
            StartCoroutine(Dash());

            nextDashTime = Time.time + dashCD;
        }
    }

    /// <summary>
    /// Currently sets the transform - which allows for precise control. Though it is possible to clip through walls/etc.
    /// TODO: make as addForce() and for the duration eliminate friction.
    /// </summary>
    IEnumerator Dash()
    {
        Vector3 a = transform.position;
        Vector3 b = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward * dashDistance;

        float step = (dashSpeed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        transform.position = b;
    }

}
