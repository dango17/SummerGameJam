using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMaster : MonoBehaviour
{
    [HideInInspector]
    public StateMachines Brain;

    [HideInInspector]
    public Score score;

    [HideInInspector]
    public Guitar guitarI;

    [HideInInspector]
    public float change;

    public bool blocked;
    public bool NearBarricade;
    public bool nearPlayer;

    [HideInInspector]
    public Block[] block;

    [HideInInspector]
    public bool Hungry;
    [HideInInspector]
    public bool bored;

    [HideInInspector]
    public float boredomChange;

    [HideInInspector]
    public float EatTimer;

    [HideInInspector]
    public Stall[] foodStall;
    [HideInInspector]
    public Entertainment[] entertainment;
    [HideInInspector]
    public Exit[] exit;

    [HideInInspector] public int hunger;
    public int boredom;

    [HideInInspector]
    public float pDistance = 4.0f;


}
