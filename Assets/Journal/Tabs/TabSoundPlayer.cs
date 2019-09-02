using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private List<AudioClip> audioClips;

    public void PlayRandomSound()
    {
        int randInt = Random.Range(0 ,audioClips.Count - 1);
        Debug.Log(randInt);
        audioPlayer.clip = audioClips[randInt];
        audioPlayer.Play();
    }
}
