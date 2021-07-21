using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public Toggle fullscreen;

    // Start is called before the first frame update
    void Start()
    {
        fullscreen.isOn = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply()
    {
        Screen.fullScreen = fullscreen.isOn;
    }
}
