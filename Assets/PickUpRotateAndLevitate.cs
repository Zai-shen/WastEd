using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotateAndLevitate : MonoBehaviour
{
    public float rotDuration = 4f;
    public float levitationHeight = 0.5f;
    public float levSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate(rotDuration));
        StartCoroutine(Levitate(levitationHeight));
    }

    IEnumerator Rotate(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation,
            transform.eulerAngles.z);
            yield return null;
        }
        StartCoroutine(Rotate(duration));
    }

    IEnumerator Levitate(float levHeight)
    {
        Vector3 a = transform.position;
        Vector3 b = new Vector3(transform.position.x,transform.position.y,transform.position.z) + new Vector3(0, levHeight, 0);

        float step = (levSpeed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        transform.position = b;
        StartCoroutine(Levitate(-levHeight));
    }

}
