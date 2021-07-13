using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd : CrowdMaster
{
    playingGuitair guitair;

    [SerializeField]
    private NavMeshAgent agent;

    //DELETE for testing some stuf only
    public GameObject sphere;

    // Start is called before the first frame update
    void Start()
    {
        //will add character reference whem they are in

        agent = GetComponent<NavMeshAgent>();
        Brain = GetComponent<StateMachines>();
        foodStall = FindObjectsOfType<FoodStall>();
        entertainment = FindObjectsOfType<Entertainment>();
        Brain.pushState(Idle, OnIdleEnter);
        Hungry = false;
        bored = false;
        hunger = 100;
        boredom = 20;
        guitair = FindObjectOfType<playingGuitair>();
        
    }

    // Update is called once per frame
    void Update()
    {
     
        //this will be removed
        if (Input.GetKeyDown(KeyCode.E))
        {
            boredom -= 5;
        }

        float distanceToFood = float.MaxValue;
        FoodStall closestStall = foodStall[0];
        foreach (FoodStall fS in foodStall)
        {
            float distFS = Vector3.Distance(transform.position, fS.transform.position);
            if(distFS < distanceToFood)
            {
                closestStall = fS;
                distanceToFood = distFS;
            }
        }

        //float distanceToEntertainment = float.MaxValue;
        //Entertainment closestE = entertainment[0];
        //foreach (Entertainment e in entertainment)
        //{
        //    float distE = Vector3.Distance(transform.position, e.transform.position);
        //    if(distE < distanceToEntertainment)
        //    {
        //        closestE = e;
        //        distanceToEntertainment = distE;
        //    }
        //}
        if(hunger <= 50)
        {
            Hungry = true;
            agent.SetDestination(closestStall.transform.position);
        }

        if (guitair.playing == true && boredom <= 10) 
        {
            //put logic here some thing like
            //a boolean for is the player doing a minigame
            //if yes, set the destination to the player
            //that should hopefully be enough without needing different logic for every minigame;

            // the bool will probably be in the minigame script, player.playing = true
            //then in here

            // if (player.Playing = true && boredom <= 10)
            //{
            //   agent.SetDestination(player.transform.position)
            //}
            //

            bored = true;
            agent.SetDestination(sphere.transform.position);
            
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
            hunger -= 2;
        }
    }
}
