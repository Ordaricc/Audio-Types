using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AudioTypes { soundEffect, music }
    public AudioTypes audioType;

    [HideInInspector] public AudioSource source;
    public string clipName;
    public AudioClip audioClip;
    public bool isLoop;
    public bool playOnAwake;

    [Range(0, 1)]
    public float volume = 0.5f;
}