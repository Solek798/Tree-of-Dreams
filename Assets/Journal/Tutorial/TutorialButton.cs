using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialData data = null;

    
    [SerializeField] private TextMeshProUGUI tutorialName = null;
    [SerializeField] private TextMeshProUGUI tutorialText = null;
    [SerializeField] private Image tutorialImage = null;
    [SerializeField] private VideoClip VideoClip = null;
    [SerializeField] private VideoPlayer VideoPlayer = null;
    [SerializeField] private RectTransform VideoRect = null;
    [SerializeField] private Vector2 VideoRectSample = new Vector2(0, 0);

    public void OnButtonClick()
    {
        tutorialName.text = data.tutorialName;
        tutorialText.text = data.tutorialText;
        tutorialImage = data.tutorialImage;
        VideoPlayer.clip = VideoClip;
        VideoRect.sizeDelta = VideoRectSample;
        VideoPlayer.Play();
    }
}
