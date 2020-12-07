using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    public void Play(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist!");
            return;
        }
        s.source.Stop();
    }

    public void UpdateVolumeOfPlayingClips()
    {
        foreach (Sound s in sounds)
        {
            float newVolume = s.volume;
            if (s.audioType == Sound.AudioTypes.music)
            {
                newVolume *= AudioOptionsManager.musicVolume;
                newVolume /= 100;
            }
            else if (s.audioType == Sound.AudioTypes.sound)
            {
                newVolume *= AudioOptionsManager.effectsVolume;
                newVolume /= 100;
            }

            s.source.volume = newVolume;
        }
    }
}