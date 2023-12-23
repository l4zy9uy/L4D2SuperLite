using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRecoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 initialGunPosition;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float kickbackz;
    [SerializeField] private float snappiness;
    [SerializeField] private float returnAmount;

    [SerializeField]
    private Transform cam;

    void Start()
    {
        initialGunPosition = transform.localPosition;
    }

    void Update()
    {
        setGunStats();
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnAmount);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);
        cam.localRotation = Quaternion.Euler(currentRotation);
        back();
    }

    public void recoil()
    {
        targetPosition -= new Vector3(0, 0, kickbackz);
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

    void back()
    {
        targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * snappiness);
        transform.localPosition = currentPosition;
    }

    void setGunStats()
    {
        var gunType = WeaponController.Instance.activeGun.GunType;
        if(gunType == GunType.AutomaticGun)
        {
            recoilX = -2f;
            recoilY = 2f;
            recoilZ = 1f;
            kickbackz = 0.2f;
            snappiness = 5f;
            returnAmount = 8f;
        }
        else if(gunType == GunType.Shotgun)
        {
            recoilX = -1f;
            recoilY = 2f;
            recoilZ = 1f;
            kickbackz = 0.02f;
            snappiness = 4f;
            returnAmount = 9f;
        }

    }
}
