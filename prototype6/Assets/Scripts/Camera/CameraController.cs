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
    
    private void Update()
    {
        if (!pOneTransform || !pTwoTransform) return;
        MoveToCenter();
        ZoomToFit();
    }

    private void MoveToCenter()
    {
        Vector3 center = (pOneTransform.position + pTwoTransform.position) / 2f;
        center.y = transform.position.y; // keep camera height fixed, zoom is handled by orthographic size / FOV

        transform.position = Vector3.Lerp(transform.position, center, Time.deltaTime * lerpSpeed);
    }
    
    private void ZoomToFit()
    {
        float distance = Vector3.Distance(pOneTransform.position, pTwoTransform.position);
        float targetZoom = Mathf.Clamp(distance + padding, minZoom, maxZoom);

        if (_cam.orthographic)
        {
            _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, targetZoom, Time.deltaTime * lerpSpeed);
        }
        else
        {
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, targetZoom, Time.deltaTime * lerpSpeed);
        }
    }
    
}
