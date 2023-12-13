using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
   [SerializeField] private int HP = 100;
   //private Animator animator;

   private void Start()
   {
      //animator = GetComponent<Animator>();
   }

   public void takeDamage(int damage)
   {
      HP -= damage;
      if (HP <= 0)
      {
         //animator.SetTrigger("DIE");
         Destroy(gameObject);
      }
      else
      {
         //animator.SetTrigger("DAMAGE");
      }
   }
}
