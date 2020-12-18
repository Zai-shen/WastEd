using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTriangleRotator : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 0.5f;
    [SerializeField]
    private float maxRotation = 90f;
    [SerializeField]
    private float rotationReverseDelay = 5f;

    private float currentRotation;
    private float threshold = 0.1f;

    private void Start()
    {
        StartCoroutine(TurnSinPositive(0f));
    }

    IEnumerator TurnSinPositive(float startDelay)
    {
        // Debug.Log("Starting rotation positive in " + startDelay + "seconds");
        yield return new WaitForSecondsRealtime(startDelay);
        Time.timeScale = 1f;

        while (!(Mathf.Abs(currentRotation - maxRotation) <= threshold))
        {
            currentRotation = maxRotation * Mathf.Sin(Time.time * turnSpeed);
            transform.rotation = Quaternion.Euler(currentRotation, currentRotation, currentRotation);
            yield return null;
        }

        StartCoroutine(TurnSinNegative(rotationReverseDelay));
        Time.timeScale = 0f;
    }

    IEnumerator TurnSinNegative(float startDelay)
    {
        // Debug.Log("Starting rotation negative in " + startDelay + "seconds");
        yield return new WaitForSecondsRealtime(startDelay);
        Time.timeScale = 1f;

        while (!(Mathf.Abs(currentRotation - (-maxRotation)) <= threshold))
        {
            currentRotation = maxRotation * Mathf.Sin(Time.time * turnSpeed);
            transform.rotation = Quaternion.Euler(currentRotation, currentRotation, currentRotation);
            yield return null;
        }

        StartCoroutine(TurnSinPositive(rotationReverseDelay));
        Time.timeScale = 0f;
    }
}
