using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float healthPoints = 1f;
    [SerializeField]
    protected bool dead;

    public bool TryTakeDamage(float dmg)
    {
        healthPoints -= dmg;

        if (healthPoints <= 0 && !dead)
        {
            dead = true;
        }

        return true;
    }

    public void LogPosition()
    {
        Debug.Log("Position of " + this.name + " " + transform.position);
    }
}
