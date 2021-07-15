using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd : CrowdMaster
{
    PlayerManager player;

    [SerializeField]
    private NavMeshAgent agent;

    [HideInInspector]
    public bool canEat;

    [HideInInspector]
    public bool canExit;

    public GameObject character;

    [SerializeField]
    private GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Brain = GetComponent<StateMachines>();
        foodStall = FindObjectsOfType<Stall>();
        exit = FindObjectsOfType<Exit>();
        entertainment = FindObjectsOfType<Entertainment>();
        Brain.pushState(Idle, OnIdleEnter);
        Hungry = false;
        bored = false;
        NearBarricade = false;
        block = FindObjectOfType<Block>();
        hunger = 100;
        boredom = 20;
        player = FindObjectOfType<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
    {

        NearBarricade = Vector3.Distance(transform.position, block.transform.position) < 3;

        float distanceToFood = float.MaxValue;
        Stall closestStall = foodStall[0];
        foreach (Stall fS in foodStall)
        {
            float distFS = Vector3.Distance(transform.position, fS.transform.position);
            if(distFS < distanceToFood)
            {
                closestStall = fS;
                distanceToFood = distFS;
            }
        }

        float distanceToExit = float.MaxValue;
        Exit closestExit = exit[0];
        foreach(Exit e in exit)
        {
            float distE = Vector3.Distance(transform.position, e.transform.position);
            if(distE < distanceToExit)
            {
                closestExit = e;
                distanceToExit = distE;
            }
        }

        if(hunger <= 50)
        {
            Hungry = true;
            canEat = Vector3.Distance(transform.position, closestStall.transform.position) < 1;
            agent.SetDestination(closestStall.transform.position);
        }

        if(player.playing == true && boredom <= 10)
        {
            bored = true;
            agent.SetDestination(character.transform.position);
        }

        else if (boredom <= 0) 
        {
            //put logic here some thing like
            //a boolean for is the player doing a minigame
            //if yes, set the destination to the player
            //that should hopefully be enough without needing different logic for every minigame;

            // the bool will probably be in the minigame script, player.playing = true
            //then in here
            bored = true;
            agent.SetDestination(closestExit.transform.position);

            if (NearBarricade)
            {
                Brain.pushState(Run, null);
            }

        }

        if (Hungry && canEat)
        {
            Brain.pushState(Eat, OnEatEnter);
        }


    }

    void Run()
    {
        float distance = Vector3.Distance(transform.position, block.transform.position);

        if(distance < pDistance)
        {
            Vector3 blockDir = transform.position - block.transform.position;

            Vector3 newPos = transform.position + blockDir;

            agent.SetDestination(newPos);

        }

        else if (Vector3.Distance(transform.position, block.transform.position) > 5.5f)
        {
            Brain.pushState(Idle, OnIdleEnter);
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
            boredom -= 2;
        }
    }

    void OnEatEnter()
    {
        agent.ResetPath();
    }

    void Eat()
    {
        float distanceToFood = float.MaxValue;
        Stall closestStall = foodStall[0];
        foreach (Stall fS in foodStall)
        {
            float distFS = Vector3.Distance(transform.position, fS.transform.position);
            if (distFS < distanceToFood)
            {
                closestStall = fS;
                distanceToFood = distFS;
            }
        }

        EatTimer -= Time.deltaTime;
        if(EatTimer <= 0)
        {
            closestStall.Eating(2, 1);
            hunger += 10;
            EatTimer = 2f;
        }

        else if (hunger >= 100)
        {
            Hungry = false;
            canEat = Vector3.Distance(transform.position, closestStall.transform.position) < 0.00;
            Brain.pushState(Idle, OnIdleEnter);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            Destroy(this.gameObject);
        }
    }
}
