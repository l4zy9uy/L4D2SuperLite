using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int ammoAutoAmount = 60;
    public int ammoShotGunAmount = 15;
    public int ammoPistolAmount = 25;
    public AmmoType ammoType;
    public enum AmmoType
    {
        AutoAmmo,
        ShotGunAmmo,
        PistolAmmo
    }
}
