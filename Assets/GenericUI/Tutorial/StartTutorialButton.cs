using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorialButton : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI = null;
    [SerializeField] private GameObject page1 = null; 
    [SerializeField] private GameObject page2 = null;

    private void Start()
    {
        PlayerScriptor.Instance.AllowInteracting = false;
        PlayerScriptor.Instance.AllowMoving = false;
    }

    public void OnArrowRightClick()
    {
        page1.active = false;
        page2.active = true;
    }

    public void OnArrowLeftClick()
    {
        page2.active = false;
        page1.active = true;
    }

    public void OnFinishTutorialClick()
    {
        PlayerScriptor.Instance.AllowInteracting = true;
        PlayerScriptor.Instance.AllowMoving = true;
        tutorialUI.active = false;
    }
}
