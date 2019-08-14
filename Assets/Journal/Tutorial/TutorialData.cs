using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/Tutorial", order = 3)]
public class TutorialData : ScriptableObject
{

    public string tutorialName;
    
    [TextArea] public string tutorialText;

    public Image tutorialImage;
}