﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCropUi : MonoBehaviour
{
    private Inventory _inventory = null;

    public void OpenUiMenu(Inventory inventory)
    {
        _inventory = inventory;
        Debug.Log(_inventory);
        this.gameObject.SetActive(true);
    }

    public void CloseUiMenu()
    {
        this.gameObject.SetActive(false);
        _inventory = null;
    }

    public void Buy(GameObject plant)
    {
        if (_inventory != null)
        {
            var price = plant.GetComponent<PlantState>()?.plantObject.buyPrice ?? _inventory.MaxCurrency + 1;
            
            _inventory.Currency -= _inventory.Currency - price > 0 ? price : 0;
            GetComponentInParent<SeedFlute>().Plant(plant);
        }

        CloseUiMenu();
    }
}
