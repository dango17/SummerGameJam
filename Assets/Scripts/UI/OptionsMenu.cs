using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
	[SerializeField]
	private Slider sensitivitySlider = null;

	public void SetCursorSensitivity() {
		PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
	}

	private void Awake() {
		if (PlayerPrefs.HasKey("Sensitivity")) {
			sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
		}
	}
}
