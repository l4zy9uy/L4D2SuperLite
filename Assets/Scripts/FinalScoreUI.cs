using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreUI : MonoBehaviour
{
    public TextMeshProUGUI finalScore;

    private void Start()
    {               
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(DialogScore.Instance.curScore);
        finalScore.text = DialogScore.Instance.curScore.ToString();
    }
}
