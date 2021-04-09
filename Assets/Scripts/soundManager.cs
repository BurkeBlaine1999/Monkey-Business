using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip Groan1;
    [SerializeField] private AudioClip Groan2;
    [SerializeField] private AudioClip Groan3;

    void Start()
    {
        InvokeRepeating("PlayClip", 2.0f, 10f);
    }

    void PlayClip()
    {
       int randNum = Random.Range(1, 3);

       if(randNum == 1){
           source.PlayOneShot(Groan1);
       }else if(randNum == 2){
            source.PlayOneShot(Groan2);
       }else{
            source.PlayOneShot(Groan3);
       }

    }
}
