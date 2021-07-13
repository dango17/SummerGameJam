using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playingGuitair : MonoBehaviour
{
    public bool playing;

    void Start()
    {
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playing = true;
            Debug.Log("playing");
        }

        if (playing == true && Input.GetKeyDown(KeyCode.Backspace))
        {
            playing = false;
            Debug.Log("not playing");
        }
    }
}
