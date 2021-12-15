using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private Rigidbody doorRb;
    [SerializeField] private bool isBeginLock, isEndLock;
    public void Start()
    {
        doorRb.constraints = RigidbodyConstraints.FreezeAll;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "key")
        {
            Debug.Log("Opening Door");
            Unlock(other) ;

        }
    }

    public void Unlock(Collider other)
    {
        doorRb.constraints = RigidbodyConstraints.None;
        if (isBeginLock)
            ScoreManager.m.StartTimer();
        if (isEndLock)
            ScoreManager.m.EndTimer();
        other.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        other.gameObject.GetComponent<Collider>().enabled = false;
        other.gameObject.transform.parent = transform;
        other.gameObject.transform.localPosition = new Vector3(-0.01f,-.2f,0.015f);
        other.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));



    }
}
