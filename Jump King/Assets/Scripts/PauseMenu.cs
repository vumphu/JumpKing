using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;

    private static bool onPause = false;

    void Update()
    {
        Debug.Log(onPause);
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            if (onPause) {
                Resume();
		    }else
            {
                Pause();
            }
        }
       
        
    }
    public void Pause()
    {
        Debug.Log("clicked esc 2" + onPause);
		Time.timeScale = 0.0f;
		pauseMenu.SetActive(true);
		Cursor.visible = true;
		// Camera.audio.Pause ();
		onPause = true;
    }
    public void Resume()
    {
        Debug.Log("clicked esc 2" + onPause);
		Time.timeScale = 1.0f;
		pauseMenu.SetActive(false);
		Cursor.visible = false;
		// Camera.audio.Pause ();
		onPause = false;
    }

    public void Home(int SceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneID);
    }
}
