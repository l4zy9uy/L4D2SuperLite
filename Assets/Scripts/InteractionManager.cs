using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kiem tra tia sung co trung dan hay khong de lay
public class InteractionManager : MonoBehaviour
{

    public static InteractionManager Instance { get; set; }
    public AmmoBox hoverAmmoBox = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            // lay object bi raycast chieu trung
            GameObject objectHitRaycast = hit.transform.gameObject;
            if (objectHitRaycast.GetComponent<AmmoBox>())
            {
                hoverAmmoBox = objectHitRaycast.gameObject.GetComponent<AmmoBox>();
                hoverAmmoBox.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Gun activeGun = WeaponController.Instance.activeGun.GetComponentInChildren<Gun>();
                    if(activeGun)
                    {
                        switch (hoverAmmoBox.ammoType)
                        {
                            // kiem tra xem thung dan hien tai co phai la dan cua pistol va hien tai dang cam khau pistol hay k
                            case AmmoBox.AmmoType.PistolAmmo:
                                    WeaponController.Instance.weapons[1]._currentBullets += hoverAmmoBox.ammoShotGunAmount;
                                    WeaponController.Instance.weapons[2]._currentBullets += hoverAmmoBox.ammoPistolAmount;
                                Destroy(objectHitRaycast.gameObject);
                                
                                break;
                            case AmmoBox.AmmoType.ShotGunAmmo:                             
                                    WeaponController.Instance.weapons[1]._currentBullets += hoverAmmoBox.ammoShotGunAmount;
                                    Destroy(objectHitRaycast.gameObject);                             
                                break;
                            case AmmoBox.AmmoType.AutoAmmo:
                                    WeaponController.Instance.weapons[0]._currentBullets += hoverAmmoBox.ammoAutoAmount;
                                    Destroy(objectHitRaycast.gameObject);
                                break;
                            case AmmoBox.AmmoType.All:
                                WeaponController.Instance.weapons[0]._currentBullets += hoverAmmoBox.ammoAutoAmount;
                                WeaponController.Instance.weapons[1]._currentBullets += hoverAmmoBox.ammoShotGunAmount;
                                WeaponController.Instance.weapons[2]._currentBullets += hoverAmmoBox.ammoShotGunAmount;
                                Destroy(objectHitRaycast.gameObject);
                                break;
                        }
                    }
                }
            }
            else
            {
                if (hoverAmmoBox)
                {
                    hoverAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }
        }
            
    }    
}
