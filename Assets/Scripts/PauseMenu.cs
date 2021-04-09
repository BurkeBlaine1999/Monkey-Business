using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Variables
    public static bool isPaused = false;
    [SerializeField]private GameObject DeathMenu;
    [SerializeField]private GameObject PausedMenu;
    [SerializeField]private GameObject Menu;

    public void Start(){
        DeathMenu.SetActive(false);
        PausedMenu.SetActive(false);
        Menu.SetActive(true);
    }

    //Check if the game is paused
    public bool GameIsPaused()
    {
        return isPaused;
    }

    //Open the pause menu and stop time
    public void PauseMenuOn()
    {
        isPaused = true;
        PausedMenu.SetActive(true);
        Menu.SetActive(false);
        Time.timeScale = 0f;
    }

    //Close the pause menu and resume time
    public void Resume()
    {
        isPaused = false;
        PausedMenu.SetActive(false);
        Menu.SetActive(true);
        Time.timeScale = 1f;
    }

    //Reload/Restart the current scene
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Exit to the main menu
    public void Quit()
    {
        SceneManager.LoadScene("Home");
        Time.timeScale = 1f;
    }
}
