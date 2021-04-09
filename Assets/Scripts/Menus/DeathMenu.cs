using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }


    public void ToggleEndMenu(float score){
        gameObject.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
