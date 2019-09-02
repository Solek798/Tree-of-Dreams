using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfStardust : MonoBehaviour, ITool
{
    [SerializeField] private float maxThrowDistance = 60.0f;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private List<AudioClip> audioClips;

    public IEnumerator Use(FarmlandSpace space)
    {
        space.bagOfStardustParticle.Play();
        space.IsNurtured = true;
        if(!(space.Plant == null))
        { 
            space.Plant.animator.Play("NurturingAnimation");
        }

        int randInt = Random.Range(0, audioClips.Count - 1);
        audioPlayer.clip = audioClips[randInt];
        audioPlayer.Play(); 

        yield return new WaitForEndOfFrame();
    }

    public bool IsUsable(FarmlandSpace space)
    {
        return space.IsSoil;
    }
    public float MaxUsingDistance => maxThrowDistance;
}
