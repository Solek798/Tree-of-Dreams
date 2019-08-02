using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    public static UIStatus Instance { get; private set; }

    private bool _dialogOpened = false;
    
    public bool DialogOpened
    {
        get => _dialogOpened;
        set
        {
            _dialogOpened = value;
            PlayerScriptor.Instance.AllowMoving = !value;
            PlayerScriptor.Instance.AllowInteracting = !value;
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
