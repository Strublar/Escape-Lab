using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordElectricPuzzle : MonoBehaviour {
    
    public GameObject electricBoxDoor;
    private bool isConnected = false;

    private void OnTriggerEnter(Collider other) {
        if (!isConnected && other.gameObject.name.Contains("Sword")) {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            Destroy(other.gameObject);
            isConnected = true;
            electricBoxDoor.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(nameof(ActivateMovement));
        }
    }
    private IEnumerator ActivateMovement() {
        while (electricBoxDoor.transform.localPosition.y > -0.52) {
            electricBoxDoor.transform.localPosition -= new Vector3(0, 0.01f, 0);
            yield return new WaitForSeconds(0.05f);
        }
    }

}
