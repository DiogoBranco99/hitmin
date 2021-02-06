using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudMusic : MonoBehaviour
{
    //public GameObject combatMusic;
    public AudioClip combatMusicAudio;
    public AudioClip backgroundSound;
    private AudioSource audioSource;
    private bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        //combatMusicAudio = combatMusic.GetComponent<AudioSource>();
        //backgroundSound = GetComponent<AudioSource>();
        PlayRegularAudioClip();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing)
        {
            PlayRegularAudioClip();
            playing = true;
        } else
        {
            audioSource.Stop();
            playing = false;
        }
    }

    public void PlayRegularAudioClip()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.PlayOneShot(backgroundSound);
    }

    public void PlayHighDamageAudioClip()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.PlayOneShot(combatMusicAudio);
    }
}
