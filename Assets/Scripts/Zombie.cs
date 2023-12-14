using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
   [SerializeField] private int HP = 100;
   private Animator animator;

   private NavMeshAgent NavAgent;



   private void Start()
   {
      animator = GetComponent<Animator>();
      NavAgent = GetComponent<NavMeshAgent>();
   }

   public void takeDamage(int damage)
   {
      HP -= damage;
      if (HP <= 0)
      {
         int randomValue = Random.Range(0,2); // 0 or 1

         if(randomValue == 0)
         {
            animator.SetTrigger("DIE1");
         }
         else{
            animator.SetTrigger("DIE2");
         }
      }
      else
      {
         animator.SetTrigger("DAMAGE");
      }
   }
   
   // private void Update()
   // {
   //    if(NavAgent.velocity.magnitude > 0.1f)
   //    {
   //       animator.SetBool("isWalking", true);
   //    }
   //    else
   //    {
   //       animator.SetBool("isWalking", false);
   //    }
   // }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, 2.5f); //Attacking //Stop attacking

      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, 18f); //Detection (Start chasing)

      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position, 21f); //(Stop chasing)


   }
}
