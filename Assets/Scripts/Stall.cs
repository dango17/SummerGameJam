using UnityEngine;

public class Stall : Interactable {
	public override void Use() {
		if (timesUsed < usesAvailable) {
			Debug.Log("Used Stall: " + gameObject.name);
			isUsed = true;
			++timesUsed;
		}
	}
}
