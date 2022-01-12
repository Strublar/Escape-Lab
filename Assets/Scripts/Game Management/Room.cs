using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public List<GameObject> lights;

    public void Start()
    {
        Desactivate();
    }

    public void Activate()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
    }

    public void Desactivate()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }
}
