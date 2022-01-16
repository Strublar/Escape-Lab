using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WheelSystem : MonoBehaviour {
    
    public GameObject flameLight;
    public GameObject wheel;
    public VisualEffect steam;
    public GameObject plank;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("torch")){
            flameLight.SetActive(true);
            steam.enabled = true;
            StartCoroutine(nameof(ActivateMovement));
        }
    }

    private IEnumerator ActivateMovement() {
        while (plank.transform.localPosition.y < 0) {
            plank.transform.localPosition += new Vector3(0, 0.05f, 0);
            wheel.transform.localRotation = new Quaternion(wheel.transform.localRotation.x+0.05f, wheel.transform.localRotation.y, wheel.transform.localRotation.z, wheel.transform.localRotation.w);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
