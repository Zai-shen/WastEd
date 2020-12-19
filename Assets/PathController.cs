using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public Material idleMaterial;
    public Material openMaterial;
    public Material closedMaterial;

    private GameObject interactableButtons;
    private GameObject symbols;
    private GameObject gates;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NotifyTouched(GameObject caller)
    {
        //Debug.Log("I see you, " + caller.name +"!");

        if (interactableButtons == null)
        {
            interactableButtons = this.transform.Find("InteractableButtons").gameObject;
        }
        if (symbols == null)
        {
            symbols = this.transform.Find("Symbols").gameObject;
        }
        if (gates == null)
        {
            gates = this.transform.Find("Gates").gameObject;
        }

        switch (caller.name)
        {
            case "IBtnSupN":
                //Debug.Log("Understood, disabling IBtnSupE.");
                //Activate this symbol
                symbols.transform.Find("Shield").gameObject
                    .GetComponentInChildren<Renderer>().material = openMaterial;
                //Open door
                gates.transform.Find("GateSupN").gameObject.SetActive(false);
                //Disable another button
                interactableButtons.transform.Find("IBtnSupE").gameObject
                    .GetComponentInChildren<OnCollisionChangeMaterial>().setClosed();
                //Disable antoher symbol
                Component[] temp = symbols.transform.Find("SecondLife").gameObject
                    .GetComponentsInChildren<Renderer>();//.material = closedMaterial;
                foreach (Renderer r in temp)
                {
                    r.material = closedMaterial;
                }
                break;
            case "IBtnSupE":
                //Debug.Log("Understood, disabling IBtnSupN.");
                //Activate this symbol
                Component[] temp1 = symbols.transform.Find("SecondLife").gameObject
                    .GetComponentsInChildren<Renderer>();
                foreach (Renderer r in temp1)
                {
                    r.material = openMaterial;
                }
                //Open door
                gates.transform.Find("GateSupE").gameObject.SetActive(false);
                //Disable another button
                interactableButtons.transform.Find("IBtnSupN").gameObject
                    .GetComponentInChildren<OnCollisionChangeMaterial>().setClosed();
                //Disable antoher symbol
                symbols.transform.Find("Shield").gameObject
                    .GetComponentInChildren<Renderer>().material = closedMaterial;
                break;
            case "IBtnSupS":
                //Debug.Log("Understood, disabling IBtnSupW.");
                //Activate this symbol
                Component[] temp2 = symbols.transform.Find("DoubleJump").gameObject
                    .GetComponentsInChildren<Renderer>();
                foreach (Renderer r in temp2)
                {
                    r.material = openMaterial;
                }
                //Open door
                gates.transform.Find("GateSupS").gameObject.SetActive(false);
                //Disable another button
                interactableButtons.transform.Find("IBtnSupW").gameObject
                    .GetComponentInChildren<OnCollisionChangeMaterial>().setClosed();
                //Disable antoher symbol
                symbols.transform.Find("Dash").gameObject
                    .GetComponentInChildren<Renderer>().material = closedMaterial;
                break;
            case "IBtnSupW":
                //Debug.Log("Understood, disabling IBtnSupS.");
                //Activate this symbol
                symbols.transform.Find("Dash").gameObject
                    .GetComponentInChildren<Renderer>().material = openMaterial;
                //Open door
                gates.transform.Find("GateSupW").gameObject.SetActive(false);
                //Disable another button
                interactableButtons.transform.Find("IBtnSupS").gameObject
                    .GetComponentInChildren<OnCollisionChangeMaterial>().setClosed();
                //Disable antoher symbol
                Component[] temp3 = symbols.transform.Find("DoubleJump").gameObject
                    .GetComponentsInChildren<Renderer>();//.material = closedMaterial;
                foreach (Renderer r in temp3)
                {
                    r.material = closedMaterial;
                }
                break;
            default:
                Debug.Log("Could not recognize: " + caller.name + "!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
