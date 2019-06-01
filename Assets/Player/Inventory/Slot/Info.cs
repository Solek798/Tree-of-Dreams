using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Info : MonoBehaviour, ICanvasRaycastFilter
{
    private Text _stackCount;
    private Image _stackIcon;


    public Slot Slot => GetComponentInParent<Slot>();

    private void Start()
    {
        _stackCount = GetComponentInChildren<Text>();
        _stackIcon = GetComponentInChildren<Image>();
    }

    public void Refresh(int count, Sprite icon = null)
    {
        _stackCount.text = count <= 1 ? "" : count.ToString();
        _stackIcon.color = count == 0 ? Color.clear : Color.white;
        if (icon) 
            _stackIcon.sprite = icon;
        
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        //print("Mouse: " + Input.mousePosition.ToString());
        //print("SP: " + sp.ToString());
        
        return true;
    }
}
