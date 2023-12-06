using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrolingState : StateMachineBehaviour
{

    float timer;
    public float patrolingTime = 10f;
    Transform player;
    NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //--- Initialize Waypoints ---//
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();

       agent.speed = patrolSpeed;
       timer = 0;
       //-- Move to the first waypoint ---//
       GameObject wayPointCluster = GameObject.FindGameObjectWithTag("wayPoint");
       foreach (Transform t in wayPointCluster.transform)
       {
           waypointsList.Add(t);
       }

       Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
       agent.SetDestination(nextPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //--- If agent arrived at waypoint, move to next waypoint ---//
       if (agent.remainingDistance <= agent.stoppingDistance)
       {
           agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
       }
        //--- Transition to Idle State --//
        timer += Time.deltaTime;
        if (timer > patrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }
        
        //--- Transition to Chase State ---//
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        
        if(distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //Stop the agent
       agent.SetDestination(agent.transform.position);
    }
}
