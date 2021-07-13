using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMaster : MonoBehaviour
{
    [HideInInspector]
    public StateMachines Brain;

    [HideInInspector]
    public float change;

    [SerializeField]
    public GameObject GreenStall;

    //Will change these name once more crap is added
    [HideInInspector]
    public bool GreenNear;
    [HideInInspector]
    public bool brownNear;
    [HideInInspector]
    public bool Hungry;
    [HideInInspector]
    public bool bored;

    public int hunger;
    public int boredom;
}
