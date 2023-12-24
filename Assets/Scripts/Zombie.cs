using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float HP = 100;
    private Animator animator;
    [SerializeField] Transform leftForeArm;
    [SerializeField] Transform rightForeArm;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    private NavMeshAgent NavAgent;
    public bool isDead;

    private void Awake()
    {
        attachTag(leftForeArm);
        attachTag(rightForeArm);
    }

    private void Start()
   {
      animator = GetComponent<Animator>();
      NavAgent = GetComponent<NavMeshAgent>();
        
    }

    private void attachTag(Transform transform)
    {
        if(transform != null)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.tag = "ZombieHand";
                attachTag(t);
            }
        }
    }
    public void takeDamage(float damage)
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
         isDead = true;
         GetComponent<CapsuleCollider>().enabled = false;
         Destroy(gameObject,3f);
      }
      else
      {
         animator.SetTrigger("DAMAGE");
        }
   }

    private void OnDestroy()
    {
        //zomie chet cong diem, update bo dem, set gia tri cho point
        CountingPoint.Instance.point++;
        CountingPoint.Instance.UpdatePointCounter(CountingPoint.Instance.point);
        CountingPoint.Instance.SetPoint(CountingPoint.Instance.point);
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
