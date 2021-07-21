using UnityEngine;
using UnityEngine.UI;

public class Boombox : MiniGame {

	//this is a very quick and dirty solution to the boombox problem
	public float timer = 4.25f;
	bool dancing;

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
		inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
		dancing = false;
	}

    private void Update()
    {
		if(dancing == true)
        {
			timer -= Time.deltaTime;
		}
		
		if (timer <= 0)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().DancingComplete();
			dancing = false;
			timer = 4.25f;
		}
	}

    public override void Use() {
		if (timesUsed >= usesAvailable) {
			return;
		}

		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
		dancing = true;
		IsMiniGameActive = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsDancing", true);

		++timesUsed;



		
	}

	public override void CompleteEvent() 
	{
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
		IsMiniGameActive = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsDancing", false);

	}
}
