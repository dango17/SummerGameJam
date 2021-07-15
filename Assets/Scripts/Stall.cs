using UnityEngine;

public class Stall : Interactable {
	public override void Use() {
		if (timesUsed < usesAvailable) {
			Debug.Log("Used Stall: " + gameObject.name);
			isUsed = true;
			++timesUsed;
		}
	}

	//this is for the AI not the player
	[HideInInspector]
	public Health StallHeath;

	private float restockTimer;

    void Start()
    {
		StallHeath = GetComponent<Health>();
		StallHeath.SetHealth(50);
    }

    private void Update()
    {
		restockTimer -= Time.deltaTime;
        if (StallHeath.currentHealth <= 25 && restockTimer <= 0)
        {
			Restock(5, 1);
			restockTimer = 2f;
        }
    }

	public void Eating(int amount, int delay)
    {
		StartCoroutine(StallHeath.DelayDamage(amount, delay));
    }

	public void Restock(int amount, int delay)
    {
		StartCoroutine(StallHeath.Regen(amount, delay));
    }
}
