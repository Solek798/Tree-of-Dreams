using System.Collections;
using UnityEngine;

public class ThxForPlayingScreen : MonoBehaviour
{
    [SerializeField] private Animation fadeOutAnimation = null;
    
    
    private void OnEnable()
    {
        PlayerScriptor.Instance.AllowMoving = false;
        PlayerScriptor.Instance.AllowInteracting = false;
    }

    public void OnContinue()
    {
        fadeOutAnimation.Play("FadeOut");
        StartCoroutine(Wait(fadeOutAnimation.GetClip("FadeOut").length));
    }

    private IEnumerator Wait(float secounds)
    {
        yield return new WaitForSeconds(secounds);
        
        TurnOff();
    }

    private void TurnOff()
    {
        PlayerScriptor.Instance.AllowMoving = true;
        PlayerScriptor.Instance.AllowInteracting = true;

        gameObject.SetActive(false);
    }
}
