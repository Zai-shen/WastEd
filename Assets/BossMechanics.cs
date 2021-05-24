using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMechanics : MonoBehaviour
{
    public GameObject leftArcher;
    public GameObject rightArcher;
    public GameObject boss;
    public GameObject upperStairPlatform;
    public GameObject midStairPlatform;
    public GameObject lowerStairPlatform;
    public GameObject lowestStairPlatform;

    private AudioManager audioManager;
    private bool movedOnce = false;
    private Vector3 floatDownTarget = new Vector3(0, -1, 0);
    void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public void NotifyTouched(string name)
    {
        if (name.Equals("IBtnL"))
        {
            Debug.Log("IBtnL touched!");
            //Activate left half of monolith

            //Move 1/2 of the additional steps to IBtnM
            MoveHalfStair();

            //Activate left flying archer
            leftArcher.SetActive(true);
        }
        else if (name.Equals("IBtnR"))
        {
            //Activate right half of monolith

            //Move 1/2 of the additional steps to IBtnM
            MoveHalfStair();

            //Activate right flying archer
            rightArcher.SetActive(true);
        }
        else if(name.Equals("IBtnM"))
        {
            //Kill boss
            boss.GetComponent<Archer>().gameObject.SetActive(false);

            //Create death effect
            boss.GetComponent<CreateParticleEffect>().Die();

            //Kill archers
            leftArcher.SetActive(false);
            rightArcher.SetActive(false);

            //Play sounds
            audioManager.Play("Yay");
        }
    }

    private void MoveHalfStair()
    {
        if (movedOnce == false)
        {
            //Move 4 stairs
            StartCoroutine(FloatDown(upperStairPlatform, floatDownTarget, 0.75f));
            StartCoroutine(FloatDown(midStairPlatform, floatDownTarget, 0.75f));
            StartCoroutine(FloatDown(lowerStairPlatform, floatDownTarget, 0.75f));
            StartCoroutine(FloatDown(lowestStairPlatform, floatDownTarget, 0.75f));

            movedOnce = true;
        }
        else
        {
            //Move 3 stairs
            StartCoroutine(FloatDown(midStairPlatform, floatDownTarget, 0.75f));
            StartCoroutine(FloatDown(lowerStairPlatform, floatDownTarget * 2, 0.75f));
            StartCoroutine(FloatDown(lowestStairPlatform, floatDownTarget * 3, 0.75f));
        }
    }

    IEnumerator FloatDown(GameObject child, Vector3 target, float speed)
    {
        Vector3 startPos = child.transform.position;

        float time = 0f;
        while (child.transform.position != startPos + target)
        {
            child.transform.position = Vector3.Lerp(startPos, startPos + target, (time / Vector3.Distance(startPos, startPos + target)) * speed);
            time += Time.deltaTime;

            yield return null;
        }
    }
}
