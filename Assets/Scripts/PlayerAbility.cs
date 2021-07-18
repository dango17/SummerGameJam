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
	public float powerLevel = 0.5f;
	private Score totalGameScore = null;
	private Score aiStunnedScore = null;
	private Slider powerLevelSlider = null;
	private Canvas gameOverMenu = null;
	private AudioSource source = null;

	[SerializeField]
	private ParticleSystem fartPart1 = null;
	[SerializeField]
	private ParticleSystem fartPart2 = null;

	public void Use() {
		if (Used) {
			return;
		}

		Used = true;
		GetComponent<InputManager>().SwitchInputMode(InputManager.InputModes.GameOver);
		RaycastHit[] hitInfo;
		hitInfo = Physics.SphereCastAll(new Ray(gameObject.transform.position, Vector3.up), powerLevel, 0.0f);
		int aiStunned = 0;

		foreach (RaycastHit hit in hitInfo) {
			if (hit.collider.CompareTag("AI")) {
				++aiStunned;
				const int scoreAmount = 10;
				totalGameScore.AddScore(scoreAmount);
				Vector3 spawnOffset = Vector3.up * 0.2f;
				Instantiate(hit.collider.GetComponent<Crowd>().RagdollCharacter,
					hit.collider.gameObject.transform.position + spawnOffset,
					Quaternion.identity);
				Destroy(hit.collider.gameObject);
			}
		}

		aiStunnedScore.AddScore(aiStunned);
		// Play fart particle effects.
		fartPart1.startLifetime = powerLevel;
		fartPart2.startLifetime = powerLevel;
		fartPart1.Play();
		fartPart2.Play();
		// Play fart sfx.
		source.Play();
		totalGameScore.ShowScore();
		aiStunnedScore.ShowScore();
		gameOverMenu.enabled = true;
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
		source = GetComponent<AudioSource>();
		totalGameScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
		aiStunnedScore = GameObject.FindGameObjectWithTag("AIStunned").GetComponent<Score>();
		powerLevelSlider = GameObject.FindGameObjectWithTag("PowerLevelSlider").GetComponent<Slider>();
		powerLevelSlider.minValue = minPowerLevel;
		powerLevelSlider.maxValue = maxPowerLevel;
		gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu").GetComponent<Canvas>();
	}
}
