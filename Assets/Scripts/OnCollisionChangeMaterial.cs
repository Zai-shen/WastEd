using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionChangeMaterial : MonoBehaviour
{
    public Material idleMaterial;
    public Material openMaterial;
    public Material closedMaterial;
    public string state = "idle";
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = idleMaterial;
        audioManager = AudioManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && state.Equals("idle"))
        {
            //Debug.Log("Changing state!");
            GetComponent<Renderer>().material = openMaterial;
            state = "open";
            audioManager.PlayAtLocation("PathActivated", this.transform.position);
            // notify superior
            if (this.transform.parent.gameObject.name.StartsWith("IBtnSup"))
            {
                transform.root.GetComponent<PathController>().NotifyTouched(this.transform.parent.gameObject);
            }
            else
            {
                this.transform.GetComponentInParent<BossMechanics>().NotifyTouched(this.name);
                //this.transform.parent.gameObject.GetComponent<BossMechanics>().NotifyTouched(this.name);
            }
        }
        else if (collision.gameObject.CompareTag("Player") && state.Equals("closed"))
        {
            audioManager.PlayAtLocation("PathDeactivated", this.transform.position);
        }
    }

    public void setClosed()
    {
        this.GetComponent<Renderer>().material = closedMaterial;
        state = "closed";
    }
}
