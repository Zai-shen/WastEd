using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticleEffect : MonoBehaviour
{
    public GameObject particleEffectPrefab;

    public void Die()
    {
        GameObject deathEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
    }

}
