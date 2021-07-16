using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour {
	/// <summary>
	/// The input to enter.
	/// </summary>
	public string CorresspondingInput { get; private set; } = "";

	private int movementSpeed = 320;
	[Tooltip("How much score to add/subtract.")]
	[SerializeField]
	private int scoreWorth = 5;
	private GuitarInterface guitarInterface = null;

	public void SetType(CustomButton button) 
	{
		GetComponent<Image>().sprite = button.GetComponent<Image>().sprite;
		CorresspondingInput = button.InputButton.ToString();
	}

    public void Destroy()
    {
		guitarInterface.Score.AddScore(scoreWorth);
		Destroy(gameObject);
	}

	private void Start() 
	{
		guitarInterface = GameObject.FindGameObjectWithTag("GuitarUI").GetComponent<GuitarInterface>();
	}

	private void Update() {
		transform.position -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("DestructionZone")) {
			guitarInterface.Score.SubtractScore(scoreWorth);
			Destroy(gameObject);
		}
	}
}
