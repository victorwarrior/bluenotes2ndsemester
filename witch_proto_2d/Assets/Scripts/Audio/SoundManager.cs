using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Sound[] musicSounds, effectsSounds;
    public AudioSource _musicSource, _effectsSource;
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float effectsVolume;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
     
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _musicSource.clip = s.clip;
            _musicSource.Play();
        }
    }

    public void PlayEffects(string name)
    {
        

        Sound s = Array.Find(effectsSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _effectsSource.volume = effectsVolume;

            if (name == "ProjectileHit")
                _effectsSource.pitch = (Random.Range(0.95f, 1.05f));

            if (name == "Sailing")
            _effectsSource.pitch     = (Random.Range(0.8f, 1f));
            _effectsSource.PlayOneShot(s.clip);
        }
    }



    public void PlayPauseEffect(string name)
    {
        Sound s = Array.Find(effectsSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else if (_effectsSource.isPlaying)
            _effectsSource.Pause();
        else
            _effectsSource.Play();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
