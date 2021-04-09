using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    // public UnityEvent OnPress = null;

    // private float yMin = 0.0f;
    // private float yMax = 0.0f;
    // private bool previousPress = false;
    // private float previousHandHeight = 0.0f;
    // private XRBaseInteractor hoverInteractor = null;


    // protected override void Awake(){
    //     onHoverEnter.AddListener(StartPress);
    //     onHoverExit.AddListener(EndPress);
    // }

    // private void OnDestroy(){
    //     onHoverEnter.RemoveListener(StartPress);
    //     onHoverExit.RemoveListener(EndPress);
    // }

    // private void StartPress(XRBaseInteractor interactor){
    //     hoverInteractor = interactor;
    //     //Use local rather than global position
    //     previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    // }

    // private void EndPress(XRBaseInteractor interactor){
    //     hoverInteractor = null;
    //     previousHandHeight = 0.0f;

    //     previousPress = false;
    //     SetYPosition(yMax);
    // }

    // private void Start(){
    //     SetMinMax();
    // }

    // private void SetMinMax(){
    //     Collider collider = GetComponent<Collider>();
    //     yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
    //     yMax = transform.localPosition.y;
    // }

    // public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    // {
    //     if(hoverInteractor){
    //         float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    //         float handDifference = previousHandHeight - newHandHeight;
    //         previousHandHeight = newHandHeight;

    //         float newPosition = transform.localPosition.y - handDifference;
    //         SetYPosition(newPosition);

    //         CheckPress();
    //     }
    // }

    // private float GetLocalYPosition(Vector3 position){
    //     Vector3 localPosition = transform.root.InverseTransformPoint(position);
    //     return localPosition.y;
    // }

    // private void SetYPosition(float position){
    //     //Ensure the Y position is clamped
    //     Vector3 newPosition = transform.localPosition;
    //     newPosition.y = Mathf.Clamp(position,yMin,yMax);
    //     transform.localPosition = newPosition;
    // }

    // private void CheckPress(){
    //     bool inPosition= InPosition();

    //     if(inPosition && inPosition != previousPress)
    //         OnPress.Invoke();

    //     previousPress = inPosition;
        
    // }

    // private bool InPosition(){
    //     //Check if the button is in position/pressed enough for pressed event

    //     float inRange = Mathf.Clamp(transform.localPosition.y,yMin,yMax + 0.01f);
    //     return transform.localPosition.y == inRange;
    // }
}