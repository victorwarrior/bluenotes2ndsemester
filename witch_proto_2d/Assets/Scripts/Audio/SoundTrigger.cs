using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip triggerClip1;

    float timeTillPlay;

    public float resetTime = 5f;
    public float volume = 0.1f;
    public float pitchVariation = 0f;

    private void Start()
    {
        timeTillPlay = 0;
    }

    private void Update()
    {
        timeTillPlay -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && timeTillPlay <= 0)
        {
            audioSource.volume = volume;
            audioSource.pitch  = Random.Range(1 - pitchVariation, 1 + pitchVariation);
            audioSource.clip   = triggerClip1;
            audioSource.Play();
            
            timeTillPlay = resetTime;
        }
    }
}
