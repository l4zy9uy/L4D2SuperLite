using System.Collections;
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

    private Image bar;
    private Image barBackground;
    private AudioSource audiosource;
    private float barValue;

    private float nextPlay;
    private bool isBeepPlaying;

    public float BarValue
    {
        get { return barValue; }
        set
        {
            barValue = Mathf.Clamp(value, 0, 100);
            UpdateValue(barValue);
        }
    }

    private void Awake()
    {
        Transform barTransform = transform.Find("Bar");
        bar = barTransform != null ? barTransform.GetComponent<Image>() : null;

        barBackground = GetComponent<Image>();
        Transform backgroundTransform = transform.Find("BarBackground");
        barBackground = backgroundTransform != null ? backgroundTransform.GetComponent<Image>() : null;

        audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        bar.color = BarColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;
        UpdateValue(barValue);
    }

    public void UpdateValue(float val)
    {
        bar.fillAmount = val / 100;
        barValue = val;

        if (Alert >= val)
        {
            bar.color = BarAlertColor;
            if (Alert >= barValue && Time.time > nextPlay && !isBeepPlaying)
            {
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
                audiosource.Stop();
                isBeepPlaying = false;
            }
            bar.color = BarColor;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
        {
            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;
            barBackground.sprite = BarBackGroundSprite;
        }
    }
#endif
}