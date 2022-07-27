using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundManager : MonoBehaviourPun
{
    public AudioSource source;
    //public AudioClip foot;
    public List<AudioSource>soundlist =  new List<AudioSource>();

    public void walk(AudioClip sound)
    {
        foreach (AudioSource audio in soundlist)
        {
            if(sound.name == audio.clip.name)
            {
                source.clip = audio.clip;
            }
        }
        source.Play();
    }

    public void stop()
    {
        source.Stop();
    }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
