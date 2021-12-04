using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObjStation : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;
    private string prefabName;
    [SerializeField]
    private Vector3 spawnPoint;

    private void Start() {
        if(spawnPoint == null) {
            spawnPoint = gameObject.transform.position;
        }
        prefabName = prefab.name;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Contains(prefabName)) {
            StopCoroutine(nameof(RespawnObj));
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name.Contains(prefabName)) {
            StartCoroutine(nameof(RespawnObj));
        }
    }

    private IEnumerator RespawnObj() {
        yield return new WaitForSeconds(1);
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
