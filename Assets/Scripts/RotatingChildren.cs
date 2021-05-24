using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingChildren : MonoBehaviour
{
    public float rotateSpeedMin = 0.40f;
    public float rotateSpeedMax = 1.10f;
    public Vector3 rotation = new Vector3(0f,90f,0f);

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            StartCoroutine(Rotating(child.gameObject, 
                rotation, 
                Random.Range(rotateSpeedMin, rotateSpeedMax)));
        }
    }

    IEnumerator Rotating(GameObject child, Vector3 targetRot, float speed)
    {
        Quaternion startRot = child.transform.rotation;

        float time = 0f;
        while (Quaternion.Angle(child.transform.rotation, startRot * Quaternion.Euler(targetRot)) >= 1f)
        {
            child.transform.Rotate(targetRot * Time.deltaTime * speed);
            time += Time.deltaTime;

            yield return null;
        }
        
        yield return Rotating(child, -targetRot, speed);
    }
}
