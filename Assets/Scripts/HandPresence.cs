using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handPrefab;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private InputDevice currentDevice;
    [SerializeField] private GameObject currentHandObject;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);

        if(devices.Count> 0)
        {
            Debug.Log("Device found " + name);
            currentDevice = devices[0];
            currentHandObject = Instantiate(handPrefab, transform);
            handAnimator = currentHandObject.GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(currentHandObject == null)
        {
            Start();
        }
        else
        {
            UpdateHandAnimation();
        }
    }

    public void UpdateHandAnimation()
    {
        if (currentDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) 
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (currentDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
