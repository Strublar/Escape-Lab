using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Liquid", menuName = "ScriptableLiquid")]
public class SLiquid : ScriptableObject {

    public Color liquidColor = Color.white;

    [Tooltip("Health over 0 (less than 2)")]
    public float liquidFluidity = 1;
}
