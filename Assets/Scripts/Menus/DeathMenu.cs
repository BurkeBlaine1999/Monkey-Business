using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Text scoreText;
    public Image backgroundImage;
    public bool IsShown = false;
    public float transition =0.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsShown )
            return;

        transition += Time.deltaTime ;
        backgroundImage.color = Color.Lerp(new Color(0,0,0,0),Color.black,transition);
    }

    public void ToggleEndMenu(float score){
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        IsShown =true;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
