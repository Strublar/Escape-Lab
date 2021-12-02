using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordeRenderer : MonoBehaviour {

    private LineRenderer lr;
    [SerializeField]
    private Transform[] points;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }
    private void Start() {
        lr.positionCount = points.Length;
    }

    public void Update() {
        for(int i =0; i<points.Length; i++) {
            lr.SetPosition(i, points[i].position);
        }
    }

}
