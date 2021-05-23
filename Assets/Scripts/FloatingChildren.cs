using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingChildren : MonoBehaviour
{
    public float floatSpeedMin = 0.25f;
    public float floatSpeedMax = 1.25f;
    public float floatDistanceMin = 0.5f;
    public float floatDistanceMax = 3f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            StartCoroutine(Floating(child.gameObject,
                new Vector3(0f, Random.Range(floatDistanceMin, floatDistanceMax), 0f),
                Random.Range(floatSpeedMin, floatSpeedMax)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Floating(GameObject child, Vector3 target, float speed)
    {
        Vector3 startPos = child.transform.position;

        float time = 0f;
        while (child.transform.position != startPos + target)
        {
            child.transform.position = Vector3.Lerp(startPos, startPos + target, (time / Vector3.Distance(startPos, startPos + target)) * speed);
            time += Time.deltaTime;

            yield return null;
        }

        yield return Floating(child, -target, speed);
    }

}
