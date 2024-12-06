using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float panSpeed = 0.5f;
    public float zoomSpeed = 2.0f;

    private Vector3 lastMousePosition;

    void Update()
    {
        HandleRotation();
        HandlePanning();
        HandleCanvasToggle();
        HandleZoom();
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1)) // right click
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float yaw = delta.x * rotationSpeed * Time.deltaTime;
            float pitch = delta.y * rotationSpeed * Time.deltaTime;

            transform.eulerAngles += new Vector3(-pitch, yaw, 0);
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed * Time.deltaTime;

            // 카메라의 오른쪽과 위쪽 벡터를 기준으로 이동
            transform.Translate(move.x * transform.right + move.y * transform.up, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            transform.Translate(Vector3.forward * scroll * zoomSpeed, Space.Self);
        }
    }

    void HandleCanvasToggle()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CanvasManager.Instance.ToggleCanvas();
        }
    }


}