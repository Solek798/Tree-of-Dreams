﻿using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    public const string AutoRunPref = "autoRun";

    [SerializeField] private AudioMixer mixer = null;
    [SerializeField] private GameObject ambientVFXSakura = null;
    [SerializeField] private GameObject ambientVFXGlow = null;
    [SerializeField] private Slider sfxSlider = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Toggle ambientToggle = null;
    [SerializeField] private Toggle autoRun = null;

    private static bool AmbientVFX { get; set; }


    private void Start()
    {
        ToggleAmbiantVFX(AmbientVFX);

        mixer.GetFloat("SFX", out var sfxValue);
        mixer.GetFloat("Music", out var musicValue);

        if (sfxSlider == null || musicSlider == null) return;
        sfxSlider.value = sfxValue;
        musicSlider.value = musicValue;
    }

    public void ToggleFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void ToggleAmbiantVFX(bool value)
    {
        AmbientVFX = value;
        ambientVFXGlow?.SetActive(value);
        ambientVFXSakura?.SetActive(value);
    }

    public void SetSFX(Single value)
    {
        mixer.SetFloat("SFX", value);
    }
    
    public void SetMusic(Single value)
    {
        mixer.SetFloat("Music", value);
    }

    public void SetAutoRun(bool value)
    {
        var t = value ? 1 : 0;
        PlayerPrefs.SetInt(AutoRunPref, t);
    }
}
