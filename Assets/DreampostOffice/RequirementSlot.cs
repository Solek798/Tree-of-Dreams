
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementSlot : MonoBehaviour, IDropTarget
{
    [SerializeField] private Image icon = null;
    [SerializeField] private Text amountText = null;
    [SerializeField] private Color iconColor = Color.white;
    [SerializeField] private Color fullFilledColor = Color.clear;
    private PlantScriptableObject _plantScriptableObject = null;

    public Sprite Icon
    {
        set
        { 
            icon.sprite = value;
            icon.color = iconColor;
        }
        get => icon.sprite;
    }
    
    public int Amount
    {
        private get
        {
            return amountText.text == string.Empty ? 0 : Convert.ToInt32(amountText.text); 
        }
        set => amountText.text = value.ToString();
    }
    
    public PlantScriptableObject PlantScriptableObject
    {
        set => _plantScriptableObject = value;
    }

    public bool Handle(GameObject draggable)
    {
        var stack = draggable.GetComponent<Stack>();

        if (stack == null || GetComponent<Toggle>().isOn) return false;
        if (stack.Peek().GetComponent<PlantState>().plantObject != _plantScriptableObject)
            return false;

        if (stack.Count > Amount)
        {
            for (var i = 0; i < stack.Count; i++)
            {
                Destroy(stack.Pop().gameObject);
                
            }
            Amount = 0;

            CheckForSatisfaction();

            return false;
        }
        else
        {
            foreach (var item in stack.PopAll())
            {
                Destroy(item.gameObject);
            }

            Amount -= stack.Count;

            CheckForSatisfaction();

            return true;
        }
    }

    private void CheckForSatisfaction()
    {
        if (Amount == 0)
        {
            MarkAsSatisfactioned();
            SendMessageUpwards("OnRequirementSatisfactioned", this);
        }
    }

    public void MarkAsSatisfactioned()
    {
        GetComponent<Toggle>().isOn = true;
        amountText.text = "";
        icon.color = fullFilledColor;
    }
}
