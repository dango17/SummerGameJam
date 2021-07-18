﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crowd : CrowdMaster
{
    PlayerManager player;
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;

    [HideInInspector]
    public bool canEat;

    [HideInInspector]
    public bool canExit;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Brain = GetComponent<StateMachines>();
        animator = GetComponent<Animator>();
        foodStall = FindObjectsOfType<Stall>();
        exit = FindObjectsOfType<Exit>();
        entertainment = FindObjectsOfType<Entertainment>();
        Brain.pushState(Idle, OnIdleEnter, null);
        Hungry = false;
        bored = false;
        NearBarricade = false;
        block = FindObjectsOfType<Block>();
        player = FindObjectOfType<PlayerManager>();
        score = FindObjectOfType<Score>();
        guitarI = FindObjectOfType<Guitar>();
    }

    // Update is called once per frame
    void Update()
    {
        nearPlayer = Vector3.Distance(transform.position, character.transform.position) < 4;

        float distanceToBlock = float.MaxValue;
        Block closestBlock = block[0];
        foreach(Block bl in block)
        {
            float distBl = Vector3.Distance(transform.position, bl.transform.position);
            if(distBl < distanceToBlock)
            {
                closestBlock = bl;
                distanceToBlock = distBl;
            }
        }

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

        if (hunger <= 50)
        {
            Hungry = true;
            canEat = Vector3.Distance(transform.position, closestStall.transform.position) < 1;
            agent.SetDestination(closestStall.transform.position);
        }

        if (guitarI.IsMiniGameActive && boredom <= 10)
        {
            agent.SetDestination(character.transform.position);
            animator.SetBool("playing", true);
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
                Brain.pushState(Run, OnRunEnter, OnRunExit);
            }
        }

        if (Hungry && canEat)
        {
            Brain.pushState(Eat, OnEatEnter, null);
        }

        if(bored && nearPlayer)
        {
            Brain.pushState(OnListenEnter, ListenToGuitar, null);
        }

        
    }

    void OnRunEnter()
    {
        animator.SetBool("playing", true);
    }

    void Run()
    {
        float distanceToBlock = float.MaxValue;
        Block closestBlock = block[0];
        foreach (Block bl in block)
        {
            float distBl = Vector3.Distance(transform.position, bl.transform.position);
            if (distBl < distanceToBlock)
            {
                closestBlock = bl;
                distanceToBlock = distBl;
            }
        }

        if (distanceToBlock < pDistance)
        {
            Vector3 blockDir = transform.position - closestBlock.transform.position;

            Vector3 newPos = transform.position + blockDir;

            agent.SetDestination(newPos);

        }

        else if (Vector3.Distance(transform.position, closestBlock.transform.position) > 5.5f)
        {
            Brain.pushState(Idle, OnIdleEnter, null);
        }
    }

    void OnRunExit()
    {
        animator.SetBool("playing", false);
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
            Brain.pushState(Walk, WalkEnter, OnWalkExit);

            change = Random.Range(1, 2);
        }
    }

    void WalkEnter()
    {
        animator.SetBool("walk", true);
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
            Brain.pushState(Idle, OnIdleEnter, null);
            hunger -= 10;
            boredom -= 10;
        }
    }

    void OnWalkExit()
    {
        animator.SetBool("walk", false);
    }

    void OnListenEnter()
    {
        agent.ResetPath();
        animator.SetBool("playing", false);
    }

    void ListenToGuitar()
    {
        boredomChange -= Time.deltaTime;

        if (nearPlayer && boredomChange <= 0)
        {
            boredom += 1;
            boredomChange = 3f;
        }

        if (score.Scored > 5 && guitarI.IsMiniGameActive == true) 
        {
            boredom += 25;
            boredomChange = 5f;
        }

        if (score.Scored < 0)
        {
            boredom -= 3;
            boredomChange = 5f;
        }

        if(guitarI.IsMiniGameActive == false)
        {
            Brain.pushState(Idle, OnIdleEnter, null);
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
            Brain.pushState(Idle, OnIdleEnter, null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("MenuThing"))
        {
            boredom += 1000000;
        }
    }
}
