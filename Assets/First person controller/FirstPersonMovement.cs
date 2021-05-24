using System.Collections;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField]
    GroundCheck groundCheck;

    [SerializeField]
    AudioSource footStep;

    private bool ableToMove = true;
    public float speed = 5;

    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float dashCD = 5f;
    private float nextDashTime = 0f;
    private Rigidbody rb;

    public float shieldDuration = 3f;
    Vector2 velocity;
    public bool ableToShield = false;
    public bool ableToSecondLife = false;
    public bool ableToDash = false;

    public bool mayDash = false;
    public bool mayShield = false;
    private float nextShieldTime = 0f;
    public float shieldCD = 10f;

    private PlayerHPHandler pHPHandler;

    private float stepDelay = 0.2f;
    private float timeSinceLastStep = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pHPHandler = GetComponent<PlayerHPHandler>();
        footStep = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (ableToMove)
        {
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
            if ((velocity.x != 0 || velocity.y != 0) && timeSinceLastStep >= stepDelay && groundCheck.isGrounded)
            {
                timeSinceLastStep -= stepDelay; 
                if (!footStep.isPlaying)
                {
                    footStep.pitch = Random.Range(1.1f,1.3f);
                    footStep.Play();
                }
            }
        }
        timeSinceLastStep += Time.fixedDeltaTime;
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

    IEnumerator Dash()
    {
        //Disable movement
        ableToMove = false;

        //Get start rotation
        Vector3 startRot = transform.forward;

        //Add rb force in direction
        float t = 0f;
        while (t <= dashDuration)
        {
            t += Time.fixedDeltaTime; // Adds frame time
            rb.velocity = startRot * dashSpeed; // Set constant force
            yield return new WaitForFixedUpdate(); // Leave the routine and return here in the next frame
        }

        //Then reset velocity
        rb.velocity = new Vector3(0,0,0);

        //Enable movement
        ableToMove = true;
    }

}
