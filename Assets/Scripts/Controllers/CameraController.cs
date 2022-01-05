using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    private float currentZoom = 7f, minZoom = 5f, maxZoom = 10f, zoomSpeed = 4f;
    public float yawSpeed;
    float currentYaw = 0;
    public Transform target;


    public float pitch = 2f;
    // Start is called before the first frame update
    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
