using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private FirstPersonMovement fpsMovement;
    private PlayerHPHandler pHPHandler;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        fpsMovement = GetComponent<FirstPersonMovement>();
        pHPHandler = GetComponent<PlayerHPHandler>();
        audioManager = AudioManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            pHPHandler.TakeDamage(1f);
            //Play sound
            audioManager.Play("PlayerCrashed");

        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            pHPHandler.TakeDamage(1f);
            //Play sound
            audioManager.Play("PlayerCrashed");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpShield"))
        {
            fpsMovement.ableToShield = true;
            Destroy(other.gameObject);
            audioManager.Play("Gong");
        }
        else if (other.CompareTag("PowerUpSecondLife"))
        {
            transform.GetComponent<PlayerHPHandler>().PowerUpSecondLife();
            Destroy(other.gameObject);
            audioManager.Play("Gong");
        }
        else if (other.CompareTag("PowerUpDash"))
        {
            fpsMovement.ableToDash = true;
            Destroy(other.gameObject);
            audioManager.Play("Gong");
        }
        else if (other.CompareTag("PowerUpDoubleJump"))
        {
            transform.GetComponent<Jump>().ableToDoubleJump = true;
            Destroy(other.gameObject);
            audioManager.Play("Gong");
        }
        else if (other.CompareTag("FallingEndless"))
        {
            pHPHandler.TakeDamage(100f);
            //Play sound
            audioManager.Play("PlayerCrashed");
        }
    }

}
