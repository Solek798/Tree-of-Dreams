﻿using System.Linq;
using UnityEngine;

public class SellArea : MonoBehaviour, IDropTarget
{
    public Inventory Inventory { get; set; }
    public Journal Journal { get; set; }
    public SleepMenu SleepMenu { get; set; }

    public bool Handle(GameObject draggable)
    {
        var stack = draggable.GetComponent<Stack>();
        
        if (stack == null || stack.Peek().GetComponent<PlantState>() == null) return false;

        var plants = 
            stack.PopAll()
                .Select(t => t.GetComponent<PlantState>());
        var stackPrice = 0;

        foreach (var plant in plants)
        {
            stackPrice += plant.plantObject.sellPrice;
            Destroy(plant.gameObject);
        }
        
        Inventory.Currency += stackPrice;
        Journal.EarningsCounter += stackPrice;

        return true;
    }
}
