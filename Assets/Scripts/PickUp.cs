using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : CrowdMaster {
	public float pickupRange;

	public bool holding;
	public bool canPickup;

	private GameObject heldblock;

	private Transform player = null;
	/// <summary>
	/// Position to place picked up objects.
	/// </summary>
	private Transform holdPosition = null;

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		holdPosition = GameObject.FindGameObjectWithTag("HoldPosition").transform;
		block = FindObjectsOfType<Block>();

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
		}

        else if (holding && distanceToBlock < pickupRange)
        {
            canPickup = false;
        }

		if (distanceToBlock > pickupRange) {
			canPickup = false;
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
	}

	public void DropObject() {


		holding = false;

		heldblock.transform.SetParent(null);
		heldblock.GetComponent<Rigidbody>().useGravity = true;
		heldblock.GetComponent<BoxCollider>().enabled = true;
		heldblock.GetComponent<Rigidbody>().freezeRotation = false;
		heldblock.GetComponent<Rigidbody>().isKinematic = false;

	}
}
