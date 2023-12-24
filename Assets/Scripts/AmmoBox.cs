using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAutoAmount = 60;
    public int ammoShotGunAmount = 15;
    public int ammoPistolAmount = 25;
    public AmmoType ammoType;
    public enum AmmoType
    {
        All,
        AutoAmmo,
        ShotGunAmmo,
        PistolAmmo
    }
}
