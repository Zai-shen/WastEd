using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float healthPoints = 1f;
    protected bool dead;

    public void TakeDamage(float dmg)
    {
        healthPoints -= dmg;

        if (healthPoints <= 0 && !dead)
        {
            dead = true;
        }
    }

    public void LogPosition()
    {
        Debug.Log("Position of " + this.name + " " + transform.position);
    }
}
