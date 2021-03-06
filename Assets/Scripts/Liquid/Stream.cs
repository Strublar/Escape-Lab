using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour {

    private LineRenderer lineRenderer;
    private ParticleSystem splashParticles;

    private Coroutine pourRoutine;
    private Vector3 targetPosition = Vector3.zero;
    private GameObject stopingObject;

    private Color color;
    private float fluidity;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Start() {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin(Color color, float fluidity) {
        this.color = color;
        this.fluidity = fluidity;
        lineRenderer.sortingOrder = 1;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = color;
        pourRoutine = StartCoroutine(nameof(BeginPour));
        StartCoroutine(nameof(UpdateParticules));
    }

    private IEnumerator BeginPour() {
        while (gameObject.activeSelf) {
            targetPosition = FindEndPoint();

            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
    }

    public void End() {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(nameof(EndPour));
    }

    private IEnumerator EndPour() {
        while (!HasReachedPosition(0, targetPosition)) {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }

        Destroy(gameObject);
    }

    public Vector3 FindEndPoint() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        return endPoint;
    }
    private GameObject FindStopObject() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);

        return hit.transform.gameObject;
    }

    private void MoveToPosition(int index, Vector3 targetPosition) {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition) {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime*1.5f*fluidity);

        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition) {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticules() {
        bool isHitting;
        Fillable fillableObj;
        Funnel funnelObj;
        while (gameObject.activeSelf) {
            splashParticles.gameObject.transform.position = targetPosition;

            isHitting = HasReachedPosition(1, targetPosition);
            splashParticles.gameObject.SetActive(isHitting);
            if (isHitting) {
                stopingObject = FindStopObject();
                fillableObj = stopingObject.GetComponent<Fillable>();
                if (fillableObj != null) {
                    fillableObj.FillContainer(color, fluidity * 0.001f);
                    fillableObj.fillamount++;
                } else {
                    funnelObj = stopingObject.GetComponent<Funnel>();
                    if (funnelObj != null) {
                        funnelObj.Fill(color, fluidity * 0.001f);
                    }
                }
            }
            yield return null;
        }
    }
}
