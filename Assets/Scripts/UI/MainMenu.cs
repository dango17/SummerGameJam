using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public void StartGame() {
		Cursor.visible = false;
		// Load the game's gameplay scene.
		SceneManager.LoadScene("Level_Whitebox");
	}

	public void QuitGame() {
		Application.Quit();
	}
}
