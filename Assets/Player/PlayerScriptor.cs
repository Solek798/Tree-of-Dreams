using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptor : MonoBehaviour
{
    public static PlayerScriptor Instance { get; private set; }

    public bool AllowMoving { get; set; } = true;
    public bool AllowInteracting { get; set; } = true;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    
}
