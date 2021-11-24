using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour {

    public int pourThreshold = 170;
    public Transform origin;
    public GameObject streamPrefab;

    private bool isPouring = false;
    private Stream currentStream;

    private void Update() {
        bool pourCheck = CalculatePourAngle() > pourThreshold;

        if (isPouring != pourCheck) {
            isPouring = pourCheck;
            if (isPouring) {
                StartPour();
            } else {
                EndPour();
            }
        }
    }

    private void StartPour() {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour() {
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle() {
        float zAngle = 180-Mathf.Abs(180-transform.rotation.eulerAngles.z); // angle raporte a 180 plutot que 360
        float xAngle = 180 - Mathf.Abs(180 - transform.rotation.eulerAngles.x);
        return Mathf.Max(zAngle, xAngle);
    }

    private Stream CreateStream() {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

}
