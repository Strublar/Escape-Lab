using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {

    public GameObject[] iceState;
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
        } else {
            CancelInvoke("SmeltGlace");
        }
    }
}
