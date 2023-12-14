using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unactiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;


    private void Awake()
    {
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
    //[SerializeField] private TextMeshProUGUI _ammo;
    private Gun _activeGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gun activeGun = WeaponController.Instance.activeGun.GetComponentInChildren<Gun>();
        if(activeGun)
        {
            magazineAmmoUI.text = $"{activeGun.bulletsLeft}";
            totalAmmoUI.text = $"{activeGun._currentBullets}";

            GunType model;
            if(WeaponController.Instance.activeGun._magazineSize == 30)
            {
                model = GunType.AutomaticGun;
            }
            else if(WeaponController.Instance.activeGun._magazineSize == 10)
            {
                model = GunType.Shotgun;
            }
            else 
            {
                model = GunType.PistolGun;
            }
            ammoTypeUI.sprite = GetAmmoSprite(model);
            activeWeaponUI.sprite = GetWeaponSprite(model);
        }

       /* _activeGun = WeaponController.Instance.activeGun;
        _ammo.SetText(_activeGun.bulletsLeft + " / " + _activeGun._currentBullets);*/
    }

    private Sprite GetWeaponSprite(GunType model)
    {
        if (WeaponController.Instance.activeGun._magazineSize == 30)
        {
            model = GunType.AutomaticGun;
            return Instantiate(Resources.Load<GameObject>("Automatic_Gun").GetComponent<SpriteRenderer>().sprite);
        }
        else if (WeaponController.Instance.activeGun._magazineSize == 10)
        {
            model = GunType.Shotgun;
            return Instantiate(Resources.Load<GameObject>("ShotGun_Gun").GetComponent<SpriteRenderer>().sprite);
        }
        else
        {
            model = GunType.PistolGun;
            return Instantiate(Resources.Load<GameObject>("Pistol_Gun").GetComponent<SpriteRenderer>().sprite);
        }
    }

    private Sprite GetAmmoSprite(GunType model)
    {
        if (WeaponController.Instance.activeGun._magazineSize == 30)
        {
            model = GunType.AutomaticGun;
            return Instantiate(Resources.Load<GameObject>("Automatic_Ammo").GetComponent<SpriteRenderer>().sprite);
        }
        else if (WeaponController.Instance.activeGun._magazineSize == 10)
        {
            model = GunType.Shotgun;
            return Instantiate(Resources.Load<GameObject>("ShotGun_Ammo").GetComponent<SpriteRenderer>().sprite);
        }
        else
        {
            model = GunType.PistolGun;
            return Instantiate(Resources.Load<GameObject>("Pistol_Ammo").GetComponent<SpriteRenderer>().sprite);
        }
    }
}
