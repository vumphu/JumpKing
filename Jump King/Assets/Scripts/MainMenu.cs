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

    public void OnNewGameClicked()
    {
        SceneManager.LoadScene(1);
        DataPersistenceManager.instance.NewGame();
    }
    public void OnLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
    }
    public void OnSaveGameClicked()
    {
        DataPersistenceManager.instance.SaveGame();
    }
    public void SaveGame()
    {
        
    }

    public void Options()
    {

    }
}
