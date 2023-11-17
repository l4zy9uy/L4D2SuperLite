using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[3];
    public int weaponIndex;

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
            weapons[i].SetActive(false);
        }
        weapons[index].SetActive(true);
        weaponIndex = index;
    }
}
