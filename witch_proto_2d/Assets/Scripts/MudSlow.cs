using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSlow : MonoBehaviour
{
    float spawnTime;
    float timeActive;
    public float fadeOutTime = 5f;
    public float waitingTime = 0.1f;
    public void Start()
    {
        spawnTime = Time.time;
    }

    public void FixedUpdate()
    {
        timeActive = Time.time - spawnTime;
        if (timeActive > fadeOutTime)
        {
            StartCoroutine(Fade());
        }
        if (GetComponent<Renderer>().material.color.a <= 0.1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Fade()
    {
        //Color c = renderer.material.color;
        Color c = GetComponent<Renderer>().material.color;
        for (float alpha = 1f; alpha>=0f; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(waitingTime);
        }
    }
}
