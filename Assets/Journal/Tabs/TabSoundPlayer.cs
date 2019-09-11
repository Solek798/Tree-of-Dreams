using System.Collections.Generic;
using UnityEngine;

public class TabSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private List<AudioClip> audioClips = null;

    public void PlayRandomSound()
    {
        int randInt = Random.Range(0 ,audioClips.Count - 1);
        Debug.Log(randInt);
        audioPlayer.clip = audioClips[randInt];
        audioPlayer.Play();
    }
}
