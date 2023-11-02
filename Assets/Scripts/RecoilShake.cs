using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilShake : MonoBehaviour
{
    [SerializeField]
    CinemachineImpulseSource screenShake;

    public void ScreenShake(Vector3 dir)
    {
        screenShake.GenerateImpulseWithVelocity(dir);
    }
}
