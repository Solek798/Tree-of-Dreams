using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfStardust : MonoBehaviour, ITool
{
    [SerializeField] private float maxThrowDistance = 60.0f;
    [SerializeField] private AudioClip BagOfStardustSfx;
    [SerializeField] private AudioSource BagOfStardustPlayer;

    public IEnumerator Use(FarmlandSpace space)
    {
        space.bagOfStardustParticle.Play();
        space.IsNurtured = true;
        if(!(space.Plant == null))
        { 
            space.Plant.animator.Play("NurturingAnimation");
        }
        BagOfStardustPlayer.clip = BagOfStardustSfx;
        BagOfStardustPlayer.Play(); 
        yield return new WaitForEndOfFrame();
    }

    public bool IsUsable(FarmlandSpace space)
    {
        return space.IsSoil;
    }

    public float MaxUsingDistance => maxThrowDistance;
}
