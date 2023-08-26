using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // To play audio, add in this line of code:
    // FindObjectOfType<AudioManager>().Play("SwitchToAir");

    // Acorn

    // Finished Level

    // Jump
    // - Player Controller 

    // Plant Healing

    // Shoot Light
    // - Player Controller 

    // Shoot Water
    // - Player Controller 

    // Switch to Air
    // - Player Controller 

    // Switch to Light
    // - Player Controller 

    // Switch to Water
    // - Player Controller 

    // Vine Growing

    public Sound[] sounds;

    public static AudioManager instance;

    public string nameOfBackgroundMusic;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play(nameOfBackgroundMusic);
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    void Update()
{
    if (Input.GetKeyUp(KeyCode.Escape))
    {
        Sound s = Array.Find(sounds, sound => sound.name == nameOfBackgroundMusic);
        if (s == null)
        {
            Debug.Log("Sound: " + nameOfBackgroundMusic + " not found!");
            return;
        }

        if (s.source.isPlaying)
        {
            s.source.Pause();
        }
        else
        {
            s.source.Play();
        }
    }
}

}
