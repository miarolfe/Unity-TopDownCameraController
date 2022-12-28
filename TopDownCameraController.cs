using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour {
    [Range(0.0f, 20.0f)] public float leftRightMovementSensitivity = 15.0f;
    [Range(0.0f, 20.0f)] public float upDownMovementSensitivity = 15.0f;
    [Range(0.0f, 20.0f)] public float forwardBackMovementSensitivity = 15.0f;
    [Range(0.0f, 150.0f)] public float horizontalRotationSensitivity = 80.0f;
    [Range(0.0f, 500.0f)] public float verticalRotationSensitivity = 250.0f;
    [Range(ViewAngleMin, ViewAngleMax)] public float viewAngle = 15.0f;
    public bool verticalRotationInverted = false;
    private const float ViewAngleMin = 0.0f;
    private const float ViewAngleMax = 75.0f;
    
    private Transform mainTransform;
    private Transform cameraTransform;

    private void Start() {
        mainTransform = transform;
        cameraTransform = transform.GetChild(0);
    }

    private void OnValidate() {
        // UpdateViewAngle();
    }

    private void UpdateViewAngle() {
        cameraTransform.eulerAngles = new Vector3(
            viewAngle, 
            cameraTransform.eulerAngles.y, 
            cameraTransform.eulerAngles.z
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
        
        if (verticalRotationInverted) {
            viewAngle -= Input.GetAxis("Mouse ScrollWheel") * verticalRotationSensitivity * Time.deltaTime;
        } else {
            viewAngle += Input.GetAxis("Mouse ScrollWheel") * verticalRotationSensitivity * Time.deltaTime;
        }
        
        viewAngle = Mathf.Clamp(viewAngle, ViewAngleMin, ViewAngleMax);
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
