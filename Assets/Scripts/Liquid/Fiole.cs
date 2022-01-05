using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiole : MonoBehaviour {
    
    public Renderer liquidRenderer;
    public SContainer container;
    private float fillStable = 0.58f;

    public GameObject keyPrefab;
    public Transform origin;
    public GameObject streamPrefab;
    public GameObject key;
    private bool isPouring = false;
    private bool hasKey = true;
    private Stream currentStream;

    private void Start() {
        liquidRenderer.material.SetFloat("_FillAmount", container.minFillAmount-0.2f);
        ChangeLiquidColor(Color.white);
    }
    private void Update() {
        //vider si c'est trop rempli
        float val = liquidRenderer.material.GetFloat("_FillAmount");
        bool shouldPour = val < fillStable;
        if (shouldPour) {
            liquidRenderer.material.SetFloat("_FillAmount", val + 0.0005f);
            if (shouldPour != isPouring) {
                isPouring = true;
                CreateStream();
                if (hasKey) {
                    Destroy(key);
                    Instantiate(keyPrefab, currentStream.FindEndPoint(), Quaternion.identity);
                    hasKey = false;
                }
            }
        } else if(isPouring) {
            Debug.Log("End");
            isPouring = false;
            EndPour();
        }
    }

    private void ChangeLiquidColor(Color newColor) {
        liquidRenderer.material.SetColor("_Tint", newColor);
        Color lightColor = new Color(newColor.r * 2f, newColor.g * 2f, newColor.b * 2f);
        liquidRenderer.material.SetColor("_RimColor", lightColor);
        liquidRenderer.material.SetColor("_TopColor", lightColor);
        liquidRenderer.material.SetColor("_FoamColor", lightColor);
    }

    public void FillContainer(Color newLiquidColor, float liquidAmount) {
        float val = liquidRenderer.material.GetFloat("_FillAmount");
        //remplir si ce n'est pas deja rempli
        if (val >= container.maxFillAmount) { 
            liquidRenderer.material.SetFloat("_FillAmount", val-liquidAmount);
        }
        //changer coulor (get liquid color if empty, turn brown if another color)
        Color actualColor = liquidRenderer.material.GetColor("_Tint");
        if (actualColor == Color.white) {
            ChangeLiquidColor(newLiquidColor);
        } else if (actualColor != newLiquidColor) {
            ChangeLiquidColor(new Color(0.5f, 0.5f, 0.5f));
        }
    }

    private void CreateStream() {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        currentStream = streamObject.GetComponent<Stream>();
        currentStream.Begin(liquidRenderer.material.GetColor("_Tint"), 0.8f);
    }

    private void EndPour() {
        currentStream.End();
        currentStream = null;
    }
}
