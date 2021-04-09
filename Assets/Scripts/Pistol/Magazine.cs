using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int numBullets = 8;

    void FixedUpdate(){
        if(numBullets == 0){
            Destroy(this.gameObject,10f);
        }
    }
}
