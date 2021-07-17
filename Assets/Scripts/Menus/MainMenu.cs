using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("You Started the Game :)");

        //String empty, main scene to go inside
        SceneManager.LoadScene("Level_Whitebox");
    } 

    public void QuitGame()
    {
        Debug.Log("You Quit the Game :(");

        //Close Application
        Application.Quit();  
    }
}
