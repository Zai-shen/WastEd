using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPHandler : Character
{
    public bool shielded = false;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //display hp        
    }

    public new void TakeDamage(float dmg)
    {
        if (shielded)
        {
            return;
        }
        else
        {
            healthPoints -= dmg;
        }

        if (healthPoints <= 0 && !dead)
        {
            dead = true;
            //Handle death
            Debug.Log("I am dying now, oh noes! At:");
            LogPosition();


            //Display fade on screen
            //Debug.Log("rotation:" + this.transform.rotation.to);
            //this.transform.rotation = Quaternion.Euler(new Vector3(-90,0,-90));
            //Debug.Log("rotation:" + this.transform.rotation);

            //Return to main menu scene
            StartCoroutine(WaitAndLoadMenu(1f));
        }
    }

    public void PowerUpSecondLife()
    {
        healthPoints += 1f;
    }

    public IEnumerator PowerUpShield(float duration)
    {
        Debug.Log("Start shield");
        shielded = true;
        yield return new WaitForSeconds(duration);
        shielded = false;
        Debug.Log("End shield");
    }

    IEnumerator WaitAndLoadMenu(float t)
    {
        yield return new WaitForSeconds(t);
        gameManager.LoadMenu();
    }
}
