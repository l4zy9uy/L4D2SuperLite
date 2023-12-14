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

   private void OnTriggerEnter(Collider other)
   {
    if (other.CompareTag("ZombieHand"))
    {
        takeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
    }
   }
}
