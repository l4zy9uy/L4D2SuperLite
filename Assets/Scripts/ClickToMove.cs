using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                // Moves the agent to the point clicked
                navAgent.SetDestination(hit.point);
            }
        }
    }
}
