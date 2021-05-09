using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void ToMainMenu(){
        SceneManager.LoadScene("Home");
    }

    public void ToZombieLevel(){
        SceneManager.LoadScene("SeaTown");
    }   

    public void ToPracticeLevel(){
        SceneManager.LoadScene("Practice");
    }   

    public void ToLevel1(){
        SceneManager.LoadScene("Level One");
    }   

    public void ToLevel2(){
        SceneManager.LoadScene("Level Two");
    }   

    public void ToLevel3(){
        SceneManager.LoadScene("Level Three");
    }  

    public void Quit(){
        Application.Quit();
    }   

    public void MenuOff()
    {
        gameObject.SetActive(false);
    }

    public void MenuOn()
    {
        gameObject.SetActive(true);
    }
    
}
