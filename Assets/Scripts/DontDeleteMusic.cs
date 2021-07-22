
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDeleteMusic : MonoBehaviour
{
    private static DontDeleteMusic volumeControl = null;
    public static DontDeleteMusic control
    {
        get { return volumeControl; }
    }
    void Awake()
    {
        if (volumeControl != null && volumeControl != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            volumeControl = this;
        }
    }
}
