using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip triggerClip1;
    public AudioClip triggerClip2;
    public AudioClip triggerClip3;

    float timer;
    bool  stopPlay;

    float timeTillPlay;

    public float resetTime = 5f;
    public float volume = 0.1f;
    public float pitchVariation = 0f;

    private void Start()
    {
        timeTillPlay = 0;

        stopPlay = false;
        timer = 0;
    }

    private void Update()
    {
        timeTillPlay -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (stopPlay)
        {
            if(timer <= 0)
            {
                audioSource.volume -= volume / 10;
                timer = 0.3f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        audioSource.volume = volume;
        if(stopPlay == true)
        {
            stopPlay = false;
        }
        if(collision.gameObject.tag == "Player" && timeTillPlay <= 0)
        {
            audioSource.volume = volume;
            audioSource.pitch  = Random.Range(1 - pitchVariation, 1 + pitchVariation);
            audioSource.clip   = triggerClip1;
            Debug.Log("play");
            if (!(triggerClip1 == null))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(triggerClip1, volume);
                }

            }
            if (!(triggerClip2 == null))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(triggerClip2, volume);
                }
            }

            if (!(triggerClip3 == null))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(triggerClip3, volume);
                }
            }

            timeTillPlay = resetTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stopPlay = true;
    }
}
