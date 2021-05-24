using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launches projectiles (using gravity) towards given target. Uses Angles or Maximum Height.
/// </summary>
public class ProjectileLauncher : MonoBehaviour
{
    #region Unity Inspector Fields

    [Tooltip("Transform of the target to be hit.")]
    public Transform target;

    [Tooltip("Prefab of the projectile to be initialised and shot.")]
    public GameObject projectile;

    [Tooltip("Use min/max angles for trajectory calculation. Else use maximum height.")]
    public bool useAngles = true;

    [Header("Angle Mode")]
    [SerializeField]
    [Tooltip("Minimum angle to take the shot.")]
    [Range(1f, 89f)]
    private float minAngle = 45f;

    [SerializeField]
    [Tooltip("Maximum angle to take the shot.")]
    [Range(1f, 89f)]
    private float maxAngle = 75f;

    [Header("Maximum Height Mode")]
    [SerializeField]
    [Tooltip("Maximum height above the launcher allowed for the projectile to reach.")]
    private float maxHeightAboveLauncher = 5f;

    [SerializeField]
    private List<GameObject> launchedProjectiles;

    #endregion

    private const float angleIncrement = 5f;
    private const int gizmoResolution = 30;

    private void OnDisable()
    {
        DestroyLaunchedProjectiles();
    }

    public void Launch()
    {
        if (TargetIsReachable())
        {
            GameObject p = Instantiate(projectile, transform.position, projectile.transform.rotation);
            launchedProjectiles.Add(p);

            Rigidbody pRB = p.GetComponent<Rigidbody>();
            pRB.useGravity = true;
            pRB.velocity = CalcLaunchData(minAngle).initialVelocity;
            pRB.angularVelocity = new Vector3(0f,Random.Range(-5f,5f),Random.Range(0.5f,2f));
        }
        else
        {
            //Debug.Log("Not reachable!");
        }
    }

    public void DestroyLaunchedProjectiles()
    {
        foreach (GameObject p in launchedProjectiles)
        {
            GameObject.Destroy(p);
        }
        launchedProjectiles.Clear();
    }

    private bool TargetIsReachable()
    {
        if (useAngles && CalcAngles(true).y <= maxAngle)
        {
            return true;
        }
        else if (!useAngles && target.position.y - transform.position.y <= maxHeightAboveLauncher)
        {
            return true;
        }

        return false;
    }

    private Vector2 CalcAngles(bool inDegrees)
    {
        Vector2 result = new Vector2();

        Vector3 dir = target.position - transform.position;
        dir = target.InverseTransformDirection(dir);

        result.x = Mathf.Atan2(dir.z, dir.x);
        result.y = Mathf.Atan2(dir.y, dir.x);

        if (inDegrees)
        {
            result *= Mathf.Rad2Deg;
        }

        //Debug.Log("xangle: " + result.x);
        //Debug.Log("yangle: " + result.y);

        return result;
    }

    private LaunchData CalcLaunchData(float angle)
    {
        float gravity = Physics.gravity.y;
        float xDist = target.position.x - transform.position.x;
        float yDist = target.position.y - transform.position.y;
        float zDist = target.position.z - transform.position.z;

        if (useAngles)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float angleSin = Mathf.Sin(angleRad);
            float angleCos = Mathf.Cos(angleRad);

            float midTerm = (yDist - angleSin * (xDist / angleCos));
            float initialVelocitySquared = (gravity * Mathf.Pow(xDist, 2)) / (2 * midTerm * Mathf.Pow(angleCos, 2));

            if (angleSin == 0 || angleCos == 0 || midTerm == 0 || initialVelocitySquared <= 0)
            {
                if (angle + angleIncrement <= maxAngle)
                {
                    return CalcLaunchData(angle + angleIncrement);
                }
            }

            float initialVelocity = Mathf.Sqrt(Mathf.Abs(initialVelocitySquared));
            float xVelocity = initialVelocity * angleCos;
            float yVelocity = initialVelocity * angleSin;
            float flightTime = xDist / xVelocity;
            float zVelocity = zDist / flightTime;
            Vector3 velocities = new Vector3(xVelocity, yVelocity, zVelocity);

            return new LaunchData(velocities, flightTime);
        }
        else
        {
            Vector3 displacementXZ = new Vector3(xDist, 0, zDist);
            float time = Mathf.Sqrt(-2 * maxHeightAboveLauncher / gravity) + Mathf.Sqrt(2 * (yDist - maxHeightAboveLauncher) / gravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maxHeightAboveLauncher);
            Vector3 velocityXZ = displacementXZ / time;

            return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (target == null)
        {
            return;
        }

        Gizmos.color = Color.blue;

        if (useAngles)
        {
            LaunchData maxLaunchData = CalcLaunchData(maxAngle);
            float maxHeight = (transform.position.y + Mathf.Pow(maxLaunchData.initialVelocity.magnitude, 2))
                * Mathf.Pow(Mathf.Sin(maxAngle * Mathf.Deg2Rad), 2)
                / (-2 * Physics.gravity.y);

            Gizmos.DrawLine(new Vector3(this.transform.position.x, maxHeight, this.transform.position.z),
                new Vector3(target.position.x, maxHeight, target.position.z));
        }
        else
        {
            Gizmos.DrawLine(new Vector3(this.transform.position.x, this.transform.position.y + maxHeightAboveLauncher, this.transform.position.z),
                new Vector3(target.position.x, this.transform.position.y + maxHeightAboveLauncher, target.position.z));
        }

        if (useAngles && !TargetIsReachable())
        {
            return;
        }

        LaunchData launchData = CalcLaunchData(minAngle);
        Vector3 previousDrawPoint = transform.position;
        Gizmos.color = Color.green;

        for (int i = 1; i <= gizmoResolution; i++)
        {
            float simulationTime = i / (float)gizmoResolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * Physics.gravity.y * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + displacement;
            Gizmos.DrawLine(previousDrawPoint, drawPoint);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

}
