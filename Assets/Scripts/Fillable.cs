using UnityEngine;

public class Fillable : MonoBehaviour {


    public Renderer liquidRenderer;
    public SContainer container;
    public float fillamount = 0;

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        liquidRenderer.material.SetFloat("_FillAmount", container.minFillAmount);
        ChangeLiquidColor(Color.white);
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
            rb.WakeUp();
            rb.mass += liquidAmount*3;
        }
        //changer coulor (get liquid color if empty, turn brown if another color)
        Color actualColor = liquidRenderer.material.GetColor("_Tint");
        if (actualColor == Color.white) {
            ChangeLiquidColor(newLiquidColor);
        } else if (actualColor != newLiquidColor) {
            ChangeLiquidColor(new Color(0.5f, 0.5f, 0.5f));
        }
    }

}
