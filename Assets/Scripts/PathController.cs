using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public Material idleMaterial;
    public Material openMaterial;
    public Material closedMaterial;

    public float doorMoveDistance = 3f;
    public float doorMoveSpeed = 0.5f;

    private GameObject interactableButtons;
    private GameObject symbols;
    private GameObject gates;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;
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
                StartCoroutine(moveDoorUpwards(gates.transform.Find("GateSupN").gameObject.transform.GetChild(0).gameObject));
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
                StartCoroutine(moveDoorUpwards(gates.transform.Find("GateSupE").gameObject.transform.GetChild(0).gameObject));
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
                StartCoroutine(moveDoorUpwards(gates.transform.Find("GateSupS").gameObject.transform.GetChild(0).gameObject));
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
                StartCoroutine(moveDoorUpwards(gates.transform.Find("GateSupW").gameObject.transform.GetChild(0).gameObject));
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

    IEnumerator moveDoorUpwards(GameObject door)
    {
        audioManager.PlayAtLocation("Door", door.transform.position);
        
        Vector3 a = door.transform.position;
        Vector3 b = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z) + door.transform.up * doorMoveDistance;

        float step = (doorMoveSpeed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            door.transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        door.transform.position = b;

        audioManager.Stop("Door");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
