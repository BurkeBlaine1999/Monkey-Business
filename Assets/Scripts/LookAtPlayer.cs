using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour{

    [SerializeField] private Transform target;

    void Update()
    {
       if(target != null)
       {
            transform.LookAt(target);
       }
    }
}
