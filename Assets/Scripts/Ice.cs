using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ice : MonoBehaviour {

    public GameObject[] iceState;
    public GameObject key;
    private int currentState = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("torch")) {
            InvokeRepeating("SmeltGlace", 0.2f, 0.5f);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("torch")) {
            CancelInvoke("SmeltGlace");
        }
    }

    private void SmeltGlace() {
        if (currentState < iceState.Length-1) {
            iceState[currentState].SetActive(false);
            currentState += 1;
            iceState[currentState].SetActive(true);
            if(key != null && currentState == iceState.Length - 2) {
                key.GetComponent<XRGrabInteractable>().enabled = true;
                key.GetComponent<Rigidbody>().useGravity = true;
            }
        } else {
            CancelInvoke("SmeltGlace");
        }
    }
}
