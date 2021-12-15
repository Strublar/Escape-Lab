using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pourable : MonoBehaviour {

    public Transform origin;
    public GameObject streamPrefab;
    public Renderer liquidRenderer;
    public SContainer container;
    public SLiquid liquid;
    public GameObject keyPrefab;
    public bool iscontainingKey = false;
    public GameObject keyModel;

    private bool isPouring = false;
    private bool isEmpty = false;
    private int pourThreshold;

    private Stream currentStream;


    private void Start() {
        liquidRenderer.material.SetFloat("_FillAmount", container.maxFillAmount);
        liquidRenderer.material.SetColor("_Tint", liquid.liquidColor);
        Color lightColor = new Color(liquid.liquidColor.r * 2f, liquid.liquidColor.g * 2f, liquid.liquidColor.b * 2f);
        liquidRenderer.material.SetColor("_RimColor", lightColor);
        liquidRenderer.material.SetColor("_TopColor", lightColor);
        liquidRenderer.material.SetColor("_FoamColor", lightColor);
        pourThreshold = container.pourThreshold;
    }

    private void Update() {
        bool pourCheck = CalculatePourAngle() > pourThreshold;

        if (isEmpty == false && isPouring != pourCheck) {
            isPouring = pourCheck;
            if (isPouring) {
                StartPour();
            } else {
                EndPour();
            }
        }
    }

    private void StartPour() {
        currentStream = CreateStream();
        currentStream.Begin(liquid.liquidColor, liquid.liquidFluidity);

        InvokeRepeating(nameof(EmptyingBottle), 0.0f, 0.05f);
    }

    private void EmptyingBottle() {
        float val = liquidRenderer.material.GetFloat("_FillAmount") + (0.01f * liquid.liquidFluidity);
        liquidRenderer.material.SetFloat("_FillAmount", val);
        if (val >= container.minFillAmount) {
            isEmpty = true;
            isPouring = false;
            //if there is a key inside
            if (iscontainingKey) {
                Destroy(keyModel);
                Instantiate(keyPrefab, currentStream.FindEndPoint(), Quaternion.identity);
            }
            EndPour();

        }
    }

    public void StartFillBotle() {
        StartCoroutine(nameof(FillBottle));
    }

    private IEnumerator FillBottle() {
        float val = liquidRenderer.material.GetFloat("_FillAmount") - 0.1f;
        while(val >= container.maxFillAmount){
            liquidRenderer.material.SetFloat("_FillAmount", val);
            val = liquidRenderer.material.GetFloat("_FillAmount") - 0.1f;
            yield return new WaitForSeconds(0.05f);
        }

    }

    private void EndPour() {
        CancelInvoke(nameof(EmptyingBottle));
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle() {
        float zAngle = 180-Mathf.Abs(180-transform.rotation.eulerAngles.z); // angle raporte a 180 plutot que 360
        float xAngle = 180 - Mathf.Abs(180 - transform.rotation.eulerAngles.x);
        return Mathf.Max(zAngle, xAngle);
    }

    private Stream CreateStream() {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

}
