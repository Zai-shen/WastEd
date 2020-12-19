using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("I'm just gonna die now...");
        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("I'm just gonna die now...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
