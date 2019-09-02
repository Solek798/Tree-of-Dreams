using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPlow : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlowDistance = 60.0f;
    [SerializeField] private AudioClip CloudPlowSfx;
    [SerializeField] private AudioSource CloudPlowPlayer;


    public IEnumerator Use(FarmlandSpace space)
    {
        if (space.IsSoil)
        {
            Debug.Log("Despawning");
            CloudPlowPlayer.clip = CloudPlowSfx;
            CloudPlowPlayer.Play();
            space.animator.Play("SoilDespawnAnimation");

            yield return new WaitForSeconds(space.animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);

            space.UpdateState();
                        
            yield return space.IsSoil = false;
        }
        else
        {
            Debug.Log("Spawning");
            CloudPlowPlayer.clip = CloudPlowSfx;
            CloudPlowPlayer.Play();
            space.animator.Play("SoilSpawnAnimation");
            yield return space.IsSoil = true;
        }
        
    }

    public bool IsUsable(FarmlandSpace space)
    {
        return !space.IsSoil && space.Lampion == null;
    }

    public float MaxUsingDistance => maxPlowDistance;
}
