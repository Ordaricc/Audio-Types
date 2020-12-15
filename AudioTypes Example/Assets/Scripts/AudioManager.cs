using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
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

            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

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

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
    }
}