using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private Rigidbody doorRb;

    public void Start()
    {
        doorRb.constraints = RigidbodyConstraints.FreezeAll;

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("object entered doorlock");
        if (other.transform.tag == "key")
        {
            Debug.Log("Key entered doorlock");
            doorRb.constraints = RigidbodyConstraints.FreezePosition |
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        }
    }
}
