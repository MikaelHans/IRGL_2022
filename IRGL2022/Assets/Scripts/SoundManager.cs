using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundManager : MonoBehaviourPun
{
    public AudioSource source;
    //public AudioClip foot;

    public void walk(AudioClip sound){
        source.clip = sound;
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
