using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private float targetAspect = 16.0f / 9.0f;
    private float windowAspect;
    private float scaleHeight;
    private Camera cam;

    private void Start()
    {
        windowAspect = (float)Screen.width / (float)Screen.height;
        scaleHeight = windowAspect / targetAspect;
        cam = gameObject.GetComponent<Camera>();

        // Letterbox.
        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        // Pillarbox.
        else
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
