using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToGame()
    {
        Debug.Log("Button Clicked!");
        SceneManager.LoadScene("OpenWorld");
    }

    // Update is called once per frame
    public void Quit(){
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
