using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour {
	private int movementSpeed = 300;

	public void SetType(Sprite sprite) {
		GetComponent<Image>().sprite = sprite;
	}

	private void Update() {
		transform.position -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
	}
}
