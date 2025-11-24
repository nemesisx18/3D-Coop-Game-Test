using UnityEngine;

public class HighlightUI : MonoBehaviour
{
    private Camera cam;

    private bool trackingCamera = false;

    private void OnEnable()
    {
        cam = Camera.main;
        trackingCamera = true;
    }

    private void Update()
    {
        if (trackingCamera)
        {
            transform.LookAt(cam.transform.position);
        }
    }

    private void OnDisable()
    {
        trackingCamera = false;
    }
}
