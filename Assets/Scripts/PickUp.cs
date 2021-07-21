using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickUp : CrowdMaster {

	public Text pickuptext;
	public Text dropText;
	public float pickupRange;

	public bool holding;
	public bool canPickup;

	private GameObject heldblock;

	public Outline outline;

	private Transform player = null;
	/// <summary>
	/// Position to place picked up objects.
	/// </summary>
	private Transform holdPosition = null;

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		holdPosition = GameObject.FindGameObjectWithTag("HoldPosition").transform;
		block = FindObjectsOfType<Block>();
		outline = GetComponentInChildren<Outline>();
		
		holding = false;
		canPickup = false;
	}

	private void Update() {
		if (block.Length == 0) {
			return;
		}

		float distanceToBlock = float.MaxValue;
		Block closestBlock = block[0];
		foreach (Block bl in block) {
			float distBl = Vector3.Distance(player.transform.position, bl.transform.position);
			if (distBl < distanceToBlock) {
				closestBlock = bl;
				distanceToBlock = distBl;
			}
		}

		if (!holding && distanceToBlock < pickupRange) {
			canPickup = true;
			pickuptext.gameObject.SetActive(true);
			
		}

        else if (holding)
        {
            canPickup = false;
			pickuptext.gameObject.SetActive(false);
			dropText.gameObject.SetActive(true);
		}


		if (distanceToBlock > pickupRange)
        {
			pickuptext.gameObject.SetActive(false);
			canPickup = false;
        }

		if (canPickup == true && distanceToBlock <= pickupRange && !holding)
		{
			outline = closestBlock.GetComponentInChildren<Outline>();

			outline.renderer.material = outline.pickUp;
		}

		if(holding && distanceToBlock <= pickupRange || distanceToBlock >= pickupRange)
        {
			pickuptext.gameObject.SetActive(false);
			outline = closestBlock.GetComponentInChildren<Outline>();

			outline.renderer.material = outline.outOfRange;
		}
	}

	public void PickUpObject() {
		if (block.Length == 0) {
			return;
		}

		holding = true;

		float distanceToBlock = float.MaxValue;
		Block closestBlock = block[0];
		foreach (Block bl in block) {
			float distBl = Vector3.Distance(player.transform.position, bl.transform.position);
			if (distBl < distanceToBlock) {
				closestBlock = bl;
				distanceToBlock = distBl;
			}
		}

		closestBlock.transform.SetParent(holdPosition);
		closestBlock.transform.position = holdPosition.position;

		closestBlock.GetComponent<BoxCollider>().enabled = false;
		closestBlock.GetComponent<Rigidbody>().useGravity = false;
		closestBlock.GetComponent<Rigidbody>().freezeRotation = true;
		closestBlock.GetComponent<Rigidbody>().isKinematic = true;
		

		heldblock = closestBlock.gameObject;
		pickuptext.gameObject.SetActive(false);

	}

	public void DropObject() {


		holding = false;

		heldblock.transform.SetParent(null);
		heldblock.GetComponent<Rigidbody>().useGravity = true;
		heldblock.GetComponent<BoxCollider>().enabled = true;
		heldblock.GetComponent<Rigidbody>().freezeRotation = false;
		heldblock.GetComponent<Rigidbody>().isKinematic = false;

		dropText.gameObject.SetActive(false);


	}
}
