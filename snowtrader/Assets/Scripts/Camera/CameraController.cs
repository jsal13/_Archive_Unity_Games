using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    private Transform player;
    private Camera cam;
    private readonly float targetAspect = 16.0f / 9.0f;
    private float windowAspect;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        cam = GetComponent<Camera>();
        windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        // Add Letterbox or Pillarbox if not 16:9.
        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 0.75f, transform.position.z);
    }

    //void OnGUI()
    //{
        //// SOURCE: https://answers.unity.com/questions/526841/changing-ortho-cam-size-according-to-resolution.html
        //float currentAspect = (float)Screen.width / (float)Screen.height;
        //Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;
    //}

}
