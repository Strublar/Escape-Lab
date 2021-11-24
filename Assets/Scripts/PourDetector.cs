using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour {

    public Transform origin;
    public GameObject streamPrefab;
    public Renderer liquidRenderer;
    public SContainer container;

    private bool isPouring = false;
    private bool isEmpty = false;
    private int pourThreshold;

    private Stream currentStream;


    private void Start() {
        liquidRenderer.material.SetFloat("_FillAmount", container.maxFillAmount);
        pourThreshold = container.pourThreshold;
    }

    private void Update() {
        bool pourCheck = CalculatePourAngle() > pourThreshold;

        if (isEmpty==false && isPouring != pourCheck) {
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

        InvokeRepeating(nameof(EmptyingBottle), 0.0f, 0.05f);
    }

    private void EmptyingBottle() {
        float val = liquidRenderer.material.GetFloat("_FillAmount") + 0.01f;
        liquidRenderer.material.SetFloat("_FillAmount", val);
        if (val >= container.minFillAmount) {
            isEmpty = true;
            isPouring = false;
            EndPour();
        }
    }

    private void EndPour() {
        CancelInvoke(nameof(EmptyingBottle));
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
