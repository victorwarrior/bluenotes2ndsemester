using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip sprintAudio;
    public AudioClip grunt1;
    public AudioClip enemyAttack;

    public GameObject forestSound;
    public GameObject swampSound;
    public GameObject graveYardSound;

    GameObject enemyLupine;
    Enemy enemyScript;

    Player playerScript;

    private float resetTimer; // time that needs to surpass cliptime to play sound.
    private int walkingInt = 1;
    public float soundDissipationModifier1;
    public float soundDissipationModifier2;
    public float soundDissipationModifier3;

    // Start is called before the first frame update

    void Start()
    {
        resetTimer = 0f;

        playerScript = GetComponent<Player>();

        enemyLupine = GameObject.FindGameObjectWithTag("Enemy");
        enemyScript = enemyLupine.GetComponent<Enemy>();


    }

    // Update is called once per frame
    void Update()
    {
        forestSound.transform.position = new Vector3(forestSound.transform.position.x, forestSound.transform.position.y, transform.position.z);
        swampSound.transform.position = new Vector3(swampSound.transform.position.x, swampSound.transform.position.y, transform.position.z);
        graveYardSound.transform.position = new Vector3(graveYardSound.transform.position.x, graveYardSound.transform.position.y, transform.position.z);
        resetTimer -= Time.deltaTime;

        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && resetTimer <= 0 && Input.GetKey(KeyCode.LeftShift) && !(playerScript.stamina <= 100))
        {
            walkingInt = 0;
            RunningSound();
        }
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && resetTimer <= 0)
        {
            if (walkingInt == 1)
            {
                WalkingSound1();
            }
        }

        /*  if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && Input.GetKeyDown(KeyCode.LeftShift))
          {
              SprintSound();
          }*/

        if(!(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")))
        {
            audioSource.Stop();
        }


        if (Input.GetKeyUp(KeyCode.LeftShift) || playerScript.stamina <= 100)
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

        if(enemyScript.mode == "attack")
        {
            audioSource3.clip = enemyAttack;
            audioSource3.volume = 0.2f;
            float pitchMod = Random.Range(0.9f, 1.1f);
            audioSource3.pitch = pitchMod;
            audioSource3.Play();
        }
        // else audioSource3.Stop();

        VolumeFunction(forestSound , 0.15f, 130 , soundDissipationModifier1);
        VolumeFunction(swampSound, 0.2f, 80 , soundDissipationModifier2);
        VolumeFunction(graveYardSound, 0.1f, 40, soundDissipationModifier3);

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
        audioSource2.clip   = grunt1;
        audioSource2.pitch  = Random.Range(0.9f, 1.3f);
        audioSource2.volume = Random.Range(0.8f, 1f);
        audioSource2.Play();
    }

    void VolumeFunction(GameObject ambientSound , float volumeMod, float maxDistance, float soundDissipationModifier)
    {
        float distance = Vector3.Distance(ambientSound.transform.position, transform.position);
        AudioSource audioSource = ambientSound.GetComponent<AudioSource>();
        audioSource.volume = volumeMod * Mathf.Exp(-distance/soundDissipationModifier);
        if(distance > maxDistance)
        {
            audioSource.Stop();
        }
        else if (distance < maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
