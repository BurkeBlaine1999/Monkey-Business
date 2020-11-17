using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReached : MonoBehaviour
{
    public float threshold = 0.02f;
    public Transform target;
    public UnityEvent OnReached;
    private bool wasReached = false;

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < threshold && !wasReached){
            //Reached target
            OnReached.Invoke();
            wasReached = true;
        } else if (distance >= threshold){
            wasReached = false;
        }
    }
}
