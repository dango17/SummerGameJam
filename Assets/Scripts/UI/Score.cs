using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public int Scored { get; private set; } = 0;
	public string ScoreType { get { return scoreType; } private set { } }

	[SerializeField]
	private string textPrefix = "";
	[SerializeField]
	private string scoreType = "";
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

	public void SaveScores() {
		Score[] gameScores = FindObjectsOfType<Score>();

		foreach (Score score in gameScores) {
			// Check if high score has already been set.
			if (PlayerPrefs.HasKey(score.ScoreType)) {
				int highScore = PlayerPrefs.GetInt(score.ScoreType);

				if (score.Scored > highScore) {
					// Set player's new high score.
					PlayerPrefs.SetInt(score.ScoreType, score.Scored);
				}
				// Create new log.
			} else {
				PlayerPrefs.SetInt(score.ScoreType, score.Scored);
			}
		}

		PlayerPrefs.Save();
	}

	private void Awake() {
		scoreText = GetComponent<Text>();
		scoreText.enabled = false;
	}
}
