using System;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [Header("Players")]
    public Transform pOneTransform;
    public Transform pTwoTransform;

    [Header("Settings")]
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float padding = 3f;
    public float lerpSpeed = 5f;

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!pOneTransform || !pTwoTransform) return;
        MoveToCenter();
        ZoomToFit();
    }

    private void MoveToCenter()
    {
        Vector3 center = (pOneTransform.position + pTwoTransform.position) / 2f;
        center.y = transform.position.y; // preserve height from ZoomToFit
        transform.position = Vector3.Lerp(transform.position, center, Time.deltaTime * lerpSpeed);
    }

    private void ZoomToFit()
    {
        float distance = Vector3.Distance(pOneTransform.position, pTwoTransform.position);
        float targetHeight = Mathf.Clamp(distance + padding, minZoom, maxZoom);

        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetHeight, Time.deltaTime * lerpSpeed);
        transform.position = pos;
    }

}
