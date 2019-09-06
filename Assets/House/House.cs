﻿using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private LampionFactory lampionFactory = null;
    [SerializeField] private Journal journal = null;
    [SerializeField] private GameObject sleepMenu = null;
    [SerializeField] private int questFrequency;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip goToSleepSound;
    [SerializeField] private AudioClip wakeUpSound;
    [SerializeField] private GameObject floatingTextPrefab;
    
    public GameObject player;
    public float maxDistanceToSleep = 10f;

    private int daysSinceLastQuest;
    private GameObject hotkeyText;
    private Vector3 offset = new Vector3(8,8, 0);


    private void Start()
    {
        lampionFactory.CreateAndSend();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDistanceToSleep)
        {
            if (!transform.GetComponentInChildren<TextMesh>())
            {
                hotkeyText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
                hotkeyText.transform.Translate(offset);
            }

            hotkeyText.transform.LookAt(Camera.main.transform);
            hotkeyText.transform.Rotate(0,180,0);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Sleep());
            }
        }
        else if (hotkeyText != null)
        {
            Destroy(hotkeyText);
        }
     

        
    }

    private bool ProccessNight()
    {
        foreach (FarmlandLevel level in farmland)
        {
            foreach (var space in level.GetAllSpaces())
            {
                space.UpdateState();
            }
        }

        if (daysSinceLastQuest == questFrequency - 1)
        {
            lampionFactory.CreateAndSend();
            daysSinceLastQuest = 0;
        }
        else
        {
            daysSinceLastQuest++;
        }

        journal.Days++;
        
        return false;
    }

    private IEnumerator Sleep()
    {
        audioPlayer.clip = goToSleepSound;
        audioPlayer.Play();
        
        Transition.Instance.FadeBlack();

        yield return new WaitForSeconds(Transition.Instance.FadeBlackTime);
        ProccessNight();
        sleepMenu.SetActive(true);

        Transition.Instance.FadeNormal();
    }

    private  IEnumerator WakeUp()
    {
        Transition.Instance.FadeBlack();

        yield return new WaitForSeconds(Transition.Instance.FadeBlackTime);
        //sleepMenu.SetActive(false);

        Transition.Instance.FadeNormal();
        
        audioPlayer.clip = wakeUpSound;
        audioPlayer.Play();
    }

    public void ResumeNight()
    {
        StartCoroutine(WakeUp());
    }
}