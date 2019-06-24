using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class RequirementsPanel : MonoBehaviour
{

    [SerializeField] private Image childImage = null;
    
    public void InitializePanel(List<GameObject> requirements)
    {
        foreach (GameObject Requirement in requirements)
        {
            Sprite reqSprite = Requirement.GetComponent<InventoryItem>().Icon;
            childImage.sprite = reqSprite;
        }
    }
}


        