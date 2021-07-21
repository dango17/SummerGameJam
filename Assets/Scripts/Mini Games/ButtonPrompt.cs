using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour {
	public enum ButtonTypes {
		Press,
		MultiplePress,
		Hold,
		Count
	}

	public int TimeToActivate { get { return TimeToActivate; } set { timeToActivate = value; } }
	public bool DestroyOnUse { get { return destroyOnUse; } set { destroyOnUse = value; } }
	public bool ShowProgressBar { get { return showProgressBar; } set { showProgressBar = value; } }
	public ButtonTypes ButtonType { get { return buttonType; } set { buttonType = value; } }
	public InputAction InputButton { get { return inputButton; } private set { } }

	[Tooltip("Number of presses to activate button or length of time to hold down depending on button type.")]
	[SerializeField]
	private int timeToActivate = 1;
	private int timesActivated = 0;
	private float duration = 0;

	private bool pressed = false;
	private bool held = false;
	[Tooltip("Is the button destroyed after being used.")]
	[SerializeField]
	private bool destroyOnUse = false;
	[Tooltip("Show/hide a slider showing the progress of the button being used.")]
	[SerializeField]
	private bool showProgressBar = false;

	private ButtonTypes buttonType = ButtonTypes.Press;

	private MethodToExecute methodToExecute = null;
	[Tooltip("The type of input to trigger the button.")]
	[SerializeField]
	private InputAction inputButton = null;
	private Slider progressBar = null;

	public delegate void MethodToExecute(ButtonPrompt buttonPrompt);

	public void Use() {
		// Reset the button's use.
		timesActivated = 0;
		methodToExecute(this);

		if (destroyOnUse) {
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Sets the method to call when the button is triggered.
	/// </summary>
	/// <param name="methodToLink"> Method to call when the button is triggered. </param>
	public void LinkMethodToButton(MethodToExecute methodToLink) {
		methodToExecute = methodToLink;
	}

	private void Awake() {
		progressBar = GetComponent<Slider>();

		if (!progressBar) {
			progressBar = GetComponentInChildren<Slider>();
		}
	}

	private void Start() {
		progressBar.minValue = timesActivated;
		progressBar.maxValue = timeToActivate;
		progressBar.value = timesActivated;
		inputButton.Enable();
	}

	// Update is called once per frame
	private void Update() {
		if (showProgressBar) {
			progressBar.gameObject.SetActive(true);
		} else {
			progressBar.gameObject.SetActive(false);
		}

		inputButton.performed += i => held = true;
		inputButton.started += i => pressed = true;
		inputButton.canceled += i => held = false;
		inputButton.canceled += i => pressed = false;

		if (buttonType == ButtonTypes.Hold) {
			if (held) {
				duration += Time.deltaTime;
				progressBar.value = duration;

				if (duration >= timeToActivate) {
					Use();
				}
			}
		} else {
			if (pressed) {
				pressed = false;
				++timesActivated;
				progressBar.value = timesActivated;

				if (timesActivated >= timeToActivate) {
					Use();
				}
			}
		}
	}
}
