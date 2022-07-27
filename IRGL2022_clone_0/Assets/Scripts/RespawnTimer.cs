using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour
{
    public float time = 10;
    public Text timerText;
    public ShrinkLogic shrinklogic;
    
    // Update is called once per frame
    void Update()
    {
        time = shrinklogic.shrinkDelay - shrinklogic.totalTime;
        Display(time);

    }
    void Display(float timedisplay)
    {
        float minute=Mathf.FloorToInt(timedisplay/60);
        float seconds=Mathf.FloorToInt(timedisplay%60);
        timerText.text=string.Format("{0:00}:{1:00}",minute,seconds);
    }
}
