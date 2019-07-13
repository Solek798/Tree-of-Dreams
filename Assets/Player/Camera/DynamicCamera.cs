using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform zoomTransform;

    [SerializeField] private float turnDumpingFactor = 1;
    [SerializeField] private float turnSpeedFactor = 1;

    [SerializeField] private float maxZoom = 80;
    [SerializeField] private float minDistance = 13;
    [SerializeField] private float maxDistance = 60;
    private float _zoomStep = 0;
    private float _zoomedDistance = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _zoomStep = maxZoom / (maxDistance - minDistance);
        Debug.Log(_zoomStep);
    }

    // Update is called once per frame
    void Update()
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
