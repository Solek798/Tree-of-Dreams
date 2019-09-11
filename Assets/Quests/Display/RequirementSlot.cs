using System;
using UnityEngine;
using UnityEngine.UI;

public class RequirementSlot : MonoBehaviour, IDropTarget
{
    [SerializeField] private Image icon = null;
    [SerializeField] private Text amountText = null;
    [SerializeField] private Color iconColor = Color.white;
    [SerializeField] private Color fullFilledColor = Color.clear;
    
    private PlantScriptableObject _plantScriptableObject = null;
    private QuestDisplay _display = null;

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
        get
        {
            return amountText.text == string.Empty ? 0 : Convert.ToInt32(amountText.text); 
        }
        set
        {
            amountText.text = value.ToString();

            if (value == 0) MarkAsSatisfactioned();
        }
    }
    
    public PlantScriptableObject PlantScriptableObject
    {
        set => _plantScriptableObject = value;
    }

    public QuestDisplay Display
    {
        set => _display = value;
    }

    public bool Handle(GameObject draggable)
    {
        var stack = draggable.GetComponent<Stack>();

        if (stack == null || GetComponent<Toggle>().isOn) return false;
        if (stack.Peek().GetComponent<PlantState>().plantObject != _plantScriptableObject)
            return false;

        if (Amount < stack.Count)
        {
            for (var i = 0; i < Amount; i++)
            {
                Destroy(stack.Pop().gameObject);
                
            }
            
            Amount = 0;
            _display.OnSlotChanged(this);

            return false;
        }
        else
        {
            foreach (var item in stack.PopAll())
            {
                Destroy(item.gameObject);
            }

            Amount -= stack.Count;
            _display.OnSlotChanged(this);

            return true;
        }
    }

    public void MarkAsSatisfactioned()
    {
        GetComponent<Toggle>().isOn = true;
        amountText.text = "";
        icon.color = fullFilledColor;
    }
}
