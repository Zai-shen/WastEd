using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private FirstPersonMovement fpsMovement;
    private PlayerHPHandler pHPHandler;
    private AudioManager audioManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        fpsMovement = GetComponent<FirstPersonMovement>();
        pHPHandler = GetComponent<PlayerHPHandler>();
        audioManager = AudioManager.Instance;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            if(pHPHandler.TryTakeDamage(1f))
            audioManager.Play("PlayerCrashed");
        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            if(pHPHandler.TryTakeDamage(1f))
            audioManager.Play("PlayerCrashed");
        }
        else if (collision.gameObject.CompareTag("BananaPeel"))
        {
            if(pHPHandler.TryTakeDamage(0.5f))
            audioManager.Play("PlayerCrashed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpShield"))
        {
            fpsMovement.ableToShield = true;
            HandlePowerUp(other);
        }
        else if (other.CompareTag("PowerUpSecondLife"))
        {
            transform.GetComponent<PlayerHPHandler>().PowerUpSecondLife();
            HandlePowerUp(other);
        }
        else if (other.CompareTag("PowerUpDash"))
        {
            fpsMovement.ableToDash = true;
            HandlePowerUp(other);
        }
        else if (other.CompareTag("PowerUpDoubleJump"))
        {
            transform.GetComponent<Jump>().ableToDoubleJump = true;
            HandlePowerUp(other);
        }
        else if (other.CompareTag("FallingEndless"))
        {
            pHPHandler.ForceTakeDamage(100f);
            audioManager.Play("PlayerCrashed");
        }
    }

    private void HandlePowerUp(Collider other)
    {
        Destroy(other.gameObject);
        audioManager.Play("Gong");
        gameManager.TryEnableEndgame();
    }

}
