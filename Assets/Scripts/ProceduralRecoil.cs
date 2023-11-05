using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRecoil : MonoBehaviour
{
    [SerializeField]
    private Vector3 currentRotation;
    [SerializeField]
    private Vector3 targetRotation;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 currentPosition;
    [SerializeField]
    private Vector3 initialGunPosition;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] float kickbackz;
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
}
