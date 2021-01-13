using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    public LightingManager lightingManager;

    public AudioSource AudioSource;

    public AudioClip daySound;
    public AudioClip nightSound;

    void Awake(){
        instance = this;
    }

    #endregion

    public GameObject player;
    private int health = 100;
    void Start(){
        //AudioSource.Play();
    }

    // void Update(){
    //     if(lightingManager.TimeOfDay < 175 || lightingManager.TimeOfDay > 775){
    //         FadeOut(AudioSource,10f);
    //         AudioSource.clip = null;
    //     }else{
    //         FadeOut(AudioSource,10f);
    //         AudioSource.Play();
    //     }
    // }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet"){
            TakeDamage(35);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0){
            Death();
        }
        return;
    }

    private void Death(){
        Debug.Log("YOU ARE DEAD!");
        Destroy(player.gameObject);
    }

    private void changeAudio(){

    }

    // public IEnumerator PlayAudio (AudioSource audioSource) {
        
    //     audioSource.clip = daySound;
    //     audioSource.Play ();
    //     double x = audioSource.clip.length;     
    //     yield return WaitForSeconds (3); 
    // }

    // public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
    //     float startVolume = audioSource.volume;
 
    //     while (audioSource.volume > 0) {
    //         audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
    //         yield return null;
    //     }
 
    //     audioSource.Stop ();
    //     audioSource.volume = startVolume;
    // }
}
