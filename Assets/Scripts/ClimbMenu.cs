using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClimbMenu : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene("ClimbLobby");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Home");
    }

    // Update is called once per frame
    public void Level1(){
        SceneManager.LoadScene("Climb_Level_1");
    }

    public void Level2(){
        SceneManager.LoadScene("Climb_Level_2");
    }

    public void Level3(){
        SceneManager.LoadScene("Climb_Level_3");
    }
}
