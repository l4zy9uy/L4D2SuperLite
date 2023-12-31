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
    public GameObject firstAidKitPrf;
    public GameObject pistolAmmoPrf;
    public GameObject akAmmoPrf;
    public GameObject shotGunAmmoPrf;
    public GameObject allGunAmmoPrf;
    public BoxCollider hand1;
    public BoxCollider hand2;
    public float spawnOffset = 1.0f;
    private bool hasSpawnedPowerup = false;


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
            int randomValue = Random.Range(0, 2); // 0 or 1

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            isDead = true;
            GetComponent<CapsuleCollider>().enabled = false;
            hand1.enabled = false;
            hand2.enabled = false;
            if (!hasSpawnedPowerup)
            {
                Invoke("SpawnPowerup", 3f);
                hasSpawnedPowerup = true;
            }
            Destroy(gameObject, 3f);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
        //(myhh) todo: zombie chet se hien ra hop qua bat ky
    }

    private void SpawnPowerup()
    {
        Debug.Log("random box");
        // Random loại Powerup (1-2: hop dan all, 3-10: first aid kit, 11-15: hop dan shotgun, 
        //                      16-20: hop dan ak, 21-30: hop dan pistol, 31-100: ko ra)
        int powerupType = Random.Range(1, 100);  
        Debug.Log(powerupType);
        Vector3 spawnPosition = transform.position + transform.up * spawnOffset;
        spawnPosition.y = 0;
        if (powerupType <= 9)
        {
            //Xuat hien ammo box all
            Instantiate(allGunAmmoPrf, spawnPosition, Quaternion.identity);
        }
        else if (powerupType > 9 && powerupType <= 10)
        {
            //xuat hien first aid kit   
            Instantiate(firstAidKitPrf, spawnPosition, Quaternion.identity);
        }
        else if (powerupType > 10 && powerupType <= 15)
        {
            //xuat hien ammo box shotgun
            Instantiate(shotGunAmmoPrf, spawnPosition, Quaternion.identity);
        }
        else if (powerupType > 15 && powerupType <= 20)
        {
            //xuat hien ammo box ak
            Instantiate(akAmmoPrf, spawnPosition, Quaternion.identity);
        }
        else if (powerupType > 20 && powerupType <= 30)
        {
            //xuat hien ammo box pistol
            Instantiate(pistolAmmoPrf, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator DelayedSpawnPowerup()
    {
        yield return new WaitForSeconds(3f); // Adjust the delay time as needed
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
