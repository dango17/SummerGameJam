using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Food : Interactable {
	/// <summary>
	/// Powers the player's ability by increasing it's radius by energy amount.
	/// </summary>
	[Tooltip("Amount of energy the player receives upon consuming the food.")]
	[SerializeField]
	private float energy = 0.2f;

	private ParticleSystem particleEffectSystem = null;
	private AudioSource audioSource = null;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			isUsed = true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>().ChangePowerLevel(energy);
			particleEffectSystem.Play();
			audioSource.Play();
			++timesUsed;
		}
		
		if (timesUsed >= usesAvailable) {
			StartCoroutine(Destroy());
		}
	}

	private void Awake() {
		particleEffectSystem = GetComponent<ParticleSystem>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
	}

	private IEnumerator Destroy() {
		// Wait until method returns true;
		yield return new WaitUntil(ReadyToDestroy);
		Deselect();
		Destroy(gameObject);
	}

	/// <summary>
	/// Returns if the script has completed all it's tasks e.g. playing audio.
	/// </summary>
	/// <returns> True if game object is okay to destroy. </returns>
	private bool ReadyToDestroy() {
		if (!audioSource.isPlaying && !particleEffectSystem.isPlaying) {
			return true;
		}

		return false;
	}
}
