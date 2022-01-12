using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public List<Room> rooms;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(Room room in rooms)
            {
                room.Activate();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Room room in rooms)
            {
                room.Desactivate();
            }
        }
    }
}
