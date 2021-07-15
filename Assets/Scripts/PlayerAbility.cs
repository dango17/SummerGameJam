using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour {
	public bool Used { get; private set; } = false;

	private float powerLevel = 0.5f;
	private Score score = null;
	private Slider powerLevelSlider = null;

	public void Use() {
		if (Used) {
			return;
		}

		Used = true;
		RaycastHit[] hitInfo;
		hitInfo = Physics.SphereCastAll(new Ray(gameObject.transform.position, Vector3.up), powerLevel, 0.0f);
		
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

	public void IncreasePower(float energy) {
		powerLevel += energy;
		powerLevelSlider.SetValueWithoutNotify(powerLevel);
	}

	public void DecreasePower(float energy) {
		powerLevel -= energy;
		powerLevelSlider.SetValueWithoutNotify(powerLevel);
	}

	private void Start() {
		score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
		powerLevelSlider = GameObject.FindGameObjectWithTag("PowerLevelSlider").GetComponent<Slider>();
	}
}
