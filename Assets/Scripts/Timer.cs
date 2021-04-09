using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class Timer : MonoBehaviour
{

    //public Text timerText;
    private float startTime;
    private bool finished = false;

    [SerializeField]private Text[] timers;

    float score = 0;
    float finalScore=0;
    string highscore;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level One"){
            highscore = "Level1Highscore";

        }else if(scene.name == "Level Two"){
            highscore = "Level2Highscore";

        }else if(scene.name == "Level Three"){
            highscore = "Level3Highscore";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(finished){return;}           

        score = Time.time - startTime;

        for(int i = 0;i != timers.Length;i++){
            timers[i].text = score.ToString("f2");
        }
        
    }

    public void Finish(){

        score = (float)(Math.Truncate((double)score*100.0) / 100.0);
        
        //Set highcore as high as possible for debugging
        //PlayerPrefs.SetFloat(highscore,900);

        if(PlayerPrefs.GetFloat(highscore) == 0){
            PlayerPrefs.SetFloat(highscore,Mathf.Infinity);
        }

        
        Debug.Log("score is " + score);

        finished = true;
      
        if(score <= PlayerPrefs.GetFloat(highscore)){
            PlayerPrefs.SetFloat(highscore,score);
            Debug.Log(highscore + " is " + PlayerPrefs.GetFloat(highscore));
            for(int i = 0;i <= timers.Length;i++){
                timers[i].color = Color.green;              
            }   

        }else if(score > PlayerPrefs.GetFloat(highscore)){
            Debug.Log(highscore + " is " + PlayerPrefs.GetFloat(highscore));
            for(int i = 0;i <= timers.Length;i++){
                timers[i].color = Color.red;
            }

        }
        
        
    }
}
