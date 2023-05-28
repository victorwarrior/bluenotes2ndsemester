using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFade
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
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

    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float desiredVolume)
    {
        audioSource.volume = 0f;
        audioSource.loop   = true;
        audioSource.Play();

        while (audioSource.volume < desiredVolume)
        {
            audioSource.volume +=  Time.deltaTime / fadeTime;
            Debug.Log("fadein");
            yield return null;
        }
    }
}
