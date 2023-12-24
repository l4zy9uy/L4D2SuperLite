using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class TimePlay : MonoBehaviour
{
    public int totalTime = 300;
    public int currentTime = 0;
    public Text txtTime;
    public DialogScore dialogScore;
    public GameObject gameOverUI;

    public static TimePlay Instance { get; set; }
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

            if (currentTime <= 0)

            {

                gameOverUI.gameObject.SetActive(true);

                //het gio setactive cho dialog, dung game va show gia tri

                dialogScore.gameObject.SetActive(true);

                //timescale = 0f : dung` game, = 1f chay bt

                //Time.timeScale = 0f;

                SceneManager.LoadScene("GameOverScene");
                DialogScore.Instance.Show();

            }

            if (Player.Instance.isDead)

            {

                currentTime = 0;

            }

            UpdateTimer(IntToTime(currentTime));

        }

    }
}
