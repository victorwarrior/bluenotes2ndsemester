using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceBorder : MonoBehaviour
{

    public float volume;
    public float volume2;
    public float volume3;

    public AudioSource ambienceSource;
    public AudioSource ambienceSource2;
    public AudioSource ambienceSource3;

    public AudioClip   audioClip;
    public AudioClip   audioClip2;
    public AudioClip   audioClip3;

    GameObject original;



    private void Start()
    {


        ambienceSource.clip  = audioClip;
        ambienceSource2.clip = audioClip2;
        ambienceSource3.clip = audioClip3;
        if (gameObject.tag == "Forest")
        {
            IEnumerator fadeInSound1 = FadeIn(ambienceSource, 1f, volume);
            ambienceSource.Play();
        }
        else
        {
            original = GameObject.FindGameObjectWithTag("Forest");
        }
    }

    private void Update()
    {
        if (ambienceSource2.isPlaying)
        {
            Debug.Log("isPlaying");
        }

        if (!ambienceSource.isPlaying)
        {
            Debug.Log("notPlaying");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(gameObject.tag == "Swamp")
            {
                Debug.Log("swampsound");
                IEnumerator fadeOutSound1 = FadeOut(ambienceSource, 1f);
                StartCoroutine(fadeOutSound1);

                IEnumerator fadeInSound2  = FadeIn(ambienceSource2, 1f, volume2);
                StartCoroutine(fadeInSound2);
            }

            else if (gameObject.tag == "GraveYard")
            {
                IEnumerator fadeOutSound1 = FadeOut(ambienceSource, 1f);
                StartCoroutine(fadeOutSound1);

                IEnumerator fadeInSound3  = FadeIn(ambienceSource3, 1f, volume3);
                StartCoroutine(fadeInSound3);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Swamp")
            {
                Debug.Log("swampsound");
                IEnumerator fadeOutSound2 = FadeOut(ambienceSource2, 1f);
                StartCoroutine(fadeOutSound2);

                IEnumerator fadeInSound1  = FadeIn(ambienceSource, 1f, volume);
                StartCoroutine(fadeInSound1);
            }

            else if (gameObject.tag == "GraveYard")
            {
                IEnumerator fadeOutSound3 = FadeOut(ambienceSource3, 1f);
                StartCoroutine(fadeOutSound3);

                IEnumerator fadeInSound1 = FadeIn(ambienceSource, 1f, volume);
                StartCoroutine(fadeInSound1);
            }

        }
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            Debug.Log("fadeout");

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float desiredVolume)
    {
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.Play();
        Debug.Log("fadein");
        while (audioSource.volume < desiredVolume)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
            Debug.Log("fadein");
            yield return null;
        }
    }
}
