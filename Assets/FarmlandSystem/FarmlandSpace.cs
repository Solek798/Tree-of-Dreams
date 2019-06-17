using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandSpace : MonoBehaviour
{
    [SerializeField] private GameObject soil;
    private Vector3Int _cell;
    private bool _isNurtured;

    public bool IsSoil
    {
        get => soil.activeInHierarchy;
        set => soil.SetActive(value);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
