using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    { 
        //String empty, main scene to go inside
        SceneManager.LoadScene("");
    } 

    public void QuitGame()
    { 
        //Close Application
        Application.Quit();  
    }
}
