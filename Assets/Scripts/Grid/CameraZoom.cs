using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 200f;
    public float minZoom = 10f;
    public float maxZoom = 100f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = this.GetComponent<Camera>();

        // default rotation value
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }

    void Update()
    {
        // wheel?
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            ZoomCamera(scroll);
        }
    }

    void ZoomCamera(float scrollAmount)
    {
        Vector3 direction = mainCamera.transform.forward;
        Vector3 position = mainCamera.transform.position;

        position += direction * scrollAmount * zoomSpeed * Time.deltaTime;

        float distance = Vector3.Distance(position, Vector3.zero); 
        if (distance > minZoom && distance < maxZoom)
        {
            mainCamera.transform.position = position;
        }
    }
}