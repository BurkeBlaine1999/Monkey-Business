
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;

    [SerializeField]private AudioClip grabClip1;
    [SerializeField]private AudioClip grabClip2;
    [SerializeField]private AudioClip grabClip3;
    [SerializeField]private AudioClip grabClip4;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    void Awake()
    {
        audioSource.Play();
    }

        public void playRandomSound(){
        int sound = Random.Range(1, 4);

        if(sound == 1){
            audioSource.PlayOneShot(grabClip1);
        }else if(sound == 2){
            audioSource.PlayOneShot(grabClip2);
        }else if(sound == 3){
            audioSource.PlayOneShot(grabClip3);
        }else if(sound == 4){
            audioSource.PlayOneShot(grabClip4);
        }
    }
}
