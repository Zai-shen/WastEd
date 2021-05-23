using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class for non playable characters.
/// </summary>
public class NPC : Character
{

    public new bool TryTakeDamage(float dmg)
    {
        healthPoints -= dmg;

        if (healthPoints <= 0 && !dead)
        {
            dead = true;
            Destroy(this.gameObject);
        }

        return true;
    }
}
