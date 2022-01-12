using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private bool isBeginLock, isEndLock;
    [SerializeField] private GameObject door;
    [SerializeField] private Rigidbody doorRb;
    [SerializeField] private int level;

    public void Start()
    {
        doorRb.constraints = RigidbodyConstraints.FreezeAll;
        door.layer = 0;
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

        if(isEndLock)
            ScoreManager.m.EndTimer();
        if(isBeginLock)
            ScoreManager.m.StartTimer();

        ScoreManager.m.SaveCurrentLevel(level);


        other.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        other.gameObject.GetComponent<Collider>().enabled = false;
        other.gameObject.transform.parent = transform;
        other.gameObject.transform.localPosition = new Vector3(-0.01f,-.2f,0.015f);
        other.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));

        door.layer = 7;


    }
}
