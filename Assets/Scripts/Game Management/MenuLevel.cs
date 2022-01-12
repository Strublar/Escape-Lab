using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevel : MonoBehaviour
{
    public int level;
    public GameObject closedChest, openedChest, key;
    public Transform waypoint;

    public List<GameObject> lights;

    

    public void Unlock()
    {
        closedChest.SetActive(false);
        openedChest.SetActive(true);
        key.SetActive(true);
    }

    public void Lock()
    {
        closedChest.SetActive(true);
        openedChest.SetActive(false);
        key.SetActive(false);
    }

    public void Select()
    {
        GameManager.gm.LoadLevel(this);
    }
}
