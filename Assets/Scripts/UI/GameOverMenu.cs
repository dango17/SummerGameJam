using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
    public void LoadMainMenu() {
        Cursor.visible = true;
        FindObjectOfType<Score>().SaveScores();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RestartGame() {
        Cursor.visible = false;
        FindObjectOfType<Score>().SaveScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

	private void Awake() {
        GetComponent<Canvas>().enabled = false;
	}
}
