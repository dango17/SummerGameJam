using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour {
	public bool Used { get; private set; } = false;

	[Tooltip("Minimum diameter for the ability's area of effect.")]
	[SerializeField]
	private float minPowerLevel = 0.0f;
	[Tooltip("Maximum diameter for the ability's area of effect.")]
	[SerializeField]
	private float maxPowerLevel = 10.0f;
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

	/// <summary>
	/// Adds/subtracts the argument amount to/from the power level.
	/// Use positive numbers for addition and negative numbers for subtraction.
	/// </summary>
	/// <param name="energy"> Amount to add/subtract from power level. </param>
	public void ChangePowerLevel(float energy) {
		powerLevel += energy;
		powerLevelSlider.SetValueWithoutNotify(powerLevel);

		if (powerLevel > maxPowerLevel) {
			powerLevel = maxPowerLevel;
		} else if (powerLevel < minPowerLevel) {
			powerLevel = minPowerLevel;
		}
	}

	private void Start() {
		score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
		powerLevelSlider = GameObject.FindGameObjectWithTag("PowerLevelSlider").GetComponent<Slider>();
		powerLevelSlider.minValue = minPowerLevel;
		powerLevelSlider.maxValue = maxPowerLevel;
	}
}
