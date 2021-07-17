using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : Interactable {
	/// <summary>
	/// Is the player engaging with the mini-game.
	/// </summary>
	public bool IsMiniGameActive { get; protected set; } = false;

	[SerializeField]
	protected InputManager inputManager = null;
	[SerializeField]
	protected GameObject interfacePrefab = null;

	public abstract void CompleteEvent();
}
