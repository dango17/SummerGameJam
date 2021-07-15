using UnityEngine;

public class PlayerAbility : MonoBehaviour {
	public bool Used { get; private set; } = false;

	private int powerLevel = 1;
	private Score score = null;

	public void Use() {
		if (Used) {
			return;
		}

		Used = true;
		RaycastHit[] hitInfo;
		hitInfo = Physics.SphereCastAll(new Ray(gameObject.transform.position, Vector3.up), powerLevel, 10.0f);
		
		foreach (RaycastHit hit in hitInfo) {
			if (hit.collider.CompareTag("AI")) {
				// TODO: send AI into ragdoll state
				const int scoreAmount = 10;
				score.AddScore(scoreAmount);
			}
		}

		score.ShowScore();
		// TODO: restart game
	}

	public void IncreasePower(int energy) {
		powerLevel += energy;
		// TODO: Update power level UI slider.
	}

	public void DecreasePower(int energy) {
		powerLevel -= energy;
		// TODO: Update power level UI slider.
	}

	private void Start() {
		score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
	}
}
