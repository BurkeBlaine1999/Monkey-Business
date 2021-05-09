using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AmmoPouch : MonoBehaviour
{

    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    public GameObject leftHand;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);

        foreach(var item in devices){
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0){
            targetDevice  = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)){
        }
    }
}
