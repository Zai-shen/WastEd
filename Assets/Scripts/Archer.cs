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
    
    [Tooltip("Target to shoot at.")]
    public GameObject target;

    [Tooltip("Rate of fire - in seconds.")]
    [Range(0.1f, 5f)]
    public float fireRate = 1.5f;

    [Tooltip("Maximum shot distance.")]
    [Range(1f, 30f)]
    public float reach = 10f;

    #endregion

    private float nextFireTime = 0f;
    private ProjectileLauncher projLauncher;
    private Transform parentTf;

    // Start is called before the first frame update
    void Start()
    {
        parentTf = this.gameObject.transform.root.transform;
        projLauncher = GetComponent<ProjectileLauncher>();

        projLauncher.target = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetIsInRange())
        {
            TurnToTarget();
            ShootIfPossible();
        }
    }

    private bool TargetIsInRange()
    {
        return Vector3.Distance(this.transform.position, target.transform.position) <= reach;
    }

    private void ShootIfPossible()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            projLauncher.Launch();
        }
    }

    private void TurnToTarget()
    {
        parentTf.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
    }

    void OnDrawGizmos() //Selected()
    {
        Color redTransparent = new Color();
        redTransparent.r = 1f;

        //Gizmos.color = Color.red;
        redTransparent.a = 0.2f;
        Gizmos.color = redTransparent;
        Gizmos.DrawSphere(this.transform.position, reach);
    }
}
