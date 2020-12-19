using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionSpawnPickUp : MonoBehaviour
{
    public GameObject pickUpToSpawn;
    public Vector3 rotationPickUp;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Playercollision");
            //SpawnPickup
            Instantiate(pickUpToSpawn, transform.parent.transform.parent.transform.position + new Vector3(2f,1f,0), Quaternion.Euler(rotationPickUp));
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
