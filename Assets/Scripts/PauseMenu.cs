using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, redCards, blueCards, winPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !winPanel.activeSelf)
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        for (int i = 0; i < redCards.transform.childCount; i++)
            redCards.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 4;
        for (int i = 0; i < blueCards.transform.childCount; i++)    
            blueCards.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 4;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        for (int i = 0; i < redCards.transform.childCount; i++)
            redCards.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 0;
        for (int i = 0; i < blueCards.transform.childCount; i++)    
            blueCards.transform.GetChild(i).GetComponent<Outline>().OutlineWidth = 0;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    } 

    public void QuitGame()
    {
        Debug.Log("Quit");
    } 
}
