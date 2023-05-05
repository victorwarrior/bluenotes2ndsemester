using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip1;
    public AudioClip audioClip2;


    private float clipTime;    // cummulative amount of time of clips.
    private float resetTimer; // time that needs to surpass cliptime to play sound.
    private float startTime; // time the script starts.
    private int walkingInt = 1;

    // Start is called before the first frame update

    void Start()
    {
        clipTime = 0f;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        resetTimer = Time.time - startTime;
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))&& resetTimer >= clipTime)
        {
            if(walkingInt == 1)
            {
                WalkingSound1();
            }
        }
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && resetTimer >= clipTime)
        {
            if (walkingInt == 2)
            {
                WalkingSound2();
            }
        }
    }

   
    public void WalkingSound1()
    {
        audioSource.clip = audioClip1;
        audioSource.Play();
        walkingInt = 2;
        clipTime += audioSource.clip.length;
       // Debug.Log("soundclip nr "+walkingInt+" is playing and the next sound will play at "+clipTime+" current time is "+resetTimer);
    }

    public void WalkingSound2()
    {
        audioSource.clip = audioClip2;
        audioSource.Play();
        walkingInt = 1;
        clipTime += audioSource.clip.length;
      //  Debug.Log("soundclip nr " + walkingInt + " is playing and the next sound will play at " + clipTime + " current time is " + resetTimer);
    }
}
