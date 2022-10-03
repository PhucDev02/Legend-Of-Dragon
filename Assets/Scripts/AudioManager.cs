using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool stopMusic;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = 1;
            s.source.loop = s.loop;
        }
        DontDestroyOnLoad(gameObject);
        stopMusic = false;
        sounds[0].source.Play();
    }

    public Sound[] sounds;

    private void Update()
    {
        if (PlayerPrefs.GetInt("allowMusic") == 0)
        {
            if (stopMusic == false)
                sounds[0].source.UnPause();
            else sounds[0].source.Pause();
        }
        else
        {
            sounds[0].source.Pause();
        }
        //Debug.Log (sounds[0].source.isPlaying) ;
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (PlayerPrefs.GetInt("allowSound") == 0)
        {
            s.source.Play();
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (PlayerPrefs.GetInt("allowSound") == 0)
        {
            s.source.Stop();
        }
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt("allowMusic", 1 - PlayerPrefs.GetInt("allowMusic"));
    }
    public void ToggleSound()
    {
        PlayerPrefs.SetInt("allowSound", 1 - PlayerPrefs.GetInt("allowSound"));
    }

}