using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnTrigger : MonoBehaviour
{
    public GameObject[] ballsToSpawn;
    public float destroyDelay = 10f;
    public bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
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

        foreach (GameObject ball in ballsToSpawn)
        {
            Destroy(ball);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
