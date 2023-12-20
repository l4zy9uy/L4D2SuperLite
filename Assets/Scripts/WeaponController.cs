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
    public Transform hipR;
    public Transform hipL;
    public Transform weaponParent;
    public Animator animator;

    public static WeaponController Instance { get; private set; }
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

    // Start is called before the first frame update
    void Start()
    {
        //default weapon is primary weapon
        switchWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.primaryWeapon)
        {
            switchWeapon(0);
            InputManager.Instance.primaryWeapon = false;
            animator.SetBool("1", true);
            animator.SetBool("2", false);
            animator.SetBool("3", false);
        }
        if(InputManager.Instance.secondaryWeapon)
        {
            switchWeapon(1);
            InputManager.Instance.secondaryWeapon = false;
            animator.SetBool("2", true);
            animator.SetBool("1", false);
            animator.SetBool("3", false);
        }
        if( InputManager.Instance.ternaryWeapon)
        {
            switchWeapon(2);
            InputManager.Instance.ternaryWeapon = false;
            animator.SetBool("3", true);
            animator.SetBool("2", false);
            animator.SetBool("1", false);
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
        recorder.BindComponentsOfType<Transform>(hipL.gameObject, false);
        recorder.BindComponentsOfType<Transform>(hipR.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(activeGun._animationClip);
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
