using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game closed!");
    }

    public void Continue()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OtherBabes()
    {

    }

    public void Options()
    {

    }
}
