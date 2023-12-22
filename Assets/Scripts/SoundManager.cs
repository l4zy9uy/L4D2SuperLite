using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {
        get;
        set;
    }

    public AudioSource shootingSoundAk47;
    public AudioSource movingPlayer;

    public List<AudioClip> audioSimpleList;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }


    public void PlayFootStep(FirstPersonController controller,bool isPlaying = false)
    {
        if(isPlaying)
        {
            controller.footStep.clip = audioSimpleList[0];
            controller.footStep.Play();
        }
        else
        {
            if(controller.footStep != null)
                controller.footStep.Stop();
        }
    }
}
