using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponSway : MonoBehaviour
{
    public float amount;
    public float maxSway;
    public float smoothAmount;

    public float tiltAmount;
    public float maxTiltSway;
    public float smoothAmountTilt;
    public bool tiltDirX, tiltDirY, tiltDirZ;
    public Quaternion swayRot;

    Vector3 initialPos;
    Quaternion initialRot;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        initialRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        tiltSway();
        rotationalSway();
    }

    private void tiltSway()
    {
        float moveX = InputManager.Instance.look.x * amount;
        float moveY = InputManager.Instance.look.y * amount;

        moveX = Mathf.Clamp(moveX, -maxSway, maxSway);
        Vector3 finalPos = new Vector3(moveX, 0, moveY);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initialPos, Time.deltaTime * smoothAmount);
    }

    private void rotationalSway()
    {
        float tiltX = InputManager.Instance.look.x * tiltAmount;
        float tiltY = InputManager.Instance.look.y * tiltAmount;

        tiltX = Mathf.Clamp(tiltX, -maxTiltSway, maxTiltSway);
        tiltY = Mathf.Clamp(tiltY, -maxTiltSway, maxTiltSway);
        Quaternion finalRot = Quaternion.Euler(new Vector3(tiltDirX?-tiltX:0, tiltDirY?-tiltY:0, tiltDirZ?-tiltY:0));
        swayRot = Quaternion.Slerp(transform.localRotation, finalRot * initialRot, Time.deltaTime * smoothAmountTilt);
        transform.localRotation = swayRot;
    }
}
