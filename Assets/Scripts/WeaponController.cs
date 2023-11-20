using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Gun[] weapons = new Gun[3];
    public int weaponIndex;
    public Gun activeGun;

    // Start is called before the first frame update
    void Start()
    {
        switchWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.primaryWeapon)
        {
            switchWeapon(0);
            InputManager.Instance.primaryWeapon = false;
        }
        if(InputManager.Instance.secondaryWeapon)
        {
            switchWeapon(1);
            InputManager.Instance.secondaryWeapon = false;
        }
        if( InputManager.Instance.ternaryWeapon)
        {
            switchWeapon(2);
            InputManager.Instance.ternaryWeapon = false;
        }
    }

    public void switchWeapon(int index)
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
        weapons[index].gameObject.SetActive(true);
        weaponIndex = index;
        activeGun = weapons[weaponIndex];
    }
}
