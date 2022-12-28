using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour {
    [Range(0.0f, 20.0f)] public float leftRightMovementSensitivity;
    [Range(0.0f, 20.0f)] public float upDownMovementSensitivity;
    [Range(0.0f, 20.0f)] public float forwardBackMovementSensitivity;
    [Range(0.0f, 150.0f)] public float horizontalRotationSensitivity;
    [Range(0.0f, 250.0f)] public float verticalRotationSensitivity;
    [Range(VIEW_ANGLE_MIN, VIEW_ANGLE_MAX)] public float viewAngle;
    private const float VIEW_ANGLE_MIN = 0.0f;
    private const float VIEW_ANGLE_MAX = 75.0f;
    
    private Transform mainTransform;
    private Transform cameraTransform;
    private new Camera camera;

    private void Start() {
        mainTransform = transform;
        cameraTransform = transform.GetChild(0);
        camera = cameraTransform.GetComponent<Camera>();
    }

    private void OnValidate() {
        // UpdateViewAngle();
    }

    private void UpdateViewAngle() {
        camera.transform.eulerAngles = new Vector3(
            viewAngle, 
            camera.transform.eulerAngles.y, 
            camera.transform.eulerAngles.z
        );
    }

    private void Update() {
        // "Mouse ScrollWheel" is the Input Axis provided by Unity
        // "Left/Right", "Up/Down", and "Forward/Back" are near-identical
        // axes with:
        // Gravity: 3
        // Dead: 0.001
        // Sensitivity: 3
        // Snap: True
        // Type: Key or Mouse Button
        
        viewAngle += Input.GetAxis("Mouse ScrollWheel") * verticalRotationSensitivity * Time.deltaTime;
        viewAngle = Mathf.Clamp(viewAngle, VIEW_ANGLE_MIN, VIEW_ANGLE_MAX);
        UpdateViewAngle();

        Vector3 translation = new Vector3 (
            Input.GetAxis("Left/Right") * leftRightMovementSensitivity * Time.deltaTime,
            Input.GetAxis("Up/Down") * upDownMovementSensitivity * Time.deltaTime,
            Input.GetAxis("Forward/Back") * forwardBackMovementSensitivity * Time.deltaTime
            );

        mainTransform.Translate(translation, Space.Self);
        
        // Rotate left and right
        mainTransform.eulerAngles = mainTransform.eulerAngles += new Vector3(0,
            Input.GetAxis("HorizontalRotation") * horizontalRotationSensitivity * Time.deltaTime, 0);
    }
}
