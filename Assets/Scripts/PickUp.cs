using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform destination;

    public GameObject heldObj;

    public bool canPickup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    public void PickUpObject()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = destination.position;
        this.transform.parent = GameObject.Find("GameObject").transform;
        heldObj = this.gameObject;
    }

    public void DropObject()
    {
        this.transform.parent = null;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
