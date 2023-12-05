using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Gun[] weapons = new Gun[3];
    public int weaponIndex;
    public Gun activeGun;
    public Transform leftGunGrip;
    public Transform rightGunGrip;
    public Transform weaponParent;

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

    [ContextMenu("Save weapon pose")]
    void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(leftGunGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(rightGunGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(activeGun._animationClip);
        UnityEditor.AssetDatabase.SaveAssets();
    }
}