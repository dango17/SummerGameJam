using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Toggle fullscreen;

    private bool isMuted;

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedRes;

    public TMP_Text resLabels;

    // Start is called before the first frame update
    void Start()
    {
        isMuted = false;

        fullscreen.isOn = Screen.fullScreen;
        bool foundRes = false;

        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedRes = i;

                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);

            selectedRes = resolutions.Count - 1;

            UpdateResLabel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateResLabel()
    {
        resLabels.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }

    public void ResLeft()
    {
        selectedRes--;
        if(selectedRes < 0)
        {
            selectedRes = 0;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedRes++;
        if(selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1; 
        }

        UpdateResLabel();
    }

    public void Apply()
    {
        //Screen.fullScreen = fullscreen.isOn;
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreen.isOn);
    }

    public void MutePress()
    {
        isMuted = !isMuted;

        AudioListener.pause = isMuted;
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
