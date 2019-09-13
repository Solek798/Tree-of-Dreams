using UnityEngine;

public class MenuContent : MonoBehaviour
{
    [SerializeField] private Animation optionsAnimation = null;
    [SerializeField] private Animation creditsAnimation = null;

    private bool creditsOpened = false;
    private bool optionsOpened = false;

    public void OpenOptions()
    {
        if (optionsOpened)
        {
            optionsAnimation.Play("OptionsPopDown");
            optionsOpened = false;
        }
        else
        {
            optionsAnimation.Play("OptionsPopUp");
            optionsOpened = true;
        }

        if (creditsOpened)
        {
            creditsAnimation.Play("CreditsPopDown");
            creditsOpened = false;
        }
    }

    public void OpenCredits()
    {
        if (creditsOpened)
        {
            creditsAnimation.Play("CreditsPopDown");
            creditsOpened = false;
        } 
        else
        {
            creditsAnimation.Play("CreditsPopUp");
            creditsOpened = true;
        }

        if (optionsOpened)
        {
            optionsAnimation.Play("OptionsPopDown");
            optionsOpened = false;
        }
    }
}
