using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    AudioSource audioSource;


    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckPointManagerScript.currentCheckpoint = transform.position;
            CheckPointManagerScript.hasCheckPoint = true;
            if (!hasActivated)
            {
                audioSource = GetComponent<AudioSource>();
                audioSource.volume = 0.2f;
                audioSource.Play();
                hasActivated = true;
            }
        }
    }
}
