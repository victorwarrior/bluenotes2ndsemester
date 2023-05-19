using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip sprintAudio;
    public AudioClip grunt1;


    private float clipTime;    // cummulative amount of time of clips.
    private float resetTimer; // time that needs to surpass cliptime to play sound.
    private int walkingInt = 1;

    // Start is called before the first frame update

    void Start()
    {
        resetTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        resetTimer -= Time.deltaTime;
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))&& resetTimer <= 0)
        {
            if(walkingInt == 1)
            {
                WalkingSound1();
            }
        }
        
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintSound();
        }
        
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && resetTimer <= 0 && Input.GetKey(KeyCode.LeftShift))
        {
            walkingInt = 0;
            RunningSound();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkingInt = 1;
        }

       /* if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && resetTimer <= 0)
        {
            if (walkingInt == 2)
            {
                WalkingSound2();
            }
        }*/


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GruntSound1();
        }
    }
    public void SprintSound()
    {
        audioSource2.clip = sprintAudio;
        audioSource2.volume = 0.1f;
        audioSource2.Play();
    }

    public void RunningSound()
    {
        audioSource.clip = footStep2;
        audioSource.volume = 0.3f;
        audioSource.Play();
        resetTimer = audioSource.clip.length;
    }
    public void WalkingSound1()
    {
        audioSource.clip = footStep1;
        audioSource.volume = 0.3f;
        audioSource.Play();
       // walkingInt = 2;
        resetTimer = audioSource.clip.length;
    }

    public void WalkingSound2()
    {
        audioSource.clip = footStep1;
        audioSource.Play();
        walkingInt = 1;
        resetTimer = audioSource.clip.length;
    }

    public void GruntSound1()
    {
        audioSource2.clip = grunt1;
        audioSource2.Play();
    }
}
