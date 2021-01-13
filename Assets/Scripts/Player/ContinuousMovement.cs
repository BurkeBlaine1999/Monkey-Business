using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode inputSource;
    private float speed = 5.0f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;
    private float fallingSpeed ;
    private float gravity = -9.81f;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;
    public Rigidbody rigidbody;
    public int forceConst = 50;
    private bool canJump;
    private InputDevice device;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis,out inputAxis);
    }

    private void FixedUpdate()
    {

        capsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0,rig.cameraGameObject.transform.eulerAngles.y,0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x,0,inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonValue))
        {   
            if(buttonValue){jump();}           
        }
        
        /*gravity
        * When falling you dont fall at a constant speed,
        * it increases over time. 
        *Therefore the falling speed is 0 unless falling.
        */
        bool isGrounded = CheckIfGrounded();
        if(isGrounded)
            fallingSpeed = 0;
        else 
            fallingSpeed += gravity * Time.fixedDeltaTime;
        
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);

    }

    void jump(){
        bool isGrounded = CheckIfGrounded();
        if(isGrounded){          
            rigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void capsuleFollowHeadset(){
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth , capsuleCenter.z);
    }

    bool CheckIfGrounded(){
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart,character.radius,Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
