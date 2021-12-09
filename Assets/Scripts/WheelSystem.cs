using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSystem : MonoBehaviour {
    
    public GameObject flameLight;
    public GameObject wheel;
    public GameObject plank;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("torch")){
            flameLight.SetActive(true);
            StartCoroutine(nameof(ActivateMovement));
        }
    }

    private IEnumerator ActivateMovement() {
        while (plank.transform.localPosition.y < 0) {
            plank.transform.localPosition += new Vector3(0, 0.05f, 0);
            wheel.transform.rotation = new Quaternion(wheel.transform.rotation.x+0.05f, wheel.transform.rotation.y, wheel.transform.rotation.z, wheel.transform.rotation.w);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
