using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTrapDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 drawPos = transform.position;
        drawPos.x += 0.5f;
        Gizmos.DrawCube(drawPos, new Vector3(1,3,2));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Check if exited in direction +x
            if (other.transform.position.x >= transform.position.x)
            {
                //Enable Mesh to render
                GetComponent<MeshRenderer>().enabled = true;

                //Disable is trigger for collision
                GetComponent<MeshCollider>().isTrigger = false;
            }
        }
    }

}
