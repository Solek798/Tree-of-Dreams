using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform m_lookAt = null;
    [SerializeField] private Transform _target = null;
    private CinemachineDollyCart _dollyCart;

    public int startSegment = 0;

    public int searchRadius = -1;

    public int stepsPerSegment = 10;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _dollyCart = GetComponent<CinemachineDollyCart>();
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var t =_dollyCart.m_Path.FindClosestPoint(_target.position, startSegment, searchRadius, stepsPerSegment);
        _dollyCart.m_Position = t;
        _camera.transform.LookAt(m_lookAt);
    }
}
