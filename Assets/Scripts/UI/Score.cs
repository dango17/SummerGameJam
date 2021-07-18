using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public int Scored { get; private set; } = 0;

	[SerializeField]
	private string textPrefix = "";
	private Text scoreText = null;

	public void AddScore(int addAmount) {
		Scored += addAmount;
		scoreText.text = textPrefix + Scored.ToString();
	}

	public void SubtractScore(int subtractAmount) {
		Scored -= subtractAmount;
		scoreText.text = textPrefix + Scored.ToString();
	}

	public void ShowScore() {
		scoreText.enabled = true;
	}

	private void Awake() {
		scoreText = GetComponent<Text>();
		scoreText.enabled = false;
	}
}
