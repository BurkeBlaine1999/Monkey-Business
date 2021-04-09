using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    [SerializeField] private Text text;

    void Awake(){
        instance = this;
    }

    #endregion

    public GameObject player;
    private int health = 100;

    [SerializeField]private GameObject deathMenu;
    [SerializeField]private GameObject Menu;

    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip hurt1;
    [SerializeField]private AudioClip hurt2;
    [SerializeField] private Text killCount;

    [SerializeField]private GameObject newHighscore;
    string highscore = "SeaTown";
    private int kills=0;
    void Start(){
        //AudioSource.Play();
        text.text = health.ToString();
        text.color = Color.green;
        health = 100;
    }

    void Update(){
        text.text = health.ToString();
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Slime Ball"){
            TakeDamage(15);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage){

        int randNum = Random.Range(1, 2);

       if(randNum == 1){
           audioSource.PlayOneShot(hurt1);
       }else{
            audioSource.PlayOneShot(hurt2);
       }

        health -= damage;
        text.text = health.ToString();

        if(health >70){text.color = Color.green;}
        else if(health < 70 && health > 40){text.color = Color.yellow;}
        else if(health < 40){text.color = Color.red;}
        else{text.color = Color.black;}

        Debug.Log("PLAYER HEALTH = " + health);
        if(health <= 0){
            text.text = "0";
            Death();
        }
        return;
    }

    private void Death(){
        if(kills > PlayerPrefs.GetInt(highscore)){           
            PlayerPrefs.SetInt(highscore,kills);
            newHighscore.SetActive(true);
        }

        Debug.Log("YOU ARE DEAD!");
        deathMenu.SetActive(true);
        Menu.SetActive(false);
        Time.timeScale = 0f;
    
    }

    public void killCounter(){
        kills++;
        killCount.text = kills.ToString();
    }



}
