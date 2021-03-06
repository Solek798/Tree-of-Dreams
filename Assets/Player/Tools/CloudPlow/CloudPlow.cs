using System.Collections;
using UnityEngine;

public class CloudPlow : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlowDistance = 60.0f;
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip fillAudio = null;
    [SerializeField] private AudioClip plowAudio = null;

    private bool isInUse = false;


    public IEnumerator Use(FarmlandSpace space)
    {
        isInUse = true;

        if (space.IsSoil)
        {
            audioPlayer.clip = fillAudio;
            audioPlayer.Play();

            space.animator.Play("SoilDespawnAnimation");

            yield return new WaitForSeconds(space.animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);

            space.UpdateState();
            isInUse = false;


            yield return space.IsSoil = false;
        }
        else
        {            
            audioPlayer.clip = plowAudio;
            audioPlayer.Play();

            space.animator.Play("SoilSpawnAnimation");

            StartCoroutine(AnimationEnds(space));
                                               
            yield return space.IsSoil = true;
        }

    }

    IEnumerator AnimationEnds(FarmlandSpace space)
    {
        yield return new WaitForSeconds(space.animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);
        isInUse = false;
    }

    public bool IsUsable(FarmlandSpace space)
    {
        return isInUse == false && space.Lampion == null && space.Plant == null;
    }

    public float MaxUsingDistance => maxPlowDistance;
}
