using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropMag : XRBaseInteractable
{

    [SerializeField]private Rigidbody rb;

    void awake(){
         //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
         rb.isKinematic =true;
    }

    public void MagGrabbed(){
        Debug.Log("Grabbed");
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic =false;
    }

    protected override void OnSelectEnter(XRBaseInteractor interactor){

        interactor = Object.FindObjectOfType<XRBaseInteractor>();

        base.OnSelectEnter(interactor);

        if(interactor is XRDirectInteractor)
            MagGrabbed();
    }
}
