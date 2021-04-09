using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
            Destroy(gameObject);  
    }
}
