using MyInputManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyInputManager
{
    public class WeaponBobbing : MonoBehaviour
    {
        public FirstPersonController firstPersonController;
        public WeaponSway weaponSway;

        [Header("Bobbing")]
        public float speedCurve;
        float curveSin { get => Mathf.Sin(speedCurve); }
        float curveCos { get => Mathf.Cos(speedCurve); }

        public float smooth = 10f;
        float smoothRot = 12f;
        public Vector3 travelLimit = Vector3.one * 0.025f;
        public Vector3 bobLimit = Vector3.one * 0.01f;
        Vector3 bobPosition;

        public float bobExaggeration;

        [Header("Bob Rotation")]
        public Vector3 multiplier;
        Vector3 bobEulerRotation;

        private void BobOffset()
        {
            speedCurve += Time.deltaTime * (firstPersonController.Grounded ? (MyInputManager.Instance.move.x + MyInputManager.Instance.move.y) * bobExaggeration : 1f) + 0.01f;

            bobPosition.x = (curveCos * bobLimit.x * (firstPersonController.Grounded ? 1 : 0)) - (MyInputManager.Instance.move.x * travelLimit.x);
            bobPosition.y = (curveSin * bobLimit.y) - (MyInputManager.Instance.move.y * travelLimit.y);
            bobPosition.z = -(MyInputManager.Instance.move.y * travelLimit.z);
        }

        private void BobRotation()
        {
            bobEulerRotation.x = (MyInputManager.Instance.move != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : multiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
            bobEulerRotation.y = (MyInputManager.Instance.move != Vector2.zero ? multiplier.y * curveCos : 0);
            bobEulerRotation.z = (MyInputManager.Instance.move != Vector2.zero ? multiplier.z * curveCos * MyInputManager.Instance.move.x : 0);
        }

        void CompositePositionRotation()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition+bobPosition, Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, weaponSway.swayRot*Quaternion.Euler(bobEulerRotation), Time.deltaTime * smoothRot);
        }

        // Update is called once per frame
        void Update()
        {
            BobOffset();
            BobRotation();
            CompositePositionRotation();
            Invoke("result", 1.0f);
        }

        private void result()
        {
            Debug.Log("rotation " + bobEulerRotation);
            Debug.Log("position " + bobPosition);
            Debug.Log("move " + Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
        }
    }
}
