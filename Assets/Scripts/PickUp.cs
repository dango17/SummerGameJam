using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform player, container;

    public float pickupRange;

    public bool holding;
    public bool canPickup;

    private void Start()
    {
        this.holding = false;
        this.canPickup = false;
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!holding && distanceToPlayer.magnitude < pickupRange)
        {
            this.canPickup = true;
        }

        if (distanceToPlayer.magnitude > pickupRange)
        {
            this.canPickup = false;
        }
    }

    public void PickUpObject()
    {
        this.holding = true;

        this.transform.SetParent(container);
        this.transform.position = container.position;

        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().freezeRotation = true;
        this.GetComponent<Rigidbody>().isKinematic = true;


    }

    public void DropObject()
    {
        holding = false;

        this.transform.SetParent(null);
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().freezeRotation = false;
        this.GetComponent<Rigidbody>().isKinematic = false;

    }
}
