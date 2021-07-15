using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMaster : MonoBehaviour
{
    [HideInInspector]
    public StateMachines Brain;

    [HideInInspector]
    public float change;

    public bool blocked;
    public bool NearBarricade;

    [HideInInspector]
    public Block block;

    [HideInInspector]
    public bool Hungry;
    [HideInInspector]
    public bool bored;

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
