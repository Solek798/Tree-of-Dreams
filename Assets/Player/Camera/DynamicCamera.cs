﻿using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Transform focusPoint = null;
    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private Transform zoomTransform = null;

    [SerializeField, Range(0, 1)] private float turnDumpingFactor = 1;
    [SerializeField, Range(1, 20)] private float turnSpeedFactor = 1;

    [Tooltip("The maximum range the Camera can travel on the ZoomDolly. " +
             "Based on the Position of the zoomDolly/TurnDolly to the focusPoint)")]
    [SerializeField, Range(0, float.PositiveInfinity)] private float maxZoom = 80;
    [Tooltip("Minimal Distance that the Player can have to the focus Point (When he is standing in front of the tree)")]
    [SerializeField, Range(0, float.PositiveInfinity)] private float minDistance = 13;
    [Tooltip("Maximum Distance that the Player can have to the focus Point (When he is at the end of the level)")]
    [SerializeField, Range(0, float.PositiveInfinity)] private float maxDistance = 60;
    
    private float _zoomStep = 0;
    private float _zoomedDistance = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        _zoomStep = maxZoom / (maxDistance - minDistance);
    }

    // Update is called once per frame
    private void Update()
    {
        // Move TurnDolly
        
        var focusToDolly = focusPoint.position - transform.position;
        var focusToPlayer = focusPoint.position - playerTransform.position;

        var focusToDolly2D = new Vector2(focusToDolly.x, focusToDolly.z);
        var focusToPlayer2D = new Vector2(focusToPlayer.x, focusToPlayer.z);

        var angle = Vector2.SignedAngle(focusToPlayer2D, focusToDolly2D);

        transform.RotateAround(focusPoint.position, Vector3.up, angle * turnDumpingFactor * turnSpeedFactor); 
        
        // Move ZoomDolly

        var currentZoom = (maxDistance - focusToPlayer2D.magnitude) * _zoomStep;
        zoomTransform.Translate(0, 0, currentZoom - _zoomedDistance);
        _zoomedDistance = currentZoom;
        
        
        // Rotate Camera
        
        cameraTransform.LookAt(playerTransform);
        var camRotation = cameraTransform.localRotation;
        var camEulers = camRotation.eulerAngles;
        camEulers.y = 0;
        camEulers.z = 0;

        camRotation.eulerAngles = camEulers;
        
        cameraTransform.Rotate(angle, 0, 0);
        
        cameraTransform.localRotation = camRotation;
        
    }
}
