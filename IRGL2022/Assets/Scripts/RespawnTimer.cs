using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour
{
    public float time = 10;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time>0){
            time-=Time.deltaTime;
        }
        else{
            time=0;
        }
        Display(time);
    }
    void Display(float timedisplay){
        if(time<0){
            time=0;
        }
        float minute=Mathf.FloorToInt(timedisplay/60);
        float seconds=Mathf.FloorToInt(timedisplay%60);
        timerText.text=string.Format("{0:00}:{1:00}",minute,seconds);
    }
}
