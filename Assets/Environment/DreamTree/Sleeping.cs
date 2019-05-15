using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleeping : MonoBehaviour
{
    public GameObject player;
    public float maxDistanceToSleep = 10f;


    private void Update()
    {
        var plants = GameObject.FindGameObjectsWithTag("Plant");


        var playerInRange = Vector3.Distance(transform.position, player.transform.position);

        if (!Input.GetButtonDown("Fire2") || !(playerInRange <= maxDistanceToSleep)) return;
        foreach (var plant in plants)
        {
            plant.GetComponent<PlantState>().UpdateCurrentState();
        }

        StartCoroutine(Wait());
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