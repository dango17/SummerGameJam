using UnityEngine;
using TMPro;

public class HighScores : MonoBehaviour {
	[SerializeField]
	private string[] highScoreKeys = null;
	private int[] highScores = null;
	[SerializeField]
	private GameObject highScoreTableElementPrefab = null;

	public bool LoadScores() {
		int counter = 0;

		foreach (string highScoreKey in highScoreKeys) {
			if (PlayerPrefs.HasKey(highScoreKey)) {
				++counter;
				int highScore = PlayerPrefs.GetInt(highScoreKey);
			}
		}

		highScores = new int[counter];
		int index = 0;

		foreach (string highScoreKey in highScoreKeys) {
			highScores[index++] = PlayerPrefs.GetInt(highScoreKey);
		}

		if (highScores.Length > 0) {
			return true;
		}

		return false;
	}

	public void DisplayScores() {
		int index = 0;

		foreach (string highScoreKey in highScoreKeys) {
			TextMeshProUGUI tableElementText = Instantiate(highScoreTableElementPrefab,
				transform.position,
				Quaternion.identity,
				transform).GetComponent<TextMeshProUGUI>();
			tableElementText.text = highScoreKey + " " + highScores[index++];
		}
	}

	private void Awake() {
		if (LoadScores()) {
			DisplayScores();
		}
	}
}
