using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Lobby : MonoBehaviour
{
    public GameObject textDisplay;
    public int seconds=10;
    public bool takingAway=false;

    void Start()
    {
        textDisplay.GetComponent<Text>().text="00:"+seconds;
    }
    void Update()
    {
        if(takingAway==false && seconds >0){
            StartCoroutine(TimerTake());
        }
    }
    IEnumerator TimerTake()
    {
        takingAway=true;
        yield return new WaitForSeconds(1);
        seconds-=1;
        if(seconds<10){
            textDisplay.GetComponent<Text>().text="00:0"+seconds;
        }
        else{
            textDisplay.GetComponent<Text>().text="00:"+seconds;
        }
       
        takingAway=false;
    }
}
