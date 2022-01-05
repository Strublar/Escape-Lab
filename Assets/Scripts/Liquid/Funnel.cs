using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour {

    public Fiole fiole;

    internal void Fill(Color color, float v) {
        fiole.FillContainer(color, v*6);
    }
}
