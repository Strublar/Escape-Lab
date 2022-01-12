using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystem : MonoBehaviour {

    public GameObject leftPlank;
    private PulleySystemPlank leftPulleySystemPlank;
    public GameObject rightPlank;
    private PulleySystemPlank rightPulleySystemPlank;
    public GameObject leftPulley;
    public GameObject rightPulley;

    private float ropeLength;
    private readonly float uperLimit = -0.2f;
    private Vector3 newPositionLow;
    private Vector3 newPositionHigh;

    private void Awake() {
        ropeLength = Mathf.Abs(leftPlank.transform.localPosition.y) + Mathf.Abs(rightPlank.transform.localPosition.y);
        leftPulleySystemPlank = leftPlank.GetComponent<PulleySystemPlank>();
        rightPulleySystemPlank = rightPlank.GetComponent<PulleySystemPlank>();
    }

    private void Update() {
        float weightDiff =0.0f;
        if (leftPulleySystemPlank.massOnPlank > rightPulleySystemPlank.massOnPlank + 0.05) {
            //more weight on left
            weightDiff = leftPulleySystemPlank.massOnPlank / Mathf.Max(rightPulleySystemPlank.massOnPlank, 1);
            newPositionHigh = rightPlank.transform.localPosition + new Vector3(0f, 0.0004f*weightDiff, 0f);
            if (newPositionHigh.y >= uperLimit)
                newPositionHigh = new Vector3(rightPlank.transform.localPosition.x, uperLimit, rightPlank.transform.localPosition.z);
            newPositionLow = new Vector3(leftPlank.transform.localPosition.x, -(ropeLength+newPositionHigh.y), leftPlank.transform.localPosition.z);

            leftPlank.transform.localPosition = newPositionLow;
            rightPlank.transform.localPosition = newPositionHigh;

        } else if (rightPulleySystemPlank.massOnPlank > leftPulleySystemPlank.massOnPlank + 0.05) {
            //more weight on right
            weightDiff = rightPulleySystemPlank.massOnPlank / Mathf.Max(leftPulleySystemPlank.massOnPlank, 1);
            newPositionHigh = leftPlank.transform.localPosition + new Vector3(0f, 0.0004f*weightDiff, 0f);
            if (newPositionHigh.y >= uperLimit)
                newPositionHigh = new Vector3(leftPlank.transform.localPosition.x, uperLimit, leftPlank.transform.localPosition.z);
            newPositionLow = new Vector3(rightPlank.transform.localPosition.x, -(ropeLength +newPositionHigh.y), rightPlank.transform.localPosition.z);

            rightPlank.transform.localPosition = newPositionLow;
            leftPlank.transform.localPosition = newPositionHigh;

        } else {
            //egal weight


        }
    }
}
