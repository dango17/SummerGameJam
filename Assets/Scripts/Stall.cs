using UnityEngine;

public class Stall : Interactable {
	public override void Use() {
		Debug.Log("Used Stall: " + gameObject.name);
	}
}
