using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Archer which launches projectiles at player.
/// </summary>
[RequireComponent(typeof(ProjectileLauncher))]
public class Archer : NPC
{
    #region Unity Inspector Fields

    [Tooltip("Rate of fire - in seconds.")]
    [Range(0.1f, 5f)]
    public float fireRate = 1.5f;

    #endregion

    private float nextFireTime = 0f;
    private GameObject player;
    private ProjectileLauncher projLauncher;

    // Start is called before the first frame update
    void Start()
    {
        projLauncher = GetComponent<ProjectileLauncher>();
        player = GameObject.FindGameObjectWithTag("Player");

        projLauncher.target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Transform tForm = this.gameObject.transform.root.transform;
        tForm.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            projLauncher.Launch();
        }
    }
}
