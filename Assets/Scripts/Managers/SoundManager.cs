using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static SoundManager _instance = null;
    public static SoundManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    List<AudioSource> currentAudioSources = new List<AudioSource>();
    bool didPlay = false;

    [Header("AudioClips")]
    public AudioMixerGroup soundFXGroup;
    public AudioClip ShotGunShot;
    public AudioClip FireballSpawn;
    public AudioClip FireBallThrow;


    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        currentAudioSources.Add(gameObject.GetComponent<AudioSource>());
    }

    public void Play(AudioClip clip, AudioMixerGroup group)
    {
        foreach(AudioSource source in currentAudioSources)
        {
            if (source.isPlaying)
                continue;

            didPlay = true;
            source.PlayOneShot(clip);
            source.outputAudioMixerGroup = group;
            break;
        }

        if (!didPlay)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            currentAudioSources.Add(temp);
            temp.PlayOneShot(clip);
            temp.outputAudioMixerGroup = group;
        }

        didPlay = false;
    }

    public void PlayGunShot()
    {
        Play(ShotGunShot, soundFXGroup);
    }

    public void PlayFireSpawn()
    {
        Play(FireballSpawn, soundFXGroup);
    }

    public void PlayFireThrow()
    {
        Play(FireBallThrow, soundFXGroup);
    }

    
    
}
