using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammo;

    public WeaponController weaponController;

    public Gun activeGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeGun = weaponController.activeGun;
        _ammo.SetText(activeGun._bulletsLeft + " / " + activeGun._currentBullets);
    }
}