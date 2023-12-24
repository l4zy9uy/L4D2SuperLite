using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScore : MonoBehaviour
{
    public Text txtHighScore;
    public Text txtCurScore;

    public int highScore = 0;
    public int curScore = 0;

    public static DialogScore Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        //khi dialog dc active goi func check
        CheckHighScore(CountingPoint.Instance.point);
    }
    public void Show()
    {
        //gan score vao text tren dialog
        txtCurScore.text = "CurrentScore: " + curScore;
        txtHighScore.text = "HighScore: " + PlayerPrefs.GetInt("High_Score");
    }
    public void CheckHighScore(int curScore)
    {
        this.curScore = curScore;
        //neu currScore > high score da dc luu trong playerprefs thi dat lai highscore =currscore roi luu lai vao playerprefs
        if(curScore > PlayerPrefs.GetInt("High_Score"))
        {
            highScore = curScore;
            PlayerPrefs.SetInt("High_Score", highScore);
        }
        else
        {
            PlayerPrefs.SetInt("Hight_Score", highScore);
        }
    }

}
