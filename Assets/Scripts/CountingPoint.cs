using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingPoint : MonoBehaviour
{
    public static CountingPoint Instance { get; private set; }
    public Text pointTxt;
    public int point;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        //khi bat dau reset bo dem ve 0
        UpdatePointCounter(0);
    }
    public void UpdatePointCounter(int point)
    {
        if (pointTxt != null)
        {
            pointTxt.text = point.ToString();
        }
    }
    public void SetPoint(int point)
    {
        this.point = point;
    }
}
