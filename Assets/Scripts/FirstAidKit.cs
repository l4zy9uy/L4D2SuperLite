using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [Header("HealthBoost")]
    public FirstPersonController player;
    private float healthToGive = 60f;
    private float radius = 2.5f;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius) 
        {
            if(Input.GetKeyDown("f"))
            {
                player.Heal(healthToGive);
                Object.Destroy(gameObject, 1.5f);
            }
        }
    }
}
