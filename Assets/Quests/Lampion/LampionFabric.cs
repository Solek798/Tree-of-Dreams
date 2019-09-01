using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class LampionFabric : MonoBehaviour
{
    [SerializeField] private GameObject lampionPrefab = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Farmland farmland;

    [SerializeField] private BoundsInt targetBounds;
    [SerializeField] private float spawnRadius = 30;
    [SerializeField] private int maxGeneratingAttempts = 10;

    public void CreateAndSend()
    {
        // Select random FarmlandSpace
        var levels = farmland.GetAllLevels();
        var targetLevel = levels[Random.Range(0, levels.Length - 1)];
        var targetSpace = targetLevel.GetRandomSpace(maxGeneratingAttempts);

        if (targetSpace == null) return;
        
        //Debug.Log(targetSpace);
        
        // create Lampion
        var newLampion = Instantiate(lampionPrefab, transform).GetComponent<Lampion>();
        newLampion.player = player;
        newLampion.questData = null;
        
        
        // Select random spawn point
        newLampion.transform.localPosition = new Vector3(spawnRadius * Random.value, 0, 0);
        newLampion.transform.RotateAround(
            newLampion.transform.parent.position, 
            Vector3.up, 
            360 * Random.value
        );

        targetSpace.Lampion = newLampion;
        newLampion.TravelTarget = targetSpace.transform.position;

        /*var children = this.gameObject
            .GetAllChildren()
            .Select(t => t.GetComponent<Lampion>())
            .Where(t => t != null)
            .ToArray();
        
        if (children.Length == 0) return;

        var child = children[Random.Range(0, children.Length)];

        child.transform.SetParent(transform.parent, true);
        
        
        child.TravelTarget = targets[_tempIndex];
        _tempIndex++;*/
    }
}
