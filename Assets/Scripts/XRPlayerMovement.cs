using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerMovement : MonoBehaviour
{
    public float speed = 1;
    [SerializeField] private CharacterController character;
    [SerializeField] private GameObject cameraObject;
    public XRNode inputSource;
    private Vector2 inputAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

    }

    private void FixedUpdate()
    {
        Quaternion headDirection = Quaternion.Euler(0, cameraObject.transform.eulerAngles.y, 0);
        Vector3 direction = headDirection * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction*Time.fixedDeltaTime*speed);
    }
}
