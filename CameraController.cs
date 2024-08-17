using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 50f;
    public float panBoarderThickness = 10f;

    public float scrollSpeed = 10f;
    public float minY = 10f;
    public float maxY = 80f;

    // Define boundaries for the camera
    public float minX = 0.5f;
    public float maxX = 74.5f;
    public float minZ = -80f;
    public float maxZ = 50f;

    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        Vector3 move = Vector3.zero;

        // Panning
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            move += Vector3.forward;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
        {
            move += Vector3.back;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            move += Vector3.right;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {
            move += Vector3.left;
        }

        // Move the camera based on panSpeed and deltaTime
        transform.Translate(move * panSpeed * Time.deltaTime, Space.World);

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // Constrain the camera within defined bounds
        pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}
