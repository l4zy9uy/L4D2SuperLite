using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{

    [Header("Bar Setting")]
    public Color BarColor;   
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public int Alert = 20;
    public Color BarAlertColor;

    [Header("Sound Alert")]
    public AudioClip sound;
    public bool repeat = true;
    public float RepeatRate = 1f;

    private Image bar, barBackground;
    private float nextPlay;
    private AudioSource audiosource;
    private Text txtTitle;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);
        }
    }

        

    private void Awake()
{
    // Kiểm tra xem thành phần có tồn tại không
    Transform barTransform = transform.Find("Bar");
    if (barTransform != null)
    {
        bar = barTransform.GetComponent<Image>();
    }

    // Kiểm tra xem thành phần có tồn tại không
    barBackground = GetComponent<Image>();

    // Kiểm tra xem thành phần có tồn tại không
    Transform backgroundTransform = transform.Find("BarBackground");
    if (backgroundTransform != null)
    {
        barBackground = backgroundTransform.GetComponent<Image>();
    }

    // Kiểm tra xem thành phần có tồn tại không
    audiosource = GetComponent<AudioSource>();
}

    private void Start()
    {
        bar.color = BarColor;
        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;
        UpdateValue(barValue);
    }

    private bool isBeepPlaying = false;
    public void UpdateValue(float val)
    {
        UnityEngine.Debug.Log("Update value...");
        bar.fillAmount = val / 100;
        barValue = val;
        UnityEngine.Debug.Log("health:" + barValue);

        if (Alert >= val)
        {
            bar.color = BarAlertColor;
            if (Alert >= barValue && Time.time > nextPlay && !isBeepPlaying)
            {
                UnityEngine.Debug.Log("Playing beep sound.");
                nextPlay = Time.time + RepeatRate;
                audiosource.clip = sound;
                audiosource.Play();
                isBeepPlaying = true;
            }
        }
        else
        {
            if (isBeepPlaying) 
            {
                UnityEngine.Debug.Log("Stopping beep sound.");
                audiosource.Stop();
                isBeepPlaying = false;
            }
            bar.color = BarColor;
        }

    }


    private void Update()
    {
        if (!Application.isPlaying)
        {           
            //pdateValue(50);

            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;

            barBackground.sprite = BarBackGroundSprite;           
        }
        else
        {
            
        }
    }

}
