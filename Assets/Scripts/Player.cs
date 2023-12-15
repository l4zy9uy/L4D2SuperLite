using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public int HP = 100;

   public void takeDamage(int damageAmount)
   {
    HP -= damageAmount;

    if(HP<= 0)
    {
        print("Player is dead");
    }
    else
    {
        print("Player hit");
    }
   }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            Debug.Log("attacked!");
            var hand = other.gameObject.GetComponent<ZombieHand>();
            takeDamage(hand.damage);
        }
    }
}
