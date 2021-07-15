using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMaster : MonoBehaviour
{
    [HideInInspector]
    public StateMachines Brain;

    [HideInInspector]
    public float change;

    //Will change these name once more crap is added
    [HideInInspector]
    public bool brownNear;
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

    [HideInInspector] public int hunger;
    public int boredom;

    
}
