using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialData data = null;

    
    [SerializeField] private TextMeshProUGUI tutorialName = null;
    [SerializeField] private TextMeshProUGUI tutorialText = null;
    [SerializeField] private Image tutorialImage = null;


    public void OnButtonClick()
    {
        tutorialName.text = data.tutorialName;
        tutorialText.text = data.tutorialText;
        tutorialImage = data.tutorialImage;
    }
}
