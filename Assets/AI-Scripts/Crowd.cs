using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd : MonoBehaviour
{
    
    private StateMachines Brain;

    [SerializeField]
    private NavMeshAgent agent;

    [HideInInspector]
    public float change;

    [SerializeField]
    private GameObject GreenStall;

    
    private bool GreenNear;

    // Start is called before the first frame update
    void Start()
    {
        //will add character reference whem they are in

        agent = GetComponent<NavMeshAgent>();
        Brain = GetComponent<StateMachines>();
        Brain.pushState(Idle, OnIdleEnter);
    }

    // Update is called once per frame
    void Update()
    {
        GreenNear = Vector3.Distance(transform.position, GreenStall.transform.position) < 10;

        if (Input.GetKeyDown(KeyCode.E) && GreenNear)
        {
            agent.SetDestination(GreenStall.transform.position);
        }
    }

    void OnIdleEnter()
    {
        agent.ResetPath();
    }

    void Idle()
    {
        change -= Time.deltaTime;
        if(change <= 0) 
        {
            Brain.pushState(Walk, WalkEnter);

            change = Random.Range(1, 2);
        
        }
    }

    void WalkEnter()
    {
        //every few seconds walk in a random direction
        Vector3 walkDirection = (Random.insideUnitSphere * 8f) + transform.position;
        NavMesh.SamplePosition(walkDirection, out NavMeshHit navHit, 3f, NavMesh.AllAreas);
        Vector3 destination = navHit.position;
        agent.SetDestination(destination);
    }

    void Walk()
    {
        //stop just before the destination, to prevent problems
        if(agent.remainingDistance <= 0.25f)
        {
            agent.ResetPath();
            Brain.pushState(Idle, OnIdleEnter);
        }
    }
}
