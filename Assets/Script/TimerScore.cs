using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerScore : MonoBehaviour
{
    [SerializeField] float startTime;

    private void Start()
    {
        startTime = Time.time;
    }
    public void TimerStop()
    {
        this.enabled = false;
    }

    void Update()
    {
        int minuts = (int)((Time.time - startTime) / 60);
        int sec = (int)((Time.time - startTime) - minuts * 60);
        if (sec / 10 == 0)
            this.GetComponent<Text>().text = minuts.ToString() + ":0" + sec.ToString();
        else
            this.GetComponent<Text>().text = minuts.ToString() + ":" + sec.ToString();
    }

    public void SetTime(float timeStart)
    {
        startTime = timeStart;
        Debug.Log(Time.time - startTime);
    }
}
