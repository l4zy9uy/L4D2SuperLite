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
                        // neu nhat dan thanh cong thi huy hop dan
                        if(activeGun.PickUpAmmo(hoverAmmoBox) == true)
                        {
                            Destroy(objectHitRaycast.gameObject);

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
