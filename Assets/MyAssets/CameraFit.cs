using UnityEngine;

[ExecuteInEditMode]
public class CameraFit : MonoBehaviour
{
    [Header("Settings")]
    public float targetWidth = 20f; // The width of your level in World Units
    public float minHeight = 10f;   // The minimum height you always want to see

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void Update()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        if (cam == null) cam = GetComponent<Camera>();

        // 1. Calculate the ratio of the screen
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = targetWidth / minHeight;

        // 2. If the screen is narrower than our level...
        if (screenRatio < targetRatio)
        {
            // ...Scale the height (Zoom Out) to fit the width
            cam.orthographicSize = targetWidth / screenRatio / 2;
        }
        else
        {
            // ...Otherwise, use the minimum height (Standard behavior)
            cam.orthographicSize = minHeight / 2;
        }
    }
}