using UnityEngine;
using UnityEngine.UI;

public class PickUp : CrowdMaster {
	public bool CanPickUp { get { return canPickup; } set { } }
	public static PickUp ClosestPickUp { get { return closestBlock.GetComponent<PickUp>(); } private set { } }
	public static GameObject HeldBlock { get; private set; } = null;

	[SerializeField]
	private float pickupRange = 1.25f;

	private static bool holding = false;
	private bool canPickup = false;

	[SerializeField]
	private string pickUpPromptText = "Left Click to Pick Up";
	[SerializeField]
	private string dropPromptText = "Right Click to Drop";

	private static Block closestBlock = null;
	private Transform player = null;
	/// <summary>
	/// Position to place picked up objects.
	/// </summary>
	private Transform holdPosition = null;

	private Text pickuptext = null;
	private Outline closestBlockOutline = null;

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		holdPosition = GameObject.FindGameObjectWithTag("HoldPosition").transform;
		nearbyBlocks = FindObjectsOfType<Block>();
		pickuptext = GameObject.FindGameObjectWithTag("PickUpText").GetComponent<Text>();
	}

	private void Update() {
		if (!holding) {
			if (nearbyBlocks.Length == 0) {
				return;
			}

			float shorestDistanceToBlock = float.MaxValue;
			closestBlock = nearbyBlocks[0];

			foreach (Block block in nearbyBlocks) {
				float distanceToBlock = Vector3.Distance(block.transform.position, player.transform.position);

				if (distanceToBlock < shorestDistanceToBlock) {
					closestBlock = block;
					closestBlockOutline = closestBlock.GetComponentInChildren<Outline>();
					shorestDistanceToBlock = distanceToBlock;
				}
			}

			if (shorestDistanceToBlock <= pickupRange) {
				canPickup = true;
				pickuptext.text = pickUpPromptText;
				closestBlockOutline.renderer.material = closestBlockOutline.pickUp;
			} else {
				canPickup = false;
				pickuptext.text = "";
				closestBlockOutline.renderer.material = closestBlockOutline.outOfRange;
			}
		}
	}

	public void PickUpObject() {
		holding = true;
		HeldBlock = closestBlock.gameObject;
		HeldBlock.transform.SetParent(holdPosition);
		HeldBlock.transform.position = holdPosition.position;
		HeldBlock.GetComponent<BoxCollider>().enabled = false;
		HeldBlock.GetComponent<Rigidbody>().useGravity = false;
		HeldBlock.GetComponent<Rigidbody>().freezeRotation = true;
		HeldBlock.GetComponent<Rigidbody>().isKinematic = true;
		pickuptext.text = dropPromptText;
		canPickup = false;
	}

	public void DropObject() {
		holding = false;
		HeldBlock.transform.SetParent(null);
		HeldBlock.GetComponent<Rigidbody>().useGravity = true;
		HeldBlock.GetComponent<BoxCollider>().enabled = true;
		HeldBlock.GetComponent<Rigidbody>().freezeRotation = false;
		HeldBlock.GetComponent<Rigidbody>().isKinematic = false;
		HeldBlock = null;
		pickuptext.text = "";
	}
}
