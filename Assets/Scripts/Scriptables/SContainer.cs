using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LiquidContainer", menuName = "ScriptableContainer")]
public class SContainer : ScriptableObject {

    public float maxFillAmount = 0.6f;
    public float minFillAmount = 1f;

    public int pourThreshold = 90;

}
