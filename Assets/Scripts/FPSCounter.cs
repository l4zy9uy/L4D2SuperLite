using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public enum DeltaTimeType
    {
        Smooth,
        Unscaled
    }

    [Tooltip("Unscaled is more accurate, but jumpy, or if your game modifies Time.timeScale. Use Smooth for smoothDeltaTime.")]
    public DeltaTimeType DeltaType = DeltaTimeType.Smooth;

    private Dictionary<int, string> CachedNumberStrings = new();

    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter;
    private int _currentAveraged;

    public static FPSCounter Instance { get; private set; }
    void Awake()
    {
        // Cache strings and create array
        {
            for (int i = 0; i < _cacheNumbersAmount; i++)
            {
                CachedNumberStrings[i] = i.ToString();
            }

            _frameRateSamples = new int[_averageFromAmount];
        }

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

    void Update()
    {
        getSample();
        calculateAverage();
    }
    void calculateAverage()
    {
        var average = 0f;

        foreach (var frameRate in _frameRateSamples)
        {
            average += frameRate;
        }

        _currentAveraged = (int)Math.Round(average / _averageFromAmount);
        _averageCounter = (_averageCounter + 1) % _averageFromAmount;
    }
    void getSample()
    {
        var currentFrame = (int)Math.Round(1f / DeltaType switch
        {
            DeltaTimeType.Smooth => Time.smoothDeltaTime,
            DeltaTimeType.Unscaled => Time.unscaledDeltaTime,
            _ => Time.unscaledDeltaTime
        });
        _frameRateSamples[_averageCounter] = currentFrame;
    }
    public void displayFPS(TextMeshProUGUI FPSText)
    {
        FPSText.text = "FPS: " + _currentAveraged switch
        {
            var x when x >= 0 && x < _cacheNumbersAmount => CachedNumberStrings[x],
            var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
            var x when x < 0 => "< 0",
            _ => "?"
        };
    }
}