using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundManager : MonoBehaviourPun
{
    public AudioSource source;


    private void Start()
    {
        //source.rolloffMode = AudioRolloffMode.Logarithmic;
        //source.maxDistance = 100;
        //source.minDistance = 100;
        //source.spatialBlend = 1;
    }

    public void walk(AudioClip sound)
    {
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
