using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePlay : MonoBehaviour
{
    public int totalTime = 300;
    private int currentTime = 0;
    public Text txtTime;
    private void Start()
    {
        currentTime = totalTime;
        StartCoroutine(TimeCountDown());
    }
    public void UpdateTimer(string time)
    {
        txtTime.text = time;
    }
    string IntToTime(int time)
    {
        //chuyen doi time
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    IEnumerator TimeCountDown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            if(currentTime <= 0)
            {
                //xu ly khi het thoi gian
            }
            UpdateTimer(IntToTime(currentTime));
        }
    }
}
