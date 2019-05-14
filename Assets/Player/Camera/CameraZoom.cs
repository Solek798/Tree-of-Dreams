﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform focusTransform;
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    public LensSettings m_Lens;
    
    //CamZoom is the position of the Focuspoint. The lower it is, the lower the Camera is looking
    public int minCamZoom = 10;
    public int maxCamZoom = -60;
    //FOV is the "Zoom" of the camera.
    public int minFOV = 30;
    public int maxFOV = 80;

    private void Update()
    {
        var camToFocusDistance = Vector3.Distance(transform.position, focusTransform.position);
        var distance = Vector3.Distance(transform.position, player.position);

 
        var zoomPercentage = distance / camToFocusDistance;
        //Zooms in by the percentage of distance relative to camToFocusDistance
        var newZoom = Mathf.Lerp(maxFOV, minFOV, zoomPercentage);
        var newZoomFocusPoint = Mathf.Lerp(minCamZoom, maxCamZoom, zoomPercentage);

        focusTransform.position = new Vector3(focusTransform.position.x, newZoomFocusPoint, focusTransform.position.z);
        
        //applies the new FOV to the Cinemachine Inhouse function
        cinemachineVirtualCamera.m_Lens.FieldOfView = newZoom;
    }
}