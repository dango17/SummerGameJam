using UnityEngine;
using UnityEngine.UI;

public class Stall : Interactable {
	//this is for the AI not the player
	[HideInInspector]
	public Health StallHeath;
	
	private float restockTimer;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			isUsed = true;
			++timesUsed;
		}
	}

	public void Eating(int amount, int delay) {
		StartCoroutine(StallHeath.DelayDamage(amount, delay));
	}

	public void Restock(int amount, int delay) {
		StartCoroutine(StallHeath.Regen(amount, delay));
	}

	private void Awake() {
		StallHeath = GetComponent<Health>();
		StallHeath.SetHealth(50);
	}

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
	}

	private void Update() {
		restockTimer -= Time.deltaTime;
		if (StallHeath.currentHealth <= 25 && restockTimer <= 0) {
			Restock(5, 1);
			restockTimer = 2f;
		}
	}
}
