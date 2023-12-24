using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {
        get;
        set;
    }
    public AudioSource menuAudio;
    public AudioSource sfxAudo;

    public AudioClip tiengSung;
    public AudioClip tiengChan;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void PlayTiengSung()
    {
        if (sfxAudo)
        {
            sfxAudo.PlayOneShot(tiengSung);
        }
    }
    public void PlayFootStep(FirstPersonController controller,bool isPlaying = false)
    {
        if(isPlaying)
        {
            controller.footStep.clip = tiengChan;
            controller.footStep.Play();
        }
        else
        {
            if(controller.footStep != null)
                controller.footStep.Stop();
        }
    }
}
