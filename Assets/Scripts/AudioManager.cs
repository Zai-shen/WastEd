using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager Instance { get { return instance; } }
    private static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        foreach (Sound snd in sounds)
        {
            snd.source = gameObject.AddComponent<AudioSource>();
            snd.source.clip = snd.clip;

            snd.source.volume = snd.volume;
            snd.source.pitch = snd.pitch;
            snd.source.loop = snd.loop;
        }
    }

    public void Play(string name)
    {
        Sound snd = Array.Find(sounds, sound => sound.name == name);
        
        if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        snd.source.Play();
    }

    public void PlayWithRandomizedPitch(string name)
    {
        Sound snd = Array.Find(sounds, sound => sound.name == name);

        if (snd == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        snd.source.pitch = UnityEngine.Random.Range(0.6f, .9f);
        snd.source.Play();
    }


    public void PlayIfIsntAlreadyPlaying(string name)
    {
        Sound snd = Array.Find(sounds, sound => sound.name == name);
        
        if (!snd.source.isPlaying) {
            Play(name);
        }
    }

    public void StopAllSounds()
    {
        foreach (Sound playingSnd in sounds)
        {
            playingSnd.source.Stop();
        }
    }

    public void StopAllSoundsBut(string name)
    {
        foreach (Sound playingSnd in sounds)
        {
            if (playingSnd.name != name)
            {
                playingSnd.source.Stop();
            }
        }
    }

    public void ChangeVolume(float cVolume)
    {
        float vol = cVolume / 10;
        Debug.Log("Changing volume to: " + vol);
        AudioListener.volume = vol;
        PlayerPrefs.SetFloat("Volume", vol);
    }

}
