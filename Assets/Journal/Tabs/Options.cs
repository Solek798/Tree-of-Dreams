using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;
    [SerializeField] private GameObject ambientVFX = null;
    
    
    // Start is called before the first frame update
    public void ToggleFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void ToggleAmbiantVFX(bool value)
    {
        ambientVFX.SetActive(value);
    }

    public void SetSFX(Single value)
    {
        mixer.SetFloat("SFX", value);
    }
    
    public void SetMusic(Single value)
    {
        mixer.SetFloat("Music", value);
    }
}
