using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
    public void LoadMainMenu() {
        FindObjectOfType<Score>().SaveScores();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RestartGame() {
        FindObjectOfType<Score>().SaveScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

	private void Awake() {
        GetComponent<Canvas>().enabled = false;
	}
}
