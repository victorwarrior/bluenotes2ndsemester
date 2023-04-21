using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource audioSource;
    AudioClip audioClip1;
    public AudioClip audioClip2;

    private float waitingTime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip1 = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            StartCoroutine(MovementPlayer());
            //Debug.Log("coroutine started");
        }
        else if (!(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")))
        {
            StopCoroutine(MovementPlayer());
        }
    }

    IEnumerator MovementPlayer()
    {
          for(int i = 0; i < 100; i++)
        {
            audioSource.Play();
            if(audioSource.clip == audioClip1)
            {
                audioSource.clip = audioClip2;
                waitingTime      = audioSource.clip.length;
                //Debug.Log("Sound playing");
            }
            else if(audioSource.clip == audioClip2)
            {
                audioSource.clip = audioClip1;
                waitingTime      = audioSource.clip.length;
            }
        }
        yield return new WaitForSeconds(waitingTime);
    }
}
