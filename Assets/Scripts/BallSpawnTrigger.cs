using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnTrigger : MonoBehaviour
{
    public GameObject[] ballsToSpawn;
    public float destroyDelay = 10f;
    public int timesTriggerAble = 1;
    
    private int timesTriggered = 0;
    private Vector3[] startPositions;

    // Start is called before the first frame update
    void Start()
    {
        startPositions = new Vector3[ballsToSpawn.Length];

        for (int i = 0; i < ballsToSpawn.Length; i++)
        {
            startPositions[i] = ballsToSpawn[i].transform.position;
            // Debug.Log(ballsToSpawn[i].transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && timesTriggered < timesTriggerAble)
        {
            timesTriggered++;
            foreach (GameObject ball in ballsToSpawn)
            {
                ball.SetActive(true);
            }
            StartCoroutine(DestroyAllBalls());
        }
    }

    IEnumerator DestroyAllBalls()
    {
        yield return new WaitForSeconds(destroyDelay);

        for (int i = 0; i < ballsToSpawn.Length; i++)
        {
            ballsToSpawn[i].transform.position = startPositions[i];
            ballsToSpawn[i].SetActive(false);
            // Destroy(ball);
        }
    }
}
