using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LiquidContainer", menuName = "ScriptableContainer")]
public class SContainer : ScriptableObject {

    public float maxFillAmount;
    public float minFillAmount;

    public int pourThreshold;

}
