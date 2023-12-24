using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public AudioSource menuAudio;
    public AudioSource sfxAudio;
    public AudioClip gunSound;
    public AudioClip footStepSound;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void playGunSound()
    {
        if (sfxAudio)
        {
            sfxAudio.PlayOneShot(gunSound);
        }
    }
    public void PlayFootStep(FirstPersonController controller,bool isPlaying = false)
    {
        if(isPlaying)
        {
            controller.footStep.clip = footStepSound;
            controller.footStep.Play();
        }
        else
        {
            if(controller.footStep != null)
                controller.footStep.Stop();
        }
    }
}
