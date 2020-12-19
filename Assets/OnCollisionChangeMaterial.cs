using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionChangeMaterial : MonoBehaviour
{
    public Material idleMaterial;
    public Material openMaterial;
    public Material closedMaterial;
    public string state = "idle";

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = idleMaterial;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && state.Equals("idle"))
        {
            //Debug.Log("Changing state!");
            GetComponent<Renderer>().material = openMaterial;
            state = "open";
            // notify superior
            transform.root.GetComponent<PathController>().NotifyTouched(this.transform.parent.gameObject);
        }
    }

    public void setClosed()
    {
        this.GetComponent<Renderer>().material = closedMaterial;
        state = "closed";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
