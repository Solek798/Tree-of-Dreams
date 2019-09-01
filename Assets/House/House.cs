using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class House : MonoBehaviour
{
    [SerializeField] private Farmland farmland = null;
    [SerializeField] private LampionFabric lampionFabric = null;
    [SerializeField] private Journal journal = null;
    public GameObject player;
    public float maxDistanceToSleep = 10f;


    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (Input.GetKeyDown(KeyCode.E) &&
            Vector3.Distance(transform.position, player.transform.position) <= maxDistanceToSleep)
        {
            SleepFunction();
            StartCoroutine(Wait());
        }

        
    }

    private bool SleepFunction()
    {
        foreach (FarmlandLevel level in farmland)
        {
            foreach (var space in level.GetAllSpaces())
            {
                space.UpdateState();
            }
        }

        lampionFabric.CreateAndSend();

        journal.Days++;

        return false;
    }

    private void FadeBlack()
    {
        var image = GetComponentInChildren<Image>();
        var tempColor = image.color;
        tempColor.a = 255f;
        image.color = tempColor;
    }

    private IEnumerator Wait()
    {
        FadeBlack();

        yield return new WaitForSeconds(1);

        FadeTransparent();
    }

    private void FadeTransparent()
    {
        var image = GetComponentInChildren<Image>();
        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }
}