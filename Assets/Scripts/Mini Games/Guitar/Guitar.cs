using UnityEngine;

public class Guitar : Interactable {
	[SerializeField]
	private GameObject guitarInterfacePrefab = null;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			Debug.Log("Used Guitar: " + gameObject.name);
			// TODO: pause player controls
			GameObject canvas = GameObject.FindGameObjectWithTag("UI");
			Instantiate(guitarInterfacePrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
			isUsed = true;
			++timesUsed;
		}
	}
}
