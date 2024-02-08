using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hands : MonoBehaviour
{
    public GameObject handModelPrefab;
    public InputDeviceCharacteristics controllerCharacteristics;

    private GameObject spawnedHandModel;
    private InputDevice targetDevice;
    private Animator handAnimator;


    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();//creates a list of input devices

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);//gets devices with the characteristics of right controller characteristics

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);//prints all input devices and characteristics to the console
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];//sets target device to be the first input device in our list

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        handAnimator.SetFloat("Trigger", triggerValue);
        targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        handAnimator.SetFloat("Grip", gripValue);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHandAnimation();
    }
}