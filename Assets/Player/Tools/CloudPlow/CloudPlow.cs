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
        CloudPlowPlayer.clip = CloudPlowSfx;
        CloudPlowPlayer.Play();
        yield return space.IsSoil = true;
    }

    public bool IsUsable(FarmlandSpace space)
    {
        return !space.IsSoil && !space.IsNurtured;
    }

    public float MaxUsingDistance => maxPlowDistance;
}
