using UnityEngine;

public class Transition : MonoBehaviour
{
    public static Transition Instance { get; private set; }

    public float FadeBlackTime => GetComponent<Animation>().GetClip("FadeBlack").length;
    public float FadeNormalTime => GetComponent<Animation>().GetClip("FadeNormal").length;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        FadeNormal();
    }

    public void FadeBlack()
    {
        GetComponent<Animation>().CrossFade("FadeBlack");
        
    }

    public void FadeNormal()
    {
        GetComponent<Animation>().CrossFade("FadeNormal");
    }
}
