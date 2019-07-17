using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SellArea : MonoBehaviour, IDropTarget
{
    [SerializeField] private Inventory inventory = null;

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
        
        inventory.Currency += stackPrice;

        return true;
    }

    private void Start()
    {
        int i = Convert.ToInt32("0");
        
        Debug.Log(i);
        Debug.Log("0".Length);
    }
}
