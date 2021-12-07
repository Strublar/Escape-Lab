using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystemPlank : MonoBehaviour {

    public float massOnPlank = 0.0f;

    private List<Rigidbody> listObjOnPlank = new List<Rigidbody>();

    private void LateUpdate() {
        massOnPlank = 0.0f;
        foreach (Rigidbody rigidbody in listObjOnPlank) {
            massOnPlank += rigidbody.mass;
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody != null)
            listObjOnPlank.Add(other.attachedRigidbody);
    }

    public void OnTriggerExit(Collider other) {
        if (other.attachedRigidbody != null)
            listObjOnPlank.Remove(other.attachedRigidbody);
    }
}
